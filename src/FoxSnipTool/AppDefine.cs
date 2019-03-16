using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSnipTool {

    //任务类型
    public enum TaskTag {
        Normal,
        RestTipReminder,    //休息提醒
    }

    //任务运行方式
    public enum TaskRunWay {
        FixTime,        //固定时间点
        RepeatTime,     //重复
    }

    //任务提示方式
    public enum TaskShowWay {
        Nune,
        Balloontip, //系统通知
        HorseLight, //跑马灯

        MessageBox, //消息框
        Animation,  //动画
    }

    //Task 列表子项tag
    public enum TaskSubItemTag {
        Title,
        RunWay,
        StartTime,
        TriggerTime,
        ShowWay,
        Desc,
    }


    //休息界面 信息显示位置
    public enum RestInfoPos {
        TopLeft,
        Top,
        TopRight,
        Left,
        Center,
        Right,
        ButtomLeft,
        Buttom,
        ButtomRight,
    }

    //休息界面 背景显示方式
    public enum RestBackgroudType
    {
        Default,    //默认
        Fixed,      //固定图片
        Random,     //随机
    }

}
