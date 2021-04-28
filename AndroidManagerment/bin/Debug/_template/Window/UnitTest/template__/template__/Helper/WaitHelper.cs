using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template__;

namespace SamsungPCCleaner_v1
{
    public static class WaitHelper
    {
        public static void WaitUntilNotShown(this WindowsDriver<WindowsElement> session, TypeEnum type, string element)
        {
            while (true)
            {
                try
                {
                    switch (type)
                    {
                        case TypeEnum.AccessibilityId:
                            session.FindElementByAccessibilityId(element);
                            break;
                        case TypeEnum.XPath:
                            session.FindElementByXPath(element);
                            break;
                        case TypeEnum.Name:
                            session.FindElementByName(element);
                            break;
                        case TypeEnum.TagName:
                            session.FindElementByTagName(element);
                            break;
                        default: break;
                    }
                }
                catch (Exception) { break; }
            }
        }

        public static void WaitUntilNotShown(this WindowsDriver<WindowsElement> session, string accessibilityIdViewSource, TypeEnum type, string element)
        {
            var source = session.FindElementByAccessibilityId(accessibilityIdViewSource);
            while (true)
            {
                try
                {
                    switch (type)
                    {
                        case TypeEnum.AccessibilityId:
                            source.FindElementByAccessibilityId(element);
                            break;
                        case TypeEnum.XPath:
                            source.FindElementByXPath(element);
                            break;
                        case TypeEnum.Name:
                            source.FindElementByName(element);
                            break;
                        case TypeEnum.TagName:
                            source.FindElementByTagName(element);
                            break;
                        default: break;
                    }
                }
                catch (Exception) { break; }
            }
        }

        public static void WaitUntilShown(this WindowsDriver<WindowsElement> session, string accessibilityIdViewSource, TypeEnum type, string element)
        {
            var source = session.FindElementByAccessibilityId(accessibilityIdViewSource);
            while (true)
            {
                try
                {
                    if(type == TypeEnum.AccessibilityId)
                    {
                        source.FindElementByAccessibilityId(element);
                        break;
                    }
                    else if(type == TypeEnum.XPath)
                    {
                        source.FindElementByXPath(element);
                        break;
                    }
                    else if(type == TypeEnum.Name)
                    {
                        source.FindElementByName(element);
                        break;
                    }
                    else if(type == TypeEnum.TagName)
                    {
                        source.FindElementByTagName(element);
                        break;
                    }
                }
                catch (Exception) {}
            }
        }

        public static void WaitUntilElementShown(this WindowsDriver<WindowsElement> session, TypeEnum type, string element, int timeOut)
        {
            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(session)
            {
                Timeout = TimeSpan.FromSeconds(timeOut),
                PollingInterval = TimeSpan.FromSeconds(0.5),
            };

            wait.IgnoreExceptionTypes(typeof(InvalidOperationException));
            wait.Until(driver =>
            {
                int eleCount = 0;
                switch (type)
                {
                    case TypeEnum.AccessibilityId:
                        eleCount = driver.FindElementsByAccessibilityId(element).Count;
                        break;
                    case TypeEnum.XPath:
                        eleCount = driver.FindElementsByXPath(element).Count;
                        break;
                    case TypeEnum.Name:
                        eleCount = driver.FindElementsByName(element).Count;
                        break;
                    case TypeEnum.TagName:
                        eleCount = driver.FindElementsByTagName(element).Count;
                        break;
                    default: break;
                }
                return eleCount > 0;
            });
        }
    }
}
