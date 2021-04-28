using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template__
{
    public static class DriverExecute
    {
        public static bool IsProcessRunning(string name)
        {
            Process[] pname = Process.GetProcessesByName("winappdriver");
            return pname.Length != 0;
        }

        public static void RunProcess(string path)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = path;
                process.StartInfo.Verb = "runas";
                process.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

	public static void KillProcess(string name)
        {
            foreach (var item in Process.GetProcessesByName(name))
            {
                item.Kill();
            }
        }
    }
}
