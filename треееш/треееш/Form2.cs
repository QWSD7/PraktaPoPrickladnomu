﻿using System;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Security.Cryptography;

namespace треееш
{
    public partial class Form2 : Form
    {
        public static int aa;

        string es = "", strEmail = "", rezstrE = "";
        string ps = "", strPass = "", rezstrP = "";

        public Form2()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-zа-я]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)[0-9a-zа-я]@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (textBox1.Text != "")
            {
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
                    Form3.user.S_L = rezstrE;


                    //Form3.user.S_L = textBox1.Text;
                }
                else { MessageBox.Show("Некорректный email"); return; }
            }
            int k = 0;
            if (textBox2.Text != "")
            {
                if (textBox2.Text.Length < 6)
                {
                    MessageBox.Show("Пароль должен содержать не менее 6 символов");
                    textBox2.Focus(); return;
                }
                for (char c = 'A'; c <= 'Z'; c++) if (textBox2.Text.Contains(c)) k++;
                for (char c = 'А'; c <= 'Я'; c++) if (textBox2.Text.Contains(c)) k++;
                if (k == 0)
                {
                    MessageBox.Show("Пароль должен содержать как минимум одну прописную букву");
                    textBox2.Focus(); return;
                }
                k = 0;
                for (char c = '0'; c <= '9'; c++) if (textBox2.Text.Contains(c)) k++;
                if (k == 0)
                {
                    MessageBox.Show("Пароль должен содержать как минимум одну цифру");
                    textBox2.Focus(); return;
                }
                if (!textBox2.Text.Contains('!') && !textBox2.Text.Contains('@') && !textBox2.Text.Contains('#') && !textBox2.Text.Contains('$') && !textBox2.Text.Contains('%') && !textBox2.Text.Contains('^'))
                {
                    MessageBox.Show("Пароль должен содержать по крайней мере один из следующих символов: ! @ # $ % ^");
                    textBox2.Focus(); return;
                }
                foreach (User uss in Form3.myUs)
                {
                    if (uss.S_L == textBox2.Text)
                    {
                        MessageBox.Show("Такой логин уже существует");
                        textBox2.Focus(); return;
                    }
                }

                if ((textBox3.Text != "") && (textBox2.Text == textBox3.Text))
                {
                    //Шифрование пароля

                    
                    if (textBox2.Text != "")
                    { // текст для шифрования

                        strPass = textBox2.Text; // это строка которую следует зашифровать
                    }
                    else { MessageBox.Show("Введите данные для шифрования"); textBox2.Focus(); return; }
                    ps = CreatePasswordP(8);
                    rezstrP = EncryptP(strPass, ps); // шифрование
                    Form3.user.S_P = rezstrP;
                     


                    //Form3.user.S_P = textBox2.Text; // пароль
                } 
                else { MessageBox.Show("Повторите пароль"); textBox3.Focus(); return; }

            }
            else { MessageBox.Show("Введите пароль"); textBox2.Focus(); return; }

            if (textBox4.Text != "") Form3.user.S_N = textBox4.Text; // имя
            else { MessageBox.Show("Введите имя"); textBox4.Focus(); return; }

            if (textBox5.Text != "") Form3.user.S_F = textBox5.Text; // фамилия
            else { MessageBox.Show("Введите фамилию"); textBox5.Focus(); return; }

            if (comboBox1.Text == comboBox1.Items[0].ToString()) Form3.user.B_S = comboBox1.Text; // пол
            else if (comboBox1.Text == comboBox1.Items[1].ToString()) Form3.user.B_S = comboBox1.Text; // пол
            else { MessageBox.Show("Выберите пол"); return; }

            Form3.user.S_B = dateTimePicker1.Text; // дата (маска ввода dd.mm.yyyy)
            

            if (comboBox2.Text != "") Form3.user.S_C = comboBox2.Text; // страна
            else { MessageBox.Show("Выберите страну"); comboBox2.Focus(); return; }
            if (comboBox3.Text == comboBox3.Items[0].ToString()) Form3.user.B_R = comboBox3.Text; // роль
            else if (comboBox3.Text == comboBox3.Items[1].ToString()) Form3.user.B_R = comboBox3.Text; // роль
            else { MessageBox.Show("Выберите роль"); return; }
            Form3.user.S_D = DateTime.Today.ToString().Substring(0, 10); // дата (dd.mm.yyyy)
            Form3.myUs.Add(new User(Form3.user)); // добавление объекта в коллекцию
            IFormatter serializer = new BinaryFormatter(); // Получить сериализатор
            using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, Form3.myUs); // сериализация всего массива people
            }
            Form3.myUs.Clear(); // очистка коллекции
            using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
            { // десериализация
                Form3.myUs = (List<User>)serializer.Deserialize(fs); // считать данные из файла в коллекцию
            }
            MessageBox.Show("Вы зарегистрированы");
            aa = 0;

            Form1 ifrm = new Form1();
            this.Hide();
            ifrm.Show();



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

            Form3.user.S_EK = res.ToString();

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

            Form3.user.S_PK = res.ToString();

            return res.ToString();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar > 127)
            {
                e.Handled = true;
            }
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

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Form2_Load(object sender, EventArgs e)
        {




        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
