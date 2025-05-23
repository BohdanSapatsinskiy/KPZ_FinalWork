using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenQA.Selenium;

namespace MultiParser.Controls
{
    public abstract class ElementBlock : Panel
    {
        public TextBox InputName { get; protected set; }
        public TextBox InputCode { get; protected set; }
        public CheckBox IsOne { get; protected set; }

        protected ElementBlock(string labelText)
        {
            BackColor = Color.LightGray;
            Width = 500;
            Height = 120;
            BorderStyle = BorderStyle.FixedSingle;
            Margin = new Padding(10);

            var label = new Label
            {
                Text = labelText,
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Top = 5,
                Left = 5
            };
            Controls.Add(label);

            InputName = new TextBox
            {
                PlaceholderText = "Назва поля",
                Width = 460,
                Top = 30,
                Left = 5
            };
            Controls.Add(InputName);

            InputCode = new TextBox
            {
                PlaceholderText = "HTML-код елемента",
                Width = 460,
                Top = 60,
                Left = 5
            };
            Controls.Add(InputCode);

            IsOne = new CheckBox
            {
                Text = "Шукати один",
                Top = 90,
                Left = 5,
                Width = 200
            };
            Controls.Add(IsOne);
        }

        public abstract void Parse(
            IWebDriver driver,
            int urlIndex,
            IDictionary<string, List<string>> parsedValues,
            Action<string> logAction);

        protected void AddParsedValue(
            IDictionary<string, List<string>> dict,
            string key,
            string value,
            int index)
        {
            if (!dict.ContainsKey(key))
                dict[key] = new List<string>();
            var list = dict[key];
            while (list.Count <= index)
                list.Add(string.Empty);
            list[index] = value;
        }
    }
}
