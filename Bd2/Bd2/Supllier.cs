using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bd2
{
    public partial class Supplier : Form
    {
        public Supplier()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            // Замените эти параметры на соответствующие значения вашей базы данных
            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
            string query = "SELECT * FROM [Supplier_New]";
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
        private void Supplier_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string supplier_name = textBox1.Text;
            string product_name = textBox2.Text;
            string material = textBox3.Text;
            string product_quantity = textBox4.Text;
            string purchase_cost = textBox5.Text;

            // Проверка на пустые поля
            if (string.IsNullOrEmpty(supplier_name) || string.IsNullOrEmpty(product_name) ||
                string.IsNullOrEmpty(material) || string.IsNullOrEmpty(product_quantity) ||
                string.IsNullOrEmpty(purchase_cost))
            {
                MessageBox.Show("Все поля должны быть заполнены!");
                return;
            }

            // Замените эти параметры на соответствующие значения вашей базы данных
            string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
            string query = "INSERT INTO Supplier_New (supplier_name, product_name, material, product_quantity, purchase_cost) " +
                           "VALUES (@supplier_name, @product_name, @material, @product_quantity, @purchase_cost)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@supplier_name", supplier_name);
                    command.Parameters.AddWithValue("@product_name", product_name);
                    command.Parameters.AddWithValue("@material", material);
                    command.Parameters.AddWithValue("@product_quantity", product_quantity);
                    command.Parameters.AddWithValue("@purchase_cost", purchase_cost);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно добавлены!");
                        LoadData(); // Обновляем DataGridView после добавления данных
                        ClearTextBoxes(); // Очищаем TextBox'ы после добавления
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла неизвестная ошибка: " + ex.Message);
                    }
                }
            }

        }
        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0]; // <--- ОБЪЯВЛЕНИЕ selectedRow ЗДЕСЬ

                int supplierID;
                if (int.TryParse(selectedRow.Cells["supplier_id"].Value?.ToString(), out supplierID))
                {
                    string supplier_name = textBox1.Text;
                    string product_name = textBox2.Text;
                    string material = textBox3.Text;
                    string product_quantity = textBox4.Text;
                    string purchase_cost = textBox5.Text;
                    string supplier_id = textBox6.Text;


                    string connectionString = "Data Source=DESKTOP-5N3TSVC;Initial Catalog=BD;Integrated Security=True;Encrypt=False"; // Замените на вашу строку подключения
                    string query = "UPDATE Supplier_New SET  supplier_name = @supplier_name, material = @material, product_name = @product_name, product_quantity = @product_quantity, purchase_cost = @purchase_cost" +
                        " WHERE supplier_id = @supplier_id";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@supplier_name", supplier_name);
                        command.Parameters.AddWithValue("@product_name", product_name);
                        command.Parameters.AddWithValue("@material", material);
                        command.Parameters.AddWithValue("@product_quantity", product_quantity);
                        command.Parameters.AddWithValue("@purchase_cost", purchase_cost);
                        command.Parameters.AddWithValue("@supplier_id", supplier_id);
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
            string query = "DELETE FROM Supplier_New WHERE supplier_id = @supplier_id"; 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    
                    string clientId = dataGridView1.SelectedRows[0].Cells["supplier_id"].Value.ToString();
                    command.Parameters.AddWithValue("@supplier_id", clientId);

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

