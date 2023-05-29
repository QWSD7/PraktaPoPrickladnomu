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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web.Services.Description;

namespace треееш
{
    public partial class Form6 : Form
    {
        
        public static List<Tovar> tovarCollection = new List<Tovar>();

        public static string selectedFile;
        public static Image selectedImage;

        DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();

        public Form6()
        {
            InitializeComponent();

            
            dataGridView1.Columns.Add("T_A", "Артикул");
            dataGridView1.Columns.Add("T_N", "Название");
            dataGridView1.Columns.Insert(2, iconColumn);
            iconColumn.HeaderText = "Фото";
            dataGridView1.Columns.Add("T_P", "Цена");
            dataGridView1.Columns.Add("T_Q", "Количество");
            

            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].Width = 120;
            

            // Загрузка данных о товарах из файла
            if (File.Exists("tovar.dat"))
            {
                using (FileStream fs = new FileStream("tovar.dat", FileMode.OpenOrCreate))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    tovarCollection = (List<Tovar>)serializer.Deserialize(fs);
                }
            }


            int n = 0; // количество объектов коллекции 
            foreach (Tovar tov in tovarCollection)
            {
                dataGridView1.Rows.Add(tov.Article, tov.Name, tov.ImageFileName, tov.Price, tov.Quantity);
                

                dataGridView1.Rows[n].Height = 250;
                n++;

            }
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            // Пример добавления товара
            Tovar newTovar = new Tovar();
            newTovar.Article = (int)numericUpDown1.Value;
            newTovar.Name = textBox1.Text;
            newTovar.ImageFileName = selectedImage;
            newTovar.Price = numericUpDown2.Value;
            newTovar.Quantity = (int)numericUpDown3.Value;

            // Проверка наличия артикула в коллекции
            bool isDuplicate = false;
            foreach (Tovar existingTovar in tovarCollection)
            {
                if (existingTovar.Article == newTovar.Article)
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
                tovarCollection.Add(newTovar);

                // Сохранение данных о товарах в файл
                using (FileStream fs = new FileStream("tovar.dat", FileMode.OpenOrCreate))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(fs, tovarCollection);
                }


                dataGridView1.Rows.Clear();
                int n = 0; // количество объектов коллекции 
                foreach (Tovar tov in tovarCollection)
                {
                    dataGridView1.Rows.Add(tov.Article, tov.Name, tov.ImageFileName, tov.Price, tov.Quantity);

                    dataGridView1.Rows[n].Height = 250;
                    n++;
                }
            }


            dataGridView1.CurrentCell = null;
            numericUpDown1.ReadOnly = false;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            textBox1.Text = "";
            pictureBox1.Image = Image.FromFile("C:\\Users\\user\\Desktop\\Prakta_Po_Prikladnomu-49-55\\PraktaPoPrickladnomu\\треееш\\треееш\\bin\\Photo\\PhotoDefalut.png");


        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображений|*.jpg;*.jpeg;*.png;*.gif|Все файлы|*.*";
            openFileDialog.Title = "Выберите файл изображения";
            openFileDialog.InitialDirectory = "C:\\Users\\user\\Desktop\\Prakta_Po_Prikladnomu-49-55\\PraktaPoPrickladnomu\\треееш\\треееш\\bin\\Photo";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFile = openFileDialog.FileName;

                selectedImage = Image.FromFile(selectedFile);
                pictureBox1.Image = selectedImage;

                pictureBox1.ImageLocation = selectedFile;
                pictureBox1.Load();

            }



        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                Tovar selectedTovar = tovarCollection[selectedIndex];

                if (MessageBox.Show("Хотите удалить запись?", "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    tovarCollection.RemoveAt(selectedIndex);
                    dataGridView1.Rows.RemoveAt(selectedIndex);

                    // Сохранение данных после удаления записи
                    IFormatter serializer = new BinaryFormatter();
                    using (FileStream fs = new FileStream("tovar.dat", FileMode.OpenOrCreate))
                    {
                        serializer.Serialize(fs, tovarCollection);
                    }
                }
            }

            dataGridView1.CurrentCell = null;
            numericUpDown1.ReadOnly = false;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            textBox1.Text = "";
            pictureBox1.Image = Image.FromFile("C:\\Users\\user\\Desktop\\Prakta_Po_Prikladnomu-49-55\\PraktaPoPrickladnomu\\треееш\\треееш\\bin\\Photo\\PhotoDefalut.png");


        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Получение индекса выбранной строки в dataGridView1
            int selectedIndex = dataGridView1.SelectedCells[0].RowIndex;

            // Проверка наличия выбранной строки
            if (selectedIndex >= 0 && selectedIndex < tovarCollection.Count)
            {
                // Получение объекта Tovar по индексу
                Tovar selectedTovar = tovarCollection[selectedIndex];

                // Проверка наличия артикула в коллекции
                bool isDuplicate = false;
                foreach (Tovar existingTovar in tovarCollection)
                {
                    if (existingTovar.Article == (int)numericUpDown1.Value && existingTovar != selectedTovar)
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
                    selectedTovar.Article = (int)numericUpDown1.Value;
                    selectedTovar.Name = textBox1.Text;
                    selectedTovar.ImageFileName = pictureBox1.Image;
                    selectedTovar.Price = numericUpDown2.Value;
                    selectedTovar.Quantity = (int)numericUpDown3.Value;

                    // Сохранение данных о товарах в файл
                    using (FileStream fs = new FileStream("tovar.dat", FileMode.OpenOrCreate))
                    {
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fs, tovarCollection);
                    }

                    // Обновление DataGridView
                    dataGridView1.Rows.Clear();
                    int n = 0; // количество объектов коллекции 
                    foreach (Tovar tov in tovarCollection)
                    {
                        dataGridView1.Rows.Add(tov.Article, tov.Name, tov.ImageFileName, tov.Price, tov.Quantity);
                        dataGridView1.Rows[n].Height = 250;
                        n++;
                    }
                }
            }

            dataGridView1.CurrentCell = null;
            numericUpDown1.ReadOnly = false;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            textBox1.Text = "";
            pictureBox1.Image = Image.FromFile("C:\\Users\\user\\Desktop\\Prakta_Po_Prikladnomu-49-55\\PraktaPoPrickladnomu\\треееш\\треееш\\bin\\Photo\\PhotoDefalut.png");



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView1.SelectedCells[0].RowIndex;
                numericUpDown1.ReadOnly = true;


                try
                {
                    numericUpDown1.Value = (int)dataGridView1.Rows[selectedIndex].Cells[0].Value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении значения numericUpDown1: " + ex.Message);
                }

                try
                {
                    textBox1.Text = dataGridView1.Rows[selectedIndex].Cells[1].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении значения textBox1: " + ex.Message);
                }

                try
                {
                    pictureBox1.Image = (Image)dataGridView1.Rows[selectedIndex].Cells[2].Value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении значения pictureBox1.Image: " + ex.Message);
                }

                try
                {
                    numericUpDown2.Value = (decimal)dataGridView1.Rows[selectedIndex].Cells["T_P"].Value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении значения numericUpDown2: " + ex.Message);
                }

                try
                {
                    numericUpDown3.Value = (int)dataGridView1.Rows[selectedIndex].Cells["T_Q"].Value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении значения numericUpDown3: " + ex.Message);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!!!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            numericUpDown1.ReadOnly = false;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            textBox1.Text = "";
            pictureBox1.Image = Image.FromFile("C:\\Users\\user\\Desktop\\Prakta_Po_Prikladnomu-49-55\\PraktaPoPrickladnomu\\треееш\\треееш\\bin\\Photo\\PhotoDefalut.png");

        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
                if (dataGridView1[0, i].FormattedValue.ToString().Contains(numericUpDown4.Value.ToString().Trim()))
                {
                    dataGridView1.CurrentCell = dataGridView1[0, i];
                    return;
                }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear(); // Очищаем таблицу
            dataGridView1.Columns.Add("T_A", "Артикул");
            dataGridView1.Columns.Add("T_N", "Название");
            dataGridView1.Columns.Insert(2, iconColumn);
            iconColumn.HeaderText = "Фото";
            dataGridView1.Columns.Add("T_P", "Цена");
            dataGridView1.Columns.Add("T_Q", "Количество");

            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].Width = 120;

            dataGridView1.Rows.Clear(); // Очищаем строки таблицы

            int i = 0;
            foreach (Tovar tovar in tovarCollection)
            {
                if (tovar.Quantity == Convert.ToInt32(textBox2.Text))
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = tovar.Article;
                    dataGridView1.Rows[i].Cells[1].Value = tovar.Name;
                    dataGridView1.Rows[i].Cells[2].Value = tovar.ImageFileName;
                    dataGridView1.Rows[i].Cells[3].Value = tovar.Price;
                    dataGridView1.Rows[i].Cells[4].Value = tovar.Quantity;
                    dataGridView1.Rows[i].Height = 250;
                    i++;
                }
            }



        }

        private void button9_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = null; // Очищаем источник данных
            dataGridView1.Rows.Clear(); // Очищаем строки таблицы

            // Заполняем DataGridView всеми данными из коллекции tovarCollection
            int i = 0;
            foreach (Tovar tovar in tovarCollection)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = tovar.Article;
                dataGridView1.Rows[i].Cells[1].Value = tovar.Name;
                dataGridView1.Rows[i].Cells[2].Value = tovar.ImageFileName;
                dataGridView1.Rows[i].Cells[3].Value = tovar.Price;
                dataGridView1.Rows[i].Cells[4].Value = tovar.Quantity;
                dataGridView1.Rows[i].Height = 250;
                i++;
            }

            textBox2.Text = "";
            dataGridView1.CurrentCell = null;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            numericUpDown4.Value = 0;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }




    [Serializable] // сериализация
    // Класс Tovar для хранения информации о товаре
    public class Tovar
    {
        private int article;
        private string name;
        private Image selectedImage;
        private decimal price;
        private int quantity;

        public int Article
        {
            get { return article; }
            set { article = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Image ImageFileName
        {
            get { return selectedImage; }
            set { selectedImage = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }
}
