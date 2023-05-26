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
using System.Threading.Tasks;
using System.Windows.Forms;
using static треееш.Form3;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace треееш
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

       

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

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

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar > 127)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-zа-я]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)[0-9a-zа-я]@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            string strEmail, es, rezstrE;
            string strPass, ps, rezstrP;


            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || comboBox1.Text != "" || comboBox2.Text != "" || dateTimePicker1.Text != "")
            {
                //Form3.myUs[Form1.Ind].S_L = textBox1.Text;


                if (Regex.IsMatch(textBox1.Text, pattern, RegexOptions.IgnoreCase))
                {
                    //шифрование Email

                    if (textBox1.Text != "")
                    { // текст для шифрования

                        strEmail = textBox1.Text; // это строка которую следует зашифровать
                    }
                    else { MessageBox.Show("Введите данные для шифрования"); textBox1.Focus(); return; }
                    es = CreatePasswordE(8);
                    rezstrE = EncryptE(strEmail, es); // шифрование
                    Form3.myUs[Form1.Ind].S_L = rezstrE;


                }
                else { MessageBox.Show("Некорректный email"); return; }


                Form3.myUs[Form1.Ind].S_N = textBox2.Text;
                Form3.myUs[Form1.Ind].S_F = textBox3.Text;
                Form3.myUs[Form1.Ind].B_S = comboBox1.Text;
                Form3.myUs[Form1.Ind].S_B = dateTimePicker1.Text;
                Form3.myUs[Form1.Ind].S_C = comboBox2.Text;


                if (textBox4.Text != "" || textBox5.Text != "")
                {
                    int k = 0;
                    if (textBox5.Text.Length < 6)
                    {
                        MessageBox.Show("Пароль должен содержать не менее 6 символов");
                        textBox5.Focus(); return;
                    }
                    for (char c = 'A'; c <= 'Z'; c++) if (textBox5.Text.Contains(Convert.ToString(c))) k++;
                    for (char c = 'А'; c <= 'Я'; c++) if (textBox5.Text.Contains(Convert.ToString(c))) k++;
                    if (k == 0)
                    {
                        MessageBox.Show("Пароль должен содержать как минимум одну прописную букву");
                        textBox5.Focus(); return;
                    }
                    k = 0;
                    for (char c = '0'; c <= '9'; c++) if (textBox5.Text.Contains(Convert.ToString(c))) k++;
                    if (k == 0)
                    {
                        MessageBox.Show("Пароль должен содержать как минимум одну цифру");
                        textBox5.Focus(); return;
                    }
                    if (!textBox5.Text.Contains(Convert.ToString('!')) && !textBox5.Text.Contains(Convert.ToString('@')) && !textBox5.Text.Contains(Convert.ToString('#')) &&
                   !textBox5.Text.Contains(Convert.ToString('$')) && !textBox5.Text.Contains(Convert.ToString('%')) && !textBox5.Text.Contains(Convert.ToString('^')))
                    {
                        MessageBox.Show("Пароль должен содержать по крайней мере один из следующих символов: ! @ # $ % ^");
                        textBox2.Focus(); return;
                    }
                    if ((textBox4.Text != "") && (textBox5.Text == textBox4.Text))
                    {
                        if (textBox5.Text != "")
                        { 
                            strPass = textBox5.Text; // это строка которую следует зашифровать
                        }
                        else { MessageBox.Show("Введите данные для шифрования"); textBox5.Focus(); return; }
                        ps = CreatePasswordP(8);
                        rezstrP = EncryptP(strPass, ps); // шифрование
                        Form3.myUs[Form1.Ind].S_P = rezstrP;

                    }
                    else { MessageBox.Show("Повторите пароль"); textBox4.Focus(); return; }

                }


                IFormatter serializer = new BinaryFormatter();
                using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fs, myUs);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            this.Close();
            form5.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public static string EncryptE(string str, string keyCrypt)
        {
            return Convert.ToBase64String(EncryptE(Encoding.UTF8.GetBytes(str), keyCrypt));
        }

        private static byte[] EncryptE(byte[] data, string key)
        {
            using (SymmetricAlgorithm sa = Rijndael.Create())
            {
                sa.Key = new PasswordDeriveBytes(key, null).GetBytes(16);
                sa.IV = new byte[16]; // Здесь используется нулевой IV, но в реальном приложении следует использовать случайно сгенерированный IV
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, sa.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
        }

        public static string CreatePasswordE(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=?&/";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            Form3.myUs[Form1.Ind].S_EK = res.ToString();

            return res.ToString();
        }


        public static string EncryptP(string str, string keyCrypt)
        {
            return Convert.ToBase64String(EncryptP(Encoding.UTF8.GetBytes(str), keyCrypt));
        }

        private static byte[] EncryptP(byte[] data, string key)
        {
            using (SymmetricAlgorithm sa = Rijndael.Create())
            {
                sa.Key = new PasswordDeriveBytes(key, null).GetBytes(16);
                sa.IV = new byte[16]; // Здесь используется нулевой IV, но в реальном приложении следует использовать случайно сгенерированный IV
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, sa.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
        }



        public static string CreatePasswordP(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=?&/";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            Form3.myUs[Form1.Ind].S_PK = res.ToString();

            return res.ToString();
        }
    }
}


