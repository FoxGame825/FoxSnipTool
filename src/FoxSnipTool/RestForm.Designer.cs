namespace FoxSnipTool {
    partial class RestForm {
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.surplusBar1 = new FoxSnipTool.SurplusBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delay1mToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delay5mToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delay10mToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delay30mToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delay1hToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.switchBgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(24, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(322, 90);
            this.label1.TabIndex = 0;
            this.label1.Text = "20:59:22";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.surplusBar1);
            this.panel1.Location = new System.Drawing.Point(240, 142);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 241);
            this.panel1.TabIndex = 2;
            // 
            // surplusBar1
            // 
            this.surplusBar1.BackColor = System.Drawing.Color.Gray;
            this.surplusBar1.Location = new System.Drawing.Point(78, 173);
            this.surplusBar1.Name = "surplusBar1";
            this.surplusBar1.Size = new System.Drawing.Size(214, 26);
            this.surplusBar1.TabIndex = 1;
            this.surplusBar1.Value = 60;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delay1mToolStripMenuItem,
            this.delay5mToolStripMenuItem,
            this.delay10mToolStripMenuItem,
            this.delay30mToolStripMenuItem,
            this.delay1hToolStripMenuItem,
            this.toolStripSeparator1,
            this.switchBgToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 186);
            // 
            // delay1mToolStripMenuItem
            // 
            this.delay1mToolStripMenuItem.Name = "delay1mToolStripMenuItem";
            this.delay1mToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.delay1mToolStripMenuItem.Text = "延长1分钟";
            this.delay1mToolStripMenuItem.Click += new System.EventHandler(this.delay1mToolStripMenuItem_Click);
            // 
            // delay5mToolStripMenuItem
            // 
            this.delay5mToolStripMenuItem.Name = "delay5mToolStripMenuItem";
            this.delay5mToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.delay5mToolStripMenuItem.Text = "延长5分钟";
            this.delay5mToolStripMenuItem.Click += new System.EventHandler(this.delay5mToolStripMenuItem_Click);
            // 
            // delay10mToolStripMenuItem
            // 
            this.delay10mToolStripMenuItem.Name = "delay10mToolStripMenuItem";
            this.delay10mToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.delay10mToolStripMenuItem.Text = "延长10分钟";
            this.delay10mToolStripMenuItem.Click += new System.EventHandler(this.delay10mToolStripMenuItem_Click);
            // 
            // delay30mToolStripMenuItem
            // 
            this.delay30mToolStripMenuItem.Name = "delay30mToolStripMenuItem";
            this.delay30mToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.delay30mToolStripMenuItem.Text = "延长30分钟";
            this.delay30mToolStripMenuItem.Click += new System.EventHandler(this.delay30mToolStripMenuItem_Click);
            // 
            // delay1hToolStripMenuItem
            // 
            this.delay1hToolStripMenuItem.Name = "delay1hToolStripMenuItem";
            this.delay1hToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.delay1hToolStripMenuItem.Text = "延长1小时";
            this.delay1hToolStripMenuItem.Click += new System.EventHandler(this.delay1hToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // switchBgToolStripMenuItem
            // 
            this.switchBgToolStripMenuItem.Name = "switchBgToolStripMenuItem";
            this.switchBgToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.switchBgToolStripMenuItem.Text = "切换背景";
            this.switchBgToolStripMenuItem.Click += new System.EventHandler(this.switchBgToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "强制退出";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // RestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 513);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.panel1);
            this.Name = "RestForm";
            this.Text = "RestForm";
            this.Load += new System.EventHandler(this.RestForm_Load);
            this.Resize += new System.EventHandler(this.RestForm_Resize);
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private SurplusBar surplusBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem delay10mToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delay30mToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delay1hToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem switchBgToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delay1mToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delay5mToolStripMenuItem;
    }
}