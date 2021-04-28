using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace template__.Config
{
    public class BaseConfig
    {
        public string _helloWorld = ConfigurationManager.AppSettings["HelloWorld"];
    }
}
