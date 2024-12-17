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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bd2
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            // Замените эти параметры на соответствующие значения вашей базы данных
            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
            string query = "SELECT * FROM [Product]";
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
            // Получаем данные из текстовых полей
            string article = textBox1.Text;
            string name = textBox2.Text;
            string material = textBox3.Text;
            string price = textBox4.Text;

            // Проверяем, чтобы текстовые поля не были пустыми
            if (string.IsNullOrWhiteSpace(article) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(material) || string.IsNullOrWhiteSpace(price))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            // Подключаемся к базе данных и добавляем запись
            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
            string insertQuery = "INSERT INTO [Product] (article, name, material, price) VALUES (@article, @name, @material, @price)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@article", article);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@material", material);
                    command.Parameters.AddWithValue("@price", price);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно добавлены!");

                        // Обновляем данные в DataGridView
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0]; // <--- ОБЪЯВЛЕНИЕ selectedRow ЗДЕСЬ

                int articleID;
                if (int.TryParse(selectedRow.Cells["article"].Value?.ToString(), out articleID))
                {
                    string article = textBox1.Text;
                    string name = textBox2.Text;
                    string material = textBox3.Text;
                    string price = textBox4.Text;
                  

                    string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False"; // Замените на вашу строку подключения
                    string query = "UPDATE Product SET  name = @name, material = @material, price = @price WHERE article = @article";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@article", article);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@material", material);
                        command.Parameters.AddWithValue("@price", price);
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
                    MessageBox.Show("Ошибка: Невозможно преобразовать article в число.");
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для обновления!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления!");
                return;
            }

            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
            string query = "DELETE FROM Product WHERE article = @article"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    
                    string clientId = dataGridView1.SelectedRows[0].Cells["article"].Value.ToString();
                    command.Parameters.AddWithValue("@article", clientId);

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
    }
}
