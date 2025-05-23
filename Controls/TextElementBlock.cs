using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace MultiParser.Controls
{
    public class TextElementBlock : ElementBlock
    {
        public TextElementBlock() : base("Текстовий елемент") { }

        public override void Parse(
            IWebDriver driver,
            int urlIndex,
            IDictionary<string, List<string>> parsedValues,
            Action<string> logAction)
        {
            string selector = InputCode.Text.Trim();
            string fieldName = InputName.Text.Trim();
            if (string.IsNullOrEmpty(selector) || string.IsNullOrEmpty(fieldName))
                return;

            try
            {
                if (IsOne.Checked)
                {
                    var el = driver.FindElement(By.CssSelector(selector));
                    string text = el.Text;
                    logAction($"Текст [{fieldName}]: {text}");
                    AddParsedValue(parsedValues, fieldName, text, 0);
                }
                else
                {
                    var els = driver.FindElements(By.CssSelector(selector));
                    for (int i = 0; i < els.Count; i++)
                    {
                        string text = els[i].Text;
                        logAction($"Текст [{fieldName}_{i + 1}]: {text}");
                        AddParsedValue(parsedValues, fieldName, text, i);
                    }
                }
            }
            catch (Exception ex)
            {
                logAction($"Error parsing text element '{fieldName}': {ex.Message}");
            }
        }
    }
}
