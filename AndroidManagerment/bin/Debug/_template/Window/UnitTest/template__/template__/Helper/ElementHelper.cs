using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace template__
{
    public static class ElementHelper
    {
        private static int _move = 10;
        private static int _heightMenu = 25;
        private static int _loop = 3;

        public static WindowsElement FindElementWithScroll(
            this WindowsDriver<WindowsElement> session,
            string ScrollAccessibilityId, TypeEnum @enum, string eleName)
        {
            //move mouse to scroll
            var scroll = session.FindElementByAccessibilityId(ScrollAccessibilityId);
            if (scroll == null) return null;

            Actions actions = new Actions(session);
            actions.MoveToElement(scroll);
            actions.MoveByOffset((scroll.Size.Width / 2) - 8, ((scroll.Size.Height / 2) - 5));
            actions.Perform();

            //loop find element
            int y = 0;
            actions = new Actions(session);
            actions.Click();

            while (y < scroll.Size.Height - _heightMenu)
            {
                //find element
                var element = scroll.FindElement(@enum, eleName);
                if (element != null
                    && element.Location.Y < y + scroll.Location.Y + scroll.Size.Height
                    && element.Displayed)
                {
                    actions.Perform();
                    return element as WindowsElement;
                }

                //action
                for (int i = 0; i < _loop; i++)
                {
                    actions.Perform();
                    y += _move;
                }
            }

            actions.Release();
            return null;
        }

        static WindowsElement FindElement(this WindowsElement scroll, TypeEnum @enum, string eleName)
        {
            AppiumWebElement ele = null;
            switch (@enum)
            {
                case TypeEnum.AccessibilityId:
                    {
                        ele = scroll.FindElementByAccessibilityId(eleName);
                        break;
                    }
                case TypeEnum.Name:
                    {
                        ele = scroll.FindElementByName(eleName);
                        break;
                    }
                case TypeEnum.TagName:
                    {
                        ele = scroll.FindElementByTagName(eleName);
                        break;
                    }
                case TypeEnum.XPath:
                    {
                        ele = scroll.FindElementByXPath(eleName);
                        break;
                    }
                default: break;
            }
            return ele as WindowsElement;
        }

        public static bool IsOutOfRange(this WindowsDriver<WindowsElement> session,
            string ScrollAccessibilityId, TypeEnum @enum, string eleName)
        {
            var scroll = session.FindElementByAccessibilityId(ScrollAccessibilityId);

            var element = scroll.FindElement(@enum, eleName);
            if (element != null
                && element.Location.Y > scroll.Location.Y + scroll.Size.Height
                && element.Displayed)
            {
                return true;
            }
            return false;
        }
    }
}
