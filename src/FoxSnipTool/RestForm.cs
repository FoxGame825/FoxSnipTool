using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FoxSnipTool {
    public partial class RestForm : Form {
        private TimeSpan closeDt; //关闭的时间
        private TimeSpan openDt;    //打开的时间

        public RestForm(TimeSpan span) {
            InitializeComponent();
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;    //隐藏窗体边框
            this.Bounds = Screen.PrimaryScreen.Bounds;
            showBg();
            this.label1.ForeColor = AppSettings.RestTimeFontColor;
            openDt = DateTime.Now.TimeOfDay;
            closeDt = openDt.Add(span);
            this.surplusBar1.Minimum = 0;
            this.surplusBar1.Maximum = System.Convert.ToInt32( Math.Max(0, (closeDt - openDt).TotalSeconds));
            this.timer1.Start();

            this.switchBgToolStripMenuItem.Enabled = AppSettings.RestBackgroundShowType == RestBackgroudType.Random;
        }

        void showBg() {
            bool bOK = false;
            if(AppSettings.RestBackgroundShowType == RestBackgroudType.Fixed) {
                if (File.Exists(AppSettings.RestBackground)) {
                    this.BackgroundImage = Image.FromFile(AppSettings.RestBackground);
                    bOK = true;
                }
            } else if(AppSettings.RestBackgroundShowType == RestBackgroudType.Random) {
                var img = getRandomFiles(AppSettings.RestRandomBackgroudFolder);
                if (img != null) {
                    this.BackgroundImage = img;
                    bOK = true;
                }
            }

            if (!bOK) {
                this.BackgroundImage = Properties.Resources.rest;
            }
        }

        Image getRandomFiles(string path) {
            if (Directory.Exists(path)) {
                var fls = Directory.GetFiles(path, "*.jpg", SearchOption.TopDirectoryOnly);
                if(fls !=null && fls.Length > 0) {
                    var rand = new System.Random();
                    var sel = fls[rand.Next(0, fls.Length - 1)];
                    return Image.FromFile(sel);
                }
            }
            return null;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            var temp = closeDt - DateTime.Now.TimeOfDay;
            this.TopMost = true;
            this.label1.Text = DateTime.Now.ToLongTimeString();
            this.surplusBar1.Content = string.Format("剩余时间:{0}", temp.ToString(@"hh\:mm\:ss"));
            this.surplusBar1.SetValue( System.Convert.ToInt32(temp.TotalSeconds)); 
            if (temp.Ticks <= 0) {
                close();
            }
        }

        private void RestForm_Load(object sender, EventArgs e) {
            int offsetW = this.panel1.Width / 4;
            int offsetH = this.panel1.Height / 4;
            switch (AppSettings.RestInfoPoint) {
                case RestInfoPos.TopLeft:
                    this.panel1.Location = new Point(offsetW, offsetH);
                    break;
                case RestInfoPos.Top:
                    this.panel1.Location = new Point(this.Width / 2 - this.panel1.Width/2, offsetH);
                    break;
                case RestInfoPos.TopRight:
                    this.panel1.Location = new Point(this.Width - offsetW - this.panel1.Width, offsetH);
                    break;
                case RestInfoPos.Left:
                    this.panel1.Location = new Point(offsetW, this.Height / 2 - this.panel1.Height/2);
                    break;
                case RestInfoPos.Center:
                    this.panel1.Location = new Point(this.Width / 2 - this.panel1.Width/2, this.Height / 2 - this.panel1.Height/2);
                    break;
                case RestInfoPos.Right:
                    this.panel1.Location = new Point(this.Width - offsetW - this.panel1.Width, this.Height / 2 - this.panel1.Height/2);
                    break;
                case RestInfoPos.ButtomLeft:
                    this.panel1.Location = new Point(offsetW, this.Height - offsetH - this.panel1.Height);
                    break;
                case RestInfoPos.Buttom:
                    this.panel1.Location = new Point(this.Width / 2 - this.panel1.Width/2, this.Height - offsetH - this.panel1.Height);
                    break;
                case RestInfoPos.ButtomRight:
                    this.panel1.Location = new Point(this.Width - offsetW - this.panel1.Width, this.Height - offsetH - this.panel1.Height);
                    break;
            }

            this.Activate();

        }

        private void RestForm_Resize(object sender, EventArgs e) {
            this.panel1.Location = new Point(this.Width / 2 - this.panel1.Width / 2, this.Height / 2 - this.panel1.Height / 2);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
            close();
        }

        private void delay1hToolStripMenuItem_Click(object sender, EventArgs e) {
            refreshDt(new TimeSpan(1, 0, 0));
        }

        private void delay30mToolStripMenuItem_Click(object sender, EventArgs e) {
            refreshDt(new TimeSpan(0, 30, 0));
        }

        private void delay10mToolStripMenuItem_Click(object sender, EventArgs e) {
            refreshDt(new TimeSpan(0, 10, 0));
        }

        private void delay1mToolStripMenuItem_Click(object sender, EventArgs e) {
            refreshDt(new TimeSpan(0, 1, 0));
        }

        private void delay5mToolStripMenuItem_Click(object sender, EventArgs e) {
            refreshDt(new TimeSpan(0, 5, 0));
        }

        void refreshDt(TimeSpan tm) {
            closeDt = closeDt.Add(tm);
            this.surplusBar1.Minimum = 0;
            this.surplusBar1.Maximum = System.Convert.ToInt32(Math.Max(0, (closeDt - openDt).TotalSeconds));
        }

        void close() {
            this.Close();
            this.Dispose();
            AppMgr.GetInstance().RefreshRestTimer();
        }

        private void switchBgToolStripMenuItem_Click(object sender, EventArgs e) {
            if(AppSettings.RestBackgroundShowType == RestBackgroudType.Random) {
                var img = getRandomFiles(AppSettings.RestRandomBackgroudFolder);
                if(img != null) {
                    this.BackgroundImage = img;
                }
            }
        }
    }
}
