using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace треееш
{
    public partial class Form9 : Form
    {
        
        List<Tovar> tovarCollection = Form6.tovarCollection;
        PictureBox[] pictureBoxes;
        Label[] labels;
        int[] artic = new int[6];
        int currentIndex = 0;
        private string imagesFolderPath = @"C:\Users\user\Desktop\Prakta_Po_Prikladnomu-49-55\PraktaPoPrickladnomu\треееш\треееш\bin\Photo";
        Form10 form10;

        public Form9()
        {
            InitializeComponent();



            pictureBoxes = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6 };
            labels = new Label[] { label1, label2, label3, label4, label5, label6 };

            // Загрузка данных о товарах из файла
            if (File.Exists("tovar.dat"))
            {
                using (FileStream fs = new FileStream("tovar.dat", FileMode.OpenOrCreate))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    tovarCollection = (List<Tovar>)serializer.Deserialize(fs);
                }
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            int direction = 1; // Направление прокрутки: 1 - влево, -1 - вправо
            currentIndex = (currentIndex + direction + tovarCollection.Count) % tovarCollection.Count;
            UpdateCarousel();
            
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            LoadCarousel();
            timer1.Enabled = true;

            textBox2.Text = ""; // Очистить textbox2 при загрузке формы
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            trackBar1.Scroll += trackBar1_Scroll; // Добавление обработчика события Scroll к trackBar1
        }


        public void LoadCarousel()
        {
            string defaultImageName = "pumba.jpg";

            for (int i = 0; i < pictureBoxes.Length; i++)
            {
                if (i < tovarCollection.Count)
                {
                    Tovar tovar = tovarCollection[i];

                    Image imageName = tovar.ImageFileName;
                    
                    if (imageName != null)
                    {
                        pictureBoxes[i].Image = imageName;
                        pictureBoxes[i].SizeMode = PictureBoxSizeMode.Zoom;

                        labels[i].Text = tovar.Name;
                        artic[i] = tovar.Article;

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

                        labels[i].Text = tovar.Name;
                        artic[i] = tovar.Article;
                    }
                }
                else
                {
                    pictureBoxes[i].Image = null;
                    labels[i].Text = "";
                    artic[i] = 0;
                }
            }
        }

        private void UpdateCarousel()
        {
            
            for (int i = 0; i < pictureBoxes.Length; i++)
            {
                int index = (currentIndex - i + tovarCollection.Count) % tovarCollection.Count;

                //int index = (currentIndex + i) % tovarCollection.Count;
                Tovar tovar = tovarCollection[index];
                
                if (tovar.ImageFileName != null)
                {
                    pictureBoxes[i].Image = tovar.ImageFileName;
                    pictureBoxes[i].SizeMode = PictureBoxSizeMode.Zoom;

                    labels[i].Text = tovar.Name;
                    artic[i] = tovar.Article;

                    pictureBoxes[i].Click += selectedPictureBox_Click;
                }
                else
                {
                    MessageBox.Show("Файл изображения не найден: " + tovar.ImageFileName);
                }
            }
        }

        private Tovar selectedTovar; // Переменная для хранения выбранного товара

        public int selectedIndex;

        private void selectedPictureBox_Click(object sender, EventArgs e)
        {

            // Получите индекс выбранной картинки из карусели
            selectedIndex = Array.IndexOf(pictureBoxes, sender as PictureBox);

            // Проверьте, что индекс находится в пределах допустимого диапазона
            if (selectedIndex >= 0 && selectedIndex < tovarCollection.Count)
            {
                // Получите данные выбранного товара
                selectedTovar = tovarCollection[selectedIndex];


                // Отобразите выбранное изображение в элементе selectedPictureBox

                if (selectedTovar.ImageFileName != null)
                {
                    selectedPictureBox.Image = selectedTovar.ImageFileName;
                    selectedPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("Файл выбранного изображения не найден: " + selectedTovar.ImageFileName);
                }

                // Отобразите название выбранного товара в элементе selectedLabel
                selectedLabel.Text = selectedTovar.Name;
                textBox1.Text = selectedTovar.Price.ToString(); // Отображение цены выбранного товара в textbox1
                textBox2.Text = selectedTovar.Quantity.ToString(); // Отображение количества выбранного товара в textbox2
            }
        }

        private void selectedLabel_Click(object sender, EventArgs e){}

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > decimal.Parse(textBox2.Text))
            {
                MessageBox.Show("Недостаточное количество товара на складе!");
                numericUpDown1.Value = decimal.Parse(textBox2.Text);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e){}

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            this.Close();
            form5.Show();
        }
        private int timerInterval = 1000;
       

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
            
            form10 = new Form10();
            this.Hide();
            form10.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int selectedIndex = currentIndex;

            if (selectedIndex >= 0 && selectedIndex < tovarCollection.Count)
            {
                Tovar selectedTovar = tovarCollection[selectedIndex];
                decimal selectedQuantity = numericUpDown1.Value; // Получите выбранное значение количества

                // Создайте экземпляр формы 10 и передайте выбранный товар, путь к изображениям и выбранное количество в качестве параметров
                form10 = new Form10(selectedTovar, imagesFolderPath, selectedQuantity);
                MessageBox.Show("Товар добавлен в корзину");
                this.Hide();
                form10.Show();
            }
            
        }
    }
}