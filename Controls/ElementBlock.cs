using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiParser.Controls
{
    public class ElementBlock : Panel
    {
        public TextBox InputName { get; protected set; }
        public TextBox InputCode { get; protected set; }
        public CheckBox IsOne { get; protected set; }

        public ElementBlock(string labelText)
        {
            this.BackColor = Color.LightGray;
            this.Width = 500;
            this.Height = 120;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Margin = new Padding(10);

            Label label = new Label
            {
                Text = labelText,
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Top = 5,
                Left = 5
            };
            this.Controls.Add(label);

            InputName = new TextBox
            {
                PlaceholderText = "Назва поля",
                Width = 460,
                Top = 30,
                Left = 5
            };
            this.Controls.Add(InputName);

            InputCode = new TextBox
            {
                PlaceholderText = "HTML-код елемента",
                Width = 460,
                Top = 60,
                Left = 5
            };
            this.Controls.Add(InputCode);

            IsOne = new CheckBox
            {
                Text = "Шукати один",
                Top = 90,
                Left = 5,
                Width = 200
            };
            this.Controls.Add(IsOne);
        }
        public abstract void Parse(
           OpenQA.Selenium.IWebDriver driver,
           int urlIndex,
           IDictionary<string, List<string>> parsedValues,
           Action<string> logAction);
    }
}