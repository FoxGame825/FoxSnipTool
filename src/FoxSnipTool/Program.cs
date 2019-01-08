using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace FoxSnipTool {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Process instance = RunningInstance();
            if (instance == null) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                AppManager.GetInstance().CheckIniConfig();
                //TaskManager.GetInstance().LoadTaskData();

                var form = new Form1();
                AppManager.GetInstance().MainForm = form;
                form.Hide();

                Application.Run();

            } else {
                //There   is   another   instance   of   this   process.   
                HandleRunningInstance(instance);
            }
        }


        public static Process RunningInstance() {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //Loop   through   the   running   processes   in   with   the   same   name   
            foreach (Process process in processes) {
                //Ignore   the   current   process   
                if (process.Id != current.Id) {
                    //Make   sure   that   the   process   is   running   from   the   exe   file.   

                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName) {
                        //Return   the   other   process   instance.   
                        return process;
                    }
                }
            }
            //No   other   instance   was   found,   return   null. 
            return null;
        }
        public static void HandleRunningInstance(Process instance) {
            //Make   sure   the   window   is   not   minimized   or   maximized   
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
            //Set   the   real   intance   to   foreground   window
            SetForegroundWindow(instance.MainWindowHandle);
        }
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;
    }
}
