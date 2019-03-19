using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Globalization;

namespace FoxSnipTool {
    public partial class SettingForm : Form {
        private List<RadioButton> restPosList = new List<RadioButton>();


        [DllImport("user32")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint control, Keys vk);
        [DllImport("user32")]
        public static extern bool UnRegisterHotKey(IntPtr hWnd, int id);


        public SettingForm() {
            InitializeComponent();

            restPosList.Add(this.radioButton7);
            restPosList.Add(this.radioButton8);
            restPosList.Add(this.radioButton9);
            restPosList.Add(this.radioButton10);
            restPosList.Add(this.radioButton11);
            restPosList.Add(this.radioButton12);
            restPosList.Add(this.radioButton13);
            restPosList.Add(this.radioButton14);
            restPosList.Add(this.radioButton15);

            //注册热键
            RegisterHotKey(this.Handle, AppSettings.CtrlF1_ID, 2, Keys.D1);
            RegisterHotKey(this.Handle, AppSettings.CtrlF2_ID, 2, Keys.D2);
            this.notifyIcon1.ShowBalloonTip(3, "FoxSnipTool", "fox截图工具启动", ToolTipIcon.None);
        }
      

        private void Form1_Load(object sender, EventArgs e) {
            this.Hide();
            this.Visible = false;
            updateNormalPanel();
            updateSnipPanel();
            updateRestPanel();
            this.tabControl1.SelectTab(0);
            resizeWindowByTab(this.tabControl1.SelectedTab.Text);

        }

        //常规
        void updateNormalPanel() {
            this.checkBox1.Checked = AppSettings.AutoStart;
            this.checkBox7.Checked = AppSettings.AutoCache;
            this.checkBox3.Checked = AppSettings.CanFixSize;
            this.checkBox4.Checked = AppSettings.OpenRest;
            this.ini_input.Text = AppSettings.IniPath;
        }

        //截图
        void updateSnipPanel() {
            this.radioButton1.Checked = false;
            this.radioButton2.Checked = false;
            this.radioButton3.Checked = false;
            switch (AppSettings.QuickSaveFormat) {
                case ".png":
                    this.radioButton1.Checked = true;
                    break;
                case ".jpg":
                    this.radioButton2.Checked = true;
                    break;
                case ".bmp":
                    this.radioButton3.Checked = true;
                    break;
            }

            this.quickSave_input.Text = AppSettings.QuickSavePath;
            this.pictureBox1.BackColor = AppSettings.MaskColor;
            this.checkBox6.Checked = AppSettings.AutoToClipboard;
            this.fixSizeWidth.Value = AppSettings.FixSize.Width;
            this.fixSizeWidth.Minimum = AppSettings.MiniModeSize.Width;
            this.fixSizeWidth.Maximum = Screen.PrimaryScreen.Bounds.Width;
            this.fixSizeHeigth.Value = AppSettings.FixSize.Height;
            this.fixSizeHeigth.Minimum = AppSettings.MiniModeSize.Height;
            this.fixSizeWidth.Maximum = Screen.PrimaryScreen.Bounds.Height;
            this.numericUpDown1.Value = AppSettings.AutoCacheImgMax;
            this.numericUpDown1.Minimum = 0;
            this.textBox4.Text = AppSettings.AutoCachePath;
            this.groupBox5.Enabled = AppSettings.CanFixSize;
            this.groupBox4.Enabled = AppSettings.AutoCache;
        }

        //休息
        void updateRestPanel() {
            //this.checkBox2.Checked = AppSettings.OpenRest;
            //this.groupBox1.Enabled = this.checkBox2.Checked;
            this.radioButton4.Checked = AppSettings.RestBackgroundShowType == RestBackgroudType.Fixed;
            this.radioButton5.Checked = AppSettings.RestBackgroundShowType == RestBackgroudType.Random;
            this.radioButton6.Checked = AppSettings.RestBackgroundShowType == RestBackgroudType.Default;


            if (AppSettings.RestBackgroundShowType == RestBackgroudType.Default) {
                this.textBox5.Enabled = false;
                this.button6.Enabled = false;
                this.textBox3.Enabled = false;
                this.button1.Enabled = false;
            } else if (AppSettings.RestBackgroundShowType == RestBackgroudType.Fixed) {
                this.textBox3.Enabled = false;
                this.button1.Enabled = false;
                this.textBox5.Enabled = true;
                this.button6.Enabled = true;
            } else if (AppSettings.RestBackgroundShowType == RestBackgroudType.Random) {
                this.textBox5.Enabled = false;
                this.button6.Enabled = false;
                this.textBox3.Enabled = true;
                this.button1.Enabled = true;
            }

            this.dateTimePicker1.Text = AppSettings.WorkTimeSpan.ToString();
            this.dateTimePicker2.Text = AppSettings.RestTimeSpan.ToString();
            this.textBox5.Text = AppSettings.RestBackground;
            this.textBox3.Text = AppSettings.RestRandomBackgroudFolder;
            this.pictureBox4.BackColor = AppSettings.RestTimeFontColor;

            foreach (var rd in this.restPosList) {
                RestInfoPos pos = (RestInfoPos)Enum.Parse(typeof(RestInfoPos), rd.Tag.ToString());
                if (pos == AppSettings.RestInfoPoint) {
                    rd.Checked = true;
                }
            }

            this.panel1.Enabled = AppSettings.OpenRest;

        }


        #region 窗体操作
        //显示菜单消息
        public void Tip(string title,string content) {
            this.notifyIcon1.ShowBalloonTip(3, title, content, ToolTipIcon.Warning);
        }
        //更新提醒任务列表
        //public void UpdateTaskList() {
        //    var dic = TaskManager.GetInstance().TaskDic;
        //    // can do sort

        //    this.listView1.BeginUpdate();

        //    this.listView1.Items.Clear();
        //    foreach (var task in dic) {
        //        ListViewItem item = new ListViewItem();
        //        item.Text = task.Value.Title;
        //        item.Tag = task.Value.UID;

        //        ListViewItem.ListViewSubItem subItem1 = new ListViewItem.ListViewSubItem();
        //        //subItem1.Tag = TaskSubItemTag.;
        //        subItem1.Text = task.Value.TaskRunWay.ToString();
        //        item.SubItems.Add(subItem1);

        //        ListViewItem.ListViewSubItem subItem2 = new ListViewItem.ListViewSubItem();
        //        //subItem2.Tag = TaskSubItemTag.方式;
        //        subItem2.Text = task.Value.ShowWay.ToString();
        //        item.SubItems.Add(subItem2);

        //        ListViewItem.ListViewSubItem subItem3 = new ListViewItem.ListViewSubItem();
        //        //subItem3.Tag = TaskSubItemTag.时间;
        //        subItem3.Text = task.Value.TriggerDateTime.ToString("HH:mm:ss");
        //        item.SubItems.Add(subItem3);

        //        ListViewItem.ListViewSubItem subItem5 = new ListViewItem.ListViewSubItem();
        //        //subItem5.Tag = TaskSubItemTag.内容;
        //        subItem5.Text = task.Value.Desc;
        //        item.SubItems.Add(subItem5);

        //        this.listView1.Items.Add(item);
        //    }

        //    this.listView1.EndUpdate();
        //}

        private void notifyIcon1_DoubleClick(object sender, EventArgs e) {
            this.Show();
            this.Activate();
            updateNormalPanel();
            updateSnipPanel();
            updateRestPanel();
        }

        private void Form1_Resize(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) {
                this.Hide();
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            this.Hide();
            AppMgr.GetInstance().SaveIniConfig();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Dispose();
            Application.Exit();
        }

        private void settingPanelToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Show();
            this.Activate();
            updateNormalPanel();
            updateSnipPanel();
            updateRestPanel();
        }

        private void allTopPictureVisibleToolStripMenuItem_Click(object sender, EventArgs e) {
            AppMgr.GetInstance().AllTopPictureVisible(true);
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e) {
            AppMgr.GetInstance().RemoveAllTopPicture();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Show();
            this.Activate();
            this.tabControl1.SelectedIndex = 2;
        }

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case 0x0312:  //这个是window消息定义的注册的热键消息  
                    if (m.WParam.ToString() == AppSettings.CtrlF1_ID.ToString()) {
                        Console.WriteLine("热键: ctrl+f1");
                        AppMgr.GetInstance().EnterScreenMode();
                    } else if (m.WParam.ToString() == AppSettings.CtrlF2_ID.ToString()) {
                        Console.WriteLine("热键: ctrl+f2");
                        AppMgr.GetInstance().EnterPickColorMode();
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

       

        private void scan_btn_Click(object sender, EventArgs e) {
            AppMgr.GetInstance().ScanQuickSavePath();
            this.quickSave_input.Text = AppSettings.QuickSavePath;
        }

        private void default_btn_Click(object sender, EventArgs e) {
            AppMgr.GetInstance().ResetDefaultQuickSavePath();
            this.quickSave_input.Text = AppSettings.QuickSavePath;
        }

        private void audio_btn_Click(object sender, EventArgs e) {
            AppMgr.GetInstance().PlayAudio();
        }

        private void radioButton1_Click(object sender, EventArgs e) {
            if (this.radioButton1.Checked)
                AppSettings.QuickSaveFormat = ".png";
            else if(this.radioButton2.Checked)
                AppSettings.QuickSaveFormat = ".jpg";
            else if(this.radioButton3.Checked)
                AppSettings.QuickSaveFormat = ".bmp";
        }


        private void pictureBox1_Click(object sender, EventArgs e) {
            System.Windows.Media.Color outCol;
            bool ok = ColorPickerWPF.ColorPickerWindow.ShowDialog(out outCol);
            if (ok) {
                Console.WriteLine(outCol.ToString());
                Color newCol = System.Drawing.ColorTranslator.FromHtml(outCol.ToString());
                this.pictureBox1.BackColor = newCol;
                AppSettings.MaskColor = newCol;
                Refresh();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(this.linkLabel1.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(this.linkLabel2.Text);
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e) {
            AppMgr.GetInstance().AllTopPictureVisible(false);
        }

        private void add_task_btn_Click(object sender, EventArgs e) {
            TaskEditor editor = new TaskEditor();
            editor.ShowDialog();
        }


        private void checkBox2_CheckedChanged_1(object sender, EventArgs e) {
            //AppSettings.OpenRest = this.checkBox2.Checked;
            //this.groupBox1.Enabled = this.checkBox2.Checked;
            //AppMgr.GetInstance().OpenRestFuncion(this.checkBox2.Checked);
        }

        private void button3_Click(object sender, EventArgs e) {
            AppSettings.RestTimeSpan = TimeSpan.Parse(this.dateTimePicker2.Text);
            AppSettings.WorkTimeSpan = TimeSpan.Parse(this.dateTimePicker1.Text);
            AppMgr.GetInstance().RefreshRestTimer();
            AppMgr.GetInstance().ShowTip("休息提醒","设置成功");
        }

        private void button1_Click(object sender, EventArgs e) {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择休息背景图片随机目录";
            if (folder.ShowDialog() == DialogResult.OK) {
                AppSettings.RestRandomBackgroudFolder  = folder.SelectedPath;
                this.textBox3.Text = AppSettings.RestRandomBackgroudFolder;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e) {
            if(this.colorDialog1.ShowDialog() == DialogResult.OK) {
                this.pictureBox4.BackColor = this.colorDialog1.Color;
                AppSettings.RestTimeFontColor = this.colorDialog1.Color;
            }
        }

        private void restInfoPosRadioChange(object sender, EventArgs e) {
            RadioButton r = sender as RadioButton;
            RestInfoPos pos = (RestInfoPos)Enum.Parse(typeof(RestInfoPos),r.Tag.ToString());
            Console.WriteLine(pos);
            AppSettings.RestInfoPoint = pos;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.AutoStart = this.checkBox1.Checked;
            AppMgr.GetInstance().AutoStart(AppSettings.AutoStart);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            //AppSettings.CanFixSize = this.checkBox3.Checked;
            //this.panel1.Enabled = this.checkBox3.Checked;
        }

        private void fixSizeWidth_ValueChanged(object sender, EventArgs e)
        {
            AppSettings.FixSize.Width = System.Convert.ToInt32( this.fixSizeWidth.Value);
        }

        private void fixSizeHeigth_ValueChanged(object sender, EventArgs e)
        {
            AppSettings.FixSize.Height = System.Convert.ToInt32(this.fixSizeHeigth.Value);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(this.linkLabel4.Text);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
            //调整窗口大小
            resizeWindowByTab(this.tabControl1.SelectedTab.Text);
        }

        void resizeWindowByTab(string name) {
            switch (name) {
                case "常规":
                    this.Size = new Size(590, 337);
                    break;
                case "截图":
                    this.Size = new Size(590, 500);
                    break;
                case "休息":
                    this.Size = new Size(590, 429);
                    break;
                case "快捷键":
                    this.Size = new Size(590, 280);
                    break;
                case "关于":
                    this.Size = new Size(590, 302);
                    break;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e) {
            AppSettings.AutoCache = this.checkBox7.Checked;
            this.groupBox4.Enabled = AppSettings.AutoCache;
            //AppMgr.GetInstance().SaveIniConfig();
        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e) {
            AppSettings.CanFixSize = this.checkBox3.Checked;
            this.groupBox5.Enabled = AppSettings.CanFixSize;
            //AppMgr.GetInstance().SaveIniConfig();
        }

        private void checkBox4_CheckedChanged_1(object sender, EventArgs e) {
            AppSettings.OpenRest = this.checkBox4.Checked;
            this.panel1.Enabled = AppSettings.OpenRest;
            AppMgr.GetInstance().OpenRestFuncion(AppSettings.OpenRest);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e) {
            AppSettings.AutoToClipboard = this.checkBox6.Checked;
            Console.WriteLine("AppSettings.AutoToClipboard = " + AppSettings.AutoToClipboard);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            AppSettings.AutoCacheImgMax = Decimal.ToInt32( this.numericUpDown1.Value);
        }

        private void button4_Click(object sender, EventArgs e) {
            AppSettings.AutoCachePath = Application.StartupPath + "\\Cache\\";
            this.textBox4.Text = AppSettings.AutoCachePath;
        }

        private void button2_Click(object sender, EventArgs e) {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择自动缓存目录";
            if (folder.ShowDialog() == DialogResult.OK) {
                AppSettings.AutoCachePath = folder.SelectedPath;
                this.textBox4.Text = AppSettings.AutoCachePath;
            }
        }

        private void button7_Click(object sender, EventArgs e) {
            AppMgr.GetInstance().ClearAutoCacheFolder();
        }

        private void backgroundTypeChanged(object sender,EventArgs e) {
            RadioButton r = sender as RadioButton;
            RestBackgroudType tp = (RestBackgroudType)Enum.Parse(typeof(RestBackgroudType), r.Tag.ToString());
            AppSettings.RestBackgroundShowType = tp;

            if(AppSettings.RestBackgroundShowType == RestBackgroudType.Default) {
                this.textBox5.Enabled = false;
                this.button6.Enabled = false;
                this.textBox3.Enabled = false;
                this.button1.Enabled = false;
            }else if(AppSettings.RestBackgroundShowType == RestBackgroudType.Fixed) {
                this.textBox3.Enabled = false;
                this.button1.Enabled = false;
                this.textBox5.Enabled = true;
                this.button6.Enabled = true;
            } else if(AppSettings.RestBackgroundShowType == RestBackgroudType.Random) {
                this.textBox5.Enabled = false;
                this.button6.Enabled = false;
                this.textBox3.Enabled = true;
                this.button1.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg(*.jpg)|*.jpg";
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                AppSettings.RestBackground = openFileDialog.FileName;
                this.textBox5.Text = AppSettings.RestBackground;
            }

        }

        
    }
}
