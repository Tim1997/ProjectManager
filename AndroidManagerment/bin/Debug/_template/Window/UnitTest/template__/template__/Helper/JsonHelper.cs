using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template__.Constant;

namespace template__
{
    public class JsonHelper : HelperConstant
    {
        public static T LoadObject<T>(string name) where T : IJsonObject, new()
        {
            IList<T> list = null;

            try
            {
                var path = GetJsonPath(name);
                if (!File.Exists(path))
                    return new T();

                using (StreamReader sr = new StreamReader(path))
                {
                    list = JsonConvert.DeserializeObject<IList<T>>(sr.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex.Message);
            }
            if (list == null
                || list.Count == 0)
                return new T();

            return list.FirstOrDefault();
        }

        public static IList<T> LoadList<T>(string name)
        {
            IList<T> list = null;

            try
            {
                var path = GetJsonPath(name);
                if (!File.Exists(path))
                    return new List<T>();

                using (StreamReader sr = new StreamReader(path))
                {
                    list = JsonConvert.DeserializeObject<IList<T>>(sr.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex.Message);
            }
            if (list == null
                || list.Count == 0)
                return new List<T>();

            return list;
        }

        public static bool SaveObject<T>(string name, T obj)
        {
            try
            {
                string path = GetJsonPath(name);
                using (StreamWriter sw = new StreamWriter(path))
                {
                    List<T> list = new List<T> { obj };
                    var json = JsonConvert.SerializeObject(list);
                    sw.Write(json);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex.Message);
            }
            return false;
        }

        public static bool SaveList<T>(string name, IList<T> list)
        {
            try
            {
                string path = GetJsonPath(name);
                using (StreamWriter sw = new StreamWriter(path))
                {
                    var json = JsonConvert.SerializeObject(list);
                    sw.Write(json);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex.Message);
            }
            return false;
        }

        public static string GetJsonPath(string name)
        {
            return String.Format("{0}{1}{2}", PathDataSamsungPCCleaner, name, ".json");
        }
    }
}
