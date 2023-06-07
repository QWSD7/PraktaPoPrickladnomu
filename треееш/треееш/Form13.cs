using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;
using static треееш.Form3;

namespace треееш
{
    public partial class Form13 : Form
    {
        private List<Tovar> cartItems;

        //Form11 form11 = new Form11();

        public List<Tovar> CartItems
        {
            get { return cartItems; }
        }

        int n = 0;
        string NamePerson;

        public Form13(List<Tovar> cartItems)
        {
            InitializeComponent();

            this.cartItems = new List<Tovar>(cartItems);

            // Заполнение listBox содержимым корзины
            foreach (Tovar tovar in cartItems)
            {
                listBox1.Items.Add(GetCartItemInfo(tovar));
            }

            // Вычисление и отображение общей стоимости добавленных товаров
            decimal totalCost = CalculateTotalCost();
            label1.Text = totalCost.ToString();


            // Загрузка данных о товарах из файла
            if (File.Exists("tovar.dat"))
            {
                using (FileStream fs = new FileStream("tovar.dat", FileMode.OpenOrCreate))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    Form6.tovarCollection = (List<Tovar>)serializer.Deserialize(fs);
                }
            }

            // Загрузка данных о товарах из файла
            if (File.Exists("operation.dat"))
            {
                using (FileStream fs = new FileStream("operation.dat", FileMode.OpenOrCreate))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    Form7.operationCollection = (List<Operation>)serializer.Deserialize(fs);
                }
            }

            foreach (User uss in Form3.myUs)
            {
                if (Form1.Ind == n)
                {
                    NamePerson = Form1.decryptedEmail = Form1.DecryptE(uss.S_L, uss.S_EK);

                }
                n++;
            }


        }

        private string GetCartItemInfo(Tovar tovar)
        {
            return $"{tovar.Article} - {tovar.Name} - {tovar.Price} - {tovar.Quantity}";
        }

        private decimal CalculateTotalCost()
        {
            decimal totalCost = 0;

            foreach (Tovar tovar in cartItems)
            {
                totalCost += tovar.Price * tovar.Quantity;
            }

            return totalCost;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                // Удаление выбранного товара из корзины
                //cartItems.RemoveAt(listBox1.SelectedIndex);
                CartItems.RemoveAt(listBox1.SelectedIndex);



                //form11.cartItems.RemoveAt(listBox1.SelectedIndex);

                listBox1.Items.RemoveAt(listBox1.SelectedIndex);

                // Обновление общей стоимости
                decimal totalCost = CalculateTotalCost();
                label1.Text = totalCost.ToString();
            }
        }

        private void updateQuantityButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                // Открываем форму для изменения количества товара
                Tovar selectedTovar = cartItems[listBox1.SelectedIndex];
                using (Form14 updateQuantityForm = new Form14(selectedTovar))
                {
                    if (updateQuantityForm.ShowDialog() == DialogResult.OK)
                    {
                        // Обновляем количество выбранного товара
                        selectedTovar.Quantity = updateQuantityForm.Quantity;

                        // Обновляем отображение информации о товаре
                        listBox1.Items[listBox1.SelectedIndex] = GetCartItemInfo(selectedTovar);

                        // Обновляем общую стоимость
                        decimal totalCost = CalculateTotalCost();
                        label1.Text = totalCost.ToString();
                    }
                }
            }
        }

        private void buyButton_Click(object sender, EventArgs e)
        {




            // Сохраняем покупку в коллекции операций и изменяем запись товара
            foreach (Tovar tovar in cartItems)
            {
                Operation operation = new Operation();
                operation.Article = tovar.Article.ToString();
                operation.Date = DateTime.Now;
                operation.OperationPrice = tovar.Price;
                operation.Quantity = tovar.Quantity;
                operation.OperationCode = 2;
                operation.AdditionalInfo = NamePerson + " - Логин пользователя"; // Можете задать дополнительную информацию




                Form7.operationCollection.Add(operation);

                // Изменяем запись товара в коллекции tovarCollection
                foreach (Tovar item in Form6.tovarCollection)
                {
                    if (item.Article == tovar.Article)
                    {
                        item.Quantity -= tovar.Quantity;
                        break;
                    }
                }
            }

            // Сохраняем данные в файлы
            using (FileStream fs = new FileStream("operation.dat", FileMode.OpenOrCreate))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(fs, Form7.operationCollection);
            }

            // Сохранение данных о товарах в файл
            using (FileStream fs = new FileStream("tovar.dat", FileMode.OpenOrCreate))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(fs, Form6.tovarCollection);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Hide();
            
        }

    }
}
