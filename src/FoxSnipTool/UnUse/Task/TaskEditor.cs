using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoxSnipTool {


    public partial class TaskEditor : Form {
        private TaskData data;

        public TaskEditor(TaskData task = null) {
            InitializeComponent();

            foreach(TaskRunWay way in Enum.GetValues(typeof(TaskRunWay))) {
                CombBoxItem item = new CombBoxItem();
                item.Text = way.ToString();
                item.Value = (int)way;
                this.type_comb.Items.Add(item);
            }
            
            foreach(TaskShowWay way in Enum.GetValues(typeof(TaskShowWay))) {
                if (way == TaskShowWay.Animation || way == TaskShowWay.MessageBox) continue; //有bug

                CombBoxItem item = new CombBoxItem();
                item.Text = way.ToString();
                item.Value = (int)way;
                this.showType_comb.Items.Add(item);
            }


            if (task == null) { //新增
                this.type_comb.SelectedIndex = (int)TaskRunWay.FixTime;
                this.showType_comb.SelectedIndex = (int)TaskShowWay.Balloontip;
                this.dateTimePicker1.Text = "00:00:00";
                this.title_text.Text = "";
                this.content_text.Text = "";
            } else {//编辑
                this.type_comb.SelectedIndex = (int)task.TaskRunWay;
                this.showType_comb.SelectedIndex = (int)task.ShowWay;
                this.dateTimePicker1.Text = task.TriggerDateTime.TimeOfDay.ToString();
                this.title_text.Text = task.Title;
                this.content_text.Text = task.Desc;
            }

            data = task;
        }

        private void ok_btn_Click(object sender, EventArgs e) {
            if(this.title_text.TextLength <= 0) {
                MessageBox.Show("标题不能为空!");
                return;
            }

            if(this.content_text.TextLength <= 0) {
                MessageBox.Show("内容不能为空!");
                return;
            }


            if(data != null) {
                TaskManager.GetInstance().RemoveTask(data.UID);
                data = null;
            }

            CombBoxItem item = this.type_comb.SelectedItem as CombBoxItem;
            CombBoxItem item2 = this.showType_comb.SelectedItem as CombBoxItem;


            TaskData newdata = new TaskData(TaskTag.Normal
                ,(TaskRunWay)item.Value
                , this.dateTimePicker1.Value
                ,this.title_text.Text
                ,this.content_text.Text
                ,(TaskShowWay)item2.Value);


            Console.WriteLine(newdata.ToString());
            TaskManager.GetInstance().AddTask(newdata);
            //AppManager.GetInstance().MainForm.UpdateTaskList();

            this.Close();
            this.Dispose();
        }

        private void cancel_btn_Click(object sender, EventArgs e) {
            this.Close();
            this.Dispose();
        }

        private void type_comb_SelectedIndexChanged(object sender, EventArgs e) {
            CombBoxItem item = this.type_comb.SelectedItem as CombBoxItem;
            var way = (TaskRunWay)item.Value;
            switch (way) {
                case TaskRunWay.FixTime:
                    this.label3.Text = "(每天的某个时间)";
                    break;
                case TaskRunWay.RepeatTime:
                    this.label3.Text = "(每隔时间段)";
                    break;
            }
        }
    }
}
