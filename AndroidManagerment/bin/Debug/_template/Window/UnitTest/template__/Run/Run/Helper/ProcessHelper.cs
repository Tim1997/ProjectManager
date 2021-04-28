using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    public class ProcessHelper
    {
        public static Process process;
        public ProcessHelper(string exe)
        {
            process = new Process();
            process.StartInfo.FileName = exe;
            process.StartInfo.Verb = "runas";
            process.Start();
        }

        public void Send(string command)
        {
            if(process != null)
            {
                IntPtr h = process.MainWindowHandle;
                SendKeys.SendWait(command);
                SendKeys.SendWait("{ENTER}");
            }
        }

        public void WaitForExist()
        {
            process.WaitForExit();
        }
    }
}
