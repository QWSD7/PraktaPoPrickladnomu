using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace треееш
{
    public partial class Form1 : Form
    {
        


        public Form1()
        {
            InitializeComponent();




        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show(); // отобразить форму
        }
      

        //Штука с паролем
        private int attempts = 0; // Счетчик попыток

        private void button1_Click(object sender, EventArgs e)
        {
            string inpuutText = textBox2.Text;
            string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^])[A-Za-z\d!@#$%^]{6,}$";
            if (!Regex.IsMatch(inpuutText, pattern))
            {
                // Неправильный пароль, увеличение счетчика попыток
                attempts++;
                MessageBox.Show("Неверный пароль! Попытка " + attempts + " из 3. \nПожалуйста, введите пароль, удовлетворяющий требованиям:\n- Минимум 6 символов\n- Минимум 1 прописная буква\n- Минимум 1 цифра\n- По крайней мере один из следующих символов: ! @ # $ % ^");
                textBox2.Text = "";

                if (attempts >= 3)
                {
                    // Блокировка кнопки
                    MessageBox.Show("Вы ввели неправильный пароль 3 раза! Попробуйте зайти позже.");
                    button1.Enabled = false;
                }
                return;
            }
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
            textBox2.Focus(); // Фокус на текстовом поле при запуске программы
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
