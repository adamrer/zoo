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

        public string Filtr
        {
            get { return tbFiltr.Text; }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

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
                tbLog.AppendText( zprava +'\n' );
                tbLog.ScrollToCaret();
            }
            else if (kam == "out" || kam == "vystup")
            {
                tbVystup.AppendText(zprava +'\n');
                tbLog.ScrollToCaret();
            }
        }


        private async void bStart_Click(object sender, EventArgs e)
        {
            //TODO: musí být vyplnìny všechny kolonky
            bSmazLog_Click(sender, e);
            Random random = new Random(12345);
            Model model = new Model(_Form1, random);

            
            for (int pocet = (int)numPocet_min.Value; pocet <= (int)numPocet_max.Value; pocet += (int)numPocet_krok.Value)
            {
                ZapisDo("======== OTEVØENO ========", "log");
                ZapisDo(model.Vypocti(pocet), "out");
                ZapisDo("======== ZAVØENO ========", "log");
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

        private void numPocet_min_ValueChanged(object sender, EventArgs e)
        {
            if (numPocet_min.Value > numPocet_max.Value)
            {
                numPocet_max.Value = numPocet_min.Value;
            }
        }

        private void numPocet_max_ValueChanged(object sender, EventArgs e)
        {
            if (numPocet_max.Value < numPocet_min.Value)
            {
                numPocet_min.Value = numPocet_max.Value;
            }
        }
    }
}