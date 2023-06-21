using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        // TODO: dodělat konstruktor udalosti
    }
    public class Halda
    {
        public List<Prvek> prvky; //TODO: dát zpátky private
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
        public Halda halda = new Halda(); //TODO: vrátit na private
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
            if (indexUd >= 0)//jestli se událost našla
            {
                halda.Odeber(halda.NajdiUdalost(kdo, co));// TODO: zkusit jestli funguje
            }
            
        }

    }
    public abstract class Proces
    {
        protected static char oddelovac = ',';
        public string ID;
        public int patro;
        protected Model model;

        public abstract void Zpracuj(Udalost ud);
        protected void log(string zprava)
        {
            //if (this.ID == "1")
            {
                string l = String.Format("{0,5} {1,12} | ", Prevadec.DigitalniPlusMinuty(model.dOteviraciDoba, model.cas), ID);
                model.form.ZapisDo(l + zprava + '\n', "log");

            }
        }
    }
    public class Lanovka : Proces
    {
        int dobaPrepravy;
        int dobaMeziNalozenim;
        List<Navstevnik> frontaNahoru;
        List<Navstevnik> frontaDolu;
        public Lanovka(Model model, string popis)
        {
            string[] popisy = popis.Split(Proces.oddelovac);
            this.ID = popisy[0];
            this.patro = int.Parse(popisy[1]);
            this.dobaPrepravy = int.Parse(popisy[2]);
            this.dobaMeziNalozenim = int.Parse(popisy[3]);
            this.frontaNahoru = new List<Navstevnik>();
            this.frontaDolu = new List<Navstevnik>();
            log($"Vytvořena lanovka {ID}");
        }
        public override void Zpracuj(Udalost ud)
        {
            switch (ud.co)
            {
                case TypUdalosti.Start:
                    break;
                default:
                    break;
            }
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
            string[] popisy = popis.Split(Proces.oddelovac);
            ID = popisy[0];
            patro = int.Parse(popisy[1]);
            rychlost = int.Parse(popisy[2]);
        }
        public int DelkaFronty()
        {
            return fronta.Count;
        }
        public abstract bool ZaradDoFronty(Navstevnik navst);

        
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
            string[] popisy = popis.Split(Proces.oddelovac);
            kapacita = int.Parse(popisy[3]);

            pocetVolnychMist = kapacita;


            model.VsechnaStanoviste.Add(this.ID, this);
            log($"Vytvořeno {this.GetType().Name} {ID}");
        }
        public override void Zpracuj(Udalost ud)
        {
            switch (ud.co)
            {
                case TypUdalosti.Start://nastává po obsloužení návštěvníka
                    if (pocetVolnychMist >= 0 && fronta.Count > 0)
                    {//uvolnilo se místo a může jít na expozici člověk z fronty
                    //je na řadě čekající z fronty
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
                case TypUdalosti.Obslouzen://

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

        protected List<string> stanoviste;// jen jména, ušetří paměť, ale budu muset hledat podle jména (mám dictionary)
        //možná list speciálních událostí

        public Navstevnik(Model model, int[] popis, List<string> stanoviste)
        {
            this.model = model;
            ID = popis[0].ToString();
            patro = popis[1];
            trpelivost = popis[2];
            hlad = popis[3];
            prichod = popis[4];
            this.stanoviste = stanoviste;


            log($"Vytvořen {this.GetType().Name} {ID}");
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
                    {
                        model.stihliVsechno++;
                        model.Odplanuj(this, TypUdalosti.Hlad);
                        log("Odchazi ze zoo");
                    }
                    break;
                case TypUdalosti.Trpelivost:
                    log("Trpelivost");
                    break;
                case TypUdalosti.Hlad:
                    log("Hlad");
                    break;
                case TypUdalosti.Obslouzen:
                    log("Odchází z " + stanoviste[0]);
                    stanoviste.RemoveAt(0);
                    model.Naplanuj(model.cas, this, TypUdalosti.Start);//jdi na dalsi stanoviste
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
        public Form1 form;
        public int stihliVsechno;

        
        Kalendar kalendar;
        Lanovka lanovka;
        Random rnd;

        public Model(Form1 form, Random rnd)
        {
            this.form = form;
            this.rnd = rnd;
            VsechnaStanoviste = new Dictionary<string, Stanoviste>();
            dOteviraciDoba = form.dobaOd();
            mZaviraciDoba = Prevadec.Digitalni2Minuty(form.dobaDo()) - Prevadec.Digitalni2Minuty(form.dobaOd());
            stihliVsechno = 0;
            VytvorStanoviste();
        }
        public void Vypocti(int pocetNavst)
        {
            cas = 0;
            kalendar = new Kalendar();
            stihliVsechno = 0;
            VytvorNavstevniky(pocetNavst);
            Udalost ud;
            while((ud = kalendar.Prvni()) != null && ud.kdy <= mZaviraciDoba)
            {
                //if (ud.kdy > kalendar.halda.prvky[1].klic || ud.kdy > kalendar.halda.prvky[2].klic)
                {
                    //Console.WriteLine(  );
                }
                cas = ud.kdy;
                ud.kdo.Zpracuj(ud);
                
            }

            Vystup($"{stihliVsechno} z {pocetNavst} stihli vše");
        }
        void Vystup(string zprava)
        {
            form.ZapisDo(zprava + '\n', "out");
        }
        void VytvorStanoviste()
        {
            System.IO.StreamReader soubor = new System.IO.StreamReader(form.VstupniSoubor());

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
        }
        
        void VytvorNavstevniky(int pocetNavst)
        {
            for (int i = 0; i < pocetNavst; i++)
            {// TODO: podle vybraného typu návstěvníků vytvářet návštěvníky

                int pocetStanovist = rnd.Next(1, 20);
                List<string> stanoviste = new List<string>();

                for (int j = 0; j < pocetStanovist; j++)
                {//vybírání stanovišť
                    int stanIndex = rnd.Next(0, VsechnaStanoviste.Count);
                    stanoviste.Add(VsechnaStanoviste.ElementAt(stanIndex).Value.ID);//jen názvy
                }
                new Navstevnik_0(this, new int[5] { i, rnd.Next(0,2), rnd.Next(5, 240), rnd.Next(20, 360), rnd.Next(0, mZaviraciDoba) }, stanoviste);
                //                                  ID,patro,         trpelivost,       hlad,              prichod,                     stanoviste
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

            return (int.Parse(cas[0]), int.Parse(cas[1]));

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

        public static string DigitalniPlusMinuty(string digit, int minuty)
        {
            (int hodiny, int minuty) hm = HodinyMinuty(digit);
            hm.hodiny += minuty / 60;
            hm.minuty += minuty % 60;
            string h = hm.hodiny.ToString();
            string m = hm.minuty.ToString();
            
            if (hm.hodiny < 10) h = $"0{hm.hodiny}";
            if (hm.minuty < 10) m = $"0{hm.minuty}";

            return $"{h}:{m}";
        }
    }

}
