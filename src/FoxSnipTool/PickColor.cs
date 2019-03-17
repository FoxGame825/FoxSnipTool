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
    public partial class PickColor : Form {
        [DllImport("gdi32")]
        public static extern uint GetPixel(IntPtr hDC, int nXPos, int nYPos);
        [DllImport("user32")]
        public static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);


        private bool RunPick;
        private IntPtr _hdc = IntPtr.Zero;
        private readonly IntPtr _hWnd = IntPtr.Zero;

        public bool CanPick {
            get {
                return RunPick && this.Visible;
            }
        }

        //采样颜色
        public Color SamplingColor {
            get {
                System.Drawing.Point p = MousePosition;
                this.pos_label.Text = string.Format("X:{0},Y:{1}", p.X, p.Y);

                uint color = GetPixel(_hdc, p.X, p.Y);
                byte r = GetRValue(color);
                byte g = GetGValue(color);
                byte b = GetBValue(color);
                return Color.FromArgb(r, g,b);
            }
        }

        public PickColor() {
            InitializeComponent();
            this.timer1.Interval = 100;
            this.timer1.Tick += Timer1_Tick;
        }

        private void Timer1_Tick(object sender, EventArgs e) {
            System.Drawing.Point p = MousePosition;
            this.pos_label.Text = string.Format("X:{0},Y:{1}", p.X, p.Y);

            uint color = GetPixel(_hdc, p.X, p.Y);
            byte r = GetRValue(color);
            byte g = GetGValue(color);
            byte b = GetBValue(color);

            this.rgb_label.Text = string.Format("RGB:{0},{1},{2}", r, g, b);
            this.pictureBox1.BackColor = Color.FromArgb(r, g, b);
            hex_label.Text = "十六进制:#" + r.ToString("X").PadLeft(2, '0') + g.ToString("X").PadLeft(2, '0') +
                               b.ToString("X").PadLeft(2, '0');
        }

        private void button1_Click(object sender, EventArgs e) {
            if (RunPick) {
                this.timer1.Stop();
                this.button1.Text = "取色";
                ReleaseDC(_hWnd, _hdc);
                Cursor = Cursors.Default;
            } else {
                this.timer1.Start();
                this.button1.Text = "停止";
                _hdc = GetDC(_hWnd);
                Cursor = Cursors.Cross;
            }

            RunPick = !RunPick;
        }

        public static byte GetRValue(uint rgb) {
            return (byte)rgb;
        }
        public static byte GetGValue(uint rgb) {
            return (byte)(((ushort)(rgb)) >> 8);
        }
        public static byte GetBValue(uint rgb) {
            return (byte)(rgb >> 16);
        }
    }
}
