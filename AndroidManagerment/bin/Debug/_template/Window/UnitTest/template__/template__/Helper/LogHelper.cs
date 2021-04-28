using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace template__
{
    public class LogHelper
    {
        private const string _folder = "TestLoggers";
        private static List<LogModel> _logModels;

        public static List<LogModel> LogModels { get 
            {
                if (_logModels == null)
                    _logModels = new List<LogModel>();
                return _logModels;
                
            }
            set => _logModels = value; }

        public void Passed(string msg)
        {
            var model = new LogModel
            {
                DateTime = DateTime.Now,
                Result = "Passed",
                Description = String.Format("[{0}]: {1}", InfomationHelper.Get2ndMethod(), msg.Trim()),
            };

            LogModels.Add(model);
        }

        public void Failed(Exception ex)
        {
            var model = new LogModel
            {
                DateTime = DateTime.Now,
                Result = "Failed",
                Description = String.Format("[{0}]: {1}", InfomationHelper.Get2ndMethod(), ex.Message.Trim()),
            };

            LogModels.Add(model);
            Assert.IsTrue(false);
        }

        public void Failed(string msg)
        {
            var model = new LogModel
            {
                DateTime = DateTime.Now,
                Result = "Failed",
                Description = String.Format("[{0}]: {1}", InfomationHelper.Get2ndMethod(), msg.Trim()),
            };

            LogModels.Add(model);
            Assert.IsTrue(false);
        }

        public void GetLogs()
        {
            if (LogModels == null || LogModels.Count == 0)
                return;

            var path = GetBaseFolderProject() + _folder;
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            FileHelper<LogModel>.ExportFileCSV(LogModels, path + '\\');

            LogModels = null;
        }

        private string GetBaseFolderProject()
        {
            var path = Environment.CurrentDirectory;
            var name = Assembly.GetCallingAssembly().GetName().Name;
            string pathBase = "";

            foreach (var item in path.Split('\\'))
            {
                if (item == name)
                    break;
                pathBase += item + '\\';
            }
            return pathBase;
        }
    }
}
