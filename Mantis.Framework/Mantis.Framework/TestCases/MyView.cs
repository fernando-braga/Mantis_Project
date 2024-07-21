using Mantis.Framework.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Mantis.Framework.TestCases.HomePage
{  
    [TestFixture]
    public class HomePageTests 
    {
        private IWebDriver driver;
        private MyViewPage MyViewPage;
        private BaseSettings baseSettings;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            baseSettings = new BaseSettings(driver);
            baseSettings.Login("Fernando_Santos", "1702Paramore**");
            MyViewPage = new MyViewPage(driver);
            MyViewPage.LoadConfigAndNavigate();
        }

        [Test]
        public void VerifyLeftMenuIcons()
        {
            MyViewPage.VerifyLeftMenuIcons();
        }

        [Test]
        public void GoToMyIssuesPageFromHomePage()
        {
            string unassignedTitle = "Não Atribuídos";
            string reportedTitle = "Relatados por Mim";
            string resolvedTitle = "Resolvidos";
            string modifiedTitle = "Modificados Recentemente (30 Dias)";
            string monitoredTitle = "Monitorados por Mim";
            MyViewPage.GoToMyIssuesPageFromHomePage(unassignedTitle, reportedTitle, resolvedTitle, modifiedTitle, monitoredTitle);
        }      

        [Test]
        public void GoToReportIssueFromHomePage()
        {
            MyViewPage.GoToReportIssueFromNavbar();
        }

        [Test]
        public void SearchIssueByID()
        {
            MyViewPage.SearchIssueByIdOnHomePage();
        }

        [Test]
        public void VerifyTimelineBox()
        {           
            MyViewPage.VerifyTimelineBox();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

    }
}
