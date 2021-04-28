using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template__.Utilities
{
    public class FolderUtility
    {
        public static List<string> GetDirectories(string path, string searchPattern = "*",
       SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (searchOption == SearchOption.TopDirectoryOnly)
                return Directory.GetDirectories(path, searchPattern).ToList();
            var directories = new List<string>();

            foreach (var item in GetDirectories(path, searchPattern))
            {
                string lastFolderName = Path.GetFileName(item);
                directories.Add(lastFolderName);
            }


            //for (var i = 0; i < directories.Count; i++)
            //    directories.AddRange(GetDirectories(directories[i], searchPattern));

            return directories;
        }

        private static List<string> GetDirectories(string path, string searchPattern)
        {
            try
            {
                return Directory.GetDirectories(path, searchPattern).ToList();
            }
            catch (UnauthorizedAccessException)
            {
                return new List<string>();
            }
        }
    }
}
