using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static треееш.Form3;

namespace треееш
{
    public partial class Form1 : Form
    {
       
        int k, k1, k2;


        public Form1()
        {
            InitializeComponent();

            Form3.myUs.Clear();


            IFormatter serializer = new BinaryFormatter(); 
            FileStream loadFile = new FileStream("people.dat ", FileMode.OpenOrCreate); 
            if (loadFile.Length != 0)
            { 
                Form3.myUs = serializer.Deserialize(loadFile) as List<User>; 
            }
            loadFile.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int a = 0;
            Form2 ifrm = new Form2();
            ifrm.ShowDialog();
            if (Form2.aa == 0) this.Close();

        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            
            string z;
            string z1 = "Администратор";

            if (comboBox1.Text == comboBox1.Items[0].ToString())
            {
                z = "Администратор";
               
            }
            else if (comboBox1.Text == comboBox1.Items[1].ToString())
            {
                z = "Пользователь";
               
            }
            else { MessageBox.Show("Выберите роль"); return; }
            string pattern;
            if (textBox1.Text != "")
            {
                pattern = @"([\$\@#%\^!]+?)";
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    MessageBox.Show("Некорректный email - не содержит по крайней мере один из следующих символов: ! @ # $ % ^");
                    return;
                }
                pattern = @"([0-9]+?)";
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    MessageBox.Show("Некорректный email - не содержит по крайней мере одну цифру");
                    return;
                }
                pattern = @"([A-ZА-Я]+?)"; //{6,20}[\w]*
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    MessageBox.Show("Некорректный email - не содержит по крайней мере одну заглавную букву");
                    return;
                }
            }
            else { MessageBox.Show("Введите email"); return; }
            k = 0;
            foreach (User uss in Form3.myUs) 
            { if (uss.S_L.Trim() == textBox1.Text.Trim()) 
                { 
                    k++; z1 = uss.B_R;

                    foreach (User uss1 in Form3.myUs) 
                    {
                        if (uss1.S_L == textBox1.Text & uss1.S_P == textBox2.Text)
                        {
                            Form5 ifrm = new Form5();
                            //this.Hide();
                            //ifrm.ShowDialog();


                            //здесть для темеровой отображение данных



                        }
                        
                        /*
                        listBox1.Items.Add(uss.B_R.ToString() + "\t" + uss.S_N + "\t" + uss.S_F + "\t" + uss.S_L + "\t" + uss.S_P + "\t" +
                       uss.B_S.ToString() + "\t" + uss.S_C + "\t" + uss.S_B + "\t" + uss.S_D);
                        n++;*/


                    }



                } 
            }
            if (k == 0)
            {
                k1++;
                if (k1 >= 3) { MessageBox.Show("Приложение будет закрыто!"); Application.Exit(); }
                MessageBox.Show("Вы забыли логин? Попробуйте еще раз");
                textBox1.Focus(); return;
            }
            if (z == "Администратор" && z != z1)
            {
                MessageBox.Show("Нет администратора с таким логином? Попробуйте еще раз");
                k1 = 0; textBox1.Focus(); return;
            }
            if (z == "Пользователь" && z != z1)
            {
                MessageBox.Show("Нет пользователя с таким логином? Попробуйте еще раз");
                k1 = 0; textBox1.Focus(); return;
            }
            k = 0;
            foreach (User uss in Form3.myUs) { if (uss.S_P == textBox2.Text) k++; }
            if (k == 0)
            {
                k2++;
                if (k2 >= 3) { MessageBox.Show("Приложение будет закрыто!"); Application.Exit(); }
                MessageBox.Show("Вы забыли пароль? Попробуйте еще раз");
                textBox2.Focus(); return;
            }

            if (comboBox1.Text == comboBox1.Items[0].ToString())
            {
                Form3 ifrm = new Form3();
                this.Hide();
                ifrm.ShowDialog();

            }
            else if (comboBox1.Text == comboBox1.Items[1].ToString())
            {
                Form5 ifrm = new Form5();
                this.Hide();
                ifrm.ShowDialog();

            }




            /*
            Form3 ifrm = new Form3();
            this.Hide();
            ifrm.ShowDialog();
            */


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar > 127)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar > 127)
            {
                e.Handled = true;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Focus(); 


        }


    private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
