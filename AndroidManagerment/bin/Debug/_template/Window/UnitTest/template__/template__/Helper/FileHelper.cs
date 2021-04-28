using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace template__
{
    public static class FileHelper<IModel> where IModel : BaseLogModel
    {
        /// <summary>
        /// ExportFileCSV
        /// </summary>
        /// <param name="models"></param>
        /// <param name="path">C:abcd\\</param>
        public static void ExportFileCSV(List<IModel> models, string path)
        {
            // name file log
            var nproject = Assembly.GetCallingAssembly().GetName().Name;
            var dtime = DateTime.Now.ToString("yyyyMMdd-HHmmss");

            var nfile = String.Format("{0}-{1}", dtime, nproject);

            // write file
            // write title
            var title = "";
            foreach (var prop in models[0].GetType().GetProperties())
            {
                title += prop.Name + ",";
            }

            // write body
            var body = "";
            foreach (var model in models)
            {
                Type myType = model.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                foreach (PropertyInfo prop in props)
                {
                    body += prop.GetValue(model, null).ToString() + ",";
                }
                body = body.Substring(0, body.Length - 1);
                body += "\r\n";
            }

            // write contents
            var contents = String.Format("{0}\r\n{1}", title.Substring(0, title.Length - 1), body);

            // write file with contents
            File.WriteAllText(path + nfile + ".csv", contents);
        }
    }
}
