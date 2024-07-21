using OpenQA.Selenium;

namespace Mantis.Framework.Pages
{
    public class LoginPage : BaseSettings
    {
        private string enteredUsername;
        public LoginPage(IWebDriver driver) : base(driver){ }

        public IWebElement FindPageTitleElement()
        {
            return Driver.FindElement(By.CssSelector(".header.lighter.bigger"));
        }

        public void EnterUserName(string username)
        {
            enteredUsername = username;

            IWebElement usernameField = Driver.FindElement(By.CssSelector("#username"));
            usernameField.SendKeys(enteredUsername);
        }

        public string GetEnteredUserName()
        {
            return enteredUsername;
        }

        public void ClickLoginBtn()
        {
            Driver.FindElement(By.CssSelector(".btn-success")).Click();
        }

        public void EnterPassword(string password)
        {
            IWebElement passwordField = Driver.FindElement(By.CssSelector("#password"));
            passwordField.SendKeys(password);
        }

        public string GetPreviousEnteredUserName()
        {
            string enteredUsername = Driver.FindElement(By.CssSelector("#hidden_username")).GetAttribute("value");
            return $"Enter password for '{enteredUsername}'";
        }

    }
}

