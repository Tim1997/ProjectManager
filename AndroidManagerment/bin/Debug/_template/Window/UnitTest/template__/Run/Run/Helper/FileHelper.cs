using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    using System.IO;
    public static class FileHelper
    {
        public static string ReadFile(string path)
        {
            string result = "";
            try
            {
                var contents = File.ReadAllText(path);
                
                foreach (var item in contents.Trim().Replace("\n", string.Empty).Split('\r'))
                {
                    if (string.IsNullOrEmpty(item) || item.Length < 2)
                        continue;

                    if (item.Trim()[0] != '/' && item.Trim()[1] != '/')
                        result += item + ",";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.IsNullOrEmpty(result)? null : result.Substring(0, result.Length - 1);
        }
    }
}
