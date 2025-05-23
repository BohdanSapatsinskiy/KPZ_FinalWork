using System;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using MultiParser.Controls;

namespace MultiParser.Parsing
{
    public class PageParser
    {
        private readonly IWebDriver driver;
        private readonly IList<ElementBlock> elements;
        private readonly Action<string> logAction;

        public IDictionary<string, List<string>> ParsedValues { get; }
            = new Dictionary<string, List<string>>();

        public string CsvSaveDirectory { get; set; }

        public PageParser(
            IWebDriver driver,
            IList<ElementBlock> elements,
            Action<string> logAction)
        {
            this.driver = driver;
            this.elements = elements;
            this.logAction = logAction;
        }

        public void ParsePage(string url, int urlIndex)
        {
            ParsedValues.Clear();
            driver.Navigate().GoToUrl(url);
            System.Threading.Thread.Sleep(2000);

            foreach (var element in elements)
            {
                element.Parse(driver, urlIndex, ParsedValues, logAction);
            }

            if (string.IsNullOrWhiteSpace(CsvSaveDirectory))
            {
                logAction("CSV save directory not set; skipping save.");
                return;
            }

            string filename = $"result-{DateTime.Now:yyyyMMdd-HHmmss}.csv";
            SaveResultsToCsv(Path.Combine(CsvSaveDirectory, filename));
        }

        private void SaveResultsToCsv(string fullPath)
        {
            using var sw = new StreamWriter(fullPath, false, Encoding.UTF8);
            sw.Write("URL_Index");
            foreach (var key in ParsedValues.Keys)
                sw.Write($",{key}");
            sw.WriteLine();

            int max = 0;
            foreach (var list in ParsedValues.Values)
                if (list.Count > max) max = list.Count;

            for (int i = 0; i < max; i++)
            {
                sw.Write(i + 1);
                foreach (var list in ParsedValues.Values)
                {
                    string val = i < list.Count ? list[i] : "";
                    sw.Write($",{val}");
                }
                sw.WriteLine();
            }

            logAction($"Results written to CSV: {fullPath}");
        }
    }
}
