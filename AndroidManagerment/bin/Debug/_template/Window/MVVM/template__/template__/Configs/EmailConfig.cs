using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template__.Configs
{
    public class EmailConfig
    {
        public static string _smtpServer = ConfigurationManager.AppSettings.Get("");
        public static string _enableSsl = ConfigurationManager.AppSettings.Get("");
        public static string _smtpPort = ConfigurationManager.AppSettings.Get("");
        public static string _smtpUser = ConfigurationManager.AppSettings.Get("");
        public static string _smtpPass = ConfigurationManager.AppSettings.Get("");
        public static string _adminEmail = ConfigurationManager.AppSettings.Get("");
    }
}
