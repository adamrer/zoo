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
    public enum TypUdalosti { Start, Trpelivost, Hlad, Obslouzen, Dojel, Specialni };
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
            //TODO: filtry oddělené čárkou
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
        Dictionary<int, List<Navstevnik>> fronty;//TODO: fronty v dictionary. 
        int horniPatro;//TODO: lanovka může být mezi jinými patry než 0 a 1
        
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
        void NalozZFronty(ref List<Navstevnik> f, Smer s)
        {

            string smer;
            if (s == Smer.Nahoru)
            {
                naposledySedelNahoru = model.cas;
                smer = "nahoru";
            }
            else
            {
                naposledySedelDolu = model.cas;
                smer = "dolu";
            }
            Navstevnik navst = f[0];
            f.RemoveAt(0);

            model.Odplanuj(navst, TypUdalosti.Trpelivost);
            model.Naplanuj(model.cas + dobaPrepravy, navst, TypUdalosti.Dojel);
            model.Naplanuj(model.cas + dobaMeziNalozenim, this, TypUdalosti.Start);
            navst.jede = true;

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
                            NalozZFronty(ref frontaNahoru, Smer.Nahoru);
                        }

                        if (frontaDolu.Count > 0 && model.cas > naposledySedelDolu)
                        {
                            NalozZFronty(ref frontaDolu, Smer.Dolu);
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

        public bool VyradZFronty(Navstevnik navst)
        {
            bool jeVeFronte = frontaNahoru.Remove(navst);
            if (jeVeFronte) return jeVeFronte;//vyrazen

            jeVeFronte = frontaDolu.Remove(navst);
            if (jeVeFronte) return jeVeFronte;//vyrazen
            
            return jeVeFronte;//nebyl tam
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
            log($"Vytvořeno {this.GetType().Name} {this.ID}");
        }
        public int DelkaFronty
        {
            get { return fronta.Count; }
        }
        public abstract bool ZaradDoFronty(Navstevnik navst);

        public bool VyradZFronty(Navstevnik navst)
        {
            return fronta.Remove(navst);
        }

        
    }
    public abstract class Obchod : Stanoviste
    {
        public Obchod(Model model, string popis) : base(model, popis)
        {

        }
        public override void Zpracuj(Udalost ud)
        {
            switch (ud.co)
            {
                case TypUdalosti.Start:
                    if (fronta.Count == 0)
                    {
                        obsluhuje = false;
                    }
                    else
                    {
                        Navstevnik navst = fronta[0];
                        fronta.RemoveAt(0);
                        model.Odplanuj(navst, TypUdalosti.Trpelivost);
                        model.Naplanuj(model.cas + rychlost, navst, TypUdalosti.Obslouzen);

                        model.Naplanuj(model.cas + rychlost, this, TypUdalosti.Start);

                    }
                    break;
                default:
                    break;
            }
        }
        public override bool ZaradDoFronty(Navstevnik navst)
        {
            fronta.Add(navst);

            if (obsluhuje) ;
            else
            {
                obsluhuje = true;
                model.Naplanuj(model.cas, this, TypUdalosti.Start);

            }
            return true;
        }

    }
    public class Obcerstveni : Obchod

    {

        public Obcerstveni(Model model, string popis) : base(model, popis)
        {

            model.VsechnaObcerstveni.Add(this.ID, this);
        }
    }
    public class Suvenyry : Obchod
    {
        public Suvenyry(Model model, string popis) : base(model, popis)
        {
            model.VsechnaStanoviste.Add(this.ID, this);
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
                        navst.obsluhovan = true;
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
                navst.obsluhovan = true;
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
        protected int rychlostJezeni;
        protected int patroPrichodu;//vrací se k autu
        protected int indexDalsihoObc;
        public bool obsluhovan;
        public bool jede;

        protected List<string> obcerstveni;
        protected List<string> stanoviste;// jen jména, ušetří paměť, ale budu muset hledat podle jména (mám dictionary)
        //TODO: možná list speciálních událostí

        public Navstevnik(Model model, int[] popis, List<string> stanoviste, List<string> obcerstveni)
        {
            this.model = model;
            ID = popis[0].ToString();
            patro = popis[1];
            patroPrichodu = patro;
            trpelivost = popis[2];
            hlad = popis[3];
            rychlostJezeni = popis[4];
            prichod = popis[5];
            this.stanoviste = stanoviste;
            this.obcerstveni = obcerstveni;
            indexDalsihoObc = 0;
            obsluhovan = false;
            jede = false;



        log($"Vytvořen {this.GetType().Name} Prichod: {Prevadec.DigitalniPlusMinuty(model.dOteviraciDoba, prichod)} #Stanov. {stanoviste.Count}");
            model.Naplanuj(prichod, this, TypUdalosti.Start);
            model.Naplanuj(prichod + hlad, this, TypUdalosti.Hlad);

        }
        public override void Zpracuj(Udalost ud)
        {
            switch (ud.co)
            {
                case TypUdalosti.Start:

                    //if (!obsluhovan)
                    { 
                        if (stanoviste.Count > 0)
                        {//má kam jít

                            Stanoviste stan = VyberDalsiStanoviste();
                            if (stan.patro == patro)
                            {// stanoviště je ve stejném patře


                                if (stan.ZaradDoFronty(this))
                                {//dostal se do fronty
                                    model.Naplanuj(model.cas + trpelivost, this, TypUdalosti.Trpelivost);
                                    log($"Je ve frontě na {stan.ID}");
                                }
                                else
                                {//je rovnou obsloužen
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
                        {//odchází
                            model.Odplanuj(this, TypUdalosti.Hlad);

                            if (patro == patroPrichodu)
                            {
                                model.stihliVsechno++;
                                log("Odchazi ze zoo");
                            }
                            else
                            {
                                model.lanovka.ZaradDoFronty(this);
                                model.Naplanuj(model.cas + trpelivost, this, TypUdalosti.Trpelivost);
                            }

                        }
                    }

                    break;

                case TypUdalosti.Trpelivost:
                    if (stanoviste.Count <= 1) ;
                    //poslední stanoviste. To už přetrpí, jinak by se mohl dostat ven o dost později
                    else
                    {
                        Stanoviste stanTrp;
                        log("Trpelivost");

                        if (model.VsechnaStanoviste.ContainsKey(stanoviste[0]))
                        {
                            stanTrp = model.VsechnaStanoviste[stanoviste[0]];

                            //přehodí na konec
                            string nenavstiveno = stanoviste[0];
                            stanoviste.RemoveAt(0);
                            stanoviste.Add(nenavstiveno);

                        }
                        else
                        {// vybere jiné občerstvení, když mu dojde trpělivost u občerstvení
                            stanTrp = model.VsechnaObcerstveni[stanoviste[0]];
                            stanoviste[0] = VyberDalsiObcerstveni().ID;
                        }
                        stanTrp.VyradZFronty(this);

                        model.Naplanuj(model.cas, this, TypUdalosti.Start);

                    }

                    break;

                case TypUdalosti.Hlad:
                    if (stanoviste.Count <= 1)
                    {//vydrží domu
                    }
                    else
                    {
                        Obcerstveni obc = VyberDalsiObcerstveni();
                        if (model.VsechnaStanoviste[stanoviste[0]].VyradZFronty(this))
                        {
                            model.Odplanuj(this, TypUdalosti.Trpelivost);
                        }
                        if (obsluhovan)
                        {// nechá se doobsloužit a pak se pujde najíst
                            stanoviste.Insert(1, obc.ID);
                        }
                        else
                        {//půjde se rovnou najíst
                            stanoviste.Insert(0, obc.ID);
                            if (!jede)
                            {
                                if (model.lanovka.VyradZFronty(this)) model.Odplanuj(this, TypUdalosti.Trpelivost);

                                model.Naplanuj(model.cas, this, TypUdalosti.Start);
                                //příště navštíví občerstvení

                            }//když jede, tak se zavolá start při vystupování
                             //jinak by se start volal dvakrát


                        }


                        log($"Má hlad. Nají se {obc.ID}");

                    }
                    break;

                case TypUdalosti.Obslouzen:
                    obsluhovan = false;
                    if (stanoviste.Count != 0)
                    {
                        if (model.VsechnaObcerstveni.ContainsKey(stanoviste[0]))
                        {//pokud byl obslouzen u obcerstveni

                            //nají se
                            model.Naplanuj(model.cas + rychlostJezeni, this, TypUdalosti.Start);
                            log($"Jí {stanoviste[0]}");
                            //naplánuje hlad
                            model.Naplanuj(model.cas + rychlostJezeni + hlad, this, TypUdalosti.Hlad);
                        }
                        else
                        {//když jí, tak nemůže jít dál
                         //jdi na dalsi stanoviste
                            model.Naplanuj(model.cas, this, TypUdalosti.Start);
                            log("Odchází z " + stanoviste[0]);
                        }
                        //můžeme odškrtnout z listu
                        stanoviste.RemoveAt(0);
                    }


                    break;

                case TypUdalosti.Dojel:
                    log("Dojel lanovkou");
                    jede = false;
                    if (patro == 0) patro = 1;
                    else patro = 0;
                    model.Naplanuj(model.cas, this, TypUdalosti.Start);
                    break;
                case TypUdalosti.Specialni:

                    break;
                default:

                    break;
            }
        }
        //první stanoviště v listu musí být to, kde právě je nebo se chystá jít
        protected abstract Stanoviste VyberDalsiStanoviste();
        protected abstract Obcerstveni VyberDalsiObcerstveni();
    }
    //TODO: návštěvníci nejsou hotoví, zatím každý dělá to samé
    public class Navstevnik_0 : Navstevnik 
    {//Základní návštěvník. Vždy si vybere to první stanoviště z listu.
        public Navstevnik_0(Model model, int[] popis, List<string> stanoviste, List<string> obcerstveni) : base(model, popis, stanoviste, obcerstveni) { }
        protected override Stanoviste VyberDalsiStanoviste()
        {
            if (model.VsechnaStanoviste.ContainsKey(stanoviste[0]))
            {
                return model.VsechnaStanoviste[stanoviste[0]];
            }
            else
            {
                return model.VsechnaObcerstveni[stanoviste[0]];
            }

        }
        protected override Obcerstveni VyberDalsiObcerstveni()
        {
            Obcerstveni vybraneObc = model.VsechnaObcerstveni[obcerstveni[indexDalsihoObc]];
            indexDalsihoObc = (indexDalsihoObc + 1) % obcerstveni.Count;
            return vybraneObc;
        }
    }
    public class Navstevnik_1 : Navstevnik
    {//Vybere stanoviště, které je ve stejném patře (i občerstvení)
        public Navstevnik_1(Model model, int[] popis, List<string> stanoviste, List<string> obcerstveni) : base(model, popis, stanoviste, obcerstveni) 
        {

        }
        
        protected override Stanoviste VyberDalsiStanoviste()
        {

            if (model.VsechnaObcerstveni.ContainsKey(stanoviste[0]))
            {// je hladový a musí jít do občerstvení
                return model.VsechnaObcerstveni[stanoviste[0]];
            }
            else
            {// vyber stanoviste na svém patře
                Stanoviste dalsi;
                for (int i = 0; i < stanoviste.Count; i++)
                {
                    dalsi = model.VsechnaStanoviste[stanoviste[i]];
                    if (dalsi.patro == patro)
                    {// našlo se stanoviste ve stejném patře
                        //přehoď na začátek
                        string tmp = stanoviste[0];
                        stanoviste[0] = stanoviste[i];
                        stanoviste[i] = tmp;
                        return dalsi;
                    }
                }
                // není žádné další stanoviste v tomto patre. Vem první
                return model.VsechnaStanoviste[stanoviste[0]];
            }
        }
        protected override Obcerstveni VyberDalsiObcerstveni()
        {
            Obcerstveni vybraneObc = model.VsechnaObcerstveni[obcerstveni[indexDalsihoObc]];
            if (vybraneObc.patro == patro)
            {
                indexDalsihoObc = (indexDalsihoObc + 1) % obcerstveni.Count;
                return vybraneObc;
            }
            for (int i = 0; i < obcerstveni.Count; i++)
            {//najdi obcerstveni ve stejném patře
                vybraneObc = model.VsechnaObcerstveni[obcerstveni[i]];
                if (vybraneObc.patro == patro)
                {
                    indexDalsihoObc = (i + 1) % obcerstveni.Count;
                    //přehoď na začátek
                    string tmp = obcerstveni[0];
                    obcerstveni[0] = obcerstveni[i];
                    obcerstveni[i] = tmp;
                    return vybraneObc;
                }

            }
            //když nenajdeš
            return model.VsechnaObcerstveni[obcerstveni[indexDalsihoObc]];
        }

    }
    public class Navstevnik_2 : Navstevnik
    {//Vybere stanoviště s nejmenší frontou
        public Navstevnik_2(Model model, int[] popis, List<string> stanoviste, List<string> obcerstveni) : base(model, popis, stanoviste, obcerstveni) { }
        
        protected override Stanoviste VyberDalsiStanoviste()
        {
            if (model.VsechnaObcerstveni.ContainsKey(stanoviste[0]))
            {//stanoviště je občerstvení. nemůžeme měnit
                return model.VsechnaObcerstveni[stanoviste[0]];
            }
            Stanoviste dalsi = model.VsechnaStanoviste[stanoviste[0]];
            for (int i = 0; i < stanoviste.Count; i++)
            {//TODO: přičítat frontu u lanovky, když bude stanoviště v jiném patře
                if (model.VsechnaStanoviste[stanoviste[i]].DelkaFronty < dalsi.DelkaFronty )
                {
                    dalsi = model.VsechnaStanoviste[stanoviste[i]];
                    if (i != 0)
                    {//prohoď
                        string tmp = stanoviste[0];
                        stanoviste[0] = stanoviste[i];
                        stanoviste[i] = tmp;                        
                    }
                }
            }
            
            return dalsi;

        }
        protected override Obcerstveni VyberDalsiObcerstveni()
        {
            Obcerstveni vybraneObc = model.VsechnaObcerstveni[obcerstveni[0]];
            for (int i = 1; i < obcerstveni.Count; i++)
            {
                if (model.VsechnaObcerstveni[obcerstveni[i]].DelkaFronty < vybraneObc.DelkaFronty)
                {
                    vybraneObc = model.VsechnaObcerstveni[obcerstveni[i]];
                }
            }
            return vybraneObc;
        }
    }
    public class Navstevnik_3 : Navstevnik
    {
        public Navstevnik_3(Model model, int[] popis, List<string> stanoviste, List<string> obcerstveni) : base(model, popis, stanoviste, obcerstveni) { }
        protected override Stanoviste VyberDalsiStanoviste()
        {
            return model.VsechnaStanoviste[stanoviste[0]];

        }
        protected override Obcerstveni VyberDalsiObcerstveni()
        {
            Obcerstveni vybraneObc = model.VsechnaObcerstveni[obcerstveni[indexDalsihoObc]];
            indexDalsihoObc = (indexDalsihoObc + 1) % obcerstveni.Count;
            return vybraneObc;
        }

    }
    public class Model
    {
        public int cas;
        public string dOteviraciDoba;
        int mZaviraciDoba;
        public Dictionary<string,Stanoviste> VsechnaStanoviste;
        public Dictionary<string, Obcerstveni> VsechnaObcerstveni;
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
            VsechnaObcerstveni = new Dictionary<string, Obcerstveni>();
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

                int pocetStanovist = rnd.Next(1, VsechnaStanoviste.Count); //TODO: snižovat horní hranici počtu stanovist podle příchodu
                int pocetObcerstveni = rnd.Next(1, VsechnaObcerstveni.Count);

                List<string> stanoviste = new List<string>();//TODO: velikost se nebude zvyšovat. Dá se použít cyklická fronta
                List<string> obcerstveni = new List<string>();

                for (int j = 0; j < pocetStanovist; j++)
                {//vybírání stanovišť
                    int stanIndex = rnd.Next(0, VsechnaStanoviste.Count);
                    stanoviste.Add(VsechnaStanoviste.ElementAt(stanIndex).Value.ID);//jen názvy
                }
                for (int j = 0; j < pocetObcerstveni; j++)
                {
                    int obcIndex = rnd.Next(0, VsechnaObcerstveni.Count);
                    obcerstveni.Add(VsechnaObcerstveni.ElementAt(obcIndex).Value.ID);

                }
                int[] popis = new int[6] { i, rnd.Next(0, 2), rnd.Next(10, 240), rnd.Next(60, 360), rnd.Next(5, 90), rnd.Next(0, mZaviraciDoba/2) };
                //                         ID,patro,          trpelivost,       hlad,              rychlost jezeni, prichod,
                new Navstevnik_1(this, popis, stanoviste, obcerstveni);
                //RozhodniDruhNavstAVytvor(i, popis, stanoviste, obcerstveni);
                
            }
        }

        void RozhodniDruhNavstAVytvor(int index, int[] popis, List<string> stanoviste, List<string> obcerstveni)
        {
            switch (form.VybranyTyp)
            {
                case 0:
                    new Navstevnik_0(this, popis, stanoviste, obcerstveni);
                    break;
                case 1:
                    new Navstevnik_1(this, popis, stanoviste, obcerstveni);
                    break;
                case 2:
                    new Navstevnik_2(this, popis, stanoviste, obcerstveni);
                    break;
                case 3:
                    switch (index%3)
                    {
                        case 0:
                            new Navstevnik_0(this, popis, stanoviste, obcerstveni);
                            break;
                        case 1:
                            new Navstevnik_1(this, popis, stanoviste, obcerstveni);
                            break;
                        case 2:
                            new Navstevnik_2(this, popis, stanoviste, obcerstveni);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
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
