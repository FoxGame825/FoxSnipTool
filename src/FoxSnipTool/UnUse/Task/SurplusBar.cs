using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoxSnipTool {
    class SurplusBar:ProgressBar {
        public string Content = "剩余时间:xxxx";


        public SurplusBar() {
            SetStyle(ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void SetValue(int val) {
            val = Math.Max(val, this.Minimum);
            this.Value = val;
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            Rectangle rect = ClientRectangle;
            Graphics g = e.Graphics;

            ProgressBarRenderer.DrawHorizontalBar(g, rect);
            rect.Inflate(-1, -1);
            if (Value > 0) {
                var clip = new Rectangle(rect.X, rect.Y, (int)((float)Value / Maximum * rect.Width), rect.Height);
                ProgressBarRenderer.DrawHorizontalChunks(g, clip);
            }


            SizeF sz = g.MeasureString(Content, AppSettings.DefaultFont);
            var location = new PointF(rect.Width / 2 - sz.Width / 2, rect.Height / 2 - sz.Height / 2 +2);
            g.DrawString(Content, AppSettings.DefaultFont, Brushes.Black, location);
            
        }
    }
}
