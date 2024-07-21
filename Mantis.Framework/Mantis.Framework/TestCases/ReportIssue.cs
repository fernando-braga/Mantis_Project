using Mantis.Framework.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace Mantis.Framework.TestCases
{
    [TestFixture]
    public class ReportIssueTests 
    {
        private IWebDriver driver;
        private ReportIssuePage ReportIssuePage;
        private MyViewPage MyViewPage;
        private BaseSettings baseSettings;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            baseSettings = new BaseSettings(driver);
            baseSettings.Login("Fernando_Santos", "1702Paramore**");
            ReportIssuePage = new ReportIssuePage(driver);
            MyViewPage = new MyViewPage(driver);
            ReportIssuePage.LoadConfigAndNavigate();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }

        [Test]
        public void CreateNewIssue()
        {
            string projectDirectory = Directory.GetCurrentDirectory();
            string imagePath = Path.Combine(projectDirectory, "Fixtures", "TimeOutEvidence.png");
                
            ReportIssuePage.GoToReportIssuePage();
            ReportIssuePage.SelectCategory();
            ReportIssuePage.SelectReproducibility();
            ReportIssuePage.SelectSeverityAndPriority();
            ReportIssuePage.EnterRandomSummary();
            ReportIssuePage.FillOutForms();           
            ReportIssuePage.UploadIssueEvidenceFile(imagePath);
            ReportIssuePage.SubmitIssue();
        }

        [Test]
        public void ConfirmNewIssueCreation()
        {              
            MyViewPage.VerifyNewCreationOnTimelineBox();
        }           

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
 }
