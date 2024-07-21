using Mantis.Framework.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Mantis.Framework.TestCases
{
    [TestFixture]
    public class ViewIssuesTests 
    {
        
        private IWebDriver driver;
        private ViewIssuesPage ViewIssuesPage;
        private BaseSettings baseSettings;
        
        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("safebrowsing.enabled", true);

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            baseSettings = new BaseSettings(driver);
            baseSettings.Login("Fernando_Santos", "1702Paramore**");
            ViewIssuesPage = new ViewIssuesPage(driver);
            ViewIssuesPage.LoadConfigAndNavigate();
        }

        [Test]
        public void VerifyFilters()
        {
            ViewIssuesPage.GoToViewIssuePage();
            ViewIssuesPage.IssuesFiltersFunctionalities();                 
            ViewIssuesPage.ResetBtn();
        }

        [Test]
        public void ApplyFilter()
        {
            ViewIssuesPage.GoToViewIssuePage();
            ViewIssuesPage.ApplyFilter();
            ViewIssuesPage.ClickApplyFilterBtn();
            ViewIssuesPage.FilterSearchResult();
        }

        [Test]
        public void SearchForIssueById()
        {
            ViewIssuesPage.GoToViewIssuePage();

        }

        [Test]
        public void ApplyFilterForInexistentIssue()
        {
            ViewIssuesPage.GoToViewIssuePage();
            ViewIssuesPage.ApplyFilterForInexistentIssue();
            ViewIssuesPage.ClickApplyFilterBtn();
            ViewIssuesPage.InexistentIssueFilterResult();
        }

        [Test]
        public void ViewingIssuesSection()
        {
            ViewIssuesPage.GoToViewIssuePage();
            ViewIssuesPage.DownloadIssuesAsCSV();
            ViewIssuesPage.DownloadIssuesAsExcel();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
