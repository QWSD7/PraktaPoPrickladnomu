using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static треееш.Form9;
using static треееш.Form6;
using System.Runtime.Serialization.Formatters.Binary;

namespace треееш
{
    public partial class Form10 : Form
    {
        private Tovar selectedTovar;
        private string imagesFolderPath;
        private decimal selectedQuantity; // Добавленное поле для хранения выбранного количества

        public static List<Tovar> tovarBasket = new List<Tovar>();

        public Form10(Tovar tovar, string folderPath, decimal quantity)
        {
            InitializeComponent();
            selectedTovar = tovar;
            imagesFolderPath = folderPath;
            selectedQuantity = quantity;


            if (File.Exists("tovarBasket.dat"))
            {
                using (FileStream fs = new FileStream("tovarBasket.dat", FileMode.Open))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    tovarBasket = (List<Tovar>)serializer.Deserialize(fs);
                }
            }

        }


        public Form10()
        {
            InitializeComponent();
            if (File.Exists("tovarBasket.dat"))
            {
                using (FileStream fs = new FileStream("tovarBasket.dat", FileMode.Open))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    tovarBasket = (List<Tovar>)serializer.Deserialize(fs);
                }
            }
            FormClosing += Form10_FormClosing; // Добавление обработчика события FormClosing
        }






        private void Form10_Load(object sender, EventArgs e)
        {
            // Загрузка данных о товарах из файла
            if (File.Exists("tovarBasket.dat"))
            {
                using (FileStream fs = new FileStream("tovarBasket.dat", FileMode.Open))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    tovarBasket = (List<Tovar>)serializer.Deserialize(fs);
                }
            }


            if (selectedTovar != null)
            {
                listBox1.Items.Add("Наименование: " + selectedTovar.Name.ToString());
                listBox1.Items.Add("Цена: " + selectedTovar.Price.ToString());
                listBox1.Items.Add("Количество: " + selectedQuantity.ToString()); // Используйте выбранное значение количества

                Image selectedImagePath = selectedTovar.ImageFileName;
                if (selectedImagePath != null)
                {
                    pictureBox1.Image = selectedTovar.ImageFileName;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("Файл выбранного изображения не найден: " + selectedImagePath);
                }

                // Отображение данных о товарах из списка в ListBox
                foreach (Tovar tovar in tovarBasket)
                {
                    listBox1.Items.Add("Наименование: " + tovar.Name);
                    listBox1.Items.Add("Цена: " + tovar.Price.ToString());
                    listBox1.Items.Add("Количество: " + tovar.Quantity.ToString());
                    listBox1.Items.Add("-----------------------------------");

                    selectedImagePath = selectedTovar.ImageFileName;
                    if (selectedImagePath != null)
                    {
                        pictureBox1.Image = selectedTovar.ImageFileName;
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        MessageBox.Show("Файл выбранного изображения не найден: " + selectedImagePath);
                    }
                }



            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            this.Close();
            form9.Show();
        }

        private void Form10_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Сохранение данных о товарах в файл
            using (FileStream fs = new FileStream("tovarBasket.dat", FileMode.OpenOrCreate))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(fs, tovarBasket);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedTovar != null)
            {
                // Добавление выбранного товара в список
                tovarBasket.Add(selectedTovar);

                // Сохранение данных о товарах в файл
                using (FileStream fs = new FileStream("tovarBasket.dat", FileMode.OpenOrCreate))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(fs, tovarBasket);
                }

                MessageBox.Show("Товар успешно добавлен в корзину!");
            }
            else
            {
                MessageBox.Show("Нет выбранного товара для добавления!");
            }
        }



       

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверка выбранного элемента в ListBox
            if (listBox1.SelectedIndex >= 0)
            {
                // Получение индекса выбранного товара
                int selectedIndex = listBox1.SelectedIndex / 5;

                // Получение выбранного товара
                Tovar selectedTovar = tovarBasket[selectedIndex];

                // Обновление значения количества в textBox
                textBox1.Text = selectedTovar.Quantity.ToString();
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            // Проверка выбранного элемента в ListBox
            if (listBox1.SelectedIndex >= 0)
            {
                // Получение индекса выбранного товара
                int selectedIndex = listBox1.SelectedIndex / 5;

                // Удаление товара из списка
                Tovar removedTovar = tovarBasket[selectedIndex];
                tovarBasket.RemoveAt(selectedIndex);

                // Сохранение данных о товарах в файл
                using (FileStream fs = new FileStream("tovarBasket.dat", FileMode.OpenOrCreate))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(fs, tovarBasket);
                }

                MessageBox.Show("Товар успешно удален из корзины!");

                // Отображение содержимого корзины
                DisplayBasketContents();
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления!");
            }
        }


        private void DisplayBasketContents()
        {
            // Очистка списка и общей стоимости
            listBox1.Items.Clear();
            decimal totalCost = 0;

            // Отображение данных о товарах в ListBox
            foreach (Tovar tovar in tovarBasket)
            {
                listBox1.Items.Add("Артикул: " + tovar.Article);
                listBox1.Items.Add("Наименование: " + tovar.Name);
                listBox1.Items.Add("Цена: " + tovar.Price.ToString());
                listBox1.Items.Add("Количество: " + tovar.Quantity.ToString());
                listBox1.Items.Add("-----------------------------------");

                totalCost += tovar.Price * tovar.Quantity;
            }

            // Вывод общей стоимости
            listBox1.Items.Add("Общая стоимость: " + totalCost.ToString());
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                // Получение индекса выбранного товара
                int selectedIndex = listBox1.SelectedIndex / 5;

                // Получение выбранного товара
                Tovar selectedTovar = tovarBasket[selectedIndex];

                // Попытка преобразования введенного значения в decimal
                if (int.TryParse(textBox1.Text, out int newQuantity))
                {
                    // Изменение количества товара
                    selectedTovar.Quantity = newQuantity;

                    // Сохранение данных о товарах в файл
                    using (FileStream fs = new FileStream("tovarBasket.dat", FileMode.OpenOrCreate))
                    {
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fs, tovarBasket);
                    }

                    // Отображение содержимого корзины
                    DisplayBasketContents();
                }
                else
                {
                    MessageBox.Show("Неверное значение количества товара!");
                }
            }
        }


    }












    /*


        private Tovar selectedTovar;
        private string imagesFolderPath;
        private decimal selectedQuantity; // Добавленное поле для хранения выбранного количества

        public static List<Tovar> tovarBasket = new List<Tovar>();

        public Form10(Tovar tovar, string folderPath, decimal quantity)
        {
            InitializeComponent();
            selectedTovar = tovar;
            imagesFolderPath = folderPath;
            selectedQuantity = quantity;

            // Загрузка данных о товарах из файла
            if (File.Exists("tovarBasket.dat"))
            {
                using (FileStream fs = new FileStream("tovarBasket.dat", FileMode.Open))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    tovarBasket = (List<Tovar>)serializer.Deserialize(fs);
                }
            }

            // Отображение содержимого корзины
            DisplayBasketContents();
        }

        public Form10()
        {
            InitializeComponent();
            // Загрузка данных о товарах из файла
            if (File.Exists("tovarBasket.dat"))
            {
                using (FileStream fs = new FileStream("tovarBasket.dat", FileMode.Open))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    tovarBasket = (List<Tovar>)serializer.Deserialize(fs);
                }
            }
            // Отображение содержимого корзины
            DisplayBasketContents();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }


        private void Form10_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Сохранение данных о товарах в файл
            using (FileStream fs = new FileStream("tovarBasket.dat", FileMode.OpenOrCreate))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(fs, tovarBasket);
            }
        } */


}
