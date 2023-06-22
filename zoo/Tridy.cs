using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace zoo
{
    public enum TypUdalosti { Start, Trpelivost, Hlad, Obslouzen, Specialni };
    public class Udalost
    {
        public int kdy;
        public Proces kdo;
        public TypUdalosti co;
        public Udalost(int kdy, Proces kdo, TypUdalosti co)
        {
            this.kdy = kdy;
            this.kdo = kdo;
            this.co = co;
        }
    }
    public class Halda
    {
        List<Prvek> prvky;
        public Halda()
        {
            prvky = new List<Prvek>();
        }

        public void Pridej(int klic, Udalost objekt)
        {
            Prvek prvek = new Prvek(klic, objekt);
            prvky.Add(prvek);
            BublejNahoru(prvky.Count - 1);
        }
        public Prvek OdeberMin()
        {
            if (prvky.Count <= 0)
            {
                return null;
            }

            Prvek koren = prvky[0];
            prvky[0] = prvky[prvky.Count - 1];
            prvky.RemoveAt(prvky.Count - 1);
            BublejDolu(0);

            return koren;
        }
        void BublejNahoru(int index)
        {
            while (index != 0 && prvky[index].klic < prvky[(index - 1) / 2].klic)
            {//dokud syn je menší než rodič a není kořen
                //prohazuj
                Prvek tmp = prvky[index];
                prvky[index] = prvky[(index-1)/2];
                prvky[(index - 1) / 2] = tmp;

                index = (index - 1) / 2;
            }
        }
        void BublejDolu(int index)
        {
            while ((index * 2)+1 < prvky.Count)
            {
                int isyn = (index * 2) + 1;
                if (isyn+1 < prvky.Count && prvky[isyn+1].klic < prvky[isyn].klic)
                {//vybere menšího syna
                    isyn += 1;
                }
                if (prvky[index].klic < prvky[isyn].klic)
                {//jsou správně
                    return;
                }
                //prohodím
                Prvek tmp = prvky[index];
                prvky[index] = prvky[isyn];
                prvky[isyn] = tmp;
                index = isyn;
            }
        }
        public int NajdiUdalost(Proces kdo, TypUdalosti co)
        {
            for (int i = 0; i < prvky.Count; i++)
            {
                if (prvky[i].objekt.kdo == kdo && prvky[i].objekt.co == co)
                {
                    return i;
                }
            }
            return -1;
        }
        public void ZmensiHodnotu(int index, int novaHodnota)
        {
            if (novaHodnota > prvky[index].klic)
            {
                Console.WriteLine("nova hodnota musi byt mensi");
                return;
            }
            prvky[index].klic = novaHodnota;
            BublejNahoru(index);
        }

        public void Odeber(int index)
        {
            ZmensiHodnotu(index, int.MinValue);
            OdeberMin();
        }

        public void Vypis()
        {
            Queue<Prvek> fronta = new Queue<Prvek>();
            fronta.Enqueue(prvky[0]);

            int velikostHladiny = 1;
            int zbytek = velikostHladiny;
            foreach (Prvek prvek in prvky)
            {
                Console.Write(prvek.klic + " ");
                zbytek--;
                if (zbytek == 0)
                {
                    velikostHladiny *= 2;
                    zbytek = velikostHladiny;
                    Console.WriteLine();
                }
            }
        }

        public class Prvek
        {
            public int klic;
            public Udalost objekt;
            public Prvek(int klic, Udalost objekt)
            {
                this.klic = klic;
                this.objekt = objekt;
            }
        }
    }

    public class Kalendar
    {
        Halda halda = new Halda();
        public Udalost Prvni()
        {
            Halda.Prvek prvek = halda.OdeberMin();
            if (prvek == null)
            {
                return null;
            }
            else
            {
                return prvek.objekt;
            }
        }
        public void Pridej(Udalost ud)
        {
            halda.Pridej(ud.kdy, ud);            
        }
        public void Odeber(Proces kdo, TypUdalosti co)
        {
            int indexUd = halda.NajdiUdalost(kdo, co);
            if (indexUd >= 0)
            {//událost se našla
                halda.Odeber(halda.NajdiUdalost(kdo, co));
            }
            
        }

    }
    public abstract class Proces
    {
        protected static char oddelovac = '\t';
        public string ID;
        public int patro;
        protected Model model;

        public abstract void Zpracuj(Udalost ud);
        protected void log(string zprava)
        {
            string logZprava = String.Format("{0,5} {1,8} | ", Prevadec.DigitalniPlusMinuty(model.dOteviraciDoba, model.cas), ID) + zprava;
            string lower = logZprava.ToLower();

            if ( model.form.Filtr == "" || ( model.form.Filtr != "" && lower.Contains(model.form.Filtr.ToLower()) ))
            {
                model.form.ZapisDo(logZprava, "log");
            }
        }
    }
    public enum Smer { Nahoru, Dolu }
    public class Lanovka : Proces
    {
        int dobaPrepravy;
        int dobaMeziNalozenim;
        List<Navstevnik> frontaNahoru;
        List<Navstevnik> frontaDolu;
        int naposledySedelNahoru;
        int naposledySedelDolu;
        public Lanovka(Model model, string popis)
        {
            this.model = model;
            string[] popisy = popis.Split(Proces.oddelovac, StringSplitOptions.RemoveEmptyEntries);
            this.ID = popisy[0];
            this.patro = int.Parse(popisy[1]); // TODO: co s patrem u lanovky
            this.dobaPrepravy = int.Parse(popisy[2]);
            this.dobaMeziNalozenim = int.Parse(popisy[3]);
            this.frontaNahoru = new List<Navstevnik>();
            this.frontaDolu = new List<Navstevnik>();
            naposledySedelDolu = -1;
            naposledySedelNahoru = -1;

            

            log($"Vytvořena lanovka {ID}");
        }
        public void Reset()
        {
            naposledySedelNahoru = -1;
            naposledySedelDolu = -1;
        }
        void PrenesPrvnihoVeFronte(ref List<Navstevnik> f, Smer s)
        {

            int patroKam;
            string smer;
            if (s == Smer.Nahoru)
            {
                naposledySedelNahoru = model.cas;
                patroKam = 1;
                smer = "nahoru";
            }
            else
            {
                naposledySedelDolu = model.cas;
                patroKam = 0;
                smer = "dolu";
            }
            Navstevnik navst = f[0];
            f.RemoveAt(0);

            model.Odplanuj(navst, TypUdalosti.Trpelivost);
            model.Naplanuj(model.cas + dobaPrepravy, navst, TypUdalosti.Start);
            model.Naplanuj(model.cas + dobaMeziNalozenim, this, TypUdalosti.Start);
            navst.patro = patroKam;

            log($"{navst.ID} jede lanovkou {smer}");
        }
        public override void Zpracuj(Udalost ud)
        {
            switch (ud.co)
            {
                case TypUdalosti.Start:
                    if (model.cas % dobaMeziNalozenim == 0)
                    {//když se dá nasednout, tak se posadí první ve frontě
                        if (frontaNahoru.Count > 0 && model.cas > naposledySedelNahoru)
                        {//nekdo je ve fronte a nikdo si jeste na tuto sedacku nesedl
                            PrenesPrvnihoVeFronte(ref frontaNahoru, Smer.Nahoru);
                        }

                        if (frontaDolu.Count > 0 && model.cas > naposledySedelDolu)
                        {
                            PrenesPrvnihoVeFronte(ref frontaDolu, Smer.Dolu);
                        }
                    }
                    else
                    {
                        model.Naplanuj((dobaMeziNalozenim - (model.cas % dobaMeziNalozenim)) + model.cas, this, TypUdalosti.Start);
                    }
                

                    

                    break;
                default:
                    break;
            }
        }

        public void ZaradDoFronty(Navstevnik navst)
        {
            if (navst.patro == 0) frontaNahoru.Add(navst);
            else frontaDolu.Add(navst);

            model.Naplanuj(model.cas, this, TypUdalosti.Start);
            
        }
    }
    public abstract class Stanoviste : Proces
    {
        protected bool obsluhuje;
        protected int rychlost;
        protected List<Navstevnik> fronta;
        
        public Stanoviste(Model model, string popis)
        {
            this.model = model;
            obsluhuje = false;
            fronta = new List<Navstevnik>();
            string[] popisy = popis.Split(Proces.oddelovac, StringSplitOptions.RemoveEmptyEntries);
            ID = popisy[0];
            patro = int.Parse(popisy[1]);
            rychlost = int.Parse(popisy[2]);
        }
        public int DelkaFronty
        {
            get { return fronta.Count; }
        }
        public abstract bool ZaradDoFronty(Navstevnik navst);

        public void VyradZFronty(Navstevnik navst)
        {
            fronta.Remove(navst);
        }

        
    }
    public class Obcerstveni : Stanoviste
    {

        public Obcerstveni(Model model, string popis) : base(model, popis)
        {

            model.VsechnaStanoviste.Add(this.ID, this);
        }
        public override void Zpracuj(Udalost ud)
        {
            
        }
        public override bool ZaradDoFronty(Navstevnik navst)
        {
            fronta.Add(navst);
            return true;
        }
    }
    public class Suvenyry : Stanoviste
    {
        public Suvenyry(Model model, string popis) : base(model, popis)
        {

            model.VsechnaStanoviste.Add(this.ID, this);
        }
        public override void Zpracuj(Udalost ud)
        {
            
        }
        public override bool ZaradDoFronty(Navstevnik navst)
        {
            fronta.Add(navst);
            return true;
        }
    }
    public class Expozice : Stanoviste
    {
        public int kapacita;
        public int pocetVolnychMist;
        public Expozice(Model model, string popis) : base(model, popis)
        {
            string[] popisy = popis.Split(Proces.oddelovac,StringSplitOptions.RemoveEmptyEntries);
            kapacita = int.Parse(popisy[3]);

            pocetVolnychMist = kapacita;


            model.VsechnaStanoviste.Add(this.ID, this);
            log($"Vytvořeno {this.GetType().Name} {ID}");
        }
        public override void Zpracuj(Udalost ud)
        {
            switch (ud.co)
            {
                case TypUdalosti.Start:
                    //nastává po obsloužení návštěvníka
                    if (pocetVolnychMist >= 0 && fronta.Count > 0)
                    {//je na řadě čekající z fronty
                        Navstevnik navst = fronta[0];
                        fronta.RemoveAt(0);
                        log($"{navst.ID} je na rade");
                        model.Odplanuj(navst, TypUdalosti.Trpelivost);
                        model.Naplanuj(model.cas + rychlost, navst, TypUdalosti.Obslouzen);
                        model.Naplanuj(model.cas + rychlost, this, TypUdalosti.Start);
                    }
                    else if(pocetVolnychMist >= 0 && pocetVolnychMist < kapacita && fronta.Count == 0)
                    {//vyprázdnila se fronta, uvolňují se místa
                        pocetVolnychMist++;
                    }
                    break;
                default:
                    break;
            }
        }
        public override bool ZaradDoFronty(Navstevnik navst)
        {
            if (pocetVolnychMist > 0)
            {
                pocetVolnychMist--;
                model.Naplanuj(model.cas + rychlost, navst, TypUdalosti.Obslouzen);
                model.Naplanuj(model.cas + rychlost, this, TypUdalosti.Start);
                return false;
            }
            else
            {
                log($"Plno, {navst.ID} zarazen do fronty");
                fronta.Add(navst);
                return true;
            }
        }
    }
    public abstract class Navstevnik : Proces
    {
        protected int trpelivost;
        protected int hlad;
        protected int prichod;
        protected int patroPrichodu;

        protected List<string> stanoviste;// jen jména, ušetří paměť, ale budu muset hledat podle jména (mám dictionary)
        //TODO: možná list speciálních událostí

        public Navstevnik(Model model, int[] popis, List<string> stanoviste)
        {
            this.model = model;
            ID = popis[0].ToString();
            patro = popis[1];
            patroPrichodu = patro;
            trpelivost = popis[2];
            hlad = popis[3];
            prichod = popis[4];
            this.stanoviste = stanoviste;


            log($"Vytvořen {this.GetType().Name} Prichod: {Prevadec.DigitalniPlusMinuty(model.dOteviraciDoba, prichod)} #Stanov. {stanoviste.Count}");
            model.Naplanuj(prichod, this, TypUdalosti.Start);
            model.Naplanuj(prichod + hlad, this, TypUdalosti.Hlad);

        }
        protected abstract Stanoviste VyberDalsiStanoviste();
    }
    public class Navstevnik_0 : Navstevnik //TODO: návštěvníci nejsou hotoví, zatím každý dělá to samé
    {
        public Navstevnik_0(Model model, int[] popis, List<string> stanoviste) : base(model, popis, stanoviste) { }
        public override void Zpracuj(Udalost ud)
        {
            switch (ud.co)
            {
                case TypUdalosti.Start:

                    if (stanoviste.Count > 0)
                    {
                        Stanoviste stan = VyberDalsiStanoviste();
                        if (stan.patro == patro)
                        {// stanoviště je ve stejném patře


                            if (stan.ZaradDoFronty(this))
                            {
                                model.Naplanuj(model.cas + trpelivost, this, TypUdalosti.Trpelivost);
                                log($"Je ve frontě na {stan.ID}");
                            }
                            else
                            {
                                log($"Je na {stan.ID}");
                            }
                        }
                        else
                        {//musí na lanovku

                            model.lanovka.ZaradDoFronty(this);
                            string smer = "nahoru";
                            if (patro == 1) smer = "dolu";
                            log($"Ceka ve fronte na lanovku smer {smer}");
                        }
                    }
                    else
                    {//nemá další stanoviště, na které by se chtěl podívat
                        
                        model.Odplanuj(this, TypUdalosti.Hlad);//už to vydrží domů
                        if (patro == patroPrichodu)
                        {
                            model.stihliVsechno++;
                            log("Odchazi ze zoo");
                        }
                        else
                        {
                            model.lanovka.ZaradDoFronty(this);
                        }
                    
                    }
                    break;

                case TypUdalosti.Trpelivost:
                    if (stanoviste.Count <= 1) ;
                    //poslední stanoviste. To už přetrpí, jinak by se mohl dostat ven o dost později
                    else
                    {
                        log("Trpelivost");
                        Stanoviste stanTrp = model.VsechnaStanoviste[stanoviste[0]];
                        stanTrp.VyradZFronty(this);

                        //přehodí na konec
                        string nenavstiveno = stanoviste[0];
                        stanoviste.RemoveAt(0);
                        stanoviste.Add(nenavstiveno);
                        model.Naplanuj(model.cas, this, TypUdalosti.Start);

                    }

                    break;

                case TypUdalosti.Hlad:

                    log("Hlad");
                    break;

                case TypUdalosti.Obslouzen:

                    log("Odchází z " + stanoviste[0]);
                    //můžeme odškrtnout z listu
                    stanoviste.RemoveAt(0);
                    //jdi na dalsi stanoviste
                    model.Naplanuj(model.cas, this, TypUdalosti.Start);
                    break;

                case TypUdalosti.Specialni:

                    break;
                default:

                    break;
            }
        }
        protected override Stanoviste VyberDalsiStanoviste()
        {
            return model.VsechnaStanoviste[stanoviste[0]];

        }
    }
    public class Navstevnik_1 : Navstevnik
    {
        public Navstevnik_1(Model model, int[] popis, List<string> stanoviste) : base(model, popis, stanoviste) { }
        public override void Zpracuj(Udalost ud)
        {

        }
        protected override Stanoviste VyberDalsiStanoviste()
        {
            return model.VsechnaStanoviste[stanoviste[0]];
        }
    }
    public class Navstevnik_2 : Navstevnik
    {
        public Navstevnik_2(Model model, int[] popis, List<string> stanoviste) : base(model, popis, stanoviste) { }
        public override void Zpracuj(Udalost ud)
        {

        }
        protected override Stanoviste VyberDalsiStanoviste()
        {
            return model.VsechnaStanoviste[stanoviste[0]];
        }
    }
    public class Navstevnik_3 : Navstevnik
    {
        public Navstevnik_3(Model model, int[] popis, List<string> stanoviste) : base(model, popis, stanoviste) { }
        public override void Zpracuj(Udalost ud)
        {

        }
        protected override Stanoviste VyberDalsiStanoviste()
        {
            return model.VsechnaStanoviste[stanoviste[0]];
        }
    }
    public class Model
    {
        public int cas;
        public string dOteviraciDoba;
        int mZaviraciDoba;
        public Dictionary<string,Stanoviste> VsechnaStanoviste;
        public Lanovka lanovka;
        public Form1 form;
        public int stihliVsechno;

        
        Kalendar kalendar;
        Random rnd;

        public Model(Form1 form, Random rnd)
        {
            this.form = form;
            this.rnd = rnd;

            VsechnaStanoviste = new Dictionary<string, Stanoviste>();
            dOteviraciDoba = form.DobaOd;//digitálně

            int mOd = Prevadec.Digitalni2Minuty(form.DobaOd);
            int mDo = Prevadec.Digitalni2Minuty(form.DobaDo);

            mZaviraciDoba = mDo - mOd;//v minutách
            if (mOd >= mDo){ mZaviraciDoba += 24 * 60; }
            
            stihliVsechno = 0;
            cas = 0;

            VytvorStanoviste();
        }
        public string Vypocti(int pocetNavst)
        {
            cas = 0;
            kalendar = new Kalendar();
            stihliVsechno = 0;

            VytvorNavstevniky(pocetNavst);
            lanovka.Reset();

            Udalost ud;
            while((ud = kalendar.Prvni()) != null && ud.kdy <= mZaviraciDoba)
            {
                cas = ud.kdy;
                ud.kdo.Zpracuj(ud);
                
            }

            return $"{stihliVsechno} z {pocetNavst} stihli vše";
        }
        void VytvorStanoviste()
        {
            System.IO.StreamReader soubor = new System.IO.StreamReader(form.VstupniSoubor);

            while (!soubor.EndOfStream)
            {
                string radek = soubor.ReadLine();
                if (radek != "")
                {
                    switch (radek[0])
                    {
                        case 'L':
                            lanovka = new Lanovka(this, radek[2..]);
                            break;
                        case 'O':
                            new Obcerstveni(this, radek[2..]);
                            break;
                        case 'E':
                            new Expozice(this, radek[2..]);
                            break;
                        case 'S':
                            new Suvenyry(this, radek[2..]);
                            break;
                        default:
                            break;
                    }
                }
            }

            soubor.Close();
        }
        
        void VytvorNavstevniky(int pocetNavst)
        {
            for (int i = 0; i < pocetNavst; i++)
            {// TODO: podle vybraného typu návstěvníků vytvářet návštěvníky

                int pocetStanovist = rnd.Next(1, 20); //TODO: snižovat horní hranici podle příchodu
                List<string> stanoviste = new List<string>();

                for (int j = 0; j < pocetStanovist; j++)
                {//vybírání stanovišť
                    int stanIndex = rnd.Next(0, VsechnaStanoviste.Count);
                    stanoviste.Add(VsechnaStanoviste.ElementAt(stanIndex).Value.ID);//jen názvy
                }
                new Navstevnik_0(this, new int[5] { i, rnd.Next(0,2), rnd.Next(5, 240), rnd.Next(20, 360), rnd.Next(0, mZaviraciDoba) }, stanoviste);
                //                                  ID,patro,         trpelivost,       hlad,              prichod,                      stanoviste
            }
        }

        public void Naplanuj(int kdy, Proces kdo, TypUdalosti co) 
        {
            kalendar.Pridej(new Udalost(kdy, kdo, co));
        }
        public void Odplanuj(Proces kdo, TypUdalosti co)
        {
            kalendar.Odeber(kdo, co);
        }
    }

    public class Prevadec
    {
        static (int, int) HodinyMinuty(string digit)
        {
            string[] cas = digit.Split(':');
            int h = int.Parse(cas[0]);
            int m = int.Parse(cas[1]);

            (int hodiny, int minuty) hm = VyresPreteceni((h, m));

            return (hm.hodiny, hm.minuty);

        }
        public static int Digitalni2Minuty(string digit)
        {
            string[] cas = digit.Split(':');
            (int, int) hm = HodinyMinuty(digit);
            return hm.Item1 * 60 + hm.Item2;
        }
        public static bool JeDigitalni(string digit)
        {
            try
            {
                Digitalni2Minuty(digit);

            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }

        static (int hodiny, int minuty) VyresPreteceni((int hodiny, int minuty) cas)
        {
            while (cas.minuty >= 60)
            {
                cas.hodiny += 1;
                cas.minuty -= 60;
            }
            while (cas.hodiny >= 24)
            {
                cas.hodiny -= 24;
            }
            return cas;
        }

        public static string DigitalniPlusMinuty(string digit, int minuty)
        {
            (int hodiny, int minuty) hm = HodinyMinuty(digit);
            hm.hodiny += minuty / 60;
            hm.minuty += minuty % 60;

            hm = VyresPreteceni(hm);

            string h = hm.hodiny.ToString();
            string m = hm.minuty.ToString();
            
            if (hm.hodiny < 10) h = $"0{hm.hodiny}";
            if (hm.minuty < 10) m = $"0{hm.minuty}";

            return $"{h}:{m}";
        }
    }

}
