using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Mantis.Framework.Pages
{
    public class MyViewPage : BaseSettings
    {
        public MyViewPage(IWebDriver driver) : base(driver) { }

        public IList<string> GetAllMenuItemsText()
        {
            var menuItems = Driver.FindElements(By.CssSelector("ul.nav.nav-list > li > a > span.menu-text"));
            var menuItemsText = menuItems.Select(item => item.Text.Trim().Split('\n').Select(line => line.Trim()));

            return menuItemsText.SelectMany(lines => lines).ToList();
        }

        public void VerifyLeftMenuIcons()
        {
            var expectedIcons = new List<string> { "Minha Visão", "Ver Tarefas", "Criar Tarefa", "Registro de Mudanças", "Planejamento" };
            var menuItemsText = GetAllMenuItemsText();

            menuItemsText.Should().Contain(expectedIcons);
        }

        public void GoToMyIssuesPageFromHomePage(string unassignedTitle, string reportedTitle, string resolvedTitle, string modifiedTitle, string monitoredTitle)
        {
            // Unassigned
            var unassignedBox = Driver.FindElements(By.CssSelector(".white"))
                                 .Where(element => element.Text.Contains(unassignedTitle))
                                 .ToList();

            unassignedBox.ForEach(element => {
                element.Click();
                string newUrl = Driver.Url;
                Assert.That(newUrl.Contains("view_all_bug_page.php?filter="), Is.True);
                Driver.Navigate().Back();
            });

            // Reported by Me
            var reportedByMeBox = Driver.FindElements(By.CssSelector(".white"))
                                 .Where(element => element.Text.Contains(reportedTitle))
                                 .ToList();

            reportedByMeBox.ForEach(element => {
                element.Click();
                string newUrl = Driver.Url;
                Assert.That(newUrl.Contains("view_all_bug_page.php?filter="), Is.True);
                Driver.Navigate().Back();
            });

            // Resolved
            var resolvedBox = Driver.FindElements(By.CssSelector(".white"))
                                 .Where(element => element.Text.Contains(resolvedTitle))
                                 .ToList();

            resolvedBox.ForEach(element => {
                element.Click();
                string newUrl = Driver.Url;
                Assert.That(newUrl.Contains("view_all_bug_page.php?filter="), Is.True);
                Driver.Navigate().Back();
            });

            // Modified Recently (30 Days)
            var recentlyModified = Driver.FindElements(By.CssSelector(".white"))
                                 .Where(element => element.Text.Contains(modifiedTitle))
                                 .ToList();

            recentlyModified.ForEach(element => {
                element.Click();
                string newUrl = Driver.Url;
                Assert.That(newUrl.Contains("view_all_bug_page.php?filter="), Is.True);
                Driver.Navigate().Back();
            });

            // Monitored by Me
            var monitoredByMe = Driver.FindElements(By.CssSelector(".white"))
                                 .Where(element => element.Text.Contains(monitoredTitle))
                                 .ToList();

            monitoredByMe.ForEach(element => {
                element.Click();
                string newUrl = Driver.Url;
                Assert.That(newUrl.Contains("view_all_bug_page.php?filter="), Is.True);
                Driver.Navigate().Back();
            });
        }

        public void GoToReportIssueFromNavbar()
        {
            IWebElement reportIssueBtn = Driver.FindElement(By.CssSelector(".btn.btn-primary.btn-sm"));
            reportIssueBtn.Click();
            string newUrl = Driver.Url;
            Assert.That(newUrl.Contains("bug_report_page.php"), Is.True);
        }

        public void SearchIssueByIdOnHomePage()
        {
            IWebElement searchForId = Driver.FindElement(By.Name("bug_id"));
            searchForId.SendKeys("0001285");
            searchForId.SendKeys(Keys.Enter);

            var confirmId = Driver.FindElement(By.CssSelector(".bug-id"));
            confirmId.Text.Contains("0001285");
        }

        public void VerifyTimelineBox()
        {
            bool isTimelinePresent = Driver.FindElements(By.Id("timeline")).Any();
            IWebElement timelineBox = Driver.FindElement(By.Id("timeline"));
            string timelineText = timelineBox.Text;
            Assert.That(timelineText.Contains("Linha do tempo"));           

        }

        public void VerifyNewCreationOnTimelineBox()
        {
            IWebElement timelineBox = Driver.FindElement(By.Id("timeline"));
            string timelineText = timelineBox.Text; 

            Assert.That(timelineText, Does.Contain("Os 777"));
        }
    } 
}
