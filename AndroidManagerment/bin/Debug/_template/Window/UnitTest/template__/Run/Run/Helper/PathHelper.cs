using System.Text;
using System.Threading.Tasks;

namespace Run
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    public static class PathHelper
    {
        const string _nameTestMethod = "TestMethods";
        const string _commandPrompt = "LaunchDevCmd.bat";
        const string _pathVisualStudio = @"C:\Program Files (x86)\Microsoft Visual Studio";
        const string _bin = "bin";
        const string _dll = ".dll";
        const string _txt = ".txt";
        
        public static string GetPathLaunchDevCmd()
        {
            dynamic visualStudioPath = null;
            try
            {
                visualStudioPath = DirectorySearch(_pathVisualStudio, _commandPrompt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // get directory command prompt developer 
            return visualStudioPath;
        }

        public static Dictionary<string,string> GetPathDllAndTestMethod()
        {
            var list = new Dictionary<string, string>();
            var basePathProject = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            var libs = "";
            bool isLib = false;

            foreach (var item in Environment.CurrentDirectory.Split('\\'))
            {
                if (item == _bin)
                    isLib = true;

                if (isLib)
                    libs += item + "\\";
            }

            var nameCurrentProject = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            foreach (var item in Directory.GetDirectories(basePathProject))
            {
                var nameFolder = Path.GetFileName(item);
                if (nameFolder != nameCurrentProject && nameFolder[0] != '.')
                {
                    var dll = String.Format(@"{0}\{1}\{2}", basePathProject, nameFolder, libs);
                    var tests = String.Format(@"{0}\{1}", basePathProject, nameFolder);

                    dll = DirectorySearch(dll, nameFolder + _dll);
                    tests = DirectorySearch(tests, _nameTestMethod + _txt);

                    if (File.Exists(dll) && File.Exists(tests))
                    {
                        list.Add(tests, dll);
                    }
                }
            }
            return list;
        }

        public static string DirectorySearch(string dir, string name)
        {
            if (!Directory.Exists(dir)) return null;

            string[] files = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories)
                                .Where(f => Path.GetFileName(f) == name).ToArray();

            return files.Count() == 0 ? null : files[0];
        }
    }
}
