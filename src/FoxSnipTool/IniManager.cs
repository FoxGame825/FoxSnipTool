using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;

namespace FoxSnipTool
{
    // ini配置文件管理器
    public class IniManager
    {
        private static IniManager m_Ins;
        private IniData _data;
        private FileIniDataParser _parser;

        public static IniManager Instance {
            get {
                if(m_Ins == null) {
                    m_Ins = new IniManager();
                    m_Ins.init();
                }
                return m_Ins;
            }
        }


        #region config data

        //鼠标滚轮每次缩放比率
        public float WheelScalePer {
            get { return _data == null ? 0.05f : System.Convert.ToSingle(_data["config"]["WheelScalePer"]); }
        }

        //是否开机启动
        public bool AutoStart {
            get { return _data == null ? false : System.Convert.ToBoolean(_data["config"]["AutoStart"]); }
        }

        //缩放topPicture 显示缩放信息持续时间{ms}
        public static int DrawScaleInfoCD = 1000;

        //最小缩放因子
        public static float MinScaleFactor = 0.1f;

        //鼠标双击最小模式尺寸
        public static Size MiniModeSize = new Size(50, 50);

        //快速保存图片路径
        public static string QuickSavePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        
        //截图音效路径
        public static string CutAudioPath = "snip";
        
        //截图快捷键ID
        public static int CtrlF1_ID = 10086;
        
        //取色快捷键ID
        public static int CtrlF2_ID = 10087;
        
        //快速保存默认格式
        public static string QuickSaveFormat = ".png";
        
        //截图遮罩颜色
        public static Color MaskColor = Color.FromArgb(100, 0, 0, 0);
        
        //截图是否固定尺寸
        public static bool CanFixSize = false;
        
        //固定尺寸
        public static Size FixSize = new Size(50, 50);

        //任务数据文件路径
        public static string TaskDataFilePath = Application.StartupPath + "\\task.bin";
        //任务计数
        public static int TaskBaseUID = 0;
        //是否运行任务
        public static bool RunTask = false;
        //跑马灯背景色
        public static Color HorseLightBackColor = Color.Gray;
        //跑马灯字体色
        public static Color HorseLightFontColor = Color.White;
        //跑马灯最小显示时间
        public const int HorseLightMinShowDuration = 3000;
        //跑马灯显示位置
        public static HorseLightShowPos HorseLightPos = HorseLightShowPos.顶部;
        //跑马灯显示动画
        public static HorseLightAnimation HorseLightAni = HorseLightAnimation.淡入;

        //提示动画字体色
        public static Color RemindAniFontColor = Color.Green;

        //是否开启休息功能
        public static bool OpenRest = false;
        //休息时长
        public static TimeSpan RestTimeSpan = new TimeSpan(0, 10, 0);
        //工作时长
        public static TimeSpan WorkTimeSpan = new TimeSpan(0, 30, 0);
        //休息背景路径
        public static string RestBackground = "";
        //休息时间字体颜色
        public static Color RestTimeFontColor = Color.GreenYellow;
        //休息信息显示位置
        public static RestInfoPos RestInfoPoint = RestInfoPos.Center;

        #endregion





        void init() {
            _parser = new FileIniDataParser();

            if (!System.IO.File.Exists(AppSettings.IniPath)) {
                genDefaultIni();
            } else {
                _data = _parser.ReadFile(AppSettings.IniPath);
            }
        }

        void genDefaultIni() {
            var d = new IniData();

            //normal
            d["normal"]["AutoStart"] = AutoStart.ToString();

            //snip
            d["snip"]["WheelScalePer"] = AppSettings.WheelScalePer.ToString();
            d["snip"]["DrawScaleInfoCD"] = AppSettings.DrawScaleInfoCD.ToString();
            d["snip"]["MinScaleFactor"] = AppSettings.MinScaleFactor.ToString();
            d["snip"]["QuickSavePath"] = AppSettings.QuickSavePath.ToString();
            d["snip"]["QuickSaveFormat"] = AppSettings.QuickSaveFormat.ToString();
            d["snip"]["MaskColor"] = AppSettings.MaskColor.ToArgb().ToString();

            //rest
            d["rest"]["RunTask"] = AppSettings.RunTask.ToString();
            d["rest"]["OpenRest"] = AppSettings.OpenRest.ToString();
            d["rest"]["RestTimeSpan"] = AppSettings.RestTimeSpan.ToString();
            d["rest"]["WorkTimeSpan"] = AppSettings.WorkTimeSpan.ToString();
            d["rest"]["RestBackground"] = AppSettings.RestBackground.ToString();
            d["rest"]["RestTimeFontColor"] = AppSettings.RestTimeFontColor.ToArgb().ToString();
            d["rest"]["RestInfoPoint"] = AppSettings.RestInfoPoint.ToString();


            _data = d;
            SaveIni();
        }

        public void SaveIni() {
            _parser.WriteFile(AppSettings.IniPath, _data);
        }
    }
}
