using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.IO;


namespace FoxSnipTool {

    //定时任务管理器
    public class TaskManager {
        private static TaskManager m_Ins;
        private TaskManager() { }

        public static TaskManager GetInstance() {
            if(m_Ins == null) {
                m_Ins = new TaskManager();
            }
            return m_Ins;
        }

        //反序列化
        public static TaskData Deserializtion(string serilierstring) {
            try {
                var slice = serilierstring.Split(new string[] { "|^|" }, StringSplitOptions.RemoveEmptyEntries);

                TaskTag tag = (TaskTag)(Convert.ToInt32(slice[0]));
                string title = slice[1];
                TaskRunWay runWay = (TaskRunWay)(Convert.ToInt32(slice[2]));
                TaskShowWay showWay = (TaskShowWay)(Convert.ToInt32(slice[3]));
                DateTime triggerTime = DateTime.Parse(slice[4]);
                string desc = slice[5];

                return new TaskData(tag, runWay, triggerTime, title, desc, showWay);
            } catch {
                return null;
            }
        }

        //序列化成string
        public static string Serialization(TaskData task) {
            return string.Format("{0}|^|{1}|^|{2}|^|{3}|^|{4}|^|{5}"
                ,(int)task.TaskTag
                ,task.Title
                ,(int)task.TaskRunWay
                ,(int)task.ShowWay
                ,task.TriggerDateTime
                ,task.Desc);
        }





        private Dictionary<int,TaskData> taskDic = new Dictionary<int, TaskData>();
        public Dictionary<int, TaskData> TaskDic { get { return taskDic; } }
        private List<TaskData> runTaskList = new List<TaskData>();
        private Queue<TaskData> waitRunQueue = new Queue<TaskData>();
        private Timer timer = new Timer();

        public TaskData QueryTaskData(int uid) {
            if (this.taskDic.ContainsKey(uid)) {
                return this.taskDic[uid];
            }
            return null;
        }

        public void LoadTaskData() {
            if (File.Exists(AppSettings.TaskDataFilePath)) {
                var lines = File.ReadAllLines(AppSettings.TaskDataFilePath);

                foreach (var line in lines) {
                    TaskData task = TaskManager.Deserializtion(line);
                    if (task != null) {
                        taskDic.Add(task.UID, task);
                    }
                }
            }
        }

        public void SaveTaskData() {
            string[] lines = new string[this.taskDic.Count];
            int idx = 0;
            foreach(var uid in this.taskDic.Keys) {
                lines[idx] = TaskManager.Serialization(this.taskDic[uid]);
                idx++;
            }

            File.WriteAllLines(AppSettings.TaskDataFilePath, lines);
        }

        public void SetTimerRun(bool run) {
            if (timer != null) {
                if (run) {
                    this.runTaskList.Clear();
                    this.waitRunQueue.Clear();
                    foreach(var task in this.taskDic) {
                        if (task.Value.Vaild()) {
                            this.runTaskList.Add(task.Value);
                        }
                    }

                    timer.Start();
                    timer.Elapsed += Timer_Elapsed;
                } else {
                    timer.Stop();
                    timer.Elapsed -= Timer_Elapsed;
                }
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            List<TaskData> list = new List<TaskData>();
            foreach(var task in this.runTaskList) {
                if(task.TaskRunWay == TaskRunWay.RepeatTime) {
                    if (DateTime.Now.TimeOfDay.Ticks - task.StartDateTime.TimeOfDay.Ticks > task.TriggerDateTime.TimeOfDay.Ticks) {
                        list.Add(task);
                        waitRunQueue.Enqueue(task);
                    }
                }else if(task.TaskRunWay ==  TaskRunWay.FixTime) {
                    if(DateTime.Now.TimeOfDay.Ticks >= task.TriggerDateTime.TimeOfDay.Ticks) {
                        list.Add(task);
                        waitRunQueue.Enqueue(task);
                    }
                }
            }

            foreach(var t in list) {
                this.runTaskList.Remove(t);
            }

            while(waitRunQueue.Count > 0) {
                doTask(waitRunQueue.Dequeue());
            }
        }

        private void doTask(TaskData task) {
            switch (task.ShowWay) {
                case TaskShowWay.Balloontip:
                    AppManager.GetInstance().ShowTip(task.Title, task.Desc);
                    break;
                case TaskShowWay.Animation:
                    AppManager.GetInstance().ShowRemindAnimateForm(task.Title + Environment.NewLine + task.Desc);
                    break;
                case TaskShowWay.HorseLight:
                    AppManager.GetInstance().ShowHorseLight(task.Title + Environment.NewLine +task.Desc);
                    break;
                case TaskShowWay.MessageBox:
                    MessageBox.Show(task.Title,task.Desc);
                    break;
            }

            if(task.TaskRunWay ==  TaskRunWay.RepeatTime) {
                task.RefreshStartDateTime();
                this.runTaskList.Add(task);
            }
        }

        public bool TimerRunning() {
            return timer.Enabled;
        }

        public void AddTask(TaskData task) {
            if (!this.taskDic.ContainsKey(task.UID)) {
                this.taskDic.Add(task.UID, task);
                if (task.Vaild()) {
                    this.runTaskList.Add(task);
                }
                SaveTaskData();
            }
        }

        public void RemoveTask(int uid) {
            this.taskDic.Remove(uid);
            foreach(var t in this.runTaskList) {
                if(t.UID == uid) {
                    this.runTaskList.Remove(t);
                    break;
                }
            }
            SaveTaskData();
        }

        public void ClearTask() {
            this.taskDic.Clear();
            this.runTaskList.Clear();
            this.waitRunQueue.Clear();
            SaveTaskData();
        }
    }
}
