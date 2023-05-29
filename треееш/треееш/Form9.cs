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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace треееш
{
    public partial class Form9 : Form
    {
        static List<Tovar> mytov = new List<Tovar>();
        PictureBox[] pictureBoxes;
        Label[] labels;
        string[] artic = new string[6];
        int currentIndex = 0;
        private string imagesFolderPath = @"C:\Users\Anastasia\Desktop\Работа";

        public Form9()
        {
            InitializeComponent();
            pictureBoxes = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6 };
            labels = new Label[] { label1, label2, label3, label4, label5, label6 };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int direction = 1; // Направление прокрутки: 1 - влево, -1 - вправо
            currentIndex = (currentIndex + direction + mytov.Count) % mytov.Count;
            UpdateCarousel();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            mytov.Add(new Tovar { Art = "артикул2", Foto = "котик.jpg", Name = "Игрушка котенка", Price = 100, Quantity = 10 });
            mytov.Add(new Tovar { Art = "артикул2", Foto = "мишка.jpg", Name = "Игрушка медведь", Price = 200, Quantity = 5 });
            mytov.Add(new Tovar { Art = "артикул2", Foto = "ферби.jpg", Name = "Игрушка ферби", Price = 300, Quantity = 8 });
            mytov.Add(new Tovar { Art = "артикул2", Foto = "пони.jpg", Name = "Игрушка поняшка", Price = 400, Quantity = 12 });
            mytov.Add(new Tovar { Art = "артикул2", Foto = "панда.jpg", Name = "Игрушка панда", Price = 500, Quantity = 3 });
            LoadCarousel();
            timer1.Enabled = true;

            textBox2.Text = ""; // Очистить textbox2 при загрузке формы
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            trackBar1.Scroll += trackBar1_Scroll; // Добавление обработчика события Scroll к trackBar1
        }

        public class Tovar
        {
            public string Art { get; set; }
            public string Foto { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }

        }

        private void LoadCarousel()
        {
            string defaultImageName = "сашка.jpg";

            for (int i = 0; i < pictureBoxes.Length; i++)
            {
                if (i < mytov.Count)
                {
                    string imageName = mytov[i].Foto;
                    string imagePath = Path.Combine(imagesFolderPath, imageName);

                    if (File.Exists(imagePath))
                    {
                        pictureBoxes[i].Image = Image.FromFile(imagePath);
                        pictureBoxes[i].SizeMode = PictureBoxSizeMode.Zoom;

                        labels[i].Text = mytov[i].Name;
                        artic[i] = mytov[i].Art;

                        // Привязка обработчика события Click к каждому PictureBox'у
                        pictureBoxes[i].Click += selectedPictureBox_Click;
                    }
                    else
                    {
                        string defaultImagePath = Path.Combine(imagesFolderPath, defaultImageName);
                        if (File.Exists(defaultImagePath))
                        {
                            pictureBoxes[i].Image = Image.FromFile(defaultImagePath);
                            pictureBoxes[i].SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            MessageBox.Show("Файл изображения по умолчанию не найден: " + defaultImagePath);
                        }

                        labels[i].Text = mytov[i].Name;
                        artic[i] = mytov[i].Art;
                    }
                }
                else
                {
                    pictureBoxes[i].Image = null;
                    labels[i].Text = "";
                    artic[i] = "";
                }
            }
        }

        private void UpdateCarousel()
        {
            for (int i = 0; i < pictureBoxes.Length; i++)
            {
                int index = (currentIndex - i + mytov.Count) % mytov.Count;
                string imageName = mytov[index].Foto;
                string fullPath = Path.Combine(imagesFolderPath, imageName);

                if (File.Exists(fullPath))
                {
                    pictureBoxes[i].Image = Image.FromFile(fullPath);
                    pictureBoxes[i].SizeMode = PictureBoxSizeMode.Zoom;

                    labels[i].Text = mytov[index].Name;
                    artic[i] = mytov[index].Art;
                }
                else
                {
                    MessageBox.Show("Файл изображения не найден: " + fullPath);
                }
            }
        }

        private void selectedPictureBox_Click(object sender, EventArgs e)
        {
            // Получите индекс выбранной картинки из карусели
            int selectedIndex = Array.IndexOf(pictureBoxes, sender as PictureBox);

            // Проверьте, что индекс находится в пределах допустимого диапазона
            if (selectedIndex >= 0 && selectedIndex < mytov.Count)
            {
                // Получите данные выбранного товара
                Tovar selectedTovar = mytov[selectedIndex];

                // Отобразите выбранное изображение в элементе selectedPictureBox
                string selectedImagePath = Path.Combine(imagesFolderPath, selectedTovar.Foto);
                if (File.Exists(selectedImagePath))
                {
                    selectedPictureBox.Image = Image.FromFile(selectedImagePath);
                    selectedPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("Файл выбранного изображения не найден: " + selectedImagePath);
                }

                // Отобразите название выбранного товара в элементе selectedLabel
                selectedLabel.Text = selectedTovar.Name;
                textBox1.Text = selectedTovar.Price.ToString(); // Отображение цены выбранного товара в textbox1
                textBox2.Text = selectedTovar.Quantity.ToString(); // Отображение количества выбранного товара в textbox2
            }
        }

        private void selectedLabel_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > decimal.Parse(textBox2.Text))
            {
                MessageBox.Show("Недостаточное количество товара на складе!");
                numericUpDown1.Value = decimal.Parse(textBox2.Text);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            this.Close();
            form5.Show();
        }
        private int timerInterval = 1000;
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int speed = trackBar1.Value; // Получение значения скорости из трекбара

            if (speed > 0)
            {
                timer1.Interval = 1000 / speed;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form10 form10 = new Form10();
            this.Close();
            form10.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int selectedIndex = currentIndex;

            if (selectedIndex >= 0 && selectedIndex < mytov.Count)
            {
                Tovar selectedTovar = mytov[selectedIndex];
                decimal selectedQuantity = numericUpDown1.Value; // Получите выбранное значение количества

                // Создайте экземпляр формы 10 и передайте выбранный товар, путь к изображениям и выбранное количество в качестве параметров
                Form10 form10 = new Form10(selectedTovar, imagesFolderPath, selectedQuantity);
                form10.Show();
            }
        }
    }
}


