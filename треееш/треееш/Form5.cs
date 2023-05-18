using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using треееш;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static треееш.Form3;

namespace треееш
{


    public partial class Form5 : Form
    {
        int n = 0; // количество объектов коллекции 
        public string[] texts = new string[8];

        public string TextPass;
        public string ZTextPass;


        public Form5()
        {
            InitializeComponent();



            foreach (User uss in Form3.myUs)
            {
                if (Form1.Ind == n)
                {
                    label9.Text = uss.S_L;

                    int count = CountCharacters(uss.S_P);

                    string zvezda = "";
                    for (int i = 0; i < count; i++)
                    {
                        zvezda += "*";
                    }
                    label10.Text = zvezda;
                    TextPass = uss.S_P;

                    ZTextPass = zvezda;

                    label11.Text = uss.S_N;
                    label12.Text = uss.S_F;
                    label13.Text = uss.B_S;
                    label14.Text = uss.S_C;
                    label15.Text = uss.S_B;
                    label16.Text = uss.S_D;

                }
                n++;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();

            form4.textBox1.Text = label9.Text;
            form4.textBox2.Text = label11.Text;
            form4.textBox3.Text = label12.Text;

            if (label13.Text == "Мужской ♂")
            {
                form4.comboBox1.SelectedIndex = 1;
            }

            //Женский ♀ 
            //Мужской ♂️




            this.Hide();
           form4.Show(); // отобразить форму
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label10.Text == TextPass)
            {
                label10.Text = ZTextPass;
                button3.Text = "Показать пароль";
            }
            else
            {
                label10.Text = TextPass;
                button3.Text = "Спрятать пароль";
            }
        }


        static int CountCharacters(string input)
        {
            int count = 0;
            foreach (char c in input)
            {
                count++;
            }
            return count;
        }

        //public string Email = "";
       // public string Pass = "";


        private void Form5_Load(object sender, EventArgs e)
        {



            




                /*

                texts[0] = label9.Text;


                if (label10.Text == TextPass)
                {
                    ZTextPass = label10.Text;
                    button3.Text = "Показать пароль";
                    texts[1] = ZTextPass;
                }
                else
                {
                    TextPass = label10.Text;
                    button3.Text = "Спрятать пароль";
                    texts[1] = TextPass;
                }

                texts[2] = label11.Text;
                texts[3] = label12.Text;
                texts[4] = label13.Text;
                texts[5] = label14.Text;
                texts[6] = label15.Text;
                texts[7] = label16.Text;
                */

        }
    }
}



//foreach (User uss in Form3.myUs)
