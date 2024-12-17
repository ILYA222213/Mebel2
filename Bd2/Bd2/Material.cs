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
    public partial class Material : Form
    {
        public Material()
        {
            InitializeComponent();
        }

        private void Material_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            // Замените эти параметры на соответствующие значения вашей базы данных
            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
            string query = "SELECT * FROM [Material_NEW]";

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

        private void button1_Click(object sender, EventArgs e)
        {
            // Замените эти параметры на соответствующие значения вашей базы данных
            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
            string name = textBox1.Text;
            string country = textBox2.Text;
            string price = textBox3.Text;


            string insertQuery = "INSERT INTO Material_New (name, country, price) VALUES (@name, @country, @price)";
            // Проверяем, чтобы текстовые поля не были пустыми
            if (string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(price))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@country", country);
                    command.Parameters.AddWithValue("@price", price);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно добавлены!");
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

                int materialID;
                if (int.TryParse(selectedRow.Cells["material_id"].Value?.ToString(), out materialID))
                {
                    string material_id = textBox4.Text;
                    string name = textBox1.Text;
                    string country = textBox2.Text;
                    string price = textBox3.Text;
                    

                    string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False"; // Замените на вашу строку подключения
                    string query = "UPDATE Material_New SET name = @name, country = @country, price = @price WHERE material_id = @material_id";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@material_id", material_id);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@country", country);
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
                    MessageBox.Show("Ошибка: Невозможно преобразовать client_id в число.");
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
            string query = "DELETE FROM Material_New WHERE material_id = @material_id"; // Предполагается, что client_id - первичный ключ

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Получаем ID из выбранной строки. Важно: убедитесь, что client_id - это имя столбца с ID!
                    string clientId = dataGridView1.SelectedRows[0].Cells["material_id"].Value.ToString();
                    command.Parameters.AddWithValue("@material_id", clientId);

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
