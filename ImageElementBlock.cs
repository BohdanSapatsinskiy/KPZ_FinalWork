using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiParser
{
    public class ImageElementBlock : Panel
    {
        public TextBox InputName { get; private set; }
        public TextBox InputCode { get; private set; }
        public TextBox InputSavePath { get; private set; }
        public CheckBox IsOne { get; private set; }
        public Button CheckValid { get; private set; }

        public ImageElementBlock()
        {
            this.BackColor = Color.LightGray;
            this.Width = 500;
            this.Height = 150;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Margin = new Padding(10);

            Label label = new Label
            {
                Text = "Фото",
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Top = 5,
                Left = 5
            };
            this.Controls.Add(label);

            InputName = new TextBox
            {
                PlaceholderText = "Назва фото",
                Width = 460,
                Top = 30,
                Left = 5
            };
            this.Controls.Add(InputName);

            InputSavePath = new TextBox
            {
                PlaceholderText = "Шлях до збереження",
                Width = 460,
                Top = 60,
                Left = 5
            };
            this.Controls.Add(InputSavePath);

            InputCode = new TextBox
            {
                PlaceholderText = "HTML-код елемента",
                Width = 460,
                Top = 90,
                Left = 5
            };
            this.Controls.Add(InputCode);

            IsOne = new CheckBox
            {
                Text = "Шукати одне фото",
                Top = 120,
                Left = 5,
                Width = 200
            };
            this.Controls.Add(IsOne);
        }
    }
}
