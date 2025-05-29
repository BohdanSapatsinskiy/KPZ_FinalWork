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
       // using System.Drawing;
// using System.Windows.Forms;

private void InitializeComponent()
{
    SuspendLayout();
    Font = new Font("Segoe UI", 7F);
    AutoScaleMode = AutoScaleMode.Dpi;
    BackColor = Color.WhiteSmoke;
    MinimumSize = new Size(950, 650);

    // ───────── LEFT: elementsPanel ────────────────────────────────────
    elementsPanel = new Panel
    {
        BackColor = Color.Gainsboro,
        Location  = new Point(20, 80),
        Size      = new Size(260, 460),
        Anchor    = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom,
        Padding   = new Padding(12)
    };
    Controls.Add(elementsPanel);

    buttonAddTextElem = CreateAccentButton("Text", /*TODO icon*/ null);
    buttonAddTextElem.Location = new Point(10, 10);
    buttonAddTextElem.Click   += buttonAddTextElem_Click;
    elementsPanel.Controls.Add(buttonAddTextElem);

    buttonAddImg = CreateAccentButton("Img", null);
    buttonAddImg.Location = new Point(10, 70);
    buttonAddImg.Click   += buttonAddImg_Click;
    elementsPanel.Controls.Add(buttonAddImg);

    label2 = new Label
    {
        Text      = "Path to save:",
        Font      = new Font("Segoe UI Semibold", 7F),
        Location  = new Point(10, 325),
        AutoSize  = true
    };
    elementsPanel.Controls.Add(label2);

    textBoxSavePath = new TextBox
    {
        ReadOnly = true,
        Location = new Point(10, 350),
        Width    = 220
    };
    elementsPanel.Controls.Add(textBoxSavePath);

    buttonSelectPath = CreateAccentButton("Select path", null);
    buttonSelectPath.Location = new Point(10, 385);
    buttonSelectPath.Width    = 220;
    buttonSelectPath.Click   += buttonSelectPath_Click;
    elementsPanel.Controls.Add(buttonSelectPath);

    // ───────── CENTER: siteMapPanel + Delete ──────────────────────────
    siteMapPanel = new FlowLayoutPanel
    {
        BackColor     = Color.Gainsboro,
        AutoScroll    = true,
        Location      = new Point(300, 80),
        Size          = new Size(540, 420),
        FlowDirection = FlowDirection.TopDown,
        WrapContents  = false,
        Anchor        = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom
    };
    Controls.Add(siteMapPanel);

    buttonDeleteItems = CreateAccentButton("Delete all items", null);
    buttonDeleteItems.Location = new Point(300, 510);
    buttonDeleteItems.Size     = new Size(540, 40);
    buttonDeleteItems.Anchor   = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
    buttonDeleteItems.Click   += buttonDeleteItems_Click;
    Controls.Add(buttonDeleteItems);

    // ───────── RIGHT: panel1 (urls) ────────────────────────────────────
    panel1 = new Panel
    {
        BackColor = Color.Gainsboro,
        Location  = new Point(860, 80),
        Size      = new Size(320, 460),
        Anchor    = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
        Padding   = new Padding(8)
    };
    Controls.Add(panel1);

    listBoxUrls = new ListBox
    {
        Dock = DockStyle.Top,
        Height = 380
    };
    panel1.Controls.Add(listBoxUrls);

    buttonReadUrls = CreateAccentButton("Add Urls", null);
    buttonReadUrls.Dock = DockStyle.Bottom;
    buttonReadUrls.Click += buttonReadUrls_Click;
    panel1.Controls.Add(buttonReadUrls);

    // ───────── TOP: toolbar ────────────────────────────────────────────
    buttonStartBrowser = CreateAccentButton("Open browser", null);
    buttonStartBrowser.Location = new Point(20, 20);
    buttonStartBrowser.Click   += buttonStartBrowser_Click;
    buttonStartBrowser.Size     = new Size(180, 38);
    buttonStartBrowser.Anchor   = AnchorStyles.Top | AnchorStyles.Left;
    Controls.Add(buttonStartBrowser);

    label1 = new Label
    {
        Text     = "Url:",
        Font     = new Font("Segoe UI Semibold", 7F),
        Location = new Point(220, 25),
        AutoSize = true
    };
    Controls.Add(label1);

    textBoxOneUrl = new TextBox
    {
        Location = new Point(260, 22),
        Size     = new Size(580, 27),
        Anchor   = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
    };
    Controls.Add(textBoxOneUrl);

    buttonCopyFromUrl = CreateAccentButton("Copy from url", null);
    buttonCopyFromUrl.Location = new Point(860, 20);
    buttonCopyFromUrl.Size     = new Size(150, 38);
    buttonCopyFromUrl.Anchor   = AnchorStyles.Top | AnchorStyles.Right;
    buttonCopyFromUrl.Click   += buttonCopyFromUrl_Click;
    Controls.Add(buttonCopyFromUrl);

    buttonCopyMany = CreateAccentButton("Copy from urls", null);
    buttonCopyMany.Location  = new Point(1020, 20);
    buttonCopyMany.Size      = new Size(150, 38);
    buttonCopyMany.Anchor    = AnchorStyles.Top | AnchorStyles.Right;
    buttonCopyMany.Click    += buttonCopyMany_Click;
    Controls.Add(buttonCopyMany);

    // ───────── BOTTOM: log ────────────────────────────────────────────
    textBoxLog = new TextBox
    {
        Multiline  = true,
        BackColor  = Color.FromArgb(30, 30, 30),
        ForeColor  = Color.Gainsboro,
        Font       = new Font("Consolas", 9F),
        ScrollBars = ScrollBars.Vertical,
        Location   = new Point(20, 560),
        Size       = new Size(1158, 120),
        Anchor     = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
    };
    Controls.Add(textBoxLog);

    // ───────── FORM FINAL ─────────────────────────────────────────────
    ClientSize = new Size(1200, 700);
    Text       = "Parser";
    ResumeLayout(false);
    PerformLayout();
}

// ───────────────────────────────────────────────────────────────────────
//  Accent‑button factory: плоский стиль + плавні кольори
private static Button CreateAccentButton(string text, Image? icon = null) =>
    new Button
    {
        Text                 = text,
        Image                = icon,
        ImageAlign           = ContentAlignment.MiddleLeft,
        TextImageRelation    = TextImageRelation.ImageBeforeText,
        FlatStyle            = FlatStyle.Flat,
        BackColor            = Color.FromArgb(0, 120, 215),
        ForeColor            = Color.White,
        Font                 = new Font("Segoe UI", 7F, FontStyle.Bold),
        Padding              = new Padding(6, 3, 6, 3),
        FlatAppearance =
        {
            BorderSize        = 0,
            MouseOverBackColor = Color.FromArgb(0, 136, 245),
            MouseDownBackColor = Color.FromArgb(0,  99, 177)
        }
    };


        #endregion

        private Panel elementsPanel;
        private Button buttonAddImg;
        private Button buttonAddTextElem;
        private FlowLayoutPanel siteMapPanel;
        private Panel panel1;
        private Button buttonReadUrls;
        private ListBox listBoxUrls;
        private TextBox textBoxOneUrl;
        private Label label1;
        private Button buttonStartBrowser;
        private Button buttonCopyFromUrl;
        private Button buttonCopyMany;
        private Button buttonDeleteItems;
        private Button buttonSelectPath;
        private Label label2;
        private TextBox textBoxSavePath;
        private TextBox textBoxLog;
    }
}