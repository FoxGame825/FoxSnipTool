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


namespace FoxSnipTool {
    public partial class Form1 : Form {
        private List<RadioButton> restPosList = new List<RadioButton>();


        [DllImport("user32")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint control, Keys vk);
        [DllImport("user32")]
        public static extern bool UnRegisterHotKey(IntPtr hWnd, int id);


        public Form1() {
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
            updateFormUI();
        }

        void updateFormUI() {
            //截图
            this.checkBox1.Checked = AppSettings.AutoStart;
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
            this.ini_input.Text = AppSettings.IniPath;
            this.pictureBox1.BackColor = AppSettings.MaskColor;

            this.checkBox3.Checked = false;
            this.panel1.Enabled = false;
            this.fixSizeWidth.Value = AppSettings.FixSize.Width;
            this.fixSizeWidth.Minimum = 1;
            this.fixSizeWidth.Maximum = Screen.PrimaryScreen.Bounds.Width;
            this.fixSizeHeigth.Value = AppSettings.FixSize.Height;
            this.fixSizeHeigth.Minimum = 1;
            this.fixSizeWidth.Maximum = Screen.PrimaryScreen.Bounds.Height;

            //休息
            this.checkBox2.Checked = AppSettings.OpenRest;
            this.groupBox1.Enabled = this.checkBox2.Checked;
            this.dateTimePicker1.Text = AppSettings.WorkTimeSpan.ToString();
            this.dateTimePicker2.Text = AppSettings.RestTimeSpan.ToString();
            this.textBox3.Text = AppSettings.RestBackground;
            this.pictureBox4.BackColor = AppSettings.RestTimeFontColor;

            foreach (var rd in this.restPosList) {
                RestInfoPos pos = (RestInfoPos)Enum.Parse(typeof(RestInfoPos), rd.Tag.ToString());
                if (pos == AppSettings.RestInfoPoint) {
                    rd.Checked = true;
                }
            }

            //
        }

        private void Form1_Activated(object sender, EventArgs e) {
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
            updateFormUI();
        }

        private void Form1_Resize(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) {
                this.Hide();
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            this.Hide();
            AppManager.GetInstance().SaveIniConfig();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Dispose();
            Application.Exit();
        }

        private void settingPanelToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Show();
            this.Activate();
            updateFormUI();
        }

        private void allTopPictureVisibleToolStripMenuItem_Click(object sender, EventArgs e) {
            AppManager.GetInstance().AllTopPictureVisible(true);
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e) {
            AppManager.GetInstance().RemoveAllTopPicture();
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
                        AppManager.GetInstance().EnterScreenMode();
                    } else if (m.WParam.ToString() == AppSettings.CtrlF2_ID.ToString()) {
                        Console.WriteLine("热键: ctrl+f2");
                        AppManager.GetInstance().EnterPickColorMode();
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

       

        private void scan_btn_Click(object sender, EventArgs e) {
            AppManager.GetInstance().ScanQuickSavePath();
            this.quickSave_input.Text = AppSettings.QuickSavePath;
        }

        private void default_btn_Click(object sender, EventArgs e) {
            AppManager.GetInstance().ResetDefaultQuickSavePath();
            this.quickSave_input.Text = AppSettings.QuickSavePath;
        }

        private void audio_btn_Click(object sender, EventArgs e) {
            AppManager.GetInstance().PlayAudio();
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
            AppManager.GetInstance().AllTopPictureVisible(false);
        }

        private void add_task_btn_Click(object sender, EventArgs e) {
            TaskEditor editor = new TaskEditor();
            editor.ShowDialog();
        }


        private void checkBox2_CheckedChanged_1(object sender, EventArgs e) {
            AppSettings.OpenRest = this.checkBox2.Checked;
            this.groupBox1.Enabled = this.checkBox2.Checked;
            AppManager.GetInstance().OpenRestFuncion(this.checkBox2.Checked);
        }

        private void button3_Click(object sender, EventArgs e) {
            AppSettings.RestTimeSpan = TimeSpan.Parse(this.dateTimePicker2.Text);
            AppSettings.WorkTimeSpan = TimeSpan.Parse(this.dateTimePicker1.Text);
            AppManager.GetInstance().RefreshRestTimer();
            AppManager.GetInstance().ShowTip("休息提醒","设置成功");
        }

        private void button1_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg(*.jpg)|*.jpg";
            if(openFileDialog.ShowDialog() == DialogResult.OK) {
                AppSettings.RestBackground = openFileDialog.FileName;
                this.textBox3.Text = AppSettings.RestBackground;
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
            AppManager.GetInstance().AutoStart(AppSettings.AutoStart);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.CanFixSize = this.checkBox3.Checked;
            this.panel1.Enabled = this.checkBox3.Checked;
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
    }
}
