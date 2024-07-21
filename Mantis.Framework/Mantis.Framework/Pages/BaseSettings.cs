using OpenQA.Selenium;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Mantis.Framework.Pages
{
    public class BaseSettings
    {
        public IWebDriver Driver;
        public TimeSpan ImplicitWaitTimeout = TimeSpan.FromSeconds(5);

        public BaseSettings(IWebDriver driver)
        {
            Driver = driver;
            SetImplicitWait(ImplicitWaitTimeout);
        }
        public void LoadConfigAndNavigate()
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fixtures", "config.json");
            string json = File.ReadAllText(jsonPath);
            var config = JObject.Parse(json);
            string baseUrl = config["BaseUrl"].ToString();

            Driver.Navigate().GoToUrl(baseUrl);
        }

        public void SetImplicitWait(TimeSpan timeout)
        {
            Driver.Manage().Timeouts().ImplicitWait = timeout;
        }

        public void NavigateToUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        private void EnterUserName(string username)
        {
            IWebElement usernameField = Driver.FindElement(By.CssSelector("#username"));
            usernameField.SendKeys(username);
        }

        private void ClickLoginBtn()
        {
            Driver.FindElement(By.CssSelector(".btn-success")).Click();
        }

        private void EnterPassword(string password)
        {
            IWebElement passwordField = Driver.FindElement(By.CssSelector("#password"));
            passwordField.SendKeys(password);
        }

        public void Login(string username, string password)
        {
            LoadConfigAndNavigate();
            EnterUserName(username);
            ClickLoginBtn();
            EnterPassword(password);
            ClickLoginBtn();
        }

        public IWebElement FindElement(By locator)
        {
            return Driver.FindElement(locator);
        }
        public void ClickElement(IWebElement element)
        {
            element.Click();
        }
        public void TypeText(IWebElement element, string text)
        {
            element.SendKeys(text);
        }
        public void CloseBrowser()
        {
            Driver.Quit();
        }
     
    }
}