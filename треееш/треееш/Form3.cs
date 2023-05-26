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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace треееш
{
    public partial class Form3 : Form
    {
        int n = 0; 

        static public User user = new User(); 
        static public List<User> myUs = new List<User>();

        static string SIdx;
        public static int aa;

        public static string DE;
        public static string DP;

        string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-zа-я]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)[0-9a-zа-я]@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        string strEmail, es, rezstrE;
        string strPass, ps, rezstrP;

        public static int SInd;

        public Form3()
        {
            InitializeComponent();

            dataGridView1.Columns.Add("B_R", "Роль");
            dataGridView1.Columns.Add("S_N", "Имя");
            dataGridView1.Columns.Add("S_F", "Фамилия");
            dataGridView1.Columns.Add("S_L", "Почта");
            dataGridView1.Columns.Add("S_P", "Пароль");
            dataGridView1.Columns.Add("B_S", "Пол");
            dataGridView1.Columns.Add("S_C", "Страна");
            dataGridView1.Columns.Add("S_B", "День рождения");
            dataGridView1.Columns.Add("S_D", "Дата регистрации");

            dataGridView1.Columns[0].Width = 150; 
            dataGridView1.Columns[1].Width = 150; 
            dataGridView1.Columns[2].Width = 150; 
            dataGridView1.Columns[3].Width = 200; 
            dataGridView1.Columns[4].Width = 150; 
            dataGridView1.Columns[5].Width = 130; 
            dataGridView1.Columns[6].Width = 150; 
            dataGridView1.Columns[7].Width = 150; 
            dataGridView1.Columns[8].Width = 150; 


            int n = 0; // количество объектов коллекции 
            foreach (User uss in myUs)
            {
                dataGridView1.Rows.Add(uss.B_R, uss.S_N, uss.S_F,  Form1.decryptedEmail = Form1.DecryptE(uss.S_L, uss.S_EK), Form1.decryptedPassword = Form1.DecryptP(uss.S_P, uss.S_PK), uss.B_S, uss.S_C, uss.S_B, uss.S_D);
                if (Form1.Ind == n)
                {
                    SIdx = uss.S_N;
                }
                n++;
            }

            DateTime currentTime = DateTime.Now;
            string greeting = "";

            if (currentTime.Hour < 6)
            {
                greeting = "Доброй ночи, " + SIdx + ".";
            }
            else if (currentTime.Hour < 12)
            {
                greeting = "Доброе утро, " + SIdx + ".";
            }
            else if (currentTime.Hour < 18)
            {
                greeting = "Добрый день, " + SIdx + ".";
            }
            else
            {
                greeting = "Добрый вечер, " + SIdx + ".";
            }
            label14.Text = greeting;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                user = myUs[dataGridView1.SelectedRows[0].Index]; 
            }
            if (MessageBox.Show("Хотите удалить запись?", "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                myUs.RemoveAt(dataGridView1.SelectedRows[0].Index);
                dataGridView1.Rows.RemoveAt(selectedIndex);

               
                IFormatter serializer = new BinaryFormatter(); // получить сериализатор 
                using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fs, myUs); // сериализация всего массива people
                }
            }
            
        }

        
        [Serializable] // сериализация
        public class User
        { // пользовательский класс
            string s_login; // логин
            string s_password; // пароль
            string b_role; // роль
            string s_name; // имя
            string s_fam; // фамилия 
            string b_sex; // пол
            string s_country; // страна
            string s_birthdate; // дата рождения
            string s_datereg; // дата регистрации
            string s_EmailKey; //ключ к почте
            string s_PassKey; //ключ к паролю

            public User()
            { // конструктор по умолчанию
                s_login = ""; s_password = ""; b_role = ""; s_name = ""; s_fam = "";
                b_sex = ""; s_country = "Россия"; s_birthdate = "00.00.0000"; s_datereg = "00.00.0000"; s_EmailKey = ""; s_PassKey = "";
            }
            public User(User x)
            { // конструктор с параметрами
                s_login = x.S_L; s_password = x.S_P; b_role = x.B_R; s_name = x.S_N; s_fam = x.S_F;
                b_sex = x.B_S; s_country = x.S_C; s_birthdate = x.S_B; s_datereg = x.S_D; s_EmailKey = x.S_EK; s_PassKey = x.S_PK;
            }
            public string B_R
            { //объявление свойства для чтения и записи поля s_name 
                get { return b_role; } // аксессор чтения поля 
                set { b_role = value; } // аксессор записи в поле
            }
            public string S_N { get { return s_name; } set { s_name = value; } }
            public string S_F { get { return s_fam; } set { s_fam = value; } }
            public string S_L { get { return s_login; } set { s_login = value; } }
            public string S_P { get { return s_password; } set { s_password = value; } }
            public string B_S { get { return b_sex; } set { b_sex = value; } }
            public string S_C { get { return s_country; } set { s_country = value; } }
            public string S_B { get { return s_birthdate; } set { s_birthdate = value; } }
            public string S_D { get { return s_datereg; } set { s_datereg = value; } }
            public string S_EK { get { return s_EmailKey; } set { s_EmailKey = value; } }
            public string S_PK { get { return s_PassKey; } set { s_PassKey = value; } }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || comboBox1.Text != "" || comboBox2.Text != "" || dateTimePicker1.Text != "")
            {
                SInd = dataGridView1.SelectedRows[0].Index;

                if (Regex.IsMatch(textBox1.Text, pattern, RegexOptions.IgnoreCase))
                {
                    if (textBox1.Text != "")
                    { 

                        strEmail = textBox1.Text; 
                    }
                    else { MessageBox.Show("Введите данные для шифрования"); textBox1.Focus(); return; }
                    es = CreatePasswordE(8, 0);
                    rezstrE = EncryptE(strEmail, es); 
                    myUs[SInd].S_L = rezstrE;


                }
                else { MessageBox.Show("Некорректный email"); return; }


                myUs[SInd].S_N = textBox2.Text;
                myUs[SInd].S_F = textBox3.Text;
                myUs[SInd].B_S = comboBox1.Text;
                myUs[SInd].S_B = dateTimePicker1.Text;
                myUs[SInd].S_C = comboBox2.Text;
                myUs[SInd].B_R = comboBox3.Text;

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
                        ps = CreatePasswordP(8, 0);
                        rezstrP = EncryptP(strPass, ps); // шифрование
                        myUs[SInd].S_P = rezstrP;


                    }
                    else { MessageBox.Show("Повторите пароль"); textBox4.Focus(); return; }
                }


                dataGridView1.Rows.Clear();
                int n = 0;
                foreach (User uss in myUs)
                {
                    dataGridView1.Rows.Add(uss.B_R, uss.S_N, uss.S_F, DE = Decrypt(uss.S_L, uss.S_EK), DP = Decrypt(uss.S_P, uss.S_PK), uss.B_S, uss.S_C, uss.S_B, uss.S_D);
                    n++;
                }
                IFormatter serializer = new BinaryFormatter();
                using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fs, myUs);
                }

                dataGridView1.CurrentCell = null;

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.Text = "";
                dateTimePicker1.Text = DateTime.Now.ToString();
                comboBox2.Text = "";
                comboBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";


            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView1.SelectedCells[0].RowIndex;

                textBox1.Text = dataGridView1.Rows[selectedIndex].Cells["S_L"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[selectedIndex].Cells["S_N"].Value.ToString();
                textBox3.Text = dataGridView1.Rows[selectedIndex].Cells["S_F"].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[selectedIndex].Cells["B_S"].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[selectedIndex].Cells["S_B"].Value.ToString());
                comboBox2.Text = dataGridView1.Rows[selectedIndex].Cells["S_C"].Value.ToString();
                comboBox3.Text = dataGridView1.Rows[selectedIndex].Cells["B_R"].Value.ToString();
                textBox4.Text = dataGridView1.Rows[selectedIndex].Cells["S_P"].Value.ToString();
                textBox5.Text = dataGridView1.Rows[selectedIndex].Cells["S_P"].Value.ToString();


            }
            catch (Exception)
            {
                // Обработка исключения, если нет выбранных ячеек или возникают другие ошибки
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {
                if (Regex.IsMatch(textBox1.Text, pattern, RegexOptions.IgnoreCase))
                {
                    if (textBox1.Text != "")
                    { 

                        strEmail = textBox1.Text; 
                    }
                    else { MessageBox.Show("Введите данные для шифрования"); textBox1.Focus(); return; }
                    es = CreatePasswordE(8, 1);
                    rezstrE = EncryptE(strEmail, es); 
                    user.S_L = rezstrE;

                }
                else { MessageBox.Show("Некорректный email"); return; }
            }
            int k = 0;
            if (textBox4.Text != "")
            {
                if (textBox4.Text.Length < 6)
                {
                    MessageBox.Show("Пароль должен содержать не менее 6 символов");
                    textBox4.Focus(); return;
                }
                for (char c = 'A'; c <= 'Z'; c++) if (textBox4.Text.Contains(c)) k++;
                for (char c = 'А'; c <= 'Я'; c++) if (textBox4.Text.Contains(c)) k++;
                if (k == 0)
                {
                    MessageBox.Show("Пароль должен содержать как минимум одну прописную букву");
                    textBox4.Focus(); return;
                }
                k = 0;
                for (char c = '0'; c <= '9'; c++) if (textBox4.Text.Contains(c)) k++;
                if (k == 0)
                {
                    MessageBox.Show("Пароль должен содержать как минимум одну цифру");
                    textBox4.Focus(); return;
                }
                if (!textBox4.Text.Contains('!') && !textBox4.Text.Contains('@') && !textBox4.Text.Contains('#') && !textBox4.Text.Contains('$') && !textBox4.Text.Contains('%') && !textBox4.Text.Contains('^'))
                {
                    MessageBox.Show("Пароль должен содержать по крайней мере один из следующих символов: ! @ # $ % ^");
                    textBox4.Focus(); return;
                }
                foreach (User uss in myUs)
                {
                    if (uss.S_L == textBox4.Text)
                    {
                        MessageBox.Show("Такой логин уже существует");
                        textBox4.Focus(); return;
                    }
                }

                if ((textBox5.Text != "") && (textBox4.Text == textBox5.Text))
                {
                    if (textBox4.Text != "")
                    { 

                        strPass = textBox4.Text; 
                    }
                    else { MessageBox.Show("Введите данные для шифрования"); textBox4.Focus(); return; }
                    ps = CreatePasswordP(8, 1);
                    rezstrP = EncryptP(strPass, ps); 
                    user.S_P = rezstrP;

                }
                else { MessageBox.Show("Повторите пароль"); textBox5.Focus(); return; }

            }
            else { MessageBox.Show("Введите пароль"); textBox4.Focus(); return; }
            if (textBox2.Text != "") user.S_N = textBox2.Text; // имя
            else { MessageBox.Show("Введите имя"); textBox2.Focus(); return; }

            if (textBox3.Text != "") user.S_F = textBox3.Text; // фамилия
            else { MessageBox.Show("Введите фамилию"); textBox3.Focus(); return; }

            if (comboBox1.Text == comboBox1.Items[0].ToString()) user.B_S = comboBox1.Text; // пол
            else if (comboBox1.Text == comboBox1.Items[1].ToString()) user.B_S = comboBox1.Text; // пол
            else { MessageBox.Show("Выберите пол"); return; }

            user.S_B = dateTimePicker1.Text; // дата (маска ввода dd.mm.yyyy)


            if (comboBox2.Text != "") user.S_C = comboBox2.Text; // страна
            else { MessageBox.Show("Выберите страну"); comboBox2.Focus(); return; }

            if (comboBox3.Text == comboBox3.Items[0].ToString()) user.B_R = comboBox3.Text; // роль
            else if (comboBox3.Text == comboBox3.Items[1].ToString()) user.B_R = comboBox3.Text; // роль
            else { MessageBox.Show("Выберите роль"); return; }

            user.S_D = DateTime.Today.ToString().Substring(0, 10); // дата (dd.mm.yyyy)
            myUs.Add(new User(user)); // добавление объекта в коллекцию
            IFormatter serializer = new BinaryFormatter(); // Получить сериализатор
            using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, myUs); // сериализация всего массива people
            }
            myUs.Clear(); // очистка коллекции
            using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
            { // десериализация
                myUs = (List<User>)serializer.Deserialize(fs); // считать данные из файла в коллекцию
            }
            MessageBox.Show("Зарегистрировано");
            aa = 0;


            dataGridView1.Rows.Clear();
            int n = 0;
            foreach (User uss in myUs)
            {
                dataGridView1.Rows.Add(uss.B_R, uss.S_N, uss.S_F, DE = Decrypt(uss.S_L, uss.S_EK), DP = Decrypt(uss.S_P, uss.S_PK), uss.B_S, uss.S_C, uss.S_B, uss.S_D);
                n++;
            }

            dataGridView1.CurrentCell = null;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToString();
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToString();
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

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


        public static string CreatePasswordE(int length, int INFO)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=?&/";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            if (INFO == 0)
            {
                myUs[SInd].S_EK = res.ToString();

            }
            else
            {
                user.S_EK = res.ToString();
            }    

            return res.ToString();
        }


        public static string EncryptP(string str, string keyCrypt)
        {
            return Convert.ToBase64String(EncryptP(Encoding.UTF8.GetBytes(str), keyCrypt));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            this.Hide();
            form6.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            this.Hide();
            form8.Show();
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



        public static string CreatePasswordP(int length, int INFO)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=?&/";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            if (INFO == 0)
            {
                myUs[SInd].S_PK = res.ToString();

            }
            else
            {
                user.S_PK = res.ToString();
            }

            return res.ToString();
        }



        public static string Decrypt(string str, string keyCrypt)
        {
            try
            {
                byte[] encryptedData = Convert.FromBase64String(str);
                byte[] decryptedData = Decrypt(encryptedData, keyCrypt);
                return Encoding.UTF8.GetString(decryptedData);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        private static byte[] Decrypt(byte[] data, string key)
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
    }
 }
