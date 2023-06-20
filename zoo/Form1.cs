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

        



        private void bStart_Click(object sender, EventArgs e)
        {
            //musí být vyplnìny všechny kolonky
            //vytvoøí model, spustí Vypocti() s poèty návštìvníkù
            //for loop pøes poèty návštìvníkù s krokem

        }
        


    }




}