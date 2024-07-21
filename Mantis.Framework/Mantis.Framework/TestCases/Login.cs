using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Mantis.Framework.Pages;
using FluentAssertions;

namespace Mantis.Tests.TestCases.Login
{  
    [TestFixture]
    public class LoginPageTests 
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);
            loginPage.LoadConfigAndNavigate();
        }

        [Test]
        public void VerifyLoginPageTitle()
        {
            loginPage.FindPageTitleElement().Text.Should().Contain("Entrar");
        }

        [Test]
        public void LoginIntoApplication()
        {
            string username = "Fernando_Santos";
            string password = "1702Paramore**";

            loginPage.EnterUserName(username);
            loginPage.ClickLoginBtn();

            string expectedPhrase = $"Enter password for '{username}'";
            string actualPhrase = loginPage.GetPreviousEnteredUserName();

            actualPhrase.Should().Be(expectedPhrase);

            loginPage.EnterPassword(password);
            loginPage.ClickLoginBtn();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
