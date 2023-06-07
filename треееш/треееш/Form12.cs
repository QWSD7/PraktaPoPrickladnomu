using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace треееш
{
    public partial class Form12 : Form
    {
        private Tovar selectedTovar;

        public Form12(Tovar selectedTovar)
        {
            InitializeComponent();

            this.selectedTovar = selectedTovar;

            // Отображаем информацию о выбранном товаре
            label1.Text = selectedTovar.Article.ToString();
            label2.Text = selectedTovar.Name;
            label3.Text = selectedTovar.Price.ToString();
            label8.Text = selectedTovar.Quantity.ToString();

            // Устанавливаем значение по умолчанию для поля количества
            numericUpDown1.Value = 1;
        }

        private void addToCartButton_Click(object sender, EventArgs e)
        {
            int quantity = (int)numericUpDown1.Value;

            // Добавляем выбранный товар в корзину с указанным количеством
            selectedTovar.Quantity = quantity;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > decimal.Parse(label8.Text))
            {
                MessageBox.Show("Недостаточное количество товара на складе!");
                numericUpDown1.Value = decimal.Parse(label8.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();
            
        }
    }
}
