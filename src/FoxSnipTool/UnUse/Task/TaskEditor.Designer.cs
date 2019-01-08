namespace FoxSnipTool {
    partial class TaskEditor {
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
            this.label1 = new System.Windows.Forms.Label();
            this.title_text = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.type_comb = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.content_text = new System.Windows.Forms.TextBox();
            this.ok_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.showType_comb = new System.Windows.Forms.ComboBox();
            this.taskEditorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.taskEditorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "标题:";
            // 
            // title_text
            // 
            this.title_text.Location = new System.Drawing.Point(54, 13);
            this.title_text.Name = "title_text";
            this.title_text.Size = new System.Drawing.Size(317, 21);
            this.title_text.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "类型:";
            // 
            // type_comb
            // 
            this.type_comb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.type_comb.FormattingEnabled = true;
            this.type_comb.Location = new System.Drawing.Point(54, 43);
            this.type_comb.Name = "type_comb";
            this.type_comb.Size = new System.Drawing.Size(107, 20);
            this.type_comb.TabIndex = 3;
            this.type_comb.SelectedIndexChanged += new System.EventHandler(this.type_comb_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(142, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "(表示重复间隔的时间)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "时间:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(54, 98);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(82, 21);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "内容:";
            // 
            // content_text
            // 
            this.content_text.Location = new System.Drawing.Point(15, 147);
            this.content_text.Multiline = true;
            this.content_text.Name = "content_text";
            this.content_text.Size = new System.Drawing.Size(356, 116);
            this.content_text.TabIndex = 9;
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(202, 283);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(75, 31);
            this.ok_btn.TabIndex = 10;
            this.ok_btn.Text = "确定";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(105, 283);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 31);
            this.cancel_btn.TabIndex = 11;
            this.cancel_btn.Text = "取消";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "方式:";
            // 
            // showType_comb
            // 
            this.showType_comb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.showType_comb.FormattingEnabled = true;
            this.showType_comb.Location = new System.Drawing.Point(54, 70);
            this.showType_comb.Name = "showType_comb";
            this.showType_comb.Size = new System.Drawing.Size(107, 20);
            this.showType_comb.TabIndex = 13;
            // 
            // taskEditorBindingSource
            // 
            this.taskEditorBindingSource.DataSource = typeof(FoxSnipTool.TaskEditor);
            // 
            // TaskEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 334);
            this.Controls.Add(this.showType_comb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.content_text);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.type_comb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.title_text);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskEditor";
            this.Text = "TaskEditor";
            ((System.ComponentModel.ISupportInitialize)(this.taskEditorBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox title_text;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox type_comb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox content_text;
        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox showType_comb;
        private System.Windows.Forms.BindingSource taskEditorBindingSource;
    }
}