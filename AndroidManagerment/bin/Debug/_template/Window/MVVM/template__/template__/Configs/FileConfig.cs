using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template__.Configs
{
    public class FileConfig
    {
        public readonly static string _baseName = ConfigurationManager.AppSettings["Template"];
    }
}
