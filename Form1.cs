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

        private void buttonStartBrowser_Click(object sender, EventArgs e)
        {

        }

        private void buttonCopyFromUrl_Click(object sender, EventArgs e)
        {

        }

        private void buttonCopyMany_Click(object sender, EventArgs e)
        {

        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {

        }
    }
}
