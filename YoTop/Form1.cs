using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoTop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "last.txt"))
            {
                webBrowser1.Navigate(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "last.txt"));
            }
            else {
                webBrowser1.Navigate("https://www.youtube.com/watch?v=21lu5zktQiQ");
            }
           

        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        double op = 1;
        bool isAdd = false;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            const Int32 WM_NCLBUTTONDBLCLK = 163;

            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                if (!isAdd)
                {
                    op -= 0.2;

                    if (op <= 0.2)
                    {
                        op = 0.2;
                        isAdd = true;
                    }
                }
                else
                {
                    op += 0.2;

                    if (op >= 1)
                    {
                        op = 1;
                        isAdd = false;
                    }
                }

                this.Opacity = op;
            }

        }

        private void btnChangeUrl_Click(object sender, EventArgs e)
        {

            // Source reference : https://dotblogs.com.tw/aquarius6913/2014/09/03/146444
            this.TopMost = false;
            string strPath = "https://www.youtube.com/watch?v=21lu5zktQiQ";
            var result = InputBox("https://www.youtube.com/watch?v=21lu5zktQiQ", "The Site you wanna go :", ref strPath);

            if (result == DialogResult.OK)
            {
                webBrowser1.Navigate(strPath);
                this.TopMost = true;
            }
            else
            {
                this.TopMost = true;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.youtube.com/watch?v=21lu5zktQiQ");
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fun toy from Donma Hsu.\r\n welcome visit my github : https://github.com/donma");
        }

        private void btnHideBorder_Click(object sender, EventArgs e)
        {
            if (this.FormBorderStyle != FormBorderStyle.None)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                btnHideBorder.Text = "-MOVABLE-";
            }
            else {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                btnHideBorder.Text = "-PIN-";
            }
        }

   

        private void statusStrip1_DoubleClick(object sender, EventArgs e)
        {
            if (this.FormBorderStyle != FormBorderStyle.None)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                btnHideBorder.Text = "-MOVABLE-";
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                btnHideBorder.Text = "-PIN-";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "last.txt", webBrowser1.Url.AbsoluteUri);
        }
    }
}
