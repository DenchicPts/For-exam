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
                string encryptedBrandName = Decrypt(reader["BrandName"].ToString(), 2);
                BrandName.Items.Add(encryptedBrandName);
            }

            reader.Close();
        }

        private  string Encrypt(string input, int shift)
        {
            string encrypted = "";

            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    char shiftedChar = (char)(c + shift);
                    if (!char.IsLetter(shiftedChar))
                    {
                        shiftedChar = (char)(c - (26 - shift)); // Circular shift to ensure the range A-Z or a-z
                    }
                    encrypted += shiftedChar;
                }
                else
                {
                    encrypted += c; // If the character is not a letter, add it as is
                }
            }

            return encrypted;
        }

        // Method for decrypting a string using bit shift
        private string Decrypt(string input, int shift)
        {
            return Encrypt(input, -shift); // Decryption is equivalent to encryption with a negative shift
        }

        private void openConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }


        private void closeConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        private void BrandName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBrand = Encrypt(BrandName.SelectedItem.ToString(),2);

            // Unlock clothing type selection
            BrandName.Enabled = true;

            ClothType.Text = string.Empty;
            // Load clothing types for the selected brand
            LoadClothTypes(selectedBrand);
        }
        private void LoadClothTypes(string brand)
        {
            // Query to retrieve the path of the proportions image based on the selected brand, clothing type, and gender
            string query = @"SELECT DISTINCT ClothType.Type 
                     FROM Brand 
                     JOIN ClothType ON Brand.id_clothType = ClothType.id 
                     WHERE Brand.BrandName = @brand";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@brand", brand);

            // Clear the ComboBox before adding new values
            ClothType.Items.Clear();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ClothType.Items.Add(reader["Type"].ToString());
                }
            }
        }

        private void ClothType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckButton.Enabled = true;
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            if (ClothType.SelectedItem == null)
            {
                // User hasn't selected a clothing type, show error message
                MessageBox.Show("Please select clothing type before checking.");
                return;
            }

            if (!manRadioBut.Checked && !femaleRadioBut.Checked)
            {
                // User hasn't selected a gender, show error message
                MessageBox.Show("Please select gender before checking.");
                return;
            }

            // Get the selected gender
            string sex = manRadioBut.Checked ? "Male" : "Female";

            // Get the selected clothing type
            string clothType = ClothType.SelectedItem.ToString();

            string brand = Encrypt(BrandName.SelectedItem.ToString(),2);
            // Execute a database query to retrieve id_proportions
            string path = "Cache\\" + GetProportionsPath(brand, clothType, sex);

            // Check if id_proportions was found
            if (path != null)
            {
                // Create a new Image window and pass the image path to it
                ImageWindow imageWindow = new ImageWindow(path, Decrypt(brand,2));
                imageWindow.Show();
            }
            else
            {
                // Display an error message if id_proportions was not found
                MessageBox.Show("Could not find a suitable photo.");
            }
        }
        private string GetProportionsPath(string brand, string clothType, string sex)
        {
            string imagePath = null;
            // Запрос для получения пути к изображению пропорций на основе выбранного бренда,
            // типа одежды и пола из таблиц Proportions, ClothType и Brand
            string query = "SELECT Proportions.Path FROM Proportions " +
                           "INNER JOIN ClothType ON Proportions.id = ClothType.id_proportions " +
                           "INNER JOIN Brand ON ClothType.id = Brand.id_clothType " +
                           "WHERE Brand.BrandName = @brand AND ClothType.Type = @clothType AND ClothType.Sex = @sex";

            using (SQLiteConnection connection = new SQLiteConnection(conString))
            {
                connection.Open();

                // Create a command to execute the database query
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@brand", brand);
                    command.Parameters.AddWithValue("@clothType", clothType);
                    command.Parameters.AddWithValue("@sex", sex);

                    // Execute the query and retrieve the result
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
