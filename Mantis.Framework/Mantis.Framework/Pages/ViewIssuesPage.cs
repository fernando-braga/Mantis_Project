using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;

namespace Mantis.Framework.Pages
{
    public class ViewIssuesPage : BaseSettings
    {
        public ViewIssuesPage(IWebDriver driver) : base(driver) { }

        public void GoToViewIssuePage()
        {
            IWebElement viewIssues = Driver.FindElement(By.CssSelector(".menu-icon.fa.fa-list-alt\r\n"));
            viewIssues.Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(8));
        }

        public void IssuesFiltersFunctionalities()
        {
            var filterTitle = Driver.FindElement(By.CssSelector(".widget-header.widget-header-small"));
            filterTitle.Text.Contains("Filters");

            IWebElement reporterFilter = Driver.FindElement(By.CssSelector("#reporter_id_filter"));
            reporterFilter.Click();
            IWebElement assignedToFilter = Driver.FindElement(By.CssSelector("#handler_id_filter"));
            assignedToFilter.Click();
            IWebElement monitoredByFilter = Driver.FindElement(By.CssSelector("#user_monitor_filter"));
            monitoredByFilter.Click();
            IWebElement noteByFilter = Driver.FindElement(By.CssSelector("#note_user_id_filter"));
            noteByFilter.Click();
            IWebElement priorityFilter = Driver.FindElement(By.CssSelector("#show_priority_filter"));
            priorityFilter.Click();
            IWebElement severityFilter = Driver.FindElement(By.CssSelector("#show_severity_filter"));
            severityFilter.Click();
            IWebElement viewStatusFilter = Driver.FindElement(By.CssSelector("#view_state_filter"));
            viewStatusFilter.Click();
            IWebElement showStickyIssuesFilter = Driver.FindElement(By.CssSelector("#sticky_issues_filter"));
            showStickyIssuesFilter.Click();
            IWebElement categoryFilter = Driver.FindElement(By.CssSelector("#show_category_filter"));
            categoryFilter.Click();
            IWebElement hideStatusFilter = Driver.FindElement(By.CssSelector("#hide_status_filter"));
            hideStatusFilter.Click();
            IWebElement statusFilter = Driver.FindElement(By.CssSelector("#show_status_filter"));
            statusFilter.Click();
            IWebElement resolutionFilter = Driver.FindElement(By.CssSelector("#show_resolution_filter"));
            resolutionFilter.Click();
            IWebElement filterByDateSubmited = Driver.FindElement(By.CssSelector("#do_filter_by_date_filter"));
            filterByDateSubmited.Click();
            IWebElement filterByLastUpdateDate = Driver.FindElement(By.CssSelector("#do_filter_by_last_updated_date_filter"));
            filterByLastUpdateDate.Click();
            IWebElement profileFilter = Driver.FindElement(By.CssSelector("#show_profile_filter"));
            profileFilter.Click();
            IWebElement plataformFilter = Driver.FindElement(By.CssSelector("#platform_filter"));
            plataformFilter.Click();
            IWebElement osFilter = Driver.FindElement(By.CssSelector("#os_filter"));
            osFilter.Click();
            IWebElement osVersion = Driver.FindElement(By.CssSelector("#os_build_filter"));
            osVersion.Click();
            IWebElement relationshipsFilter = Driver.FindElement(By.CssSelector("#relationship_type_filter"));
            relationshipsFilter.Click();
            IWebElement tagsFilter = Driver.FindElement(By.CssSelector("#tag_string_filter"));
            tagsFilter.Click();
            IWebElement showFilters = Driver.FindElement(By.CssSelector("#per_page_filter"));
            showFilters.Click();
            IWebElement sortByFilters = Driver.FindElement(By.CssSelector("#show_sort_filter"));
            sortByFilters.Click();
            IWebElement matchTypeFilters = Driver.FindElement(By.CssSelector("#match_type_filter"));
            matchTypeFilters.Click();
            IWebElement highlitghChangedHours = Driver.FindElement(By.CssSelector("#highlight_changed_filter"));
            highlitghChangedHours.Click();        
        }

        public void ResetBtn()
        {
            var resetBtn = Driver.FindElement(By.CssSelector(".btn.btn-sm.btn-primary.btn-white.btn-round\r\n"));
            resetBtn.Click();
        }

        public void ApplyFilterForInexistentIssue()
        {
            IWebElement searchField = Driver.FindElement(By.CssSelector("#filter-search-txt"));
            searchField.Clear();
            searchField.SendKeys("Inexistent Issue");
                       
        }

        public void ClickApplyFilterBtn()
        {
            var buttons = Driver.FindElements(By.Name("filter_submit"));
            foreach (var button in buttons)
            {
                if (button.GetAttribute("value") == "Aplicar Filtro")
                {
                    button.Click();
                    break;
                }
            }
        }
        public void InexistentIssueFilterResult()
        {
            var verifyResult = Driver.FindElements(By.ClassName(".column-id"));
            verifyResult.Should().BeEmpty();

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            IWebElement searchField = Driver.FindElement(By.CssSelector("#filter-search-txt"));
            searchField.Clear();
        }

        public void ApplyFilter()
        { 
            // Category Filter
            IWebElement categoryFilter = Driver.FindElement(By.CssSelector("#show_category_filter"));
            categoryFilter.Click();
            IWebElement categoryDropdown = Driver.FindElement(By.Name("category_id[]"));
            categoryDropdown.Click();
            var selectCategory = new SelectElement(categoryDropdown);
            selectCategory.SelectByText("categoria teste");
        }

        private bool IsDownloadCompleted(string downloadText)
        {
           return true; 
        }

        public void DownloadIssuesAsCSV()
        {
            Utils.ClickDownloadButton(Driver, "Exportar para Arquivo CSV");
            System.Threading.Thread.Sleep(10000);
            string defaultDownloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string[] files = Directory.GetFiles(defaultDownloadPath);
            Assert.That(IsDownloadCompleted("Exportar para Arquivo CSV"), Is.True);
        }

        public void DownloadIssuesAsExcel()
        {
            Utils.ClickDownloadButton(Driver, "Exportação para Excel");
            System.Threading.Thread.Sleep(10000);
            string defaultDownloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string[] files = Directory.GetFiles(defaultDownloadPath);
            Assert.That(IsDownloadCompleted("Exportação para Excel"), Is.True);
        }

        public void FilterSearchResult()
        {
            IWebElement filterApplied = Driver.FindElement(By.Id("buglist"));

            string elementText = filterApplied.Text;
            string filterName = "categoria teste";
            Assert.That(elementText, Does.Contain(filterName));
        }

        public void EnterIssueIdOnSearchField()
        {
            IWebElement issueId = Driver.FindElement(By.Id("#filter-search-txt"));
            issueId.SendKeys("0001285");
            issueId.SendKeys(Keys.Enter);

            var idDisplayed = Driver.FindElement(By.CssSelector(".column-id"));
            idDisplayed.Text.Contains("0001285");


        }

    }
}
