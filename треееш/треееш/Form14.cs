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

namespace треееш
{
    public partial class Form14 : Form
    {
        public int Quantity
        {
            get { return (int)numericUpDown1.Value; }
        }

        public Form14(Tovar selectedTovar)
        {
            InitializeComponent();

            // Отображаем информацию о выбранном товаре
            label1.Text = selectedTovar.Article.ToString();
            label2.Text = selectedTovar.Name;
            label3.Text = selectedTovar.Price.ToString();

            // Устанавливаем текущее значение количества
            numericUpDown1.Value = selectedTovar.Quantity;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
