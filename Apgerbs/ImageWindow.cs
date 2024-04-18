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
            this.Text = brand;


            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void SetImage(string imagePath)
        {
            try
            {
                Image image = Image.FromFile(imagePath);

                pictureBox.Image = image;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;


                int imageWidth = image.Width;
                int imageHeight = image.Height;


                int maxWidth = 600;
                int maxHeight = 600;

                // If the image dimensions exceed the maximum values, reduce them
                if (imageWidth > maxWidth || imageHeight > maxHeight)
                {
                    double widthRatio = (double)maxWidth / imageWidth;
                    double heightRatio = (double)maxHeight / imageHeight;
                    double ratio = Math.Min(widthRatio, heightRatio);

                    imageWidth = (int)(imageWidth * ratio);
                    imageHeight = (int)(imageHeight * ratio);
                }


                pictureBox.Size = new Size(imageWidth, imageHeight);
                pictureBox.Dock = DockStyle.Fill;

                this.ClientSize = new Size(imageWidth, imageHeight);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Image loading error: " + ex.Message);
            }
        }
    }
}
