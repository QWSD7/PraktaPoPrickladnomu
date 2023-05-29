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

namespace треееш
{
    public partial class Form10 : Form
    {
        private Form9.Tovar selectedTovar;
        private string imagesFolderPath;
        private decimal selectedQuantity; // Добавленное поле для хранения выбранного количества

        public Form10()
        {
            InitializeComponent();
        }

        // Обновленный конструктор с добавленным параметром для передачи количества
        public Form10(Form9.Tovar tovar, string folderPath, decimal quantity)
        {
            InitializeComponent();
            selectedTovar = tovar;
            imagesFolderPath = folderPath;
            selectedQuantity = quantity;
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            if (selectedTovar != null)
            {
                listBox1.Items.Add("Наименование: " + selectedTovar.Name.ToString());
                listBox1.Items.Add("Цена: " + selectedTovar.Price.ToString());
                listBox1.Items.Add("Количество: " + selectedQuantity.ToString()); // Используйте выбранное значение количества

                string selectedImagePath = Path.Combine(imagesFolderPath, selectedTovar.Foto);
                if (File.Exists(selectedImagePath))
                {
                    pictureBox1.Image = Image.FromFile(selectedImagePath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("Файл выбранного изображения не найден: " + selectedImagePath);
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
