using OpenQA.Selenium;
using System;
using System.IO;

namespace Mantis.Framework.Pages
{
    public static class Utils
    {
        private static readonly string downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        
        static Utils()
        {
            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }
        }

        public static void ClickDownloadButton(IWebDriver driver, string buttonText)
        {
            var buttons = driver.FindElements(By.CssSelector("a.btn.btn-primary.btn-white.btn-round.btn-sm"));

            foreach (var button in buttons)
            {
                if (button.Text.Trim() == buttonText)
                {
                    button.Click();
                    return;
                }
            }
        }

    }
}
