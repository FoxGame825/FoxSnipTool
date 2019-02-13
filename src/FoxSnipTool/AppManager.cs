using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpWin_JD.CaptureImage;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.IO;
using System.Timers;
using System.Threading;
using IWshRuntimeLibrary;

namespace FoxSnipTool {
    public class AppManager {
        private static AppManager m_Ins;
        private AppManager() { }

        public static AppManager GetInstance() {
            if (m_Ins == null) {
                m_Ins = new AppManager();
                m_Ins.Init();
            }
            return m_Ins;
        }

        private void Init() {
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;
        }


        #region ini operator
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);
        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void IniWriteValue(string Section, string Key, string Value) {
            WritePrivateProfileString(Section, Key, Value, AppSettings.IniPath);
        }
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key,string def = "") {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, def, temp, 255, AppSettings.IniPath);
            return temp.ToString();
        }
        public byte[] IniReadValues(string section, string key) {
            byte[] temp = new byte[255];
            int i = GetPrivateProfileString(section, key, "", temp, 255, AppSettings.IniPath);
            return temp;
        }
        ///// <summary>
        ///// 删除ini文件下所有段落
        ///// </summary>
        //public void ClearAllSection() {
        //    IniWriteValue(null, null, null);
        //}
        ///// <summary>
        ///// 删除ini文件下personal段落下的所有键
        ///// </summary>
        ///// <param name="Section"></param>
        //public void ClearSection(string Section) {
        //    IniWriteValue(Section, null, null);
        //}

        public void CheckIniConfig() {
            if (!System.IO.File.Exists(AppSettings.IniPath)) {
                MessageBox.Show("配置文件不存在,生成默认配置!");
                //生成默认ini
                SaveIniConfig();
            } else {
                LoadIniConfig();
            }

            var dd =IniManager.Instance;
        }

        public void SaveIniConfig() {
            IniWriteValue("config", "WheelScalePer", AppSettings.WheelScalePer.ToString());
            IniWriteValue("config", "AutoStart", AppSettings.AutoStart.ToString());
            IniWriteValue("config", "DrawScaleInfoCD", AppSettings.DrawScaleInfoCD.ToString());
            IniWriteValue("config", "MinScaleFactor", AppSettings.MinScaleFactor.ToString());
            IniWriteValue("config", "QuickSavePath", AppSettings.QuickSavePath.ToString());
            IniWriteValue("config", "QuickSaveFormat", AppSettings.QuickSaveFormat.ToString());
            IniWriteValue("config", "MaskColor", AppSettings.MaskColor.ToArgb().ToString());
            IniWriteValue("config", "RunTask", AppSettings.RunTask.ToString());

            IniWriteValue("config", "OpenRest", AppSettings.OpenRest.ToString());
            IniWriteValue("config", "RestTimeSpan", AppSettings.RestTimeSpan.ToString());
            IniWriteValue("config", "WorkTimeSpan", AppSettings.WorkTimeSpan.ToString());
            IniWriteValue("config", "RestBackground", AppSettings.RestBackground.ToString());
            IniWriteValue("config", "RestTimeFontColor", AppSettings.RestTimeFontColor.ToArgb().ToString());
            IniWriteValue("config", "RestInfoPoint", AppSettings.RestInfoPoint.ToString());

        }


        public void LoadIniConfig() {
            AppSettings.WheelScalePer = Convert.ToSingle(IniReadValue("config", "WheelScalePer","0.05"));
            AppSettings.AutoStart = Convert.ToBoolean(IniReadValue("config", "AutoStart","false"));
            AppSettings.DrawScaleInfoCD = Convert.ToInt32(IniReadValue("config", "DrawScaleInfoCD","1000"));
            AppSettings.MinScaleFactor = Convert.ToSingle(IniReadValue("config", "MinScaleFactor","0.1"));
            AppSettings.QuickSavePath = IniReadValue("config", "QuickSavePath", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            AppSettings.QuickSaveFormat = IniReadValue("config", "QuickSaveFormat",".png");
            AppSettings.RunTask = Convert.ToBoolean(IniReadValue("config", "RunTask","false"));
            string maskColorStr = IniReadValue("config", "MaskColor", Color.FromArgb(100, 0, 0, 0).ToString());
            AppSettings.MaskColor = Color.FromArgb(System.Convert.ToInt32( maskColorStr));

            AppSettings.OpenRest = Convert.ToBoolean(IniReadValue("config", "OpenRest", "false"));
            AppSettings.RestTimeSpan = TimeSpan.Parse((IniReadValue("config", "RestTimeSpan", "00:10:00")));
            AppSettings.WorkTimeSpan = TimeSpan.Parse((IniReadValue("config", "WorkTimeSpan", "00:30:00")));
            AppSettings.RestBackground = (IniReadValue("config", "RestBackground", ""));
            string restColorStr = IniReadValue("config", "RestTimeFontColor", Color.GreenYellow.ToArgb().ToString());
            AppSettings.RestTimeFontColor = Color.FromArgb(System.Convert.ToInt32(restColorStr));
            AppSettings.RestInfoPoint = (RestInfoPos)Enum.Parse(typeof(RestInfoPos), IniReadValue("config", "RestInfoPoint", RestInfoPos.Center.ToString()));

            OpenRestFuncion(AppSettings.OpenRest);
        }

        #endregion


        public SettingForm MainForm;
        private CaptureImageTool captureImg;
        private List<TopPicture> topPictureList = new List<TopPicture>();
        private HorseLight horseLight;
        private RemindAnimationForm remindAniForm;
        private PickColor PickColorForm;



        #region 休息
        private System.Timers.Timer timer = new System.Timers.Timer();
        private DateTime TempWorkDate = DateTime.Now + AppSettings.WorkTimeSpan;


        public void OpenRestFuncion(bool bOpen) {
            if (bOpen) {
                TempWorkDate = DateTime.Now + AppSettings.WorkTimeSpan;
                timer.Start();
            } else {
                timer.Stop();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            if(TempWorkDate.TimeOfDay.Ticks - DateTime.Now.TimeOfDay.Ticks <= 0) {
                timer.Stop();
                //OpenRestForm();

                Thread th = new Thread(()=> {
                    Application.Run(new RestForm(AppSettings.RestTimeSpan));
                });
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            Console.WriteLine(TempWorkDate.TimeOfDay.Ticks - DateTime.Now.TimeOfDay.Ticks);
        }

        //public void OpenRestForm() {
        //    //var restForm = new RestForm(AppSettings.RestTimeSpan);
        //    //restForm.Activate();
        //    //restForm.ShowDialog();

        //}

        public void RefreshRestTimer() {
            Console.WriteLine("RefreshRestTimer");
            TempWorkDate = DateTime.Now + AppSettings.WorkTimeSpan;
            timer.Start();
        }

        #endregion


        #region 任务 

        //显示提示
        public void ShowTip(string title,string content) {
            if (MainForm != null) {
                MainForm.Tip(title, content);
            }
        }

        //显示跑马灯
        public void ShowHorseLight(string content) {
            CloseHorseLight();
            horseLight = new HorseLight(content);
            horseLight.ShowDialog();
        }

        public void CloseHorseLight() {
            if (horseLight != null) {
                horseLight.Close();
                horseLight.Dispose();
            }
            horseLight = null;
            GC.Collect();
        }

        //显示提示动画界面
        public void ShowRemindAnimateForm(string content) {
            CloseRemindAnimateForm();
            remindAniForm = new RemindAnimationForm(content);
            remindAniForm.ShowDialog();
        }

        public void CloseRemindAnimateForm() {
            if(remindAniForm != null) {
                remindAniForm.Close();
                remindAniForm.Dispose();
            }
            remindAniForm = null;
            GC.Collect();
        }

        #endregion


        #region 截图

        //进入截图模式
        public void EnterScreenMode() {
            CancelScreenMode();
            if (AppSettings.CanFixSize)
            {
                captureImg = new CaptureImageTool(AppSettings.MaskColor, AppSettings.FixSize);
            }
            else
            {
                captureImg = new CaptureImageTool(AppSettings.MaskColor);
            }

            captureImg.SelectCursor = Cursors.Cross;
            captureImg.DrawCursor = Cursors.VSplit;
            captureImg.OnZhiDingHandler += CaptureImg_OnZhiDingHandler;
            captureImg.OnExitHandler += CaptureImg_OnExitHandler;
            captureImg.Show();
        }

        private void CaptureImg_OnExitHandler()
        {
            CancelScreenMode();
        }

        private void CaptureImg_OnZhiDingHandler(Image img) {
            PlayAudio();
            CreateTopPicture(img);
            CancelScreenMode();
        }

        //取消截图模式
        public void CancelScreenMode() {
            if (captureImg != null) {
                captureImg.Close();
                captureImg.Dispose();
                //captureImg = null;
            }
            GC.Collect();
        }

        //创建topPicture
        public void CreateTopPicture(Image img) {
            TopPicture top = new TopPicture(img);
            this.topPictureList.Add(top);
            top.StartPosition = FormStartPosition.CenterScreen;
            top.Show();
            //top.Location = Cursor.Position;
        }

        //删除topPicture
        public void RemoveTopPicture(TopPicture pic) {
            if (this.topPictureList.Contains(pic)) {
                this.topPictureList.Remove(pic);
            }
            pic.Close();
            pic.Dispose();
            GC.Collect();
        }

        public void RemoveAllTopPicture() {
            foreach(var pic in this.topPictureList) {
                pic.Close();
                pic.Dispose();
            }
            this.topPictureList.Clear();
            GC.Collect();
        }

        public void AllTopPictureVisible(bool vis) {
            foreach(var pic in this.topPictureList) {
                pic.Visible = vis;
            }
        }
        #endregion



        #region 取色
        //进入取色模式
        public void EnterPickColorMode() {
            CancelPickColorMode();
            this.PickColorForm = new PickColor();
            this.PickColorForm.Show();
        }

        //取消取色模式
        public void CancelPickColorMode() {
            if(this.PickColorForm != null) {
                this.PickColorForm.Close();
            }
            this.PickColorForm = null;
        }
        #endregion

        #region setting panel
        //重置默认快速保存路径(桌面)
        public void ResetDefaultQuickSavePath() {
            AppSettings.QuickSavePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }
        //选择快速保存路径
        public void ScanQuickSavePath() {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择保存文件夹";
            if (dialog.ShowDialog() == DialogResult.OK) {
                if (string.IsNullOrEmpty(dialog.SelectedPath)) {
                    MessageBox.Show("文件夹路径不能为空", "提示");
                    return;
                }
                Console.WriteLine("选择的快速保存文件夹:" + dialog.SelectedPath);
                AppSettings.QuickSavePath = dialog.SelectedPath;
            }
        }

        //播放音效
        public void PlayAudio() {
            SoundPlayer player = new SoundPlayer(
                Properties.Resources.ResourceManager.GetStream(AppSettings.CutAudioPath));
            player.Play();
        }

        //开机启动
        public void AutoStart(bool auto)
         {
            /// <param name="directory">快捷方式所处的文件夹</param>
            /// <param name="shortcutName">快捷方式名称</param>
            /// <param name="targetPath">目标路径</param>
            /// <param name="description">描述</param>
            /// <param name="iconLocation">图标路径，格式为"可执行文件或DLL路径, 图标编号"，
            /// 例如System.Environment.SystemDirectory + "\\" + "shell32.dll, 165"</param>
            string directory = AppSettings.CurUserSystemStartupFolder;
            string shortcutName = "FoxSnipTool";
            string targetPath = Application.ExecutablePath;
            string description = "FoxSnipTool";
            string iconLocation = string.Empty;

            if (auto)
            {
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }

                string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);//创建快捷方式对象
                shortcut.TargetPath = targetPath;//指定目标路径
                shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);//设置起始位置
                shortcut.WindowStyle = 1;//设置运行方式，默认为常规窗口
                shortcut.Description = description;//设置备注
                shortcut.IconLocation = string.IsNullOrEmpty(iconLocation) ? targetPath : iconLocation;//设置图标路径
                shortcut.Save();//保存快捷方式
            }
            else
            {
                System.IO.File.Delete(Path.Combine(AppSettings.CurUserSystemStartupFolder, "FoxSnipTool.lnk"));
            }
         }

        #endregion
    }
}
