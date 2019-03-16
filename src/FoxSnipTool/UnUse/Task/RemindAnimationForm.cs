using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FoxSnipTool
{
    public partial class RemindAnimationForm : Form
    {

        AnimateImage image;
        private int Duration;
        private string Content;

        public RemindAnimationForm(string content) {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;    //隐藏窗体边框
            this.TransparencyKey = this.BackColor;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            image = new AnimateImage(null/*Properties.Resources.defaultAni*/);
            image.OnFrameChanged += new EventHandler<EventArgs>(OnFrameChanged);
            this.StartPosition = FormStartPosition.CenterScreen;

            //this.BackgroundImage = Properties.Resources.defaultAni;

            Win32.AnimateWindow(this.Handle, 200, Win32.AW_CENTER);
            this.label1.Text = content;
            this.label1.ForeColor = AppSettings.RemindAniFontColor;
            this.label1.BackColor = Color.Gray;

            Duration = AppSettings.HorseLightMinShowDuration;
            Content = content;
        }


        private void OnFrameChanged(object o, EventArgs e) {
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            lock (image.Image) {
                e.Graphics.DrawImage(image.Image, 0, 0, this.Width, this.Height);
            }
            e.Graphics.DrawString(Content, this.label1.Font, Brushes.BurlyWood, 0, 0);
        }

        private void RemindAnimationForm_Load(object sender, EventArgs e) {
            image.Play();
            this.timer1.Start();
        }


        public class Win32
        {
            public const Int32 AW_HOR_POSITIVE = 0x00000001; // 从左到右打开窗口
            public const Int32 AW_HOR_NEGATIVE = 0x00000002; // 从右到左打开窗口
            public const Int32 AW_VER_POSITIVE = 0x00000004; // 从上到下打开窗口
            public const Int32 AW_VER_NEGATIVE = 0x00000008; // 从下到上打开窗口
            public const Int32 AW_CENTER = 0x00000010;
            public const Int32 AW_HIDE = 0x00010000; // 在窗体卸载时若想使用本函数就得加上此常量
            public const Int32 AW_ACTIVATE = 0x00020000; //在窗体通过本函数打开后，默认情况下会失去焦点，除非加上本常量
            public const Int32 AW_SLIDE = 0x00040000;
            public const Int32 AW_BLEND = 0x00080000; // 淡入淡出效果
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern bool AnimateWindow(
            IntPtr hwnd, // handle to window
            int dwTime, // duration of animation
            int dwFlags // animation type
            );
        }

        private void RemindAnimationForm_FormClosing(object sender, FormClosingEventArgs e) {
            Win32.AnimateWindow(this.Handle, 1000, Win32.AW_SLIDE | Win32.AW_HIDE | Win32.AW_BLEND);
        }

        private void timer1_Tick(object sender, EventArgs e) {
            Duration -= this.timer1.Interval;
            if (Duration <= 0) {
                this.timer1.Dispose();
                AppMgr.GetInstance().CloseRemindAnimateForm();
            }
        }
    }
}
