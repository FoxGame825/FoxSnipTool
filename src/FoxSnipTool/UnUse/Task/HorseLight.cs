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
    public enum HorseLightShowPos {
        顶部,
        中心,
        底部
    }

    public enum HorseLightAnimation {
        左到右,
        右到左,
        缩放,
        淡入,
    }

    public partial class HorseLight : Form {
        private int Duration;

        public HorseLight(string content) {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;    //隐藏窗体边框

            int screenW = Screen.PrimaryScreen.Bounds.Width;
            int screenH = Screen.PrimaryScreen.Bounds.Height;

            this.Width = screenW;
            this.Height = screenH / 5;
            this.StartPosition = FormStartPosition.Manual;

            switch (AppSettings.HorseLightPos) {
                case HorseLightShowPos.顶部:
                    this.Location = new Point(0, 0);
                    break;
                case HorseLightShowPos.中心:
                    this.Location = new Point(0, screenH / 2 - this.Height / 2);
                    break;
                case HorseLightShowPos.底部:
                    this.Location = new Point(0, screenH - this.Height);
                    break;
            }

            this.label1.Text = content;
            this.label1.ForeColor = AppSettings.HorseLightFontColor;
            this.label1.Location = new Point(this.Width / 2 - this.label1.Width / 2, this.Height / 2 - this.label1.Height / 2);
            this.BackColor = AppSettings.HorseLightBackColor;

            switch (AppSettings.HorseLightAni) {
                case HorseLightAnimation.左到右:
                    Win32.AnimateWindow(this.Handle, 1000, Win32.AW_HOR_POSITIVE);
                    break;
                case HorseLightAnimation.右到左:
                    Win32.AnimateWindow(this.Handle, 1000, Win32.AW_HOR_NEGATIVE);
                    break;
                case HorseLightAnimation.缩放:
                    Win32.AnimateWindow(this.Handle, 1000, Win32.AW_CENTER);
                    break;
                case HorseLightAnimation.淡入:
                    Win32.AnimateWindow(this.Handle, 1000, Win32.AW_BLEND);
                    break;
                default:
                    Win32.AnimateWindow(this.Handle, 1000, Win32.AW_BLEND);
                    break;
            }

            Duration = AppSettings.HorseLightMinShowDuration;
        }

        public class Win32 {
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

        private void HorseLight_Load(object sender, EventArgs e) {
            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            Duration -= this.timer1.Interval;
            if(Duration <= 0) {
                this.timer1.Dispose();
                AppManager.GetInstance().CloseHorseLight();
            }
        }

        private void HorseLight_FormClosing(object sender, FormClosingEventArgs e) {
            Win32.AnimateWindow(this.Handle, 1000, Win32.AW_SLIDE | Win32.AW_HIDE | Win32.AW_BLEND);
        }
    }
}
