using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace template__
{
    public class BaseSession : BaseConstant
    {

        protected static LogHelper Log;
        protected static WindowsDriver<WindowsElement> Session;
        protected static Actions Actions;

        protected static void Init(string url, string id, TestContext context)
        {
            if (Session == null)
            {
                //close app
                Close();

                //check condition
                PreInit(ref url);

                try
                {
                    AppiumOptions appCapabilities = new AppiumOptions();
                    appCapabilities.AddAdditionalCapability("app", id);
                    appCapabilities.AddAdditionalCapability("ms:experimental-webdriver", true);
                    Session = new WindowsDriver<WindowsElement>(new Uri(url), appCapabilities);
                }
                catch (Exception)
                {
                    AppiumOptions desktopCapabilities = new AppiumOptions();
                    desktopCapabilities.AddAdditionalCapability("app", "Root");
                    var desktop = new WindowsDriver<WindowsElement>(new Uri(url), desktopCapabilities);

                    WindowsElement recovery = desktop.FindElementByName(NameApp);
                    var hwid = (Int32.Parse(recovery.GetAttribute("NativeWindowHandle"))).ToString("x");

                    AppiumOptions app = new AppiumOptions();
                    app.AddAdditionalCapability("appTopLevelWindow", hwid);
                    app.AddAdditionalCapability("ms:experimental-webdriver", true);
                    Session = new WindowsDriver<WindowsElement>(new Uri(url), app);
                }

                //wait load
                TimeWait();

                //create actions
                if (Actions == null)
                    Actions = new Actions(Session);
            }
        }

        protected static void Init(string id, TestContext context)
        {
            Init(null, id, context);
        }

        protected static void Init(string id)
        {
            Init(null, id, null);
        }

        protected static void Close()
        {
            //write log file
            if (Log != null)
                Log.GetLogs();

            // quit
            if (Session != null)
            {
                Session.Quit();
                Session = null;
            }

        }

        private static void PreInit(ref string url)
        {
            //check url
            if (url == null)
                url = WindowsApplicationDriverUrl;

            //check log create
            if (Log == null)
                Log = new LogHelper();

            //check winappdriver
            if (!DriverExecute.IsProcessRunning(AssemblyNameWinAppDriver))
                DriverExecute.RunProcess(PathWinAppDriver);

            //close app if app is running
            if (DriverExecute.IsProcessRunning(AssemblyNameApp))
                DriverExecute.KillProcess(AssemblyNameApp);
        }

        private static void TimeWait()
        {
            // time to init session
            Session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);

            // Wait for 5 seconds or however long it is needed for the right window to appear/for the splash screen to be dismissed
            Thread.Sleep(TimeSpan.FromSeconds(1.5));

            // Return all window handles associated with this process/application.
            // At this point hopefully you have one to pick from. Otherwise you can
            // simply iterate through them to identify the one you want.
            var allWindowHandles = Session.WindowHandles;

            // Assuming you only have only one window entry in allWindowHandles and it is in fact the correct one,
            // switch the session to that window as follows. You can repeat this logic with any top window with the same
            // process id (any entry of allWindowHandles)
            Session.SwitchTo().Window(allWindowHandles[0]);
        }
    }
}