using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace треееш
{
    public partial class Form11 : Form
    {
        private List<Tovar> tovarCollection;
        public List<Tovar> cartItems = new List<Tovar>();

        private int carouselOffset;
        private int carouselItemWidth;

        private const int TimerInterval = 10;

        private int animationStep = 0;
        private int animationDuration = 20; // Длительность анимации в миллисекундах

        public Form11()
        {
            InitializeComponent();

            flowLayoutPanel1.Height = 200;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = false;
            Controls.Add(flowLayoutPanel1);

            // Загрузка данных о товарах и заполнение карусели
            LoadTovarData();

            timer1.Interval = TimerInterval;
            timer1.Tick += CarouselTimer_Tick;
            carouselOffset = 0;
            carouselItemWidth = 150;
            timer1.Start();
            
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 10;
            trackBar1.Value = 2;
            trackBar1.ValueChanged += TrackBarSpeed_ValueChanged;

        }

        private void TrackBarSpeed_ValueChanged(object sender, EventArgs e)
        {
            // Изменяем интервал таймера на основе значения скорости из TrackBar
            int speed = trackBar1.Value;
            timer1.Interval = TimerInterval / speed;
        }

        private void CarouselTimer_Tick(object sender, EventArgs e)
        {
            // Проверяем, если есть элементы в карусели
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                // Получаем горизонтальную прокрутку панели
                int scrollPosition = flowLayoutPanel1.HorizontalScroll.Value;
                int maxScrollPosition = flowLayoutPanel1.HorizontalScroll.Maximum;
                int scrollStep = trackBar1.Value; // Шаг прокрутки (значение получено с помощью trackBar)

                // Проверяем, если прокрутка достигла конца
                if (scrollPosition +flowLayoutPanel1.Width >= maxScrollPosition)
                {
                    if (flowLayoutPanel1.Controls.Count > 0)
                    {
                        // Получаем первый элемент
                        Control firstItem = flowLayoutPanel1.Controls[0];

                        // Удаляем первый элемент из начала списка
                        flowLayoutPanel1.Controls.RemoveAt(0);

                        // Добавляем первый элемент в конец списка
                        flowLayoutPanel1.Controls.Add(firstItem);
                    }

                    // Устанавливаем прокрутку в начало
                    flowLayoutPanel1.HorizontalScroll.Value = 0;
                }
                else
                {

                    // Плавно изменяем прокрутку на каждом шаге таймера
                    if (animationStep < animationDuration)
                    {
                        // Вычисляем новое значение прокрутки с учетом анимации
                        int newScrollPosition = scrollPosition + (scrollStep * animationStep / animationDuration);

                        // Устанавливаем новое значение прокрутки
                        flowLayoutPanel1.HorizontalScroll.Value = newScrollPosition;

                        // Увеличиваем счетчик шагов анимации
                        animationStep++;
                    }
                    else
                    {
                        // Прокручиваем панель на шаг прокрутки
                        flowLayoutPanel1.HorizontalScroll.Value += scrollStep;
                    }
                }
            }
        }
        

        private void LoadTovarData()
        {
            if (File.Exists("tovar.dat"))
            {
                using (FileStream fs = new FileStream("tovar.dat", FileMode.Open))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    tovarCollection = (List<Tovar>)serializer.Deserialize(fs);

                    // Добавляем копии первого элемента после последнего элемента
                    int tovarCount = tovarCollection.Count;
                    for (int i = 0; i < tovarCount; i++)
                    {
                        Tovar tovar = tovarCollection[i];

                        if (tovar.ImageFileName != null)
                        {
                            Image image = tovar.ImageFileName;
                            AddImageToCarousel(image, tovar);
                        }
                    }
                }
            }
            else
            {
                tovarCollection = new List<Tovar>();
            }
        }

        

        private void AddImageToCarousel(Image image, Tovar tovar)
        {
            // Создание PictureBox для отображения изображения
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = image;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Width = 150;
            pictureBox.Height = 150;

            // Установка значения Tag
            pictureBox.Tag = tovar;

            // Обработчик события клика на изображении
            pictureBox.Click += PictureBox_Click;

            // Добавление PictureBox в панель карусели
            flowLayoutPanel1.Controls.Add(pictureBox);
        }



        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            Tovar selectedTovar = (Tovar)pictureBox.Tag;

            // Открываем форму для добавления товара в корзину
            using (Form12 addToCartForm = new Form12(selectedTovar))
            {
                if (addToCartForm.ShowDialog() == DialogResult.OK)
                {
                    // Добавляем товар в корзину
                    cartItems.Add(selectedTovar);
                }
            }
        }


        private void viewCartButton_Click(object sender, EventArgs e)
        {
            // Открываем форму корзины для просмотра и редактирования содержимого
            using (Form13 cartForm = new Form13(cartItems))
            {
                if (cartForm.ShowDialog() == DialogResult.OK)
                {
                    // Обновляем список товаров в корзине
                    cartItems = cartForm.CartItems;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            this.Hide();
            form5.Show();
        }
    }
}
