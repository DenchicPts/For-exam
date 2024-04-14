using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apgerbs
{
    public partial class ImageWindow : Form
    {
        public ImageWindow(string imagePath, string brand)
        {
            InitializeComponent();
            SetImage(imagePath);
            this.Text = brand; // Устанавливаем название формы равным значению переменной brand

            // Заблокируем возможность изменения размеров окна
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void SetImage(string imagePath)
        {
            try
            {
                // Загружаем изображение из файла по указанному пути
                Image image = Image.FromFile(imagePath);

                // Устанавливаем изображение в PictureBox
                pictureBox.Image = image;

                // Масштабируем изображение для заполнения всего PictureBox
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                // Получаем размеры изображения
                int imageWidth = image.Width;
                int imageHeight = image.Height;

                // Задаем предельные значения для размеров окна
                int maxWidth = 600;
                int maxHeight = 600;

                // Если размеры изображения больше предельных значений, уменьшаем их
                if (imageWidth > maxWidth || imageHeight > maxHeight)
                {
                    double widthRatio = (double)maxWidth / imageWidth;
                    double heightRatio = (double)maxHeight / imageHeight;
                    double ratio = Math.Min(widthRatio, heightRatio);

                    imageWidth = (int)(imageWidth * ratio);
                    imageHeight = (int)(imageHeight * ratio);
                }

                // Устанавливаем размеры PictureBox
                pictureBox.Size = new Size(imageWidth, imageHeight);

                // Устанавливаем PictureBox, чтобы он заполнял все доступное пространство формы
                pictureBox.Dock = DockStyle.Fill;

                // Устанавливаем размеры формы
                this.ClientSize = new Size(imageWidth, imageHeight);
            }
            catch (Exception ex)
            {
                // Обработка ошибок загрузки изображения
                MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
            }
        }
    }
}
