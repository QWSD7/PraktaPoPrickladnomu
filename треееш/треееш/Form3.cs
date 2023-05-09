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

namespace треееш
{
    public partial class Form3 : Form
    {
        int n = 0; // количество объектов коллекции 

        static public User user = new User(); // объект класса
        static public List<User> myUs = new List<User>(); // коллекция объектов класса - пользователей

        public Form3()
        {
            
            InitializeComponent();



           
           
            int n = 0; // количество объектов коллекции 
            foreach (User uss in myUs)
            { // вывод коллекции пользователей, загружаемой из файла в Form3
                listBox1.Items.Add(uss.B_R.ToString() + "\t" + uss.S_N + "\t" + uss.S_F + "\t" + uss.S_L + "\t" + uss.S_P + "\t" +
               uss.B_S.ToString() + "\t" + uss.S_C + "\t" + uss.S_B + "\t" + uss.S_D);
                n++;
            }
        }


    

    private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show(); // отобразить форму
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            foreach (User uss in myUs)
            { // вывод коллекции пользователей, загружаемой из файла в Form3
                listBox1.Items.Add(uss.B_R.ToString() + "\t" + uss.S_N + "\t" + uss.S_F + "\t" + uss.S_L + "\t" + uss.S_P + "\t" +
               uss.B_S.ToString() + "\t" + uss.S_C + "\t" + uss.S_B + "\t" + uss.S_D);
                n++;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // удаление записи из listBox1
           if (this.listBox1.Items.Count > 0)
                if (this.listBox1.SelectedIndex > -1) 
                { 
                    user = myUs[this.listBox1.SelectedIndex]; 
                }
            if (MessageBox.Show("Хотите удалить запись?", "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                myUs.RemoveAt(this.listBox1.SelectedIndex);
                this.listBox1.Items.Clear(); // очистка списка ListBox 
                n = 0;
                foreach (User uss in myUs)
                {
                    listBox1.Items.Add(uss.B_R.ToString() + "\t" + uss.S_N + "\t" + uss.S_F + "\t" + uss.S_L + "\t" + uss.S_P + "\t" +
                   uss.B_S.ToString() + "\t" + uss.S_C + "\t" + uss.S_B + "\t" + uss.S_D);
                    n++;
                }
                // обновить данные в файле!!!
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
            bool b_role; // роль
            string s_name; // имя
            string s_fam; // фамилия 
            bool b_sex; // пол
            string s_country; // страна
            string s_birthdate; // дата рождения
            string s_datereg; // дата регистрации
            public User()
            { // конструктор по умолчанию
                s_login = ""; s_password = ""; b_role = false; s_name = ""; s_fam = "";
                b_sex = false; s_country = "Россия"; s_birthdate = "00.00.0000"; s_datereg = "00.00.0000";
            }
            public User(User x)
            { // конструктор с параметрами
                s_login = x.S_L; s_password = x.S_P; b_role = x.B_R; s_name = x.S_N; s_fam = x.S_F;
                b_sex = x.B_S; s_country = x.S_C; s_birthdate = x.S_B; s_datereg = x.S_D;
            }
            public bool B_R
            { //объявление свойства для чтения и записи поля s_name 
                get { return b_role; } // аксессор чтения поля 
                set { b_role = value; } // аксессор записи в поле
            }
            public string S_N { get { return s_name; } set { s_name = value; } }
            public string S_F { get { return s_fam; } set { s_fam = value; } }
            public string S_L { get { return s_login; } set { s_login = value; } }
            public string S_P { get { return s_password; } set { s_password = value; } }
            public bool B_S { get { return b_sex; } set { b_sex = value; } }
            public string S_C { get { return s_country; } set { s_country = value; } }
            public string S_B { get { return s_birthdate; } set { s_birthdate = value; } }
            public string S_D { get { return s_datereg; } set { s_datereg = value; } }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            /*
            Form3 ifrm = new Form3();
            this.Hide();
            ifrm.ShowDialog(); // форма авторизации
            */
        }
    }
 }
