namespace FoxSnipTool {
    partial class PickColor {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PickColor));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pos_label = new System.Windows.Forms.Label();
            this.hex_label = new System.Windows.Forms.Label();
            this.rgb_label = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.获取RGBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取十六进制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 39);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(138, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "取色";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pos_label);
            this.groupBox1.Controls.Add(this.hex_label);
            this.groupBox1.Controls.Add(this.rgb_label);
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 86);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息";
            // 
            // pos_label
            // 
            this.pos_label.AutoSize = true;
            this.pos_label.Location = new System.Drawing.Point(7, 21);
            this.pos_label.Name = "pos_label";
            this.pos_label.Size = new System.Drawing.Size(89, 12);
            this.pos_label.TabIndex = 2;
            this.pos_label.Text = "坐标:(200,500)";
            // 
            // hex_label
            // 
            this.hex_label.AutoSize = true;
            this.hex_label.Location = new System.Drawing.Point(7, 63);
            this.hex_label.Name = "hex_label";
            this.hex_label.Size = new System.Drawing.Size(101, 12);
            this.hex_label.TabIndex = 1;
            this.hex_label.Text = "十六进制:#FF00FF";
            // 
            // rgb_label
            // 
            this.rgb_label.AutoSize = true;
            this.rgb_label.Location = new System.Drawing.Point(7, 42);
            this.rgb_label.Name = "rgb_label";
            this.rgb_label.Size = new System.Drawing.Size(101, 12);
            this.rgb_label.TabIndex = 0;
            this.rgb_label.Text = "RGB: 255,200,255";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.获取RGBToolStripMenuItem,
            this.获取十六进制ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 48);
            // 
            // 获取RGBToolStripMenuItem
            // 
            this.获取RGBToolStripMenuItem.Name = "获取RGBToolStripMenuItem";
            this.获取RGBToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.获取RGBToolStripMenuItem.Text = "获取RGB";
            // 
            // 获取十六进制ToolStripMenuItem
            // 
            this.获取十六进制ToolStripMenuItem.Name = "获取十六进制ToolStripMenuItem";
            this.获取十六进制ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.获取十六进制ToolStripMenuItem.Text = "获取十六进制";
            // 
            // PickColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(230, 156);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PickColor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PickColor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label pos_label;
        private System.Windows.Forms.Label hex_label;
        private System.Windows.Forms.Label rgb_label;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 获取RGBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取十六进制ToolStripMenuItem;
    }
}