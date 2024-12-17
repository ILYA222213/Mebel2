using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bd2
{
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
        }

        private void Order_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            // Замените эти параметры на соответствующие значения вашей базы данных
            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
            string query = "SELECT * FROM [Order]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; // Здесь dataGridView1 — имя вашего элемента DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Получаем данные из элементов управления
            string orderNumber = textBox1.Text;
            DateTime orderDate = dateTimePicker1.Value;
            DateTime deliveryDate = dateTimePicker2.Value;
            string productArticle = textBox4.Text;

            // Замените эти параметры на соответствующие значения вашей базы данных
            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";

            // SQL-запрос для вставки данных
            string query = "INSERT INTO [Order] (order_number, order_date, delivery_date, product_article) VALUES (@OrderNumber, @OrderDate, @DeliveryDate, @ProductArticle)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Добавляем параметры к команде
                    command.Parameters.AddWithValue("@OrderNumber", orderNumber);
                    command.Parameters.AddWithValue("@OrderDate", orderDate);
                    command.Parameters.AddWithValue("@DeliveryDate", deliveryDate);
                    command.Parameters.AddWithValue("@ProductArticle", productArticle);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery(); // Выполняем запрос

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные успешно добавлены!");
                            LoadData(); // Обновляем данные в DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при добавлении данных.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для редактирования!");
                return;
            }

            string oldOrderNumber = dataGridView1.SelectedRows[0].Cells["order_number"].Value.ToString();
            string oldOrderDateStr = dataGridView1.SelectedRows[0].Cells["order_date"].Value.ToString();

            //Преобразование даты из строки в DateTime. Важно: формат должен соответствовать формату в БД!
            DateTime oldOrderDate;
            if (!DateTime.TryParse(oldOrderDateStr, out oldOrderDate))
            {
                MessageBox.Show("Ошибка: Неверный формат даты!");
                return;
            }

            string orderNumber = textBox1.Text;
            DateTime orderDate = dateTimePicker1.Value;
            DateTime deliveryDate = dateTimePicker2.Value;
            string productArticle = textBox4.Text;

            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
            string query = @"UPDATE [Order] 
           SET order_number = @OrderNumber, order_date = @OrderDate, delivery_date = @DeliveryDate, product_article = @ProductArticle
           WHERE order_number = @OldOrderNumber AND order_date = @OldOrderDate";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderNumber", orderNumber);
                    command.Parameters.AddWithValue("@OrderDate", orderDate);
                    command.Parameters.AddWithValue("@DeliveryDate", deliveryDate);
                    command.Parameters.AddWithValue("@ProductArticle", productArticle);
                    command.Parameters.AddWithValue("@OldOrderNumber", oldOrderNumber);
                    command.Parameters.AddWithValue("@OldOrderDate", oldOrderDate);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные успешно обновлены!");
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении данных. Возможно, нет записи с такими данными.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления!");
                return;
            }

            string orderNumber = dataGridView1.SelectedRows[0].Cells["order_number"].Value.ToString(); //Замените "order_number" на имя столбца с номером заказа в вашем DataGridView

            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
            string query = "DELETE FROM [Order] WHERE order_number = @OrderNumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderNumber", orderNumber);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные успешно удалены!");
                            LoadData(); // Обновляем DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при удалении данных. Возможно, запись с таким номером заказа не существует.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
        }
    }

    }

