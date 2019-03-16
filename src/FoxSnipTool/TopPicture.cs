using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;


namespace FoxSnipTool {
    public class TopPicture :Form {

        #region 窗体 置顶
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern System.IntPtr GetForegroundWindow();
        #endregion

        #region 无边框 窗体阴影 
        private const int CS_DropSHADOW = 0x20000;
        private ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem RemoveToolStripMenuItem;
        private ToolStripMenuItem BorderColorToolStripMenuItem;
        private ToolStripMenuItem toClipBoardToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem quickSaveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toumingToolStripMenuItem;
        private ToolStripMenuItem per10ToolStripMenuItem;
        private ToolStripMenuItem per30ToolStripMenuItem;
        private ToolStripMenuItem per50ToolStripMenuItem;
        private ToolStripMenuItem per70ToolStripMenuItem;
        private ToolStripMenuItem per100ToolStripMenuItem;
        private const int GCL_STYLE = (-26);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);
        #endregion


        private bool mousePressd;
        private Point mouseStartPoint;
        private float scaleFactor;
        private Size originSize;
        private Timer drawInfoTimer;
        private int drawScaleInfoCD;
        private bool miniMode;
        private Color bordeColor;
        private long id;
        public long ID { get { return id; } }

        public TopPicture(Image img) {
            InitializeComponent();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW); //无边框阴影
            this.FormBorderStyle = FormBorderStyle.None;    //隐藏窗体边框
            this.BackgroundImage = img;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Width = img.Width;
            this.Height = img.Height;
            this.originSize = this.Bounds.Size;
            this.scaleFactor = 1f;
            this.drawInfoTimer = new Timer();
            this.drawInfoTimer.Interval = 100;
            this.drawInfoTimer.Tick += DrawInfoTimer_Tick;
            mousePressd = false;
            miniMode = false;
            bordeColor = AppSettings.RandomBorderColor();
            id = DateTime.Now.ToFileTime();
        }

        private void DrawInfoTimer_Tick(object sender, EventArgs e) {
            this.drawScaleInfoCD -= this.drawInfoTimer.Interval;
            if(drawScaleInfoCD <= 0) {
                this.drawInfoTimer.Stop();
                this.drawScaleInfoCD = 0;
                Refresh();
            }
        }

        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BorderColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toClipBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.quickSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.RemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toumingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.per10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.per30ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.per50ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.per70ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.per100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BorderColorToolStripMenuItem,
            this.toClipBoardToolStripMenuItem,
            this.toolStripSeparator2,
            this.quickSaveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.RemoveToolStripMenuItem,
            this.toumingToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 148);
            // 
            // BorderColorToolStripMenuItem
            // 
            this.BorderColorToolStripMenuItem.Image = global::FoxSnipTool.Properties.Resources.color_wheel_24px_526860_easyicon_net;
            this.BorderColorToolStripMenuItem.Name = "BorderColorToolStripMenuItem";
            this.BorderColorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.BorderColorToolStripMenuItem.Text = "边框颜色";
            this.BorderColorToolStripMenuItem.Click += new System.EventHandler(this.BorderColorToolStripMenuItem_Click);
            // 
            // toClipBoardToolStripMenuItem
            // 
            this.toClipBoardToolStripMenuItem.Image = global::FoxSnipTool.Properties.Resources.clipboard_24px_28293_easyicon_net;
            this.toClipBoardToolStripMenuItem.Name = "toClipBoardToolStripMenuItem";
            this.toClipBoardToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.toClipBoardToolStripMenuItem.Text = "放入剪切板";
            this.toClipBoardToolStripMenuItem.Click += new System.EventHandler(this.toClipBoardToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // quickSaveToolStripMenuItem
            // 
            this.quickSaveToolStripMenuItem.Image = global::FoxSnipTool.Properties.Resources.folder_lightning_power_24px_7077_easyicon_net;
            this.quickSaveToolStripMenuItem.Name = "quickSaveToolStripMenuItem";
            this.quickSaveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quickSaveToolStripMenuItem.Text = "快速保存";
            this.quickSaveToolStripMenuItem.Click += new System.EventHandler(this.quickSaveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::FoxSnipTool.Properties.Resources.disk_save_all_24px_4650_easyicon_net;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "另存为";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // RemoveToolStripMenuItem
            // 
            this.RemoveToolStripMenuItem.Image = global::FoxSnipTool.Properties.Resources.delete_24px_520120_easyicon_net;
            this.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem";
            this.RemoveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.RemoveToolStripMenuItem.Text = "删除";
            this.RemoveToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
            // 
            // toumingToolStripMenuItem
            // 
            this.toumingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.per100ToolStripMenuItem,
            this.per70ToolStripMenuItem,
            this.per50ToolStripMenuItem,
            this.per30ToolStripMenuItem,
            this.per10ToolStripMenuItem});
            this.toumingToolStripMenuItem.Name = "toumingToolStripMenuItem";
            this.toumingToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.toumingToolStripMenuItem.Text = "透明度";
            // 
            // per10ToolStripMenuItem
            // 
            this.per10ToolStripMenuItem.Name = "per10ToolStripMenuItem";
            this.per10ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.per10ToolStripMenuItem.Tag = "10";
            this.per10ToolStripMenuItem.Text = "10%";
            this.per10ToolStripMenuItem.Click += new System.EventHandler(this.setOpacity);
            // 
            // per30ToolStripMenuItem
            // 
            this.per30ToolStripMenuItem.Name = "per30ToolStripMenuItem";
            this.per30ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.per30ToolStripMenuItem.Tag = "30";
            this.per30ToolStripMenuItem.Text = "30%";
            this.per30ToolStripMenuItem.Click += new System.EventHandler(this.setOpacity);
            // 
            // per50ToolStripMenuItem
            // 
            this.per50ToolStripMenuItem.Name = "per50ToolStripMenuItem";
            this.per50ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.per50ToolStripMenuItem.Tag = "50";
            this.per50ToolStripMenuItem.Text = "50%";
            this.per50ToolStripMenuItem.Click += new System.EventHandler(this.setOpacity);
            // 
            // per70ToolStripMenuItem
            // 
            this.per70ToolStripMenuItem.Name = "per70ToolStripMenuItem";
            this.per70ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.per70ToolStripMenuItem.Tag = "70";
            this.per70ToolStripMenuItem.Text = "70%";
            this.per70ToolStripMenuItem.Click += new System.EventHandler(this.setOpacity);
            // 
            // per100ToolStripMenuItem
            // 
            this.per100ToolStripMenuItem.Checked = true;
            this.per100ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.per100ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.per100ToolStripMenuItem.Name = "per100ToolStripMenuItem";
            this.per100ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.per100ToolStripMenuItem.Tag = "s";
            this.per100ToolStripMenuItem.Text = "100%";
            this.per100ToolStripMenuItem.Click += new System.EventHandler(this.setOpacity);
            // 
            // TopPicture
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Name = "TopPicture";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.TopPicture_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TopPicture_Paint);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void TopPicture_Load(object sender, EventArgs e) {
            SetWindowPos(GetForegroundWindow(), -1, 0, 0, 0, 0, 1 | 2);
            this.TopMost = true;
        }


        #region 窗体控制
        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            switch (e.KeyCode) {
                case Keys.Left:
                    this.Location = new Point(this.Location.X - 1, this.Location.Y);
                    break;
                case Keys.Right:
                    this.Location = new Point(this.Location.X + 1, this.Location.Y);
                    break;
                case Keys.Up:
                    this.Location = new Point(this.Location.X, this.Location.Y -1);
                    break;
                case Keys.Down:
                    this.Location = new Point(this.Location.X, this.Location.Y + 1);
                    break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            base.OnKeyUp(e);
            if(e.KeyCode == Keys.Escape) {
                //this.Close();
                AppMgr.GetInstance().RemoveTopPicture(this);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if(e.Button == MouseButtons.Left) {
                mousePressd = true;
                mouseStartPoint = e.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (mousePressd) {
                Point offset = new Point(e.Location.X - mouseStartPoint.X, e.Location.Y - mouseStartPoint.Y);
                this.Location = new Point(this.Location.X + offset.X, this.Location.Y + offset.Y);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            if(e.Button == MouseButtons.Left) {
                mousePressd = false;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);
            scaleFactor = e.Delta > 0 ? scaleFactor += AppSettings.WheelScalePer : scaleFactor -= AppSettings.WheelScalePer;
            scaleFactor = Math.Max(scaleFactor, AppSettings.MinScaleFactor);

            this.Width = Convert.ToInt32( this.originSize.Width * scaleFactor);
            this.Height = Convert.ToInt32(this.originSize.Height * scaleFactor);

            this.drawScaleInfoCD = AppSettings.DrawScaleInfoCD;
            if (!this.drawInfoTimer.Enabled) {
                this.drawInfoTimer.Start();
            }
            Refresh();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e) {
            base.OnMouseDoubleClick(e);
            if (miniMode) {
                this.Width = this.originSize.Width;
                this.Height = this.originSize.Height;
                miniMode = false;
                Refresh();
            } else {
                this.Width = AppSettings.MiniModeSize.Width;
                this.Height = AppSettings.MiniModeSize.Height;
                miniMode = true;
            }
        }
        #endregion


        #region paint
        private void TopPicture_Paint(object sender, PaintEventArgs e) {
            var g = e.Graphics;

            Pen p = new Pen(bordeColor);
            g.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);

            if(this.drawScaleInfoCD > 0) {
                DrawScaleInfo(g);
            }
        }

        private void DrawScaleInfo(Graphics g) {
            string info = string.Format("{0:f2}%", scaleFactor * 100);
            Size textSize = TextRenderer.MeasureText(info, this.Font);
            Rectangle textRect = new Rectangle(
                0, 0, textSize.Width, textSize.Height);
            g.FillRectangle(Brushes.Black, textRect);
            g.DrawString(info, this.Font, Brushes.White, 0, 0);
        }
        #endregion



        #region 操作
        //图片放入剪切板
        private void ImgToClipBoard() {
            Clipboard.SetImage(this.BackgroundImage);
            AppMgr.GetInstance().ShowTip("", "图片已放入剪切板");
        }

        //打开颜色选择器
        private void OpenColorSelectPanel() {
            System.Windows.Media.Color outCol;
            //bug : 置顶截图 会遮挡颜色选择器 , 暂时无法修复, 该颜色选择器是三方库
            bool ok = ColorPickerWPF.ColorPickerWindow.ShowDialog(out outCol);
            if (ok) {
                Console.WriteLine(outCol.ToString());
                Color newCol = System.Drawing.ColorTranslator.FromHtml(outCol.ToString());
                this.bordeColor = newCol;
                Refresh();
            }
        }

        //快速保存
        private void QuickSaveImg() {
            try {
                string fileName = string.Format("{0}{1}", id,AppSettings.QuickSaveFormat);
                this.BackgroundImage.Save(Path.Combine(AppSettings.QuickSavePath, fileName));
            } catch {
                MessageBox.Show("保存失败,你还没有截取过图片或已经清空图片!");
            }
        }

        //另存为
        private void SaveImg() {
            SaveFileDialog saveImageDialog = new SaveFileDialog();
            saveImageDialog.Title = "图片保存";
            saveImageDialog.Filter = @"jpeg|*.jpg|bmp|*.bmp|png|*.png";
            if (saveImageDialog.ShowDialog() == DialogResult.OK) {
                string fileName = saveImageDialog.FileName.ToString();

                if (fileName != "" && fileName != null) {
                    string fileExtName = fileName.Substring(fileName.LastIndexOf(".") + 1).ToString();

                    System.Drawing.Imaging.ImageFormat imgformat = null;

                    if (fileExtName != "") {
                        switch (fileExtName) {
                            case "jpg":
                                imgformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                                break;
                            case "bmp":
                                imgformat = System.Drawing.Imaging.ImageFormat.Bmp;
                                break;
                            case "png":
                                imgformat = System.Drawing.Imaging.ImageFormat.Png;
                                break;
                            default:
                                MessageBox.Show("只能存取为: jpg,bmp,gif 格式");
                                return;
                        }

                        try {
                            this.BackgroundImage.Save(saveImageDialog.FileName, imgformat);
                        } catch {
                            MessageBox.Show("保存失败,你还没有截取过图片或已经清空图片!");
                        }
                    }
                }
            }

        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e) {
            //this.Close();
            AppMgr.GetInstance().RemoveTopPicture(this);
        }

        private void BorderColorToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenColorSelectPanel();
        }

        private void toClipBoardToolStripMenuItem_Click(object sender, EventArgs e) {
            ImgToClipBoard();
        }

        private void quickSaveToolStripMenuItem_Click(object sender, EventArgs e) {
            QuickSaveImg();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveImg();
        }

        private void setOpacity(object sender, EventArgs e) {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            this.per10ToolStripMenuItem.Checked = false;
            this.per30ToolStripMenuItem.Checked = false;
            this.per50ToolStripMenuItem.Checked = false;
            this.per70ToolStripMenuItem.Checked = false;
            this.per100ToolStripMenuItem.Checked = false;

            item.Checked = true;

            if(this.per100ToolStripMenuItem.Checked == true) {
                this.Opacity = 1;
            } else if(this.per10ToolStripMenuItem.Checked == true) {
                this.Opacity = 0.1f;
            } else if(this.per30ToolStripMenuItem.Checked == true) {
                this.Opacity = 0.3f;
            } else if(this.per50ToolStripMenuItem.Checked == true) {
                this.Opacity = 0.5f;
            } else if(this.per70ToolStripMenuItem.Checked == true) {
                this.Opacity = 0.7f;
            }

        }

        #endregion


    }
}
