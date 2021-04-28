using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Run
{
    class Program
    {
        const string _vstest = "vstest.console.exe";
        const string _tests = @"/Tests:";

        static void Main(string[] args)
        {
            // get path file dll and testmethod
            var listPath = PathHelper.GetPathDllAndTestMethod();
            if (listPath.Count == 0) return;

            string command = null;
            foreach (var item in listPath)
            {
                // key: testmethod , value: dll
                var pathDll = item.Value;
                var pathTestMethods = item.Key;

                var contents = FileHelper.ReadFile(pathTestMethods);
                command = String.Format("{0} {1} {2}{3}", _vstest, pathDll, _tests, contents);
            }

            // run dev cmd
            var process = new ProcessHelper(PathHelper.GetPathLaunchDevCmd());
            if (command != null)
            {
                Console.WriteLine("Command: " + command);
                process.Send(command);
            }
            process.WaitForExist();
        }
    }
}
