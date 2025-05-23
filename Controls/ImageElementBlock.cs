using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using OpenQA.Selenium;

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
            Controls.Add(InputSavePath);

            InputCode.Top = 90;
            IsOne.Top = 120;
            Height = 160;
        }

        public override void Parse(
            IWebDriver driver,
            int urlIndex,
            IDictionary<string, List<string>> parsedValues,
            Action<string> logAction)
        {
            string selector = InputCode.Text.Trim();
            string fieldName = InputName.Text.Trim();
            string saveFolder = InputSavePath.Text.Trim();
            if (string.IsNullOrEmpty(selector) ||
                string.IsNullOrEmpty(fieldName))
                return;

            try
            {
                var elements = IsOne.Checked
                    ? new[] { driver.FindElement(By.CssSelector(selector)) }
                    : driver.FindElements(By.CssSelector(selector)).ToArray();

                for (int i = 0; i < elements.Length; i++)
                {
                    string url = elements[i].GetAttribute("src");
                    if (string.IsNullOrWhiteSpace(url))
                    {
                        logAction($"Image URL is empty for '{fieldName}'");
                        continue;
                    }

                    string filenameBase = $"{fieldName}_{urlIndex}_{i + 1}";
                    string savedPath = DownloadImage(url, saveFolder, filenameBase, logAction);
                    if (!string.IsNullOrEmpty(savedPath))
                    {
                        logAction($"Downloaded image [{fieldName}_{i + 1}]: {savedPath}");
                        AddParsedValue(parsedValues, fieldName, savedPath, i);
                    }
                }
            }
            catch (Exception ex)
            {
                logAction($"Error parsing image element '{fieldName}': {ex.Message}");
            }
        }

        private string DownloadImage(
            string imageUrl,
            string saveDirectory,
            string fileBaseName,
            Action<string> logAction)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(saveDirectory))
                {
                    logAction("Save path not specified for images.");
                    return null;
                }

                if (!Directory.Exists(saveDirectory))
                    Directory.CreateDirectory(saveDirectory);

                var uri = new Uri(imageUrl);
                string ext = Path.GetExtension(uri.AbsolutePath);
                string fname = fileBaseName + (string.IsNullOrEmpty(ext) ? ".jpg" : ext);
                string path = Path.Combine(saveDirectory, fname);

                using var client = new WebClient();
                client.Headers.Add("User-Agent", "Mozilla/5.0");
                client.DownloadFile(imageUrl, path);

                return path;
            }
            catch (Exception ex)
            {
                logAction($"Error downloading image: {ex.Message}");
                return null;
            }
        }
    }
}
