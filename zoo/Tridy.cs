using System;
using System.Collections.Generic;
using System.Linq;
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
            if (prvky.Count == 1)
            {
                Prvek prvek = prvky[0];
                prvky.Remove(prvek);
                return prvek;
            }

            Prvek koren = prvky[0];
            prvky[0] = prvky[prvky.Count - 1];
            prvky.Remove(prvky[prvky.Count - 1]);
            BublejDolu(0);

            return koren;
        }
        void BublejNahoru(int index)
        {
            int rodic = index / 2;
            if (index <= 0)
            {
                return;
            }

            if (prvky[index].klic < prvky[rodic].klic)
            {
                Prvek tmp = prvky[index];
                prvky[index] = prvky[rodic];
                prvky[rodic] = tmp;

            }
            BublejNahoru(rodic);
        }
        void BublejDolu(int index)
        {
            int levy = 2 * index + 1;
            int pravy = 2 * index + 2;

            int nejmensi = index;
            if (levy < prvky.Count && prvky[levy].klic < prvky[nejmensi].klic)
            {
                nejmensi = levy;
            }
            if (pravy < prvky.Count && prvky[pravy].klic < prvky[nejmensi].klic)
            {
                nejmensi = pravy;
            }
            if (nejmensi != index)
            {
                Prvek tmp = prvky[index];
                prvky[index] = prvky[nejmensi];
                prvky[nejmensi] = tmp;

                BublejDolu(nejmensi);
            }
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
            return halda.OdeberMin().objekt;
        }
        public void Pridej(Udalost ud)
        {
            halda.Pridej(ud.kdy, ud);            
        }
        public void Odeber(Proces kdo, TypUdalosti co)
        {

        }
    public abstract class Proces
    {
        public string ID;
        public int patro;
        protected Model model;
        // TODO: dodělat proces
        public abstract void Zpracuj(Udalost ud);
        protected void log(string zprava)
        {
            model.form.ZapisDo($"{model.cas}, {ID} | {zprava}\n", "log");
        }
    }
    public class Lanovka : Proces
    {
        int dobaPrepravy;
        int dobaMeziNalozenim;
        List<Navstevnik> fronta;
        public Lanovka(Model model, string popis)
        {
            string[] popisy = popis.Split(' ');
            this.ID = popisy[0];
            this.patro = int.Parse(popisy[1]);
            this.dobaPrepravy = int.Parse(popisy[2]);
            this.dobaMeziNalozenim = int.Parse(popisy[3]);
            this.fronta = new List<Navstevnik>();
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
            obsluhuje = false;
            fronta = new List<Navstevnik>();
            model.VsechnaStanoviste.Add(this.ID, this);
            log($"Vytvořeno {this.GetType().Name} {ID}");
        }
        public int DelkaFronty()
        {
            return fronta.Count;
        }

        
    }
    public class Obcerstveni : Stanoviste
    {
        public Obcerstveni(Model model, string popis) : base(model, popis)
        {

        }
        public override void Zpracuj(Udalost ud)
        {
            
        }
    }
    public class Suvenyry : Stanoviste
    {
        public Suvenyry(Model model, string popis) : base(model, popis)
        {

        }
        public override void Zpracuj(Udalost ud)
        {
            
        }
    }
    public class Expozice : Stanoviste
    {
        public Expozice(Model model, string popis) : base(model, popis)
        {
        }
        public override void Zpracuj(Udalost ud)
        {

        }
    }
    public abstract class Navstevnik : Proces
    {
        protected int trpelivost;
        protected int hlad;
        protected int prichod;
        protected List<Stanoviste> stanoviste;
        //možná list speciálních událostí
        protected abstract Stanoviste VyberDalsiStanoviste();
    }
    public class Navstevnik_0 : Navstevnik //TODO: návštěvníci nejsou hotoví, zatím každý dělá to samé
    {
        public override void Zpracuj(Udalost ud)
        {

        }
        protected override Stanoviste VyberDalsiStanoviste()
        {
            return stanoviste[0];
        }
    }
    public class Navstevnik_1 : Navstevnik
    {
        public override void Zpracuj(Udalost ud)
        {

        }
        protected override Stanoviste VyberDalsiStanoviste()
        {
            return stanoviste[0];
        }
    }
    public class Navstevnik_2 : Navstevnik
    {
        public override void Zpracuj(Udalost ud)
        {

        }
        protected override Stanoviste VyberDalsiStanoviste()
        {
            return stanoviste[0];
        }
    }
    public class Navstevnik_3 : Navstevnik
    {
        public override void Zpracuj(Udalost ud)
        {

        }
        protected override Stanoviste VyberDalsiStanoviste()
        {
            return stanoviste[0];
        }
    }
    public class Model
    {
        public int cas;
        int zaviraciDoba;
        public Dictionary<string,Stanoviste> VsechnaStanoviste; // TODO: dictionary podle jména?
        public Form1 form;

        
        Kalendar kalendar;
        Lanovka lanovka;

        public Model(Form1 form, Random rnd)
        {
            this.form = form;
            VytvorStanoviste();
        }
        public void Vypocti(int pocetNavst)
        {
            
            kalendar = new Kalendar();
            VytvorNavstevniky(pocetNavst);
            Udalost ud;
            while((ud = kalendar.Prvni()) != null && ud.kdy <= zaviraciDoba)
            {
                cas = ud.kdy;
                ud.kdo.Zpracuj(ud);
            }
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
                            lanovka = new Lanovka(this, radek[1..]);
                            break;
                        case 'O':
                            new Obcerstveni(this, radek[1..]);
                            break;
                        case 'E':
                            new Expozice(this, radek[1..]);
                            break;
                        case 'S':
                            new Suvenyry(this, radek[1..]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        void VytvorNavstevniky(int pocetNavst)
        {

        }
    }

}
