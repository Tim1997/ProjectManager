using Base;
using Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class JsonHelper<T> where T : BaseModel
    {
        private static JsonHelper<T> _ins;
        public static JsonHelper<T> Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new JsonHelper<T>();
                return _ins;
            }
            set => _ins = value;
        }

        // create, read file
        public T GetFile(string pathFile)
        {
            T file = null;
            try
            {
                string contents = File.ReadAllText(pathFile);
                file = JsonConvert.DeserializeObject<T>(contents);

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return file;
        }
        public List<T> GetAllFile(string pathFolder = null)
        {
            if (pathFolder == null)
                pathFolder = Constants.BaseConstant.BASE_PATH_FOLDER_DATA;

            List<T> files = new List<T>();
            foreach (string filePath in Directory.EnumerateFiles(pathFolder, "*.json"))
            {
                //Console.WriteLine(Path.GetDirectoryName(filePath));
                try
                {
                    string contents = File.ReadAllText(filePath);
                    var jsons = JsonConvert.DeserializeObject<T>(contents);
                    if (jsons != null)
                        files.Add(jsons);

                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    files = null;
                }
            }
            return files;
        }
        public T FindFile(string id, string pathFolder = null)
        {
            if (pathFolder == null)
                pathFolder = Constants.BaseConstant.BASE_PATH_FOLDER_DATA;

            T file = null;
            foreach (var item in GetAllFile(pathFolder))
            {
                if (id == item.Id)
                    file = item;
            }
            return file;
        }

        public void CreateFile(T obj, string pathFolder = null)
        {
            if (pathFolder == null)
                pathFolder = Constants.BaseConstant.BASE_PATH_FOLDER_DATA;

            string json = JsonConvert.SerializeObject(obj);

            System.IO.Directory.CreateDirectory(pathFolder);
            System.IO.File.WriteAllText(pathFolder + "/" + obj.Id + ".json", json);
        }
        public void CreateFiles(List<T> objs)
        {
            foreach (var obj in objs)
            {
                CreateFile(obj);
            }
        }

        public void DeleteFile(string id, string pathFolder = null)
        {
            if (pathFolder == null)
                pathFolder = Constants.BaseConstant.BASE_PATH_FOLDER_DATA;

            var pathFile = pathFolder + "/" + id + ".json";
            if (File.Exists(pathFile))
            {
                File.Delete(pathFile);
            }
        }
        public void DeleteFiles(List<string> ids)
        {
            foreach (var id in ids)
            {
                DeleteFile(id);

            }
        }

        public void ReplaceFile(T obj, string pathFile)
        {
            string json = JsonConvert.SerializeObject(obj);
            System.IO.File.WriteAllText(pathFile, json);
        }
    }
}
