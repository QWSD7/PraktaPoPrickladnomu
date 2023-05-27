using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static треееш.Form3;

namespace треееш
{
    public partial class Form7 : Form
    {


        public static List<Operation> operationCollection = new List<Operation>();

        public Form7()
        {
            InitializeComponent();

            dataGridView1.Columns.Add("O_A", "Артикул_Товара");
            dataGridView1.Columns.Add("O_D", "Дата_прихода/расхода_товара");
            dataGridView1.Columns.Add("O_P", "Цена_продажная");
            dataGridView1.Columns.Add("O_Q", "Количество_прихода/расхода");
            dataGridView1.Columns.Add("O_C", "Код_операции");
            dataGridView1.Columns.Add("O_N", " № Поставзика / Login покупателя / Причина_списания");
            
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 300;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 550;
            


            // Чтение данных из файла товаров и получение коллекции артикулов товаров
            List<int> productArtList = new List<int>();
            List<decimal> productPriList = new List<decimal>();

            using (FileStream fs = new FileStream("tovar.dat", FileMode.Open))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                List<Tovar> tovarCollection = (List<Tovar>)serializer.Deserialize(fs);

                foreach (Tovar tovar in tovarCollection)
                {
                    productArtList.Add(tovar.Article);
                }

                foreach (Tovar tovar in tovarCollection)
                {
                    productPriList.Add(tovar.Price);
                }
            }

            // Добавление артикулов товаров в ComboBox1
            comboBox1.Items.Clear(); // Очистка существующих элементов в ComboBox1
            comboBox1.DataSource = productArtList; // Привязка коллекции артикулов товаров к ComboBox1

            // Добавление артикулов товаров в ComboBox1
            comboBox2.Items.Clear(); // Очистка существующих элементов в ComboBox1
            comboBox2.DataSource = productPriList; // Привязка коллекции артикулов товаров к ComboBox1



            // Чтение данных из файла товаров и получение коллекции артикулов товаров
            List<int> providerPCList = new List<int>();
            
            using (FileStream fs = new FileStream("provider.dat", FileMode.Open))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                List<Provider> providerCollection = (List<Provider>)serializer.Deserialize(fs);

                foreach (Provider prov in providerCollection)
                {
                    providerPCList.Add(prov.ProviderCode);
                }
            }

            // Добавление артикулов товаров в ComboBox1
            comboBox4.Items.Clear(); // Очистка существующих элементов в ComboBox1
            comboBox4.DataSource = providerPCList; // Привязка коллекции артикулов товаров к ComboBox1


            // Чтение данных из файла товаров и получение коллекции артикулов товаров
            List<string> userLogList = new List<string>();

            using (FileStream fs = new FileStream("people.dat ", FileMode.Open))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                List<User> userCollection = (List<User>)serializer.Deserialize(fs);

                foreach (Form3.User user in userCollection)
                {
                    
                    userLogList.Add(Form1.decryptedEmail = Form1.DecryptE(user.S_L, user.S_EK));


                }
            }

            // Добавление артикулов товаров в ComboBox1
            comboBox5.Items.Clear(); // Очистка существующих элементов в ComboBox1
            comboBox5.DataSource = userLogList; // Привязка коллекции артикулов товаров к ComboBox1






            // Загрузка данных о товарах из файла
            if (File.Exists("operation.dat"))
            {
                using (FileStream fs = new FileStream("operation.dat", FileMode.OpenOrCreate))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    operationCollection = (List<Operation>)serializer.Deserialize(fs);
                }
            }


            int n = 0; // количество объектов коллекции 
            foreach (Operation oper in operationCollection)
            {
                dataGridView1.Rows.Add(oper.Article, oper.Date, oper.OperationPrice, oper.Quantity, oper.OperationCode, oper.AdditionalInfo );

                n++;

            }

        }

        Tovar foundTovar = null;

        private void button3_Click(object sender, EventArgs e)
        {
            int addInf;

            // Пример добавления товара
            Operation newOper = new Operation();
            newOper.Article = comboBox1.SelectedItem.ToString();
            newOper.Date = dateTimePicker1.Value;
            newOper.OperationPrice = Convert.ToDecimal(comboBox2.SelectedItem);
            newOper.Quantity = (int)numericUpDown1.Value;

            if (radioButton2.Checked == true)
            {
                List<int> productArtList = new List<int>();
                List<decimal> productPriList = new List<decimal>();
                List<Tovar> tovarCollection;

                using (FileStream fs = new FileStream("tovar.dat", FileMode.Open))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    tovarCollection = (List<Tovar>)serializer.Deserialize(fs);

                    

                    foundTovar = tovarCollection.Find(tovar => tovar.Article == (int)comboBox1.SelectedItem);

                   
                }

                if (foundTovar != null)
                {
                    decimal price = foundTovar.Price;


                    foundTovar.Price = (decimal)comboBox2.SelectedItem;
                    foundTovar.Quantity = foundTovar.Quantity + (int)numericUpDown1.Value;

                    using (FileStream fs = new FileStream("tovar.dat", FileMode.OpenOrCreate))
                    {
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fs, tovarCollection);
                    }

                }




                newOper.OperationCode = 1;
                addInf = 0;
                newOper.AdditionalInfo = comboBox4.SelectedItem.ToString() + " - Номер поставщика";
            }
            else if (radioButton3.Checked == true)
            {

                List<int> productArtList = new List<int>();
                List<decimal> productPriList = new List<decimal>();
                List<Tovar> tovarCollection;

                using (FileStream fs = new FileStream("tovar.dat", FileMode.Open))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    tovarCollection = (List<Tovar>)serializer.Deserialize(fs);



                    foundTovar = tovarCollection.Find(tovar => tovar.Article == (int)comboBox1.SelectedItem);


                }

                if (foundTovar != null)
                {
                    decimal price = foundTovar.Price;


                    foundTovar.Price = (decimal)comboBox2.SelectedItem;
                    foundTovar.Quantity = foundTovar.Quantity - (int)numericUpDown1.Value; ////////////////////////////////////

                    using (FileStream fs = new FileStream("tovar.dat", FileMode.OpenOrCreate))
                    {
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fs, tovarCollection);
                    }

                }







                newOper.OperationCode = 2;
                addInf = 1;
                newOper.AdditionalInfo = comboBox5.SelectedItem.ToString() + " - Логин пользователя";
            }
            else if (radioButton1.Checked == true)
            {

                List<int> productArtList = new List<int>();
                List<decimal> productPriList = new List<decimal>();
                List<Tovar> tovarCollection;

                using (FileStream fs = new FileStream("tovar.dat", FileMode.Open))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    tovarCollection = (List<Tovar>)serializer.Deserialize(fs);



                    foundTovar = tovarCollection.Find(tovar => tovar.Article == (int)comboBox1.SelectedItem);


                }

                if (foundTovar != null)
                {
                    decimal price = foundTovar.Price;


                    foundTovar.Price = (decimal)comboBox2.SelectedItem;
                    foundTovar.Quantity = foundTovar.Quantity - (int)numericUpDown1.Value;

                    using (FileStream fs = new FileStream("tovar.dat", FileMode.OpenOrCreate))
                    {
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fs, tovarCollection);
                    }

                }







                newOper.OperationCode = 0;
                addInf = 2;
                newOper.AdditionalInfo = comboBox6.SelectedItem.ToString() + " - Причина списания";
            }

            operationCollection.Add(newOper);

            // Сохранение данных о товарах в файл
            using (FileStream fs = new FileStream("operation.dat", FileMode.OpenOrCreate))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(fs, operationCollection);
            }

            dataGridView1.Rows.Clear();
            int n = 0; // количество объектов коллекции 
            foreach (Operation oper in operationCollection)
            {
                dataGridView1.Rows.Add(oper.Article, oper.Date, oper.OperationPrice, oper.Quantity, oper.OperationCode, oper.AdditionalInfo);

                n++;

            }

            dataGridView1.CurrentCell = null;
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            comboBox2.SelectedIndex = -1;
            numericUpDown1.Value = 0;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int addInf;
            //изменение данных
            // Получение индекса выбранной строки в dataGridView1
            int selectedIndex = dataGridView1.SelectedCells[0].RowIndex;
            // Проверка наличия выбранной строки
            if (selectedIndex >= 0 && selectedIndex < operationCollection.Count)
            {
                // Получение объекта Tovar по индексу
                Operation selectedOperation = operationCollection[selectedIndex];

                // Обновление значений свойств объекта Tovar
                selectedOperation.Article = comboBox1.SelectedItem.ToString();
                selectedOperation.Date = dateTimePicker1.Value;
                selectedOperation.OperationPrice = Convert.ToDecimal(comboBox2.SelectedItem);
                selectedOperation.Quantity = (int)numericUpDown1.Value;

                if (radioButton2.Checked == true)
                {
                    selectedOperation.OperationCode = 1;
                    addInf = 0;
                    selectedOperation.AdditionalInfo = comboBox4.SelectedItem.ToString() + " - Номер поставщика";
                }
                else if (radioButton3.Checked == true)
                {
                    selectedOperation.OperationCode = 2;
                    addInf = 1;
                    selectedOperation.AdditionalInfo = comboBox5.SelectedItem.ToString() + " - Логин пользователя";
                }
                else if (radioButton1.Checked == true)
                {
                    selectedOperation.OperationCode = 0;
                    addInf = 2;
                    selectedOperation.AdditionalInfo = comboBox6.SelectedItem.ToString() + " - Причина списания";
                }

                // Сохранение данных о товарах в файл
                using (FileStream fs = new FileStream("operation.dat", FileMode.OpenOrCreate))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(fs, operationCollection);
                }

                dataGridView1.Rows.Clear();
                int n = 0; // количество объектов коллекции 
                foreach (Operation oper in operationCollection)
                {
                    dataGridView1.Rows.Add(oper.Article, oper.Date, oper.OperationPrice, oper.Quantity, oper.OperationCode, oper.AdditionalInfo);

                    n++;
                }

            }

            dataGridView1.CurrentCell = null;
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            comboBox2.SelectedIndex = -1;
            numericUpDown1.Value = 0;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //удаление данных
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                Operation selectedOperation = operationCollection[selectedIndex];

                if (MessageBox.Show("Хотите удалить запись?", "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    operationCollection.RemoveAt(selectedIndex);
                    dataGridView1.Rows.RemoveAt(selectedIndex);

                    // Сохранение данных о товарах в файл
                    using (FileStream fs = new FileStream("operation.dat", FileMode.OpenOrCreate))
                    {
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fs, operationCollection);
                    }
                }
            }

            dataGridView1.CurrentCell = null;
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            comboBox2.SelectedIndex = -1;
            numericUpDown1.Value = 0;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            comboBox2.SelectedIndex = -1;
            numericUpDown1.Value = 0;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView1.SelectedCells[0].RowIndex;

               
                comboBox1.SelectedItem = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells[0].Value);
                dateTimePicker1.Value = (DateTime)dataGridView1.Rows[selectedIndex].Cells[1].Value;
                comboBox2.SelectedItem = dataGridView1.Rows[selectedIndex].Cells[2].Value;
                numericUpDown1.Value = (int)dataGridView1.Rows[selectedIndex].Cells[3].Value;
                int operationCode = (int)dataGridView1.Rows[selectedIndex].Cells[4].Value;

                if (operationCode == 0)
                {
                    radioButton1.Select();

                    string selectedItem = dataGridView1.Rows[selectedIndex].Cells[5].Value.ToString();
                    int dashIndex = selectedItem.IndexOf("-");
                    if (dashIndex != -1)
                    {
                        selectedItem = selectedItem.Substring(0, dashIndex).Trim();
                    }

                    comboBox6.SelectedItem = selectedItem;

                    comboBox4.SelectedIndex = -1;
                    comboBox5.SelectedIndex = -1;

                }
                else if (operationCode == 1)
                {
                    radioButton2.Select();

                    string selectedItem = dataGridView1.Rows[selectedIndex].Cells[5].Value.ToString();
                    int dashIndex = selectedItem.IndexOf("-");
                    if (dashIndex != -1)
                    {
                        selectedItem = selectedItem.Substring(0, dashIndex).Trim();
                    }

                    comboBox4.SelectedItem = Convert.ToInt32(selectedItem);

                    comboBox5.SelectedIndex = -1;
                    comboBox6.SelectedIndex = -1;

                }
                else if (operationCode == 2)
                {
                    radioButton3.Select();

                    string selectedItem = dataGridView1.Rows[selectedIndex].Cells[5].Value.ToString();
                    int dashIndex = selectedItem.IndexOf("-");
                    if (dashIndex != -1)
                    {
                        selectedItem = selectedItem.Substring(0, dashIndex).Trim();
                    }

                    comboBox5.SelectedItem = selectedItem;

                    comboBox4.SelectedIndex = -1;
                    comboBox6.SelectedIndex = -1;

                }


               
                
               



            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!!!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear(); // Очищаем таблицу


            dataGridView1.Columns.Add("O_A", "Артикул_Товара");
            dataGridView1.Columns.Add("O_D", "Дата_прихода/расхода_товара");
            dataGridView1.Columns.Add("O_P", "Цена_продажная");
            dataGridView1.Columns.Add("O_Q", "Количество_прихода/расхода");
            dataGridView1.Columns.Add("O_C", "Код_операции");
            dataGridView1.Columns.Add("O_N", " № Поставзика / Login покупателя / Причина_списания");

            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 300;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 550;

            dataGridView1.Rows.Clear(); // Очищаем строки таблицы

            int i = 0;
            
            foreach (Operation oper in operationCollection)
            {

                string additionalInfo = oper.AdditionalInfo;
                string[] parts = additionalInfo.Split('-');
                string supplierNumberString = parts[0].Trim(); // Получаем первую часть и удаляем лишние пробелы
                int supplierNumber;
                int.TryParse(supplierNumberString, out supplierNumber);


                if (supplierNumber == numericUpDown2.Value && oper.Date >= dateTimePicker2.Value && oper.Date <= dateTimePicker3.Value)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = oper.Article;
                    dataGridView1.Rows[i].Cells[1].Value = oper.Date;
                    dataGridView1.Rows[i].Cells[2].Value = oper.OperationPrice;
                    dataGridView1.Rows[i].Cells[3].Value = oper.Quantity;
                    dataGridView1.Rows[i].Cells[4].Value = oper.OperationCode;
                    dataGridView1.Rows[i].Cells[5].Value = oper.AdditionalInfo;
                   
                    i++;
                }
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null; // Очищаем источник данных
            dataGridView1.Rows.Clear(); // Очищаем строки таблицы


            int i = 0;
            foreach (Operation oper in operationCollection)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = oper.Article;
                dataGridView1.Rows[i].Cells[1].Value = oper.Date;
                dataGridView1.Rows[i].Cells[2].Value = oper.OperationPrice;
                dataGridView1.Rows[i].Cells[3].Value = oper.Quantity;
                dataGridView1.Rows[i].Cells[4].Value = oper.OperationCode;
                dataGridView1.Rows[i].Cells[5].Value = oper.AdditionalInfo;
                i++;
            }

            dataGridView1.CurrentCell = null;
            numericUpDown2.Value = 0;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
        }

        int i = 0;

        private void button7_Click(object sender, EventArgs e)
        {
            

            for (; i < dataGridView1.RowCount; i++)
                if (dataGridView1[0, i].FormattedValue.ToString().Contains(numericUpDown4.Value.ToString().Trim()))
                {
                    dataGridView1.CurrentCell = dataGridView1[0, i];
                    if (i < dataGridView1.RowCount - 1) i++;
                    else i = 0;
                    return;
                }
            if (i > (dataGridView1.RowCount - 1)) i = 0;

            /*
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string additionalInfo = dataGridView1[0, i].FormattedValue.ToString();

                // Извлекаем номер поставщика из additionalInfo
                string[] parts = additionalInfo.Split('-');
                string supplierNumberString = parts[0].Trim();
                int supplierNumber;

                if (int.TryParse(supplierNumberString, out supplierNumber))
                {
                    // Сравниваем полученный номер поставщика с искомым значением
                    if (supplierNumber == (int)numericUpDown4.Value)
                    {
                        // Устанавливаем текущую ячейку на найденную строку
                        dataGridView1.CurrentCell = dataGridView1[0, i];
                        
                    }
                }
            }
            */
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            numericUpDown4.Value = 0;
        }
    }
}




[Serializable]
public class Operation
{
    private string article;
    private DateTime date;
    private decimal operationPrice;
    private int quantity;
    private int operationCode;
    private string additionalInfo;

    public string Article
    {
        get { return article; }
        set { article = value; }
    }

    public DateTime Date
    {
        get { return date; }
        set { date = value; }
    }

    public decimal OperationPrice
    {
        get { return operationPrice; }
        set { operationPrice = value; }
    }

    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    public int OperationCode
    {
        get { return operationCode; }
        set { operationCode = value; }
    }

    public string AdditionalInfo
    {
        get { return additionalInfo; }
        set { additionalInfo = value; }
    }
}