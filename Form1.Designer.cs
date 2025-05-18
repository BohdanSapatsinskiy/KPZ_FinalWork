namespace MultiParser
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            elementsPanel = new Panel();
            label2 = new Label();
            textBoxSavePath = new TextBox();
            buttonSelectPath = new Button();
            buttonAddImg = new Button();
            buttonAddTextElem = new Button();
            siteMapPanel = new FlowLayoutPanel();
            panel1 = new Panel();
            buttonReadUrls = new Button();
            buttonCheckUrlList = new Button();
            listBoxUrls = new ListBox();
            textBoxOneUrl = new TextBox();
            label1 = new Label();
            buttonStartBrowser = new Button();
            buttonCopyFromUrl = new Button();
            buttonCopyMany = new Button();
            buttonValidate = new Button();
            buttonDeleteItems = new Button();
            textBoxLog = new TextBox();
            elementsPanel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // elementsPanel
            // 
            elementsPanel.BackColor = SystemColors.ActiveBorder;
            elementsPanel.Controls.Add(label2);
            elementsPanel.Controls.Add(textBoxSavePath);
            elementsPanel.Controls.Add(buttonSelectPath);
            elementsPanel.Controls.Add(buttonAddImg);
            elementsPanel.Controls.Add(buttonAddTextElem);
            elementsPanel.Location = new Point(31, 89);
            elementsPanel.Name = "elementsPanel";
            elementsPanel.Size = new Size(260, 474);
            elementsPanel.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.Location = new Point(61, 343);
            label2.Name = "label2";
            label2.Size = new Size(134, 28);
            label2.TabIndex = 9;
            label2.Text = "Path to save:";
            // 
            // textBoxSavePath
            // 
            textBoxSavePath.Location = new Point(18, 374);
            textBoxSavePath.Name = "textBoxSavePath";
            textBoxSavePath.ReadOnly = true;
            textBoxSavePath.Size = new Size(225, 27);
            textBoxSavePath.TabIndex = 10;
            // 
            // buttonSelectPath
            // 
            buttonSelectPath.Location = new Point(18, 407);
            buttonSelectPath.Name = "buttonSelectPath";
            buttonSelectPath.Size = new Size(225, 42);
            buttonSelectPath.TabIndex = 9;
            buttonSelectPath.Text = "Select path";
            buttonSelectPath.UseVisualStyleBackColor = true;
            buttonSelectPath.Click += buttonSelectPath_Click;
            // 
            // buttonAddImg
            // 
            buttonAddImg.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonAddImg.Location = new Point(18, 80);
            buttonAddImg.Name = "buttonAddImg";
            buttonAddImg.Size = new Size(225, 57);
            buttonAddImg.TabIndex = 1;
            buttonAddImg.Text = "Img";
            buttonAddImg.UseVisualStyleBackColor = true;
            buttonAddImg.Click += buttonAddImg_Click;
            // 
            // buttonAddTextElem
            // 
            buttonAddTextElem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonAddTextElem.Location = new Point(18, 17);
            buttonAddTextElem.Name = "buttonAddTextElem";
            buttonAddTextElem.Size = new Size(225, 57);
            buttonAddTextElem.TabIndex = 0;
            buttonAddTextElem.Text = "Text";
            buttonAddTextElem.UseVisualStyleBackColor = true;
            buttonAddTextElem.Click += buttonAddTextElem_Click;
            // 
            // siteMapPanel
            // 
            siteMapPanel.AutoScroll = true;
            siteMapPanel.BackColor = SystemColors.ActiveBorder;
            siteMapPanel.FlowDirection = FlowDirection.TopDown;
            siteMapPanel.Location = new Point(311, 89);
            siteMapPanel.Name = "siteMapPanel";
            siteMapPanel.Size = new Size(550, 426);
            siteMapPanel.TabIndex = 1;
            siteMapPanel.WrapContents = false;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveBorder;
            panel1.Controls.Add(buttonReadUrls);
            panel1.Controls.Add(buttonCheckUrlList);
            panel1.Controls.Add(listBoxUrls);
            panel1.Location = new Point(877, 89);
            panel1.Name = "panel1";
            panel1.Size = new Size(331, 474);
            panel1.TabIndex = 2;
            // 
            // buttonReadUrls
            // 
            buttonReadUrls.Location = new Point(167, 401);
            buttonReadUrls.Name = "buttonReadUrls";
            buttonReadUrls.Size = new Size(150, 48);
            buttonReadUrls.TabIndex = 2;
            buttonReadUrls.Text = "Add Urls";
            buttonReadUrls.UseVisualStyleBackColor = true;
            buttonReadUrls.Click += buttonReadUrls_Click;
            // 
            // buttonCheckUrlList
            // 
            buttonCheckUrlList.Location = new Point(11, 401);
            buttonCheckUrlList.Name = "buttonCheckUrlList";
            buttonCheckUrlList.Size = new Size(150, 48);
            buttonCheckUrlList.TabIndex = 1;
            buttonCheckUrlList.Text = "Check List";
            buttonCheckUrlList.UseVisualStyleBackColor = true;
            buttonCheckUrlList.Click += buttonCheckUrlList_Click;
            // 
            // listBoxUrls
            // 
            listBoxUrls.FormattingEnabled = true;
            listBoxUrls.Location = new Point(11, 11);
            listBoxUrls.Name = "listBoxUrls";
            listBoxUrls.Size = new Size(308, 384);
            listBoxUrls.TabIndex = 0;
            // 
            // textBoxOneUrl
            // 
            textBoxOneUrl.Location = new Point(311, 26);
            textBoxOneUrl.Name = "textBoxOneUrl";
            textBoxOneUrl.Size = new Size(550, 27);
            textBoxOneUrl.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(260, 22);
            label1.Name = "label1";
            label1.Size = new Size(45, 28);
            label1.TabIndex = 4;
            label1.Text = "Url:";
            // 
            // buttonStartBrowser
            // 
            buttonStartBrowser.Location = new Point(31, 18);
            buttonStartBrowser.Name = "buttonStartBrowser";
            buttonStartBrowser.Size = new Size(206, 42);
            buttonStartBrowser.TabIndex = 2;
            buttonStartBrowser.Text = "Open browser";
            buttonStartBrowser.UseVisualStyleBackColor = true;
            buttonStartBrowser.Click += buttonStartBrowser_Click;
            // 
            // buttonCopyFromUrl
            // 
            buttonCopyFromUrl.Location = new Point(867, 18);
            buttonCopyFromUrl.Name = "buttonCopyFromUrl";
            buttonCopyFromUrl.Size = new Size(171, 42);
            buttonCopyFromUrl.TabIndex = 5;
            buttonCopyFromUrl.Text = "Copy from url";
            buttonCopyFromUrl.UseVisualStyleBackColor = true;
            buttonCopyFromUrl.Click += buttonCopyFromUrl_Click;
            // 
            // buttonCopyMany
            // 
            buttonCopyMany.Location = new Point(1044, 18);
            buttonCopyMany.Name = "buttonCopyMany";
            buttonCopyMany.Size = new Size(171, 42);
            buttonCopyMany.TabIndex = 6;
            buttonCopyMany.Text = "Copy from urls";
            buttonCopyMany.UseVisualStyleBackColor = true;
            buttonCopyMany.Click += buttonCopyMany_Click;
            // 
            // buttonValidate
            // 
            buttonValidate.Location = new Point(311, 521);
            buttonValidate.Name = "buttonValidate";
            buttonValidate.Size = new Size(270, 42);
            buttonValidate.TabIndex = 7;
            buttonValidate.Text = "Validating items for finding";
            buttonValidate.UseVisualStyleBackColor = true;
            buttonValidate.Click += buttonValidate_Click;
            // 
            // buttonDeleteItems
            // 
            buttonDeleteItems.Location = new Point(591, 521);
            buttonDeleteItems.Name = "buttonDeleteItems";
            buttonDeleteItems.Size = new Size(270, 42);
            buttonDeleteItems.TabIndex = 8;
            buttonDeleteItems.Text = "Delete all items";
            buttonDeleteItems.UseVisualStyleBackColor = true;
            buttonDeleteItems.Click += buttonDeleteItems_Click;
            // 
            // textBoxLog
            // 
            textBoxLog.BackColor = Color.Black;
            textBoxLog.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxLog.ForeColor = Color.White;
            textBoxLog.Location = new Point(31, 569);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ScrollBars = ScrollBars.Vertical;
            textBoxLog.Size = new Size(1177, 143);
            textBoxLog.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1233, 724);
            Controls.Add(textBoxLog);
            Controls.Add(buttonDeleteItems);
            Controls.Add(buttonValidate);
            Controls.Add(buttonCopyMany);
            Controls.Add(buttonCopyFromUrl);
            Controls.Add(buttonStartBrowser);
            Controls.Add(label1);
            Controls.Add(textBoxOneUrl);
            Controls.Add(panel1);
            Controls.Add(siteMapPanel);
            Controls.Add(elementsPanel);
            Name = "Form1";
            Text = "Parser";
            elementsPanel.ResumeLayout(false);
            elementsPanel.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel elementsPanel;
        private Button buttonAddImg;
        private Button buttonAddTextElem;
        private FlowLayoutPanel siteMapPanel;
        private Panel panel1;
        private Button buttonReadUrls;
        private Button buttonCheckUrlList;
        private ListBox listBoxUrls;
        private TextBox textBoxOneUrl;
        private Label label1;
        private Button buttonStartBrowser;
        private Button buttonCopyFromUrl;
        private Button buttonCopyMany;
        private Button buttonValidate;
        private Button buttonDeleteItems;
        private Button buttonSelectPath;
        private Label label2;
        private TextBox textBoxSavePath;
        private TextBox textBoxLog;
    }
}
