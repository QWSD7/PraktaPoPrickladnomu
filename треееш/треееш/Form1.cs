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
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Emit;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;

namespace треееш
{
    public partial class Form1 : Form
    {

        int k, k1, k2;
        Form5 ifrm5 = new Form5();
        int cap = 0;
        bool captchaValidated = false;

        public string es, strEmail = "", rezstrE = "";
        public string ps, strPass = "", rezstrP = "";


        public static string decryptedEmail;
        public static string decryptedPassword;

        public static int Ind;

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


            textBox1.Text = "Ilove1@mail.ru";
            textBox2.Text = "Ilove1YOU!";

            //textBox1.Text = "Paxomov.2004@list.ru";
            //textBox2.Text = "Mne18letiun!";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int a = 0;
            Form2 ifrm = new Form2();
            ifrm.Show();
            if (Form2.aa == 0) this.Hide();
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


        private void button1_Click(object sender, EventArgs e)
        {
            string mail = textBox1.Text;
            string password = textBox2.Text;

            string shifr = "";
            string shifrP = "";

            foreach (User user in Form3.myUs)
            {
                if (user.S_L != "" && user.S_P != "")
                {
                    if (DecryptE(user.S_L, user.S_EK) == mail && DecryptP(user.S_P, user.S_PK) == password)
                    {
                        decryptedEmail = DecryptE(user.S_L, user.S_EK);
                        shifr = user.S_L;

                        decryptedPassword = DecryptP(user.S_P, user.S_PK);
                        shifrP = user.S_P;

                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(decryptedEmail) && !string.IsNullOrEmpty(decryptedPassword))
            {
                Ind = Ind = Form3.myUs.FindIndex(a => a.S_L == shifr);
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
                textBox1.Focus();
            }

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
            { if (uss.S_L.Trim() == shifr.Trim())
                {
                    k++; z1 = uss.B_R;



                    foreach (User uss1 in Form3.myUs)
                    {
                        if (uss1.S_L == shifr & uss1.S_P == shifrP)
                        {

                            ifrm5.label9.Text = decryptedEmail;


                            int count = CountCharacters(decryptedPassword);

                            string zvezda = "";
                            for (int i = 0; i < count; i++)
                            {
                                zvezda += "*";
                            }
                            ifrm5.label10.Text = zvezda;

                            ifrm5.TextPass = decryptedPassword;


                           // ifrm5.label10.Text = decryptedPassword;


                            ifrm5.ZTextPass = zvezda;

                            ifrm5.label11.Text = uss.S_N;
                            ifrm5.label12.Text = uss.S_F;
                            ifrm5.label13.Text = uss.B_S;
                            ifrm5.label14.Text = uss.S_C;
                            ifrm5.label15.Text = uss.S_B;
                            ifrm5.label16.Text = uss.S_D;

                            ifrm5.NameUser = uss.S_N;
                        }
                    }


                    DateTime currentTime = DateTime.Now;
                    string greeting = "";

                    if (currentTime.Hour < 6)
                    {
                        greeting = "Доброй ночи, " + ifrm5.NameUser;
                    }
                    else if (currentTime.Hour < 12)
                    {
                        greeting = "Доброе утро, " + ifrm5.NameUser;
                    }
                    else if (currentTime.Hour < 18)
                    {
                        greeting = "Добрый день, " + ifrm5.NameUser;
                    }
                    else
                    {
                        greeting = "Добрый вечер, " + ifrm5.NameUser;
                    }

                    ifrm5.label17.Text = greeting;


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
            foreach (User uss in Form3.myUs) { if (uss.S_P == shifrP) k++; }
            if (k == 0)
            {
                k2++;
                if (k2 >= 3) { MessageBox.Show("Приложение будет закрыто!"); Application.Exit(); }
                MessageBox.Show("Вы забыли пароль? Попробуйте еще раз");
                textBox2.Focus(); return;
            }
            if (!captchaValidated)
            {
                MessageBox.Show("Подтвердите CAPTCHA!");
                return;
            }

            if (comboBox1.Text == comboBox1.Items[0].ToString())
            {
                Form3 ifrm = new Form3();
                this.Hide();
                ifrm.Show();

            }
            else if (comboBox1.Text == comboBox1.Items[1].ToString())
            {
                this.Hide();
                ifrm5.Show();

            }
        }




        public static string DecryptE(string str, string keyCrypt)
        {
            try
            {
                byte[] encryptedData = Convert.FromBase64String(str);
                byte[] decryptedData = DecryptE(encryptedData, keyCrypt);
                return Encoding.UTF8.GetString(decryptedData);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        private static byte[] DecryptE(byte[] data, string key)
        {
            using (SymmetricAlgorithm sa = Rijndael.Create())
            {
                sa.Key = new PasswordDeriveBytes(key, null).GetBytes(16);
                sa.IV = new byte[16]; // Здесь используется нулевой IV, но в реальном приложении следует использовать тот же IV, который использовался при шифровании
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (CryptoStream cs = new CryptoStream(ms, sa.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (MemoryStream decryptedMs = new MemoryStream())
                        {
                            cs.CopyTo(decryptedMs);
                            return decryptedMs.ToArray();
                        }
                    }
                }
            }
        }




      
        public static string DecryptP(string str, string keyCrypt)
        {
            try
            {
                byte[] encryptedData = Convert.FromBase64String(str);
                byte[] decryptedData = DecryptP(encryptedData, keyCrypt);
                return Encoding.UTF8.GetString(decryptedData);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        private static byte[] DecryptP(byte[] data, string key)
        {
            using (SymmetricAlgorithm sa = Rijndael.Create())
            {
                sa.Key = new PasswordDeriveBytes(key, null).GetBytes(16);
                sa.IV = new byte[16]; // Здесь используется нулевой IV, но в реальном приложении следует использовать тот же IV, который использовался при шифровании
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (CryptoStream cs = new CryptoStream(ms, sa.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (MemoryStream decryptedMs = new MemoryStream())
                        {
                            cs.CopyTo(decryptedMs);
                            return decryptedMs.ToArray();
                        }
                    }
                }
            }
        }



        //Paxomov.2004@list.ru
        //Mne18letiun!



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

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random r1 = new Random();
            cap = r1.Next(10000, 99999);
            var image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            var font = new Font("Georgia", 45, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, this.pictureBox1.Width, this.pictureBox1.Height);
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.White, Color.LightBlue);
            graphics.FillRectangle(hatchBrush, rect);
            graphics.DrawString(cap.ToString(), font, Brushes.Black, new Point(0, 0));
            pictureBox1.Image = image;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == cap.ToString())
            {
                MessageBox.Show("Верно! Вы не бот!");
                captchaValidated = true;
                button1.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ошибка! НАЙДЕН БОТ!!!");
                captchaValidated = false;
                button1.Enabled = false;
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

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
