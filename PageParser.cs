using OpenQA.Selenium;
using System.Text;

namespace MultiParser
{
    public class PageParser
    {
        private IWebDriver driver;
        private IEnumerable<ElementBlock> elements;
        private Action<string> logAction;

        private Dictionary<string, List<string>> parsedValues = new Dictionary<string, List<string>>();

        public string CsvSaveDirectory { get; set; }

        public PageParser(IWebDriver driver, IEnumerable<ElementBlock> elements, Action<string> logAction)
        {
            this.driver = driver;
            this.elements = elements;
            this.logAction = logAction;
        }

        public void ParsePage(string url, int urlIndex)
        {
            parsedValues.Clear();
            driver.Navigate().GoToUrl(url);
            System.Threading.Thread.Sleep(2000);

            //Парсинг
            foreach (var element in elements)
            {
                try
                {
                    if (element is TextElementBlock textElement)
                    {
                        ParseTextElement(textElement);
                    }
                    else if (element is ImageElementBlock imageElement)
                    {
                        ParseImageElement(imageElement, urlIndex);
                    }
                }
                catch (Exception ex)
                {
                    logAction($"Помилка обробки елемента {element.InputName.Text}: {ex.Message}");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(CsvSaveDirectory))
            {
                string fileName = $"result-{DateTime.Now:yyyyMMdd-HHmmss}.csv";
                string fullPath = Path.Combine(CsvSaveDirectory, fileName);
                SaveResultsToCsv(fullPath);
            }
            else
            {
                logAction("Не вказано шлях збереження CSV-файлу — CSV не збережено.");
                return;
            }
        }

        private void AddParsedValue(string name, string value, int index = 0)
        {
            if (!parsedValues.ContainsKey(name))
                parsedValues[name] = new List<string>();

            while (parsedValues[name].Count <= index)
                parsedValues[name].Add("");

            parsedValues[name][index] = value;
        }

        private void ParseTextElement(TextElementBlock textElement)
        {
            string code = textElement.InputCode.Text.Trim();
            if (string.IsNullOrEmpty(code)) return;

            string fieldName = textElement.InputName.Text.Trim();

            if (textElement.IsOne.Checked)
            {
                var el = driver.FindElement(By.CssSelector(code));
                string value = el.Text;
                logAction($"Текст [{fieldName}]: {value}");
                AddParsedValue(fieldName, value);
            }
            else
            {
                var els = driver.FindElements(By.CssSelector(code));
                for (int i = 0; i < els.Count; i++)
                {
                    string value = els[i].Text;
                    logAction($"Текст [{fieldName}_{i + 1}]: {value}");
                    AddParsedValue(fieldName, value, i);
                }
            }
        }

        private void ParseImageElement(ImageElementBlock imageElement, int urlIndex)
        {
            string code = imageElement.InputCode.Text.Trim();
            if (string.IsNullOrEmpty(code)) return;

            string saveDirectory = imageElement.InputSavePath.Text.Trim();
            string baseFileName = imageElement.InputName.Text.Trim();

            if (string.IsNullOrWhiteSpace(saveDirectory))
            {
                logAction("Шлях збереження зображення не вказано.");
                return;
            }

            if (imageElement.IsOne.Checked)
                ParseSingleImage(code, saveDirectory, baseFileName, imageElement);
            else
                ParseMultipleImages(code, saveDirectory, baseFileName, imageElement, urlIndex);
        }

        private void ParseSingleImage(string cssSelector, string saveDirectory, string baseFileName, ImageElementBlock imageElement)
        {
            try
            {
                var el = driver.FindElement(By.CssSelector(cssSelector));
                string src = el.GetAttribute("src");

                imageElement.DownloadImage(src, saveDirectory, baseFileName);
                logAction($"Фото [{baseFileName}] збережено");

                string localPath = Path.Combine(saveDirectory, baseFileName + Path.GetExtension(src));
                AddParsedValue(baseFileName, localPath);
            }
            catch (Exception ex)
            {
                logAction($"Помилка обробки одного фото: {ex.Message}");
            }
        }

        private int GetMaxAllowedImages()
        {
            if (parsedValues.Count == 0)
                return 0;

            var firstKey = parsedValues.Keys.First();
            var firstList = parsedValues[firstKey];
            int limit = firstList.Count;

            logAction($"Ліміт фото для завантаження встановлено на {limit} (довжина першого списку текстових даних)");
            return limit;
        }

        private void ParseMultipleImages(string cssSelector, string saveDirectory, string baseFileName, ImageElementBlock imageElement, int urlIndex)
        {
            try
            {
                int maxAllowedImages = GetMaxAllowedImages();
                if (maxAllowedImages == 0)
                {
                    logAction("parsedValues порожній — пропускаємо парсинг фото.");
                    return;
                }

                int loadedCount = 0;
                int iteration = 0;

                while (true)
                {
                    var currentEls = GetVisibleImageElements(cssSelector);

                    if (NoNewImagesLoaded(currentEls.Count, loadedCount))
                        break;

                    ProcessNewImages(currentEls, loadedCount, maxAllowedImages, baseFileName, saveDirectory, imageElement, urlIndex);

                    loadedCount = currentEls.Count;
                    iteration++;

                    if (ShouldStop(iteration, loadedCount, maxAllowedImages))
                        break;

                    System.Threading.Thread.Sleep(1500);
                }
            }
            catch (Exception ex)
            {
                logAction($"Помилка обробки кількох фото: {ex.Message}");
            }
        }

        private List<IWebElement> GetVisibleImageElements(string cssSelector)
        {
            return driver.FindElements(By.CssSelector(cssSelector)).ToList();
        }

        private bool NoNewImagesLoaded(int currentCount, int loadedCount)
        {
            return currentCount <= loadedCount;
        }

        private bool ShouldStop(int iteration, int loadedCount, int maxAllowedImages)
        {
            if (iteration > 50)
            {
                logAction("Досягнуто ліміт ітерацій при прокрутці — зупинка.");
                return true;
            }

            if (loadedCount >= maxAllowedImages)
            {
                logAction("Досягнуто максимальну кількість фото — зупинка.");
                return true;
            }

            return false;
        }

        private void ProcessNewImages(List<IWebElement> elements, int startIndex, int maxAllowedImages, string baseFileName, string saveDirectory, ImageElementBlock imageElement, int urlIndex)
        {
            for (int i = startIndex; i < elements.Count && i < maxAllowedImages; i++)
            {
                var el = elements[i];

                ScrollToElement(el);
                System.Threading.Thread.Sleep(500);

                string src = el.GetAttribute("src");
                string indexedFileName = $"{baseFileName}_{urlIndex}_{i + 1}";

                SaveImage(src, saveDirectory, indexedFileName, baseFileName, imageElement, i);
            }
        }

        private void ScrollToElement(IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        private void SaveImage(string src, string saveDirectory, string fileName, string baseFileName, ImageElementBlock imageElement, int index)
        {
            imageElement.DownloadImage(src, saveDirectory, fileName);
            logAction($"Фото [{fileName}] збережено");

            string localPath = Path.Combine(saveDirectory, fileName + Path.GetExtension(src));
            AddParsedValue(baseFileName, localPath, index);
        }

        private void SaveResultsToCsv(string filePath)
        {
            try
            {
                int maxRows = parsedValues.Values.Max(list => list.Count);

                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // Заголовок
                    var headers = parsedValues.Keys.ToList();
                    writer.WriteLine(string.Join(";", headers));

                    // Дані рядки
                    for (int i = 0; i < maxRows; i++)
                    {
                        var row = headers.Select(h =>
                        {
                            if (parsedValues[h].Count > i)
                                return EscapeCsv(parsedValues[h][i]);
                            else
                                return "";
                        });

                        writer.WriteLine(string.Join(";", row));
                    }
                }

                logAction($"CSV-файл збережено: {filePath}");
            }
            catch (Exception ex)
            {
                logAction($"Помилка при збереженні CSV: {ex.Message}");
                return;
            }
        }

        private string EscapeCsv(string value)
        {
            if (value.Contains("\"") || value.Contains(";") || value.Contains("\n"))
                return $"\"{value.Replace("\"", "\"\"")}\"";
            return value;
        }
    }
}
