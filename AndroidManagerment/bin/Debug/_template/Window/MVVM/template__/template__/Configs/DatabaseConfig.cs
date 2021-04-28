using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template__.Configs
{
    public class DatabaseConfig
    {
        public readonly string _json = ConfigurationManager.AppSettings["Json"];
        public readonly string _pathDatabase = AppDomain.CurrentDomain.BaseDirectory + @"\_data\";
    }
}
