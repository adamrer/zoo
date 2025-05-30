﻿// Diskrétní simulace zoologické zahrady
// Adam Řeřicha, I. ročník bakalářského studia, studijní skupina 38
// letní semestr 2022/23
// Programování II (NPRG031)



using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace zoo
{
    public enum TypUdalosti { Start, Trpelivost, Hlad, Obslouzen, Dojel, Zavreno };
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
        public int dobaPrepravy;
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
            this.patro = int.Parse(popisy[1]); // dolní patro, označuje lanovku. Lanovka bude mezi patry: patro a patro+1
            this.dobaPrepravy = int.Parse(popisy[2]);
            this.dobaMeziNalozenim = int.Parse(popisy[3]);
            this.frontaNahoru = new List<Navstevnik>();
            this.frontaDolu = new List<Navstevnik>();

            naposledySedelDolu = -1;
            naposledySedelNahoru = -1;

            this.model.lanovky.Add(patro, this);

            log($"Vytvořena lanovka {ID}");
        }
        public int DelkaFrontyNahoru()
        {
            return frontaNahoru.Count;
        }
        public int DelkaFrontyDolu()
        {
            return frontaDolu.Count;
        }
        public void Reset()
        {
            naposledySedelNahoru = -1;
            naposledySedelDolu = -1;
        }
        void NalozZFronty(ref List<Navstevnik> f, Smer s)
        {

            string smer;
            Navstevnik navst = f[0];
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
                    {//naplanuj udalost až bude možné nasednout
                        model.Naplanuj((dobaMeziNalozenim - (model.cas % dobaMeziNalozenim)) + model.cas, this, TypUdalosti.Start);
                    }
                    break;
                default:
                    break;
            }
        }

        public void ZaradDoFronty(Navstevnik navst)
        {
            string smer;

            if (navst.patro == patro)
            {
                frontaNahoru.Add(navst);
                smer = "nahoru";
            }

            else 
            { 
                frontaDolu.Add(navst);
                smer = "dolu";
            }


            model.Naplanuj(model.cas, this, TypUdalosti.Start);

            log($"{navst.ID} ceka ve fronte v {navst.patro}. patre smer {smer}");
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
        public int rychlost;
        protected List<Navstevnik> fronta;
        
        public Stanoviste(Model model, string popis)
        {
            this.model = model;
            obsluhuje = false;
            fronta = new List<Navstevnik>();
            string[] popisy = popis.Split(Proces.oddelovac, StringSplitOptions.RemoveEmptyEntries);
            ID = popisy[0];
            patro = int.Parse(popisy[1]);
            if (patro > model.maxPatro) model.maxPatro = patro;
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
                        navst.obsluhovan = true;
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
            {//je volno
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
            obsluhovan = false;//potřeba kvůli hladu
            jede = false;//potřeba kvůli hladu


            model.pocetNavstVPatre[patro]++;
            log($"Vytvořen {this.GetType().Name} Prichod: {patroPrichodu}.p {Prevadec.DigitalniPlusMinuty(model.dOteviraciDoba, prichod)} #Stanov. {stanoviste.Count}");
            model.Naplanuj(prichod, this, TypUdalosti.Start);
            model.Naplanuj(prichod + hlad, this, TypUdalosti.Hlad);
            model.Naplanuj(model.mZaviraciDoba, this, TypUdalosti.Zavreno);

        }

        public override void Zpracuj(Udalost ud)
        {
            switch (ud.co)
            {
                case TypUdalosti.Start:

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
                            if (model.lanovky.Count > patro-1)
                            {
                                VyberLanovku(stan.patro).ZaradDoFronty(this);

                            }
                            else
                            {
                                log("neni tu lanovka");
                            }
                        }
                    }
                    else
                    {//odchází
                        model.Odplanuj(this, TypUdalosti.Hlad);

                        if (patro == patroPrichodu)
                        {
                            model.stihliVsechno++;
                            model.straveneCasyVZoo[Convert.ToInt32(ID)] = model.cas - prichod;
                            model.pocetNavstVPatre[patro]--;
                            log("Odchazi ze zoo");

                        }
                        else
                        {
                            Lanovka l = VyberLanovku(patroPrichodu);
                            l.ZaradDoFronty(this);
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
                                //vyrad z fronty na lanovku 
                                if (model.VsechnaStanoviste[stanoviste[1]].patro!=patro && VyberLanovku(model.VsechnaStanoviste[stanoviste[1]].patro).VyradZFronty(this)) model.Odplanuj(this, TypUdalosti.Trpelivost);

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
                    model.pocetNavstVPatre[patro]--;
                    if (stanoviste.Count > 0)
                    {
                        Stanoviste st;
                        //vyber stanoviste kam jede
                        try { st = model.VsechnaStanoviste[stanoviste[0]]; }
                        catch { st = model.VsechnaObcerstveni[stanoviste[0]]; }
                        //podle smeru zmen patro
                        
                        if (st.patro > patro) patro++;
                        else if (st.patro == patro) ;// při jízdě si přidal občerstvení se stejným patrem do listu
                        else patro--;

                    }
                    else
                    {//jde ven
                        
                        if (patroPrichodu > patro) patro++;
                        else if (patroPrichodu == patro) ;
                        else patro--;
                    }
                    model.pocetNavstVPatre[patro]++;
                    model.Naplanuj(model.cas, this, TypUdalosti.Start);
                    break;

                case TypUdalosti.Zavreno:
                    if (model.straveneCasyVZoo[Convert.ToInt32(ID)] == 0)
                    {//jestli ještě neodešel, tak se zapiš, jak dlouho jsi byl v zoo.
                        model.straveneCasyVZoo[Convert.ToInt32(ID)] = model.mZaviraciDoba - prichod;
                        model.pocetNavstVPatre[patro]--;
                    }
                    break;
                default:

                    break;
            }
        }
        //první stanoviště v listu musí být to, kde právě je nebo se chystá jít
        protected abstract Stanoviste VyberDalsiStanoviste();
        protected abstract Obcerstveni VyberDalsiObcerstveni(); 
        protected Lanovka VyberLanovku(int patroKam)
        {
            Lanovka lanovka;
            if (patroKam > patro)
            {//jede nahoru, jde na lanovku s nižším patrem, ve kterém je tento návštěvník
                lanovka = model.lanovky[patro];
            }
            else
            {//jede dolu
                lanovka = model.lanovky[patro - 1];
            }
            return lanovka;
        }
    }
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
            {
                Stanoviste stan = model.VsechnaStanoviste[stanoviste[i]];
                int delkaFronty = stan.DelkaFronty;
                if (stan.patro > patro)
                {//když je v jiném patře, přičti delku fronty
                //na ostatní fronty nevidí
                    
                    delkaFronty += VyberLanovku(stan.patro).DelkaFrontyNahoru();
                }
                else if (stan.patro < patro)
                {
                    delkaFronty += VyberLanovku(stan.patro).DelkaFrontyDolu();
                }

                if (delkaFronty < dalsi.DelkaFronty )
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
    public class Navstevnik_3  : Navstevnik
    {//nebudou vybíraví a nají se tam, kde je nejmenší fronta ve stejném patře
     //půjdou do patra, kde je nejméně lidí
        public Navstevnik_3(Model model, int[] popis, List<string> stanoviste, List<string> obcerstveni) : base(model, popis, stanoviste, obcerstveni) { }
        protected override Stanoviste VyberDalsiStanoviste()
        {
            if (model.VsechnaObcerstveni.ContainsKey(stanoviste[0]))
            {// je hladový a musí jít do občerstvení
                return model.VsechnaObcerstveni[stanoviste[0]];
            }
            else
            {
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
                // není žádné další stanoviste v tomto patre. Vem to, které je v patře s nejmenším počtem návštěvníků
                int nejPatro = patro;
                for (int i = 0; i < model.pocetNavstVPatre.Length; i++)
                {
                    if (model.pocetNavstVPatre[i] < model.pocetNavstVPatre[nejPatro] && model.pocetNavstVPatre[i] != 0)
                    {
                        nejPatro = i;
                    }
                }

                for (int i = 0; i < stanoviste.Count; i++)
                {
                    if (model.VsechnaStanoviste[stanoviste[i]].patro == nejPatro)
                    {
                        if (i != 0)
                        {//prohoď
                            string tmp = stanoviste[0];
                            stanoviste[0] = stanoviste[i];
                            stanoviste[i] = tmp;
                        }
                        return model.VsechnaStanoviste[stanoviste[i]];
                    }
                }
                return model.VsechnaStanoviste[stanoviste[0]];
            }

        }
        protected override Obcerstveni VyberDalsiObcerstveni()
        {//vybírá ze všech občerstvení
            Obcerstveni vybraneObc = model.VsechnaObcerstveni.First().Value;

            foreach (KeyValuePair<string, Obcerstveni> obc in model.VsechnaObcerstveni)
            {
                if (vybraneObc.patro != patro && obc.Value.patro == patro)
                {
                    vybraneObc = obc.Value;
                }
                else if (obc.Value.patro == patro && obc.Value.DelkaFronty < vybraneObc.DelkaFronty)
                {//stejné patro a nejmenší fronta
                    vybraneObc = obc.Value;
                }
            }
            return vybraneObc;

        }

    }
    public class Model
    {
        public int cas;
        public string dOteviraciDoba;
        public int mZaviraciDoba;
        public int maxPatro;
        public Dictionary<string,Stanoviste> VsechnaStanoviste;
        public Dictionary<string, Obcerstveni> VsechnaObcerstveni;
        public Dictionary<int, Lanovka> lanovky;//jedna lanovka na patro
        public Form1 form;
        public int stihliVsechno;
        public int nemuzouStihnout;
        public int[] straveneCasyVZoo;
        public int[] pocetNavstVPatre;
        
        Kalendar kalendar;
        Random rnd;

        public Model(Form1 form, Random rnd)
        {
            this.form = form;
            this.rnd = rnd;

            VsechnaStanoviste = new Dictionary<string, Stanoviste>();//bez občerstvení
            VsechnaObcerstveni = new Dictionary<string, Obcerstveni>();
            lanovky = new Dictionary<int, Lanovka>();

            dOteviraciDoba = form.DobaOd;//digitálně
            int mOd = Prevadec.Digitalni2Minuty(form.DobaOd);
            int mDo = Prevadec.Digitalni2Minuty(form.DobaDo);
            mZaviraciDoba = mDo - mOd;//v minutách
            if (mOd >= mDo){ mZaviraciDoba += 24 * 60; }
            
            maxPatro = 0;

            VytvorStanoviste();
        }
        public string Vypocti(int pocetNavst)
        {
            cas = 0;
            kalendar = new Kalendar();
            stihliVsechno = 0;
            nemuzouStihnout = 0;
            straveneCasyVZoo = new int[pocetNavst];
            pocetNavstVPatre = new int[maxPatro + 1];

            VytvorNavstevniky(pocetNavst);
            
            foreach (KeyValuePair<int, Lanovka> parLanovka in lanovky)
            {
                parLanovka.Value.Reset();
            }

            Udalost ud;
            while((ud = kalendar.Prvni()) != null && ud.kdy <= mZaviraciDoba)
            {
                cas = ud.kdy;
                ud.kdo.Zpracuj(ud);
                
            }

            string vystup;
            if (form.CheckData)
            {
                vystup = $"{stihliVsechno},{pocetNavst},{nemuzouStihnout},{Prumer(straveneCasyVZoo)}";
            }
            else
            {
                vystup = $"{stihliVsechno} z {pocetNavst} stihli vše. {nemuzouStihnout} mělo moc velký plán. \n" +
                         $"Průměrný čas návštěvníka v zoo {Prevadec.DigitalniPlusMinuty("00:00", Prumer(straveneCasyVZoo))}";
            }
            return vystup;
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
                            new Lanovka(this, radek[2..]);

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
            {

                int pocetStanovist = rnd.Next(form.PocetStan_min, form.PocetStan_max); 
                int pocetObcerstveni = rnd.Next(form.PocetObc_min, form.PocetObc_max);

                List<string> stanoviste = new List<string>();
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
                int[] popis = new int[6] { 
                    //jméno (ID)
                    i, 
                    //patro, ve kterém přijde
                    rnd.Next(0, maxPatro), 
                    //za jak dlouho mu dojde trpělivost ve frontě
                    rnd.Next(form.Trpelivost_min, form.Trpelivost_max), 
                    //za jak dlouho dostane hlad (v minutách)
                    rnd.Next(form.Hlad_min, form.Hlad_max),
                    //jak dlouho bude jíst
                    rnd.Next(form.Jezeni_min, form.Jezeni_max), 
                    //max minut od času otevření (příchod)
                    rnd.Next(0, Prevadec.Digitalni2Minuty(Prevadec.DigitalniPlusMinuty("00:00", Prevadec.Digitalni2Minuty(form.Prichod))) - Prevadec.Digitalni2Minuty(dOteviraciDoba)) 
                };

                //vytvoří návštěvníka a vypočítá, jestli to jde stihnout v nejlepším případě
                if (!MuzeToStihnout(stanoviste, obcerstveni, popis, RozhodniDruhNavstAVytvor(i, popis, stanoviste, obcerstveni)))
                {
                    nemuzouStihnout++;
                }

            }
        }
        bool MuzeToStihnout(List<string> stanoviste, List<string> obcerstveni, int[] popis, int druh)
        {//sečte rychlosti stanovist ze stanoviste, z toho zjistí, kolikrát bude jíst a rychlost jezení se přičte
        //neřeší možné fronty, které nastanou při simulaci
            int minDelkaNavstevy = 0;
            int p = VsechnaStanoviste[stanoviste[0]].patro;
            foreach (string stan in stanoviste)
            {
                Stanoviste st = VsechnaStanoviste[stan];
                if (st.patro > p && druh == 0)//jen pro návštěvníka druhu 0
                {
                    while (st.patro > p)
                    {
                        minDelkaNavstevy += lanovky[p].dobaPrepravy;
                        p++;
                    }
                }
                else if (st.patro < p && druh == 0)
                {
                    while (st.patro < p)
                    {
                        minDelkaNavstevy += lanovky[p - 1].dobaPrepravy;
                        p--;
                    }
                }
                minDelkaNavstevy += st.rychlost;
            }
            int delkaStan = minDelkaNavstevy;//čas se stanovišti
            for (int i = 0; i < delkaStan / popis[3] && i < obcerstveni.Count; i++)
            {
                minDelkaNavstevy += VsechnaObcerstveni[obcerstveni[i]].rychlost;
            }

            //form.ZapisDo($"minimalne stravi v zoo {Prevadec.DigitalniPlusMinuty("00:00",minDelkaNavstevy)} casu\n" +
            //    $"cas na jidlo: {Prevadec.DigitalniPlusMinuty("00:00",(mZaviraciDoba - popis[5]) - minDelkaNavstevy)}\n" +
            //    $"bude jíst {minDelkaNavstevy / popis[3]}x {Prevadec.DigitalniPlusMinuty("00:00", popis[4])}", "log");

            //      stanoviste          jezení
            int d = minDelkaNavstevy + (minDelkaNavstevy / popis[3]) * popis[4];

            return (d < (mZaviraciDoba - popis[5]));//nezapočítána přeprava při občerstvení
            
        }
        int RozhodniDruhNavstAVytvor(int index, int[] popis, List<string> stanoviste, List<string> obcerstveni)
        {
            int druh;
            switch (form.VybranyTyp)
            {
                case 0:
                    druh = 0;
                    new Navstevnik_0(this, popis, stanoviste, obcerstveni);
                    break;
                case 1:
                    druh = 1;
                    new Navstevnik_1(this, popis, stanoviste, obcerstveni);
                    break;
                case 2:
                    druh = 2;
                    new Navstevnik_2(this, popis, stanoviste, obcerstveni);
                    break;
                case 3:
                    druh = 3;
                    new Navstevnik_3(this, popis, stanoviste, obcerstveni);
                    break;
                case 4:
                    switch (index % 4)
                    {
                        case 0:
                            druh = 0;
                            new Navstevnik_0(this, popis, stanoviste, obcerstveni);
                            break;
                        case 1:
                            druh = 1;
                            new Navstevnik_1(this, popis, stanoviste, obcerstveni);
                            break;
                        case 2:
                            druh = 2;
                            new Navstevnik_2(this, popis, stanoviste, obcerstveni);
                            break;
                        case 3:
                            druh = 3;
                            new Navstevnik_3(this, popis, stanoviste, obcerstveni);
                            break;
                        default:
                            druh = -1;
                            break;
                    }
                    break;
                default:
                    druh = -1;
                    break;
            }
            return druh;
        }

        public void Naplanuj(int kdy, Proces kdo, TypUdalosti co) 
        {
            kalendar.Pridej(new Udalost(kdy, kdo, co));
        }
        public void Odplanuj(Proces kdo, TypUdalosti co)
        {
            kalendar.Odeber(kdo, co);
        }
        public int Prumer(int[] hodnoty)
        {
            int suma = 0;
            foreach (int hodnota in hodnoty)
            {
                suma += hodnota;
            }
            return (suma / hodnoty.Length);
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
