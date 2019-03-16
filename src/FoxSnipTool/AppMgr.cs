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

namespace FoxSnipTool
{
    public class AppMgr
    {
        private static AppMgr m_Ins;

        public SettingForm MainForm;
        private CaptureImageTool captureImg;
        private List<TopPicture> topPictureList = new List<TopPicture>();
        private HorseLight horseLight;
        private RemindAnimationForm remindAniForm;
        private PickColor PickColorForm;
        private System.Timers.Timer timer = new System.Timers.Timer();
        private DateTime TempWorkDate = DateTime.Now + AppSettings.WorkTimeSpan;
        private Queue<string> autoCacheQueue = new Queue<string>();

        private AppMgr() { }
        public static AppMgr GetInstance() {
            if (m_Ins == null) {
                m_Ins = new AppMgr();
                m_Ins.Init();
            }
            return m_Ins;
        }
        private void Init() {
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;
        }


        /// <summary>
        /// 显示提示
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        public void ShowTip(string title, string content) {
            if (MainForm != null) {
                MainForm.Tip(title, content);
            }
        }

        #region 休息
        
        public void OpenRestFuncion(bool bOpen) {
            if (bOpen) {
                TempWorkDate = DateTime.Now + AppSettings.WorkTimeSpan;
                timer.Start();
            } else {
                timer.Stop();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            if (TempWorkDate.TimeOfDay.Ticks - DateTime.Now.TimeOfDay.Ticks <= 0) {
                timer.Stop();
                //OpenRestForm();

                Thread th = new Thread(() =>
                {
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
            if (remindAniForm != null) {
                remindAniForm.Close();
                remindAniForm.Dispose();
            }
            remindAniForm = null;
            GC.Collect();
        }

        #endregion


        #region 截图

        /// <summary>
        /// 进入截图模式
        /// </summary>
        public void EnterScreenMode() {
            CancelScreenMode();
            if (AppSettings.CanFixSize) {
                captureImg = new CaptureImageTool(AppSettings.MaskColor, AppSettings.FixSize);
            } else {
                captureImg = new CaptureImageTool(AppSettings.MaskColor);
            }

            captureImg.SelectCursor = Cursors.Cross;
            captureImg.DrawCursor = Cursors.VSplit;

            captureImg.OnZhiDingHandler += (Image img) =>
            {
                PlayAudio();
                CreateTopPicture(img);
                if (AppSettings.AutoToClipboard) {
                    Clipboard.SetImage(img);
                }
                if (AppSettings.AutoCache) {
                    SaveAutoCacheImg(img);
                }

                CancelScreenMode();
            };

            captureImg.OnExitHandler += () => {
                CancelScreenMode();
            };
           
            captureImg.Show();
        }

        /// <summary>
        /// 取消截图模式
        /// </summary>
        public void CancelScreenMode() {
            if (captureImg != null) {
                captureImg.Close();
                captureImg.Dispose();
            }
            GC.Collect();
        }

        /// <summary>
        /// 创建置顶图片
        /// </summary>
        /// <param name="img"></param>
        public void CreateTopPicture(Image img) {
            TopPicture top = new TopPicture(img);
            this.topPictureList.Add(top);
            top.StartPosition = FormStartPosition.CenterScreen;
            top.Show();
            //top.Location = Cursor.Position;
        }

        /// <summary>
        /// 删除置顶图片
        /// </summary>
        /// <param name="pic"></param>
        public void RemoveTopPicture(TopPicture pic) {
            if (this.topPictureList.Contains(pic)) {
                this.topPictureList.Remove(pic);
            }
            pic.Close();
            pic.Dispose();
            GC.Collect();
        }

        /// <summary>
        /// 移除所有置顶图片
        /// </summary>
        public void RemoveAllTopPicture() {
            foreach (var pic in this.topPictureList) {
                pic.Close();
                pic.Dispose();
            }
            this.topPictureList.Clear();
            GC.Collect();
        }

        /// <summary>
        /// 显示隐藏所有置顶图片
        /// </summary>
        /// <param name="vis"></param>
        public void AllTopPictureVisible(bool vis) {
            foreach (var pic in this.topPictureList) {
                pic.Visible = vis;
            }
        }

        /// <summary>
        /// 保存自动缓存图片
        /// </summary>
        /// <param name="img"></param>
        public void SaveAutoCacheImg(Image img) {
            if (!Directory.Exists(AppSettings.AutoCachePath)) {
                Directory.CreateDirectory(AppSettings.AutoCachePath);
            }

            string name = string.Format("cache_{0}.jpg", DateTime.Now.ToFileTime());
            //超出最大缓存,移除头缓存图片
            if(autoCacheQueue.Count >= AppSettings.AutoCacheImgMax) {
                System.IO.File.Delete(AppSettings.AutoCachePath + autoCacheQueue.Dequeue());
            }
            autoCacheQueue.Enqueue(name);
            img.Save(AppSettings.AutoCachePath + name);
        }

        /// <summary>
        /// 加载自动缓存图片
        /// </summary>
        public void LoadAutoCacheImg() {
            if (Directory.Exists(AppSettings.AutoCachePath) && AppSettings.AutoCacheImgMax > 0) {
                var allfl = Directory.GetFiles(AppSettings.AutoCachePath, "*.jpg", SearchOption.TopDirectoryOnly);
                int count = AppSettings.AutoCacheImgMax > allfl.Length ? allfl.Length : AppSettings.AutoCacheImgMax;

                foreach(var iter in allfl) {
                    if(iter.IndexOf("cache_") > 0) {
                        var img = Image.FromFile(iter);
                        if (img != null) {
                            CreateTopPicture(img);

                            string name = Path.GetFileNameWithoutExtension(iter);
                            autoCacheQueue.Enqueue(name);
                            Console.WriteLine(name);

                            if (autoCacheQueue.Count >= AppSettings.AutoCacheImgMax) {
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 清除缓存文件夹
        /// </summary>
        public void ClearAutoCacheFolder() {
            if (Directory.Exists(AppSettings.AutoCachePath)) {
                foreach (var iter in Directory.GetFiles(AppSettings.AutoCachePath, "*", SearchOption.TopDirectoryOnly)) {
                    System.IO.File.Delete(iter);
                }
                ShowTip("自动缓存","缓存清理完成!");
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
            if (this.PickColorForm != null) {
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
        public void AutoStart(bool auto) {
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

            if (auto) {
                if (!System.IO.Directory.Exists(directory)) {
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
            } else {
                System.IO.File.Delete(Path.Combine(AppSettings.CurUserSystemStartupFolder, "FoxSnipTool.lnk"));
            }
        }

        #endregion







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
        public string IniReadValue(string Section, string Key, string def = "") {
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

        /// <summary>
        /// 检查配置文件
        /// </summary>
        public void CheckIniConfig() {
            if (!System.IO.File.Exists(AppSettings.IniPath)) {
                MessageBox.Show("配置文件不存在,生成默认配置!");
                //生成默认ini
                SaveIniConfig();
            } else {
                LoadIniConfig();
            }
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        public void SaveIniConfig() {
            IniWriteValue("normal", "AutoStart", AppSettings.AutoStart.ToString());
            IniWriteValue("normal", "AutoCache", AppSettings.AutoCache.ToString());
            IniWriteValue("normal", "OpenRest", AppSettings.OpenRest.ToString());
            IniWriteValue("normal", "Language", AppSettings.Language.ToString());
            IniWriteValue("normal", "CanFixSize", AppSettings.CanFixSize.ToString());

            IniWriteValue("snip", "QuickSaveFormat", AppSettings.QuickSaveFormat.ToString());
            IniWriteValue("snip", "QuickSavePath", AppSettings.QuickSavePath.ToString());
            IniWriteValue("snip", "MaskColor", AppSettings.MaskColor.ToArgb().ToString());
            IniWriteValue("snip", "AutoToClipboard", AppSettings.AutoToClipboard.ToString());
            IniWriteValue("snip", "CanFixSize", AppSettings.CanFixSize.ToString());
            IniWriteValue("snip", "FixSizeWidth", AppSettings.FixSize.Width.ToString());
            IniWriteValue("snip", "FixSizeHeight", AppSettings.FixSize.Height.ToString());
            IniWriteValue("snip", "AutoCachePath", AppSettings.AutoCachePath);
            IniWriteValue("snip", "AutoCacheImgMax", AppSettings.AutoCacheImgMax.ToString());


            IniWriteValue("config", "RunTask", AppSettings.RunTask.ToString());
            IniWriteValue("config", "RestTimeSpan", AppSettings.RestTimeSpan.ToString());
            IniWriteValue("config", "WorkTimeSpan", AppSettings.WorkTimeSpan.ToString());
            IniWriteValue("config", "RestBackground", AppSettings.RestBackground.ToString());
            IniWriteValue("config", "RestTimeFontColor", AppSettings.RestTimeFontColor.ToArgb().ToString());
            IniWriteValue("config", "RestInfoPoint", AppSettings.RestInfoPoint.ToString());

        }


        public void LoadIniConfig() {
            AppSettings.AutoStart = Convert.ToBoolean(IniReadValue("normal", "AutoStart", AppSettings.AutoStart.ToString()));
            AppSettings.AutoCache = Convert.ToBoolean(IniReadValue("normal", "AutoCache", AppSettings.AutoCache.ToString()));
            AppSettings.OpenRest = Convert.ToBoolean(IniReadValue("normal", "OpenRest", AppSettings.OpenRest.ToString()));
            AppSettings.CanFixSize = Convert.ToBoolean(IniReadValue("normal", "CanFixSize", AppSettings.CanFixSize.ToString()));
            AppSettings.Language = IniReadValue("normal", "Language", AppSettings.Language);

            AppSettings.QuickSavePath = IniReadValue("snip", "QuickSavePath", AppSettings.QuickSavePath);
            AppSettings.QuickSaveFormat = IniReadValue("snip", "QuickSaveFormat", AppSettings.QuickSaveFormat);
            AppSettings.AutoToClipboard = Convert.ToBoolean(IniReadValue("snip", "AutoToClipboard", AppSettings.AutoToClipboard.ToString()));
            string maskColorStr = IniReadValue("snip", "MaskColor", AppSettings.MaskColor.ToArgb().ToString());
            AppSettings.MaskColor = Color.FromArgb(System.Convert.ToInt32(maskColorStr));
            AppSettings.AutoToClipboard = Convert.ToBoolean(IniReadValue("snip", "AutoToClipboard", AppSettings.AutoToClipboard.ToString()));
            int w = Convert.ToInt32(IniReadValue("snip", "FixSizeWidth", AppSettings.FixSize.Width.ToString()));
            int h = Convert.ToInt32(IniReadValue("snip", "FixSizeHeight", AppSettings.FixSize.Height.ToString()));
            AppSettings.FixSize = new Size(w, h);
            AppSettings.AutoCachePath = IniReadValue("snip", "AutoCachePath", AppSettings.AutoCachePath);
            AppSettings.AutoCacheImgMax = Convert.ToInt32(IniReadValue("snip", "AutoCacheImgMax", AppSettings.AutoCacheImgMax.ToString()));



            AppSettings.RunTask = Convert.ToBoolean(IniReadValue("config", "RunTask", "false"));
            AppSettings.RestTimeSpan = TimeSpan.Parse((IniReadValue("config", "RestTimeSpan", "00:10:00")));
            AppSettings.WorkTimeSpan = TimeSpan.Parse((IniReadValue("config", "WorkTimeSpan", "00:30:00")));
            AppSettings.RestBackground = (IniReadValue("config", "RestBackground", ""));
            string restColorStr = IniReadValue("config", "RestTimeFontColor", Color.GreenYellow.ToArgb().ToString());
            AppSettings.RestTimeFontColor = Color.FromArgb(System.Convert.ToInt32(restColorStr));
            AppSettings.RestInfoPoint = (RestInfoPos)Enum.Parse(typeof(RestInfoPos), IniReadValue("config", "RestInfoPoint", RestInfoPos.Center.ToString()));

            OpenRestFuncion(AppSettings.OpenRest);
        }

        #endregion

    }
}
