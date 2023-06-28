using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace zoo
{
    public partial class Form1 : Form
    {
        public static Form1 _Form1;
        public Form1()
        {
            InitializeComponent();
            _Form1 = this;
        }

        public string VstupniSoubor
        {
            get { return lVstup_soubor.Text; }
        }
        public string DobaOd
        {
            get { return tbOd.Text; }
        }
        public string DobaDo
        {
            get { return tbDo.Text; }
        }

        public int VybranyTyp
        {
            get { return cbTyp_navstevniku.SelectedIndex; }
        }

        public string Filtr
        {
            get { return tbFiltr.Text; }
        }
        public int PocetStan_min
        {
            get { return (int)numPocetStan_min.Value; }
        }
        public int PocetStan_max
        {
            get { return (int)numPocetStan_max.Value; }
        }
        public int PocetObc_min
        {
            get { return (int)numPocetObc_min.Value; }
        }
        public int PocetObc_max
        {
            get { return (int)numPocetObc_max.Value; }
        }
        public string Prichod
        {
            get { return tbPrichod.Text; }
        }
        public int Trpelivost_min
        {
            get { return (int)numTrpelivost_min.Value; }
        }
        public int Trpelivost_max
        {
            get { return (int)numTrpelivost_max.Value; }
        }
        public int Hlad_min
        {
            get { return (int)numHlad_min.Value; }
        }
        public int Hlad_max
        {
            get { return (int)numHlad_max.Value; }
        }
        public int Jezeni_min
        {
            get { return (int)numJezeni_min.Value; }
        }
        public int Jezeni_max
        {
            get { return (int)numJezeni_max.Value; }
        }
        public bool CheckData
        {
            get { return checkData.Checked; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbTyp_navstevniku.SelectedIndex = 0;
        }
        private void bVstup_soubor_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                lVstup_soubor.Text = dialog.FileName;
            }
        }

        public void ZapisDo(string zprava, string kam)
        {
            if (checkLog.Checked && kam == "log")
            {
                tbLog.AppendText(zprava + '\n');
                tbLog.ScrollToCaret();
            }
            else if (kam == "out" || kam == "vystup")
            {
                tbVystup.AppendText(zprava + '\n');
                tbVystup.ScrollToCaret();
            }
        }


        private void bStart_Click(object sender, EventArgs e)
        {
            //kontrola vstupu
            if (Prevadec.JeDigitalni(tbOd.Text) &&
                Prevadec.JeDigitalni(tbDo.Text) &&
                Prevadec.JeDigitalni(tbPrichod.Text) &&
                System.IO.File.Exists(lVstup_soubor.Text))
            {
                bSmazLog_Click(sender, e);
                Random random = new Random((int)numSeed.Value);
                Model model = new Model(_Form1, random);

                lPocitani.Visible = true;
                bStart.Enabled = false;

                for (int pocet = (int)numPocet_min.Value; pocet <= (int)numPocet_max.Value; pocet += (int)numPocet_krok.Value)
                {
                    ZapisDo("======== OTEVØENO ========", "log");
                    ZapisDo(model.Vypocti(pocet), "out");
                    ZapisDo("======== ZAVØENO ========", "log");
                }
                bStart.Enabled = true;
                lPocitani.Visible = false;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Vyberte vstupní textový soubor a zadejte otevírací dobu, pøíchod ve tvaru hh:mm");
            }




        }

        private void bSmazLog_Click(object sender, EventArgs e)
        {
            tbLog.Text = "";
        }

        private void bSmazOut_Click(object sender, EventArgs e)
        {
            tbVystup.Text = "";
        }
        void minMaxSpravne(ref NumericUpDown min, ref NumericUpDown max, string changed)
        {
            if (changed == "min")
            {
                if (min.Value > max.Value)
                {
                    max.Value = min.Value;
                }
            }

            if (changed == "max")
            {
                if (min.Value > max.Value)
                {
                    min.Value = max.Value;
                }
            }

        }

        private void numPocet_min_ValueChanged(object sender, EventArgs e)
        {
            minMaxSpravne(ref numPocet_min, ref numPocet_max, "min");
        }

        private void numPocet_max_ValueChanged(object sender, EventArgs e)
        {
            minMaxSpravne(ref numPocet_min, ref numPocet_max, "max");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numPocetStan_min_ValueChanged(object sender, EventArgs e)
        {

            minMaxSpravne(ref numPocetStan_min, ref numPocetStan_max, "min");
        }

        private void numPocetStan_max_ValueChanged(object sender, EventArgs e)
        {

            minMaxSpravne(ref numPocetStan_min, ref numPocetStan_max, "max");
        }

        private void numPocetObc_min_ValueChanged(object sender, EventArgs e)
        {

            minMaxSpravne(ref numPocetObc_min, ref numPocetObc_max, "min");
        }

        private void numPocetObc_max_ValueChanged(object sender, EventArgs e)
        {
            minMaxSpravne(ref numPocetObc_min, ref numPocetObc_max, "max");

        }

        private void numTrpelivost_min_ValueChanged(object sender, EventArgs e)
        {

            minMaxSpravne(ref numTrpelivost_min, ref numTrpelivost_max, "min");
        }

        private void numTrpelivost_max_ValueChanged(object sender, EventArgs e)
        {

            minMaxSpravne(ref numTrpelivost_min, ref numTrpelivost_max, "max");
        }

        private void numHlad_min_ValueChanged(object sender, EventArgs e)
        {

            minMaxSpravne(ref numHlad_min, ref numHlad_max, "min");
        }

        private void numHlad_max_ValueChanged(object sender, EventArgs e)
        {

            minMaxSpravne(ref numHlad_min, ref numHlad_max, "max");
        }

        private void numJezeni_min_ValueChanged(object sender, EventArgs e)
        {

            minMaxSpravne(ref numJezeni_min, ref numJezeni_max, "min");
        }

        private void numJezeni_max_ValueChanged(object sender, EventArgs e)
        {

            minMaxSpravne(ref numJezeni_min, ref numJezeni_max, "max");
        }
    }
}