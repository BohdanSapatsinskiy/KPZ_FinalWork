using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Security.Policy;

namespace MultiParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAddTextElem_Click(object sender, EventArgs e)
        {
            var block = new TextElementBlock();
            siteMapPanel.Controls.Add(block);
            block.Top = siteMapPanel.Controls.Count * (block.Height + 10);
        }

        private void buttonAddImg_Click(object sender, EventArgs e)
        {
            var block = new ImageElementBlock();
            siteMapPanel.Controls.Add(block);
            block.Top = siteMapPanel.Controls.Count * (block.Height + 10);
        }

        private IWebDriver driver;
        private void buttonStartBrowser_Click(object sender, EventArgs e)
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--profile-directory=Default");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");
            options.AddUserProfilePreference("useAutomationExtension", false);
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36");
            options.AddArguments("--incognito");

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            driver = new ChromeDriver(service, options);
            driver.Url = "https://www.google.com";

        }

        private void buttonCopyFromUrl_Click(object sender, EventArgs e)
        {
            string url = textBoxOneUrl.Text.Trim();
            if (!string.IsNullOrWhiteSpace(url))
            {
                RunParsing(url, 0);
            }
            else
            {
                textBoxLog.AppendText("URL порожній.\r\n");
            }
        }

        private void buttonCopyMany_Click(object sender, EventArgs e)
        {
            var urls = listBoxUrls.Items.Cast<string>()
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

            if (urls.Count == 0)
            {
                textBoxLog.AppendText("У списку немає URL-ів для парсингу.\r\n");
                return;
            }
            for (int i = 0; i < urls.Count; i++) {
                RunParsing(urls[i], i);
            }
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            bool hasElements = false;

            foreach (Control control in siteMapPanel.Controls)
            {
                if (control is TextElementBlock textBlock)
                {
                    textBlock.InputCode.Text = HtmlCleaner.CleanHtmlToSelector(textBlock.InputCode.Text);
                    hasElements = true;
                }
                else if (control is ImageElementBlock imgBlock)
                {
                    imgBlock.InputCode.Text = HtmlCleaner.CleanHtmlToSelector(imgBlock.InputCode.Text);
                    hasElements = true;
                }
            }

            if (!hasElements)
            {
                MessageBox.Show("Немає елементів для валідації.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void buttonReadUrls_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Текстові файли (*.txt)|*.txt";
                openFileDialog.Title = "Оберіть файл з посиланнями";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] urls = System.IO.File.ReadAllLines(openFileDialog.FileName);

                    listBoxUrls.Items.Clear();
                    foreach (var url in urls)
                    {
                        if (!string.IsNullOrWhiteSpace(url))
                        {
                            listBoxUrls.Items.Add(url.Trim());
                        }
                    }
                }
            }
        }


        private void buttonCheckUrlList_Click(object sender, EventArgs e)
        {
            if (listBoxUrls.Items.Count == 0)
            {
                MessageBox.Show("Список посилань порожній.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string firstUrl = listBoxUrls.Items[0].ToString();
            Uri baseUri;

            try
            {
                baseUri = new Uri(firstUrl);
            }
            catch (UriFormatException)
            {
                MessageBox.Show("Перше посилання некоректне.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string baseHost = baseUri.Host.Replace("www.", "");

            List<string> filteredUrls = new List<string>();

            foreach (var item in listBoxUrls.Items)
            {
                if (item is string url)
                {
                    try
                    {
                        Uri currentUri = new Uri(url);
                        if (currentUri.Host.Replace("www.", "") == baseHost)
                        {
                            filteredUrls.Add(url);
                        }
                    }
                    catch
                    {

                    }
                }
            }

            listBoxUrls.Items.Clear();
            listBoxUrls.Items.AddRange(filteredUrls.ToArray());

            MessageBox.Show($"Залишено {filteredUrls.Count} посилань з сайту {baseHost}.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonDeleteItems_Click(object sender, EventArgs e)
        {
            if (siteMapPanel.Controls.Count == 0)
            {
                MessageBox.Show("Немає елементів для видалення.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            for (int i = siteMapPanel.Controls.Count - 1; i >= 0; i--)
            {
                Control control = siteMapPanel.Controls[i];
                siteMapPanel.Controls.Remove(control);
                control.Dispose();
            }

            MessageBox.Show("Всі елементи були успішно видалені.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonSelectPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Оберіть папку для збереження файлу";
                folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxSavePath.Text = folderDialog.SelectedPath;
                }
            }
        }
        private bool CheckFilesForAllElementsBeforeParsing(List<ElementBlock> elements)
        {
            foreach (var element in elements)
            {
                if (element is ImageElementBlock imageElement)
                {
                    string saveDirectory = imageElement.InputSavePath.Text.Trim();
                    string baseFileName = imageElement.InputName.Text.Trim();

                    if (string.IsNullOrWhiteSpace(saveDirectory) || string.IsNullOrWhiteSpace(baseFileName))
                    {
                        MessageBox.Show("Не вказано шлях або ім'я файлу.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    string extension = ".jpg";

                    string fullPath = Path.Combine(saveDirectory, baseFileName + extension);
                    if (File.Exists(fullPath))
                    {
                        MessageBox.Show($"Файл з ім'ям {baseFileName}{extension} вже існує у папці {saveDirectory}. Парсинг не розпочнеться.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    string indexedFilePath = Path.Combine(saveDirectory, baseFileName + "_1" + extension);
                    if (File.Exists(indexedFilePath))
                    {
                        MessageBox.Show($"Файл з ім'ям {baseFileName}_1{extension} вже існує у папці {saveDirectory}. Парсинг не розпочнеться.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }


        private void RunParsing(string url, int indexUrl)
        {
            try
            {
                textBoxLog.AppendText($"Старт парсингу для: {url}\r\n");

                var elements = siteMapPanel.Controls.OfType<ElementBlock>().ToList();
                textBoxLog.AppendText($"Знайдено елементів: {elements.Count}\r\n");

                if (!CheckFilesForAllElementsBeforeParsing(elements))
                    return;

                var parser = new PageParser(driver, elements, (text) => textBoxLog.AppendText(text + "\r\n"));
                parser.CsvSaveDirectory = textBoxSavePath.Text.Trim();
                parser.ParsePage(url, indexUrl);

                textBoxLog.AppendText($"Парсинг завершено для: {url}\r\n");
            }
            catch (Exception ex)
            {
                textBoxLog.AppendText("Помилка: " + ex.Message + "\r\n");
            }
        }
    }
}
