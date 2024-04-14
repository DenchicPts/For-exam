using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Apgerbs
{

    public partial class Form1 : Form
    {
        private static string dbCommand = "";
        private static BindingSource bindingSrc;
        private static string conString = "Data Source=apgerbs.db;Version=3;";

        private static SQLiteConnection connection = new SQLiteConnection(conString);

        private static string sql;

        public Form1()
        {
            InitializeComponent();
            using (SQLiteConnection connection = new SQLiteConnection())
            {

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            openConnection();
            CheckButton.Enabled = false;
            LoadBrands();
        }

        private void LoadBrands()
        {
            string query = "SELECT DISTINCT BrandName FROM Brand";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                BrandName.Items.Add(reader["BrandName"].ToString());
            }

            reader.Close();
        }

        private void openConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
                MessageBox.Show("The connection is: " + connection.State.ToString());
            }
        }


        private void closeConnection()
        {
            if (connection.State == ConnectionState.Closed) // need to change to Open
            {
                connection.Close();
                MessageBox.Show("The connection is: " + connection.State.ToString());
            }
        }

        private void BrandName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBrand = BrandName.SelectedItem.ToString();

            // Разблокировать выбор типа одежды
            BrandName.Enabled = true;

            ClothType.Text = string.Empty;
            // Загрузить типы одежды для выбранного бренда
            LoadClothTypes(selectedBrand);
        }
        private void LoadClothTypes(string brand)
        {
            // Запрос для получения всех уникальных типов одежды на основе выбранного бренда из таблицы Brand
            string query = @"SELECT DISTINCT ClothType.Type 
                     FROM Brand 
                     JOIN ClothType ON Brand.id_clothType = ClothType.id 
                     WHERE Brand.BrandName = @brand";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@brand", brand);

            // Очищаем ComboBox перед добавлением новых значений
            ClothType.Items.Clear();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string type = reader["Type"].ToString();
                    // Проверяем, был ли уже добавлен этот тип одежды в ComboBox
                    if (!ClothType.Items.Contains(type))
                    {
                        ClothType.Items.Add(type); // Добавляем только новые значения Type в ComboBox
                    }
                }
            }
        }

        private void ClothType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckButton.Enabled = true;
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            // Проверяем, был ли выбран тип одежды
            if (ClothType.SelectedItem == null)
            {
                // Пользователь не выбрал тип одежды, показываем сообщение об ошибке
                MessageBox.Show("Пожалуйста, выберите тип одежды перед проверкой.");
                return;
            }

            // Проверяем, был ли выбран пол
            if (!manRadioBut.Checked && !femaleRadioBut.Checked)
            {
                // Пользователь не выбрал пол, показываем сообщение об ошибке
                MessageBox.Show("Пожалуйста, выберите пол перед проверкой.");
                return;
            }

            // Получаем выбранный пол
            string sex = manRadioBut.Checked ? "Male" : "Female";

            // Получаем выбранный тип одежды
            string clothType = ClothType.SelectedItem.ToString();

            string brand = BrandName.SelectedItem.ToString();
            // Выполняем запрос к базе данных для получения id_proportions
            string path = "Cache\\" + GetProportionsPath(brand, clothType, sex);

            // Проверяем, было ли найдено id_proportions
            if (path != null)
            {
                // Создаем новое окно Image и передаем в него путь к изображению
                ImageWindow imageWindow = new ImageWindow(path, brand);
                imageWindow.Show();
            }
            else
            {
                // Выводим сообщение об ошибке, если не удалось найти id_proportions
                MessageBox.Show("Не удалось найти подходящую фотографию.");
            }
        }
        private string GetProportionsPath(string brand, string clothType, string sex)
        {
            string imagePath = null;
            string query = "SELECT Proportions.Path FROM Proportions " +
                           "INNER JOIN ClothType ON Proportions.id = ClothType.id_proportions " +
                           "INNER JOIN Brand ON ClothType.id = Brand.id_clothType " +
                           "WHERE Brand.BrandName = @brand AND ClothType.Type = @clothType AND ClothType.Sex = @sex";

            using (SQLiteConnection connection = new SQLiteConnection(conString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@brand", brand);
                    command.Parameters.AddWithValue("@clothType", clothType);
                    command.Parameters.AddWithValue("@sex", sex);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        imagePath = result.ToString();
                    }
                }
            }

            return imagePath;
        }
    }
}
