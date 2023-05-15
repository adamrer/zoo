using System;
using System.Collections.Generic;
using System.Drawing;

namespace CSHra
{

    abstract class Prvek
    {
        //public abstract Bitmap MujObrazek();
    }

    abstract class PohyblivyPrvek : Prvek
    {
        public Mapa mapa;
        public int x; 
        public int y;
        public int Smer;
        public abstract void UdelejKrok();
    }

    enum StisknutaSipka { zadna, doleva, nahoru, doprava, dolu };

    class Hrdina : PohyblivyPrvek
    {
        public Hrdina(Mapa mapa, int kdex, int kdey)
        {
            this.mapa = mapa;
            this.x = kdex;
            this.y = kdey;
        }
        public override void UdelejKrok()
        {
            int nove_x = x;
            int nove_y = y;

            // ###########################################################

            switch (mapa.stisknutaSipka)
            {
                case StisknutaSipka.zadna:
                    break;
                case StisknutaSipka.doleva:
                    // ...tady neco schazi...
                    break;
                case StisknutaSipka.nahoru:
                    // ...tady neco schazi...
                    break;
                case StisknutaSipka.doprava:
                    // ...tady neco schazi...
                    break;
                case StisknutaSipka.dolu:
                    // ...tady neco schazi...
                    break;
                default:
                    break;
            }

            // ###########################################################
            if (mapa.JeVolnoNeboHlina(nove_x, nove_y))
            {
                mapa.Presun(x, y, nove_x, nove_y); // presune obsah mapy a pokud je tam pohybliny prvek, zmeni mu x a y
            }
        }
    }

    class Prisera : PohyblivyPrvek
    {
        public Prisera(Mapa mapa, int kdex, int kdey, char charSmer)
        {
            this.mapa = mapa;
            this.x = kdex;
            this.y = kdey;

            Smer = "<^>v".IndexOf(charSmer);
        }
        public override void UdelejKrok()
        {
            // ###########################################################
            // ...tady neco schazi...
            // ###########################################################
        }

    }

    class Balvan : PohyblivyPrvek
    {
        protected int minule = +1;
        public Balvan(Mapa mapa, int kdex, int kdey)
        {
            this.mapa = mapa;
            this.x = kdex;
            this.y = kdey;
        }
        public override void UdelejKrok()
        {
            // ###########################################################
            // ...tady neco schazi...
            // ###########################################################
        }
    }
    class Diamant : Balvan
    {
        public Diamant(Mapa mapa, int kdex, int kdey)
            : base(mapa, kdex, kdey)
        {
        }
        public override void UdelejKrok()
        {
            // ###########################################################
            // ...tady neco schazi...
            // ###########################################################
        }

    }

    public enum Stav { nezacala, bezi, vyhra, prohra };
    class Mapa
    {
        private char[,] plan;
        int sirka;
        int vyska;
        public int ZbyvaDiamantu;

        public Stav stav = Stav.nezacala;


        Bitmap[] ikonky;
        int sx; // velikost kosticky ikonek

        public Hrdina hrdina;
        public List<PohyblivyPrvek> PohyblivePrvkyKromeHrdiny;

        public StisknutaSipka stisknutaSipka;


        public Mapa(string cestaMapa, string cestaIkonky)
        {
            NactiIkonky(cestaIkonky);
            NactiMapu(cestaMapa);
            stav = Stav.bezi;
        }

        public void Presun(int zX, int zY, int naX, int naY)
        {
            char c = plan[zX, zY];
            plan[zX, zY] = ' ';
            plan[naX, naY] = c;

            // podivat se, jestli tam nestal hrdina:
            if (c=='H')
            {
                hrdina.x = naX;
                hrdina.y = naY;
                return; // kdyz na [zY,zX] stoji hrdina, tak tam nic jineho nestoji
            }

            // najit pripadny pohyblivyPrvek a zmenit mu polohu :
            foreach (PohyblivyPrvek po in PohyblivePrvkyKromeHrdiny)
            {
                if ((po.x == zX) && (po.y == zY))
                {
                    po.x = naX;
                    po.y = naY;
                    break; // jakmile tam stoji jeden, tak uz tam nikdo jiny nestoji
                }
            }

        }

        public void ZrusPohyblivyPrvek(int zX, int zY)
        {
            // najit pohyblivyPrvek a vyhodit ho ze seznamu :
            for (int i = 0; i < PohyblivePrvkyKromeHrdiny.Count; i++)
            {
                if ((PohyblivePrvkyKromeHrdiny[i].x == zX) && (PohyblivePrvkyKromeHrdiny[i].y == zY))
                {
                    PohyblivePrvkyKromeHrdiny.RemoveAt(i); // 1. vyhodit ze seznamu pohyblivych prvku...
                    plan[zX, zY] = ' ';                    // 2. ...a z planu!
                    break;
                }
            }
        }


        public bool JeBalvan(int x, int y)
        {
            return plan[x, y] == 'B';
        }

        public bool JeDiamant(int x, int y)
        {
            return plan[x, y] == 'D';
        }

        public bool JeVolno(int x, int y)
        {
            return (plan[x, y] == ' ');
        }

        public bool JeVolnoNeboHlina(int x, int y)
        {
            return (plan[x, y] == ' ') || (plan[x, y] == 'h');
        }

        public bool JeOtevrenyVychod(int x, int y)
        {
            return plan[x, y] == 'e';
        }

        public void OtevriVychod()
        {
            for (int x = 0; x < sirka; x++)
            {
                for (int y = 0; y < vyska; y++)
                {
                    if (plan[x, y] == 'E')
                        plan[x, y] = 'e';
                }
            }

        }


        public void NactiMapu(string cesta)
        {
            PohyblivePrvkyKromeHrdiny = new List<PohyblivyPrvek>();

            System.IO.StreamReader sr = new System.IO.StreamReader(cesta);
            sirka = int.Parse(sr.ReadLine());
            vyska = int.Parse(sr.ReadLine());
            plan = new char[sirka, vyska];
            ZbyvaDiamantu = 0;

            for (int y = 0; y < vyska; y++)
            {
                string radek = sr.ReadLine();
                for (int x = 0; x < sirka; x++)
                {
                    char znak = radek[x];
                    plan[x, y] = znak;

                    // vytvorit pripadne pohyblive objekty:
                    switch (znak)
                    {
                        case 'H':
                            this.hrdina = new Hrdina(this, x, y);
                            break;

                        case '<':
                        case '^':
                        case '>':
                        case 'v':
                            Prisera prisera = new Prisera(this, x, y, znak);
                            PohyblivePrvkyKromeHrdiny.Add(prisera);
                            break;

                        case 'B':
                            Balvan balvan = new Balvan(this, x, y);
                            PohyblivePrvkyKromeHrdiny.Add(balvan);
                            break;

                        case 'D':
                            Diamant diamant = new Diamant(this, x, y);
                            PohyblivePrvkyKromeHrdiny.Add(diamant);
                            ZbyvaDiamantu++;
                            break;

                        default:
                            break;
                    }
                }
            }
            sr.Close();
        }
        public void NactiIkonky(string cesta)
        {
            Bitmap bmp = new Bitmap(cesta);
            this.sx = bmp.Height;
            int pocet = bmp.Width / sx; // predpokladam, ze to jsou kosticky v rade
            ikonky = new Bitmap[pocet];
            for (int i = 0; i < pocet; i++)
            {
                Rectangle rect = new Rectangle(i * sx, 0, sx, sx);
                ikonky[i] = bmp.Clone(rect, System.Drawing.Imaging.PixelFormat.DontCare);
            }
        }

        public void VykresliSe(Graphics g, int sirkaVyrezuPixely, int vyskaVyrezuPixely)
        {
            int sirkaVyrezu = sirkaVyrezuPixely / sx;
            int vyskaVyrezu = vyskaVyrezuPixely / sx;

            if (sirkaVyrezu > sirka)
                sirkaVyrezu = sirka;

            if (vyskaVyrezu > vyska)
                vyskaVyrezu = vyska;

            // urcit LHR vyrezu:
            int dx = hrdina.x - sirkaVyrezu / 2;
            if (dx < 0)
                dx = 0;
            if (dx + sirkaVyrezu - 1 >= this.sirka)
                dx = this.sirka - sirkaVyrezu;

            int dy = hrdina.y - vyskaVyrezu / 2;
            if (dy < 0)
                dy = 0;
            if (dy + vyskaVyrezu - 1 >= this.vyska)
                dy = this.vyska - vyskaVyrezu;

            for (int x = 0; x < sirkaVyrezu; x++)
            {
                for (int y = 0; y < vyskaVyrezu; y++)
                {
                    int mx = dx + x; // index do mapy
                    int my = dy + y; // index do mapy

                    char c = plan[mx, my];
                    int indexObrazku = " hHB<^>vXDEe".IndexOf(c); // 0..

                    g.DrawImage(ikonky[indexObrazku], x * sx, y * sx);
                }
            }
        }

        public void PohniVsemiPrvky(StisknutaSipka stisknutaSipka)
        {
            this.stisknutaSipka = stisknutaSipka;
            foreach (PohyblivyPrvek p in PohyblivePrvkyKromeHrdiny)
            {
                p.UdelejKrok();
            }

            hrdina.UdelejKrok();
        }
    }
}
