using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FoxSnipTool {
    public static class AppSettings {

        //当前用户 系统启动文件夹路径
        public static string CurUserSystemStartupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        //全局用户 系统启动文件夹路径
        public static string GlobalUserSystemStartupFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);

        //截图快捷键ID
        public static int CtrlF1_ID = 10086;
        //取色快捷键ID
        public static int CtrlF2_ID = 10087;

        //--------------------------------------常规设置---------------------------------------//
        //是否开机启动
        public static bool AutoStart = false;
        //自动缓存
        public static bool AutoCache = false;
        //语言
        public static string Language = "简体中文";
        //是否开启休息功能
        public static bool OpenRest = false;
        //截图是否固定尺寸
        public static bool CanFixSize = false;
        //ini配置路径
        public static string IniPath = Application.StartupPath + "\\config.ini";


        //--------------------------------------截图设置---------------------------------------//
        //鼠标滚轮每次缩放比率
        public static float WheelScalePer = 0.05f;
        //缩放topPicture 显示缩放信息持续时间{ms}
        public static int DrawScaleInfoCD = 1000;
        //最小缩放因子
        public static float MinScaleFactor = 0.1f;
        //鼠标双击最小模式尺寸
        public static Size MiniModeSize = new Size(50, 50);
        //截图音效路径
        public static string CutAudioPath = "snip";

        //快速保存图片路径
        public static string QuickSavePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        //快速保存默认格式
        public static string QuickSaveFormat = ".png";
        //截图遮罩颜色
        public static Color MaskColor = Color.FromArgb(100, 0, 0, 0);
        //固定尺寸
        public static Size FixSize = new Size(50,50);
        //截图自动放入剪切板
        public static bool AutoToClipboard = false;
        //自动缓存图片路径
        public static string AutoCachePath = Application.StartupPath + "\\Cache\\";
        //自动缓存图片最大数
        public static int AutoCacheImgMax = 50;
        


        //---------------------------------------任务设置---------------------------------------------//
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
        

        #region 休息
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
        public static Font DefaultFont = new Font("微软雅黑", 9);

        #endregion

        //随机边框颜色数组
        private static Color[] BorderColorArray = new Color[10]{
            Color.White,
            Color.Violet,
            Color.Tomato,
            Color.Thistle,
            Color.SteelBlue,
            Color.SpringGreen,
            Color.Tan,
            Color.Transparent,
            Color.Turquoise,
            Color.Teal
        };

        //随机边框颜色
        public static Color RandomBorderColor() {
            Random ran = new Random();
            return BorderColorArray[ran.Next(0,BorderColorArray.Length)];
        }
    }
    

}
