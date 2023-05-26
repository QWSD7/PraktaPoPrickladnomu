using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace треееш
{
    public partial class Form8 : Form
    {
        List<Provider> providerCollection = new List<Provider>();

        public Form8()
        {
            InitializeComponent();

            dataGridView1.Columns.Add("P_NP", "Номер_поставщика");
            dataGridView1.Columns.Add("P_NO", "Название_организации");
            dataGridView1.Columns.Add("P_Co", "Страна");
            dataGridView1.Columns.Add("P_Ci", "Город");
            dataGridView1.Columns.Add("P_S", "Улица");
            dataGridView1.Columns.Add("P_HN", "Номер_дома");
            dataGridView1.Columns.Add("P_PN", "Телефон");
            dataGridView1.Columns.Add("P_CP", "ФИО_руководиетеля_или_контактера");

            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 190;
            dataGridView1.Columns[5].Width = 160;
            dataGridView1.Columns[6].Width = 160;
            dataGridView1.Columns[7].Width = 260;

            // Загрузка данных о товарах из файла
            if (File.Exists("provider.dat"))
            {
                using (FileStream fs = new FileStream("provider.dat", FileMode.OpenOrCreate))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    providerCollection = (List<Provider>)serializer.Deserialize(fs);
                }
            }


            int n = 0; // количество объектов коллекции 
            foreach (Provider prov in providerCollection)
            {
                dataGridView1.Rows.Add(prov.ProviderCode, prov.OrganizationName, prov.Country, prov.City, prov.Street, prov.HomeNumber, prov.PhoneNumber, prov.ContactPerson);


               // dataGridView1.Rows[n].Height = 250;
                n++;

            }




        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Пример добавления товара
            Provider newProv = new Provider();
            newProv.ProviderCode = (int)numericUpDown1.Value;
            newProv.OrganizationName = textBox1.Text;
            newProv.Country = comboBox1.SelectedItem.ToString();
            newProv.City = textBox2.Text;
            newProv.Street = comboBox2.SelectedItem.ToString();
            newProv.HomeNumber = Convert.ToInt32(comboBox3.SelectedItem);
            newProv.PhoneNumber = textBox3.Text;
            newProv.ContactPerson = textBox4.Text;

            bool isDuplicate = false;
            foreach (Provider existingProvider in providerCollection)
            {
                if (existingProvider.ProviderCode == newProv.ProviderCode)
                {
                    isDuplicate = true;
                    break;
                }
            }

            if (isDuplicate)
            {
                MessageBox.Show("Номер продавца уже существует. Пожалуйста, выберите другой номер продавца.");
            }
            else
            {
                providerCollection.Add(newProv);

                // Сохранение данных о товарах в файл
                using (FileStream fs = new FileStream("provider.dat", FileMode.OpenOrCreate))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(fs, providerCollection);
                }


                dataGridView1.Rows.Clear();
                int n = 0; // количество объектов коллекции 
                foreach (Provider prov in providerCollection)
                {
                    dataGridView1.Rows.Add(prov.ProviderCode, prov.OrganizationName, prov.Country, prov.City, prov.Street, prov.HomeNumber, prov.PhoneNumber, prov.ContactPerson);

                    // dataGridView1.Rows[n].Height = 250;
                    n++;
                }

            }
            dataGridView1.CurrentCell = null;
            numericUpDown1.ReadOnly = false;
            numericUpDown1.Value = 0;
            textBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            textBox2.Text = "";
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            textBox3.Text = "";
            textBox4.Text = "";


        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            numericUpDown1.ReadOnly = false;
            numericUpDown1.Value = 0;
            textBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            textBox2.Text = "";
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Получение индекса выбранной строки в dataGridView1
            int selectedIndex = dataGridView1.SelectedCells[0].RowIndex;

            // Проверка наличия выбранной строки
            if (selectedIndex >= 0 && selectedIndex < providerCollection.Count)
            {
                // Получение объекта Tovar по индексу
                Provider selectedProvider = providerCollection[selectedIndex];

                // Проверка наличия артикула в коллекции
                bool isDuplicate = false;
                foreach (Provider existingProvider in providerCollection)
                {
                    if (existingProvider.ProviderCode == (int)numericUpDown1.Value && existingProvider != selectedProvider)
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (isDuplicate)
                {
                    MessageBox.Show("Артикул уже существует. Пожалуйста, выберите другой артикул.");
                }
                else
                {
                    // Обновление значений свойств объекта Tovar

                    selectedProvider.ProviderCode = (int)numericUpDown1.Value;
                    selectedProvider.OrganizationName = textBox1.Text;
                    selectedProvider.Country = comboBox1.SelectedItem.ToString();
                    selectedProvider.City = textBox2.Text;
                    selectedProvider.Street = comboBox2.SelectedItem.ToString();
                    selectedProvider.HomeNumber = Convert.ToInt32(comboBox3.SelectedItem);
                    selectedProvider.PhoneNumber = textBox3.Text;
                    selectedProvider.ContactPerson = textBox4.Text;


                    // Сохранение данных о товарах в файл
                    using (FileStream fs = new FileStream("provider.dat", FileMode.OpenOrCreate))
                    {
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fs, providerCollection);
                    }


                    dataGridView1.Rows.Clear();
                    int n = 0; // количество объектов коллекции 
                    foreach (Provider prov in providerCollection)
                    {
                        dataGridView1.Rows.Add(prov.ProviderCode, prov.OrganizationName, prov.Country, prov.City, prov.Street, prov.HomeNumber, prov.PhoneNumber, prov.ContactPerson);

                        // dataGridView1.Rows[n].Height = 250;
                        n++;
                    }

                }

                dataGridView1.CurrentCell = null;
                numericUpDown1.ReadOnly = false;
                numericUpDown1.Value = 0;
                textBox1.Text = "";
                comboBox1.SelectedIndex = -1;
                textBox2.Text = "";
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                textBox3.Text = "";
                textBox4.Text = "";


            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                Provider selectedProvider = providerCollection[selectedIndex];

                if (MessageBox.Show("Хотите удалить запись?", "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    providerCollection.RemoveAt(selectedIndex);
                    dataGridView1.Rows.RemoveAt(selectedIndex);

                    // Сохранение данных о товарах в файл
                    using (FileStream fs = new FileStream("provider.dat", FileMode.OpenOrCreate))
                    {
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fs, providerCollection);
                    }
                }
            }


            dataGridView1.CurrentCell = null;
            numericUpDown1.ReadOnly = false;
            numericUpDown1.Value = 0;
            textBox1.Text = "";
            comboBox1.SelectedItem = -1;
            textBox2.Text = "";
            comboBox2.SelectedItem = -1;
            comboBox3.SelectedItem = -1;
            textBox3.Text = "";
            textBox4.Text = "";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("P_NP", "Номер_поставщика");
            dataGridView1.Columns.Add("P_NO", "Название_организации");
            dataGridView1.Columns.Add("P_Co", "Страна");
            dataGridView1.Columns.Add("P_Ci", "Город");
            dataGridView1.Columns.Add("P_S", "Улица");
            dataGridView1.Columns.Add("P_HN", "Номер_дома");
            dataGridView1.Columns.Add("P_PN", "Телефон");
            dataGridView1.Columns.Add("P_CP", "ФИО_руководиетеля_или_контактера");

            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].Width = 200;
            dataGridView1.Columns[6].Width = 200;
            dataGridView1.Columns[7].Width = 200;

            dataGridView1.Rows.Clear(); // Очищаем строки таблицы

            int i = 0;
            foreach (Provider prov in providerCollection)
            {
                if (prov.Country == comboBox5.SelectedItem.ToString())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = prov.ProviderCode;
                    dataGridView1.Rows[i].Cells[1].Value = prov.OrganizationName;
                    dataGridView1.Rows[i].Cells[2].Value = prov.Country;
                    dataGridView1.Rows[i].Cells[3].Value = prov.City;
                    dataGridView1.Rows[i].Cells[4].Value = prov.Street;
                    dataGridView1.Rows[i].Cells[5].Value = prov.HomeNumber;
                    dataGridView1.Rows[i].Cells[6].Value = prov.PhoneNumber;
                    dataGridView1.Rows[i].Cells[7].Value = prov.ContactPerson;

                    i++;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null; // Очищаем источник данных
            dataGridView1.Rows.Clear(); // Очищаем строки таблицы
            int i = 0;
            foreach (Provider prov in providerCollection)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = prov.ProviderCode;
                dataGridView1.Rows[i].Cells[1].Value = prov.OrganizationName;
                dataGridView1.Rows[i].Cells[2].Value = prov.Country;
                dataGridView1.Rows[i].Cells[3].Value = prov.City;
                dataGridView1.Rows[i].Cells[4].Value = prov.Street;
                dataGridView1.Rows[i].Cells[5].Value = prov.HomeNumber;
                dataGridView1.Rows[i].Cells[6].Value = prov.PhoneNumber;
                dataGridView1.Rows[i].Cells[7].Value = prov.ContactPerson;
                i++;
            }

            dataGridView1.CurrentCell = null;
            comboBox5.SelectedItem = -1;

           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
                if (dataGridView1[0, i].FormattedValue.ToString().Contains(numericUpDown2.Value.ToString().Trim()))
                {
                    dataGridView1.CurrentCell = dataGridView1[0, i];
                    return;
                }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            numericUpDown2.Value = 0;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView1.SelectedCells[0].RowIndex;
                numericUpDown1.ReadOnly = true;

                numericUpDown1.Value = (int)dataGridView1.Rows[selectedIndex].Cells[0].Value;
                textBox1.Text = dataGridView1.Rows[selectedIndex].Cells[1].Value.ToString();
                comboBox1.SelectedItem = dataGridView1.Rows[selectedIndex].Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.Rows[selectedIndex].Cells[3].Value.ToString();
                comboBox2.SelectedItem = dataGridView1.Rows[selectedIndex].Cells[4].Value.ToString();
                comboBox3.SelectedItem = dataGridView1.Rows[selectedIndex].Cells[5].Value.ToString();
                textBox3.Text = dataGridView1.Rows[selectedIndex].Cells[6].Value.ToString();
                textBox4.Text = dataGridView1.Rows[selectedIndex].Cells[7].Value.ToString();




            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!!!");
            }
        }
    }
}

[Serializable]
class Provider
{
    private int providerCode;
    private string organizationName;
    private string country;
    private string city;
    private string street;
    private int homeNumber;
    private string phoneNumber;
    private string contactPerson;

    public int ProviderCode
    {
        get { return providerCode; }
        set { providerCode = value; }
    }

    public string OrganizationName
    {
        get { return organizationName; }
        set { organizationName = value; }
    }

    public string Country
    {
        get { return country; }
        set { country = value; }
    }

    public string City
    {
        get { return city; }
        set { city = value; }
    }

    public string Street
    {
        get { return street; }
        set { street = value; }
    }

    public int HomeNumber
    {
        get { return homeNumber; }
        set { homeNumber = value; }
    }

    public string PhoneNumber
    {
        get { return phoneNumber; }
        set { phoneNumber = value; }
    }

    public string ContactPerson
    {
        get { return contactPerson; }
        set { contactPerson = value; }
    }
}