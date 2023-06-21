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
                tbLog.Text += zprava;
            }
            else if (kam == "out" || kam == "vystup")
            {
                tbVystup.Text += zprava;
            }
        }

        public string VstupniSoubor()
        {
            return lVstup_soubor.Text;
        }

        public string dobaOd()
        {
            return tbOd.Text;
        }
        public string dobaDo()
        {
            return tbDo.Text;
        }




        private void bStart_Click(object sender, EventArgs e)
        {
            //musí být vyplnìny všechny kolonky
            //vytvoøí model, spustí Vypocti() s poèty návštìvníkù
            // TODO: for loop pøes poèty návštìvníkù s krokem

            Random random = new Random(12345);
            Model model = new Model(_Form1, random);
            model.Vypocti((int)numPocet_min.Value);


        }

        private void bSmazLog_Click(object sender, EventArgs e)
        {
            tbLog.Text = "";
        }

        private void bSmazOut_Click(object sender, EventArgs e)
        {
            tbVystup.Text = "";
        }
    }




}