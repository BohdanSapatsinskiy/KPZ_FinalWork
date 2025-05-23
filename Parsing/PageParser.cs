using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MultiParser.Parsing
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
            {
                try
                {
                    var el = driver.FindElement(By.CssSelector(code));
                    string src = el.GetAttribute("src");

                    imageElement.DownloadImage(src, saveDirectory, baseFileName);
                    logAction($"Фото [{baseFileName}] збережено");

                    string localPath = Path.Combine(saveDirectory, baseFileName + Path.GetExtension(src));
                    AddParsedValue(baseFileName, localPath);
                }
                catch (Exception ex)
                {
                    logAction($"Помилка обробки одного фото: {ex.Message}");
                    return;
                }
            }
            else
            {
                try
                {
                    // Отримуємо ліміт: довжину першого списку в parsedValues
                    int maxAllowedImages = 0;
                    if (parsedValues.Count > 0)
                    {
                        var firstKey = parsedValues.Keys.First();
                        var firstList = parsedValues[firstKey];
                        maxAllowedImages = firstList.Count;
                        logAction($"Ліміт фото для завантаження встановлено на {maxAllowedImages} (довжина першого списку текстових даних)");
                    }
                    else
                    {
                        logAction("parsedValues порожній — пропускаємо парсинг фото.");
                        return;
                    }

                    int loadedCount = 0;
                    int iteration = 0;

                    while (true)
                    {
                        var currentEls = driver.FindElements(By.CssSelector(code)).ToList();

                        // Якщо нових елементів немає — вихід
                        if (currentEls.Count <= loadedCount)
                            break;

                        for (int i = loadedCount; i < currentEls.Count; i++)
                        {
                            // Якщо вже завантажили фото більше ніж дозволено — обриваємо цикл
                            if (i >= maxAllowedImages)
                            {
                                logAction($"Досягнуто максимальну кількість фото: {maxAllowedImages}. Зупинка завантаження.");
                                break;
                            }

                            var el = currentEls[i];

                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", el);
                            System.Threading.Thread.Sleep(500); // час на підвантаження

                            string src = el.GetAttribute("src");
                            string indexedFileName = $"{baseFileName}_{urlIndex}_{i + 1}";


                            imageElement.DownloadImage(src, saveDirectory, indexedFileName);
                            logAction($"Фото [{indexedFileName}] збережено");

                            string localPath = Path.Combine(saveDirectory, indexedFileName + Path.GetExtension(src));
                            AddParsedValue(baseFileName, localPath, i);
                        }

                        loadedCount = currentEls.Count;
                        iteration++;

                        if (iteration > 50)
                        {
                            logAction("Досягнуто ліміт ітерацій при прокрутці — зупинка.");
                            break;
                        }

                        // Якщо вже досягли максимальну кількість фото, теж вихід
                        if (loadedCount >= maxAllowedImages)
                            break;

                        System.Threading.Thread.Sleep(1500);
                    }
                }
                catch (Exception ex)
                {
                    logAction($"Помилка обробки кількох фото: {ex.Message}");
                    return;
                }
            }
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
