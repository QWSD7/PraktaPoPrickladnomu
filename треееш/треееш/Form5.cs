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
        public string NameUser;


        public Form5()
        {
            InitializeComponent();

            
            foreach (User uss in Form3.myUs)
            {
                if (Form1.Ind == n)
                {
                    label9.Text = Form1.decryptedEmail = Form1.DecryptE(uss.S_L, uss.S_EK);

                    int count = CountCharacters(Form1.decryptedPassword = Form1.DecryptP(uss.S_P, uss.S_PK));

                    string zvezda = "";
                    for (int i = 0; i < count; i++)
                    {
                        zvezda += "*";
                    }
                    label10.Text = zvezda;
                    TextPass = Form1.decryptedPassword = Form1.DecryptP(uss.S_P, uss.S_PK);

                    ZTextPass = zvezda;

                    label11.Text = uss.S_N;
                    NameUser = uss.S_N;
                    label12.Text = uss.S_F;
                    label13.Text = uss.B_S;
                    label14.Text = uss.S_C;
                    label15.Text = uss.S_B;
                    label16.Text = uss.S_D;

                }
                n++;
            }

            DateTime currentTime = DateTime.Now;
            string greeting = "";

            if (currentTime.Hour < 6)
            {
                greeting = "Доброй ночи, " + NameUser;
            }
            else if (currentTime.Hour < 12)
            {
                greeting = "Доброе утро, " + NameUser;
            }
            else if (currentTime.Hour < 18)
            {
                greeting = "Добрый день, " + NameUser;
            }
            else
            {
                greeting = "Добрый вечер, " + NameUser;
            }

            label17.Text = greeting;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();

            form4.textBox1.Text = label9.Text;
            form4.textBox2.Text = label11.Text;
            form4.textBox3.Text = label12.Text;

            if (label13.Text == "Мужской ♂️")
            {
                form4.comboBox1.Text = "Мужской ♂";
            }
            else if (label13.Text == "Женский ♀ ")
            {
                form4.comboBox1.Text = "Женский ♀ ";
            }

            
            form4.dateTimePicker1.Text = label15.Text;

            if (label14.Text == "Россия")
            {
                form4.comboBox2.Text = "Pоссия";
            }
            else if (label14.Text == "США")
            {
                form4.comboBox2.Text = "США";
            }
            else if (label14.Text == "Италия")
            {
                form4.comboBox2.Text = "Италия";
            }
            else if (label14.Text == "Франция")
            {
                form4.comboBox2.Text = "Франция";
            }
            else if (label14.Text == "Испания")
            {
                form4.comboBox2.Text = "Испания";
            }
            else if (label14.Text == "Германия")
            {
                form4.comboBox2.Text = "Германия";
            }

            
            this.Hide();
           form4.Show(); 
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show(); 
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


        private void Form5_Load(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            this.Close();
            form9.Show();
        }
    }
}