using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiParser.Controls
{
    public class ImageElementBlock : ElementBlock
    {
        public TextBox InputSavePath { get; private set; }

        public ImageElementBlock() : base("Фото")
        {
            InputSavePath = new TextBox
            {
                PlaceholderText = "Шлях для збереження фото",
                Width = 460,
                Top = 60,
                Left = 5
            };

            InputCode.Top = 90;
            IsOne.Top = 120;
            this.Height = 150;

            this.Controls.Add(InputSavePath);
            this.Controls.SetChildIndex(InputSavePath, 2);
        }

        public void DownloadImage(string imageUrl, string saveDirectory, string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(saveDirectory))
                {
                    MessageBox.Show("Шлях до папки для збереження не вказано.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!Directory.Exists(saveDirectory))
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                string fullPath = Path.Combine(saveDirectory, fileName);

                if (string.IsNullOrWhiteSpace(imageUrl))
                {
                    MessageBox.Show("URL зображення порожній.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!imageUrl.StartsWith("http"))
                {
                    MessageBox.Show($"Некоректний URL зображення: {imageUrl}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string extension = Path.GetExtension(new Uri(imageUrl).AbsolutePath);
                if (string.IsNullOrEmpty(extension))
                {
                    extension = ".jpg";
                }

                string savePath = Path.Combine(saveDirectory, fileName + extension);

                using (var client = new System.Net.WebClient())
                {
                    client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
                    client.DownloadFile(imageUrl, savePath);
                }

                Console.WriteLine($"Зображення завантажено: {savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні зображення:\n{ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
