using template__.Configs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template__.Utilities
{
    public class FileUtility : FileConfig
    {
        private static void ReplaceFileWithName(string src, string dest, string nameSpace)
        {
            string[] lines = File.ReadAllLines(src);
            var extend = Path.GetExtension(src);
            var nameFile = Path.GetFileNameWithoutExtension(src);
            nameFile = nameFile.Replace(_baseName, nameSpace);

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace(_baseName, nameSpace);
            }

            File.WriteAllLines(dest + "\\" + nameFile + extend, lines);
        }

        public static void CopyReplaceFolder(string src, string dest, string nameSpace)
        {
            #region kiểm tra tồn tại của folder và tạo mới
            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);
            #endregion

            #region đọc tất cả file và copy, replace sang dest mới
            string[] pathFiles = Directory.GetFiles(src);

            foreach (var pathFile in pathFiles)
            {
                string extend = Path.GetExtension(pathFile);
                string nameFile = Path.GetFileName(pathFile);

                if (extend == ".suo") continue;
                if (extend != ".dll")
                    ReplaceFileWithName(pathFile, dest, nameSpace);
                else
                {
                    string destFolder = Path.Combine(dest, nameFile);
                    File.Copy(pathFile, destFolder);
                }
            }
            #endregion

            #region Đọc tất cả folder trong src rồi copy folder vào dest
            string[] pathFolders = Directory.GetDirectories(src);
            foreach (string pathFolder in pathFolders)
            {
                string nameFolder = Path.GetFileName(pathFolder);
                if (nameFolder != ".vs")
                {
                    nameFolder = nameFolder.Replace(_baseName, nameSpace);
                    string destFolder = Path.Combine(dest, nameFolder);

                    CopyReplaceFolder(pathFolder, destFolder, nameSpace);
                }
            }
            #endregion
        }
    }
}
