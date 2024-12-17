using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml.Linq;

namespace Bd2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            // Замените эти параметры на соответствующие значения вашей базы данных
            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
            string query = "SELECT * FROM [Client]";
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Получение значений из TextBox-ов
            string id = textBox6.Text;
            string fio = textBox1.Text;
            string phoneNumber = textBox2.Text;
            string address = textBox3.Text;
            string email = textBox4.Text;
            string orderNumber = textBox5.Text;

            // Замените эти параметры на соответствующие значения вашей базы данных
            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
            string query = "INSERT INTO Client (client_id, client_fio, phone_number, adress, email, order_number) VALUES (@client_id, @client_fio, @phone_number, @adress, @email, @order_number)";
            // Проверяем, чтобы текстовые поля не были пустыми
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(phoneNumber) ||
                string.IsNullOrWhiteSpace(fio) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(email) ||string.IsNullOrWhiteSpace(orderNumber))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@client_id", id);
                command.Parameters.AddWithValue("@client_fio", fio);
                command.Parameters.AddWithValue("@phone_number", phoneNumber);
                command.Parameters.AddWithValue("@adress", address);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@order_number", orderNumber);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Данные успешно сохранены");
                    LoadData(); // Обновляем данные в DataGridView  

                    // Очищаем все TextBox после успешного сохранения
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Создаем экземпляр формы OrderForm
            Order orderForm = new Order();

            // Отображаем форму (можно использовать ShowDialog для модального окна)
            orderForm.Show(); // или orderForm.ShowDialog();
        }

        private void материалыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Создаем экземпляр формы OrderForm
            Material materialForm = new Material();

            // Отображаем форму (можно использовать ShowDialog для модального окна)
            materialForm.Show(); // или orderForm.ShowDialog();


        }

        private void продукцияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Создаем экземпляр формы Product
            Product productForm = new Product();

            // Открываем форму
            productForm.Show(); // Используйте ShowDialog(), если хотите, чтобы форма была модальной
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0]; // <--- ОБЪЯВЛЕНИЕ selectedRow ЗДЕСЬ

                int clientId;
                if (int.TryParse(selectedRow.Cells["client_id"].Value?.ToString(), out clientId))
                {
                    string fio = textBox1.Text;
                    string phoneNumber = textBox2.Text;
                    string address = textBox3.Text;
                    string email = textBox4.Text;
                    string orderNumber = textBox5.Text;

                    string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False"; // Замените на вашу строку подключения
                    string query = "UPDATE Client SET client_fio = @client_fio, phone_number = @phone_number, adress = @adress, email = @email, order_number = @order_number WHERE client_id = @client_id";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@client_id", clientId);
                        command.Parameters.AddWithValue("@client_fio", fio);
                        command.Parameters.AddWithValue("@phone_number", phoneNumber);
                        command.Parameters.AddWithValue("@adress", address);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@order_number", orderNumber);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("Данные успешно обновлены");
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Невозможно преобразовать client_id в число.");
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для обновления!");
            }
        }

        private void поставщикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Создаем экземпляр формы Product
            Supplier supplierForm = new Supplier();

            // Открываем форму
            supplierForm.Show(); // Используйте ShowDialog(), если хотите, чтобы форма была модальной
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления!");
                return;
            }

            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
            string query = "DELETE FROM Client WHERE client_id = @client_id"; // Предполагается, что client_id - первичный ключ

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Получаем ID из выбранной строки. Важно: убедитесь, что client_id - это имя столбца с ID!
                    string clientId = dataGridView1.SelectedRows[0].Cells["client_id"].Value.ToString();
                    command.Parameters.AddWithValue("@client_id", clientId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Строка успешно удалена!");
                            LoadData(); // Обновляем DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при удалении строки.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
    }

