using System;


namespace FoxSnipTool {

    public class TaskData {
        public readonly int UID;    //唯一id
        private TaskTag taskTag_;   //任务类型
        private TaskRunWay taskRunWay_;  //任务运行方式
        private DateTime startDateTime_;    //任务开始日期
        private DateTime triggerDateTime_;  //任务触发日期
        private string title_;      //任务标题
        private string desc_;   //任务描述
        private TaskShowWay showWay_;   //显示方式

        public string Title { get => title_; set => title_ = value; }
        public string Desc { get => desc_; set => desc_ = value; }
        public TaskShowWay ShowWay { get => showWay_; set => showWay_ = value; }
        public DateTime TriggerDateTime { get => triggerDateTime_;
            set {
                triggerDateTime_ = value;
                RefreshStartDateTime();
            }
        }
        public TaskRunWay TaskRunWay { get => taskRunWay_; set => taskRunWay_ = value; }
        public DateTime StartDateTime { get => startDateTime_; set => startDateTime_ = value; }
        public TaskTag TaskTag { get => taskTag_; }

        public TaskData(TaskTag tag,TaskRunWay runWay,DateTime triggerTm,string title = "",string desc = "",TaskShowWay showWay =  TaskShowWay.Nune ) {
            AppSettings.TaskBaseUID++;
            UID = AppSettings.TaskBaseUID;
            taskRunWay_ = runWay;
            startDateTime_ = DateTime.Now;
            triggerDateTime_ = triggerTm;
            title_ = title;
            desc_ = desc;
            showWay_ = showWay;
        }

        public void RefreshStartDateTime() {
            startDateTime_ = DateTime.Now;
        }

        //是否有效,重复执行一直有效
        public bool Vaild() {
            if (taskRunWay_ == TaskRunWay.RepeatTime)
                return true;
            else
                return DateTime.Now.TimeOfDay.Ticks < triggerDateTime_.TimeOfDay.Ticks;
        }

        public override string ToString() {
            return string.Format("UID:{0},Title:{1},RunWay:{2},StartTime:{3},TriggerTime:{4},ShowWay:{5},Desc:{6}",
                UID,Title, taskRunWay_, startDateTime_,triggerDateTime_,showWay_,desc_);
        }
    }
}
