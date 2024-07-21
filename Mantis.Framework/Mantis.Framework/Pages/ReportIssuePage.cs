using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace Mantis.Framework.Pages
{
    public class ReportIssuePage : BaseSettings
    {
        public ReportIssuePage(IWebDriver driver) : base(driver) { }

        public void GoToReportIssuePage()
        {
            Driver.FindElements(By.CssSelector(".menu-text"))
              .FirstOrDefault(e => e.Text.Contains("Criar Tarefa"))
              ?.Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        }

        public void SelectCategory()
        {
            IWebElement chooseCategory = Driver.FindElement(By.CssSelector("#category_id"));
            chooseCategory.Click();
            IWebElement chooseCategoryDropdown = Driver.FindElement(By.Name("category_id"));
            chooseCategoryDropdown.Click();
            var selectCategory = new SelectElement(chooseCategoryDropdown);
            selectCategory.SelectByText("[Todos os Projetos] categoria teste");
        }

        public void SelectReproducibility()
        {
            IWebElement reproducibilityDropdown = Driver.FindElement(By.CssSelector("#reproducibility"));
            reproducibilityDropdown.Click();
            var selectElement = new SelectElement(reproducibilityDropdown);
            IList<IWebElement> options = selectElement.Options;
            Random random = new Random();
            int index = random.Next(1, options.Count); 
            selectElement.SelectByIndex(index);
        }

        public void SelectSeverityAndPriority()
        {
            By severityDropdownLocator = By.Id("severity");
            By priorityDropdownLocator = By.Id("priority");
            
            string selectedSeverity = SelectRandomOptionFromDropdown(severityDropdownLocator, new List<string> { "trivial", "texto", "mínimo", "recurso", "grande", "travamento", "obstáculo" });

            List<string> priorityOptions;
            if (new List<string> { "grande", "travamento", "obstáculo" }.Contains(selectedSeverity))
            {
                priorityOptions = new List<string> { "alta", "urgente", "imediato" };
            }
            else
            {
                priorityOptions = new List<string> { "nenhuma", "baixa", "normal", "alta", "urgente", "imediato" };
            }

            SelectRandomOptionFromDropdown(priorityDropdownLocator, priorityOptions);
        }

        private string SelectRandomOptionFromDropdown(By dropdownLocator, List<string> optionsToConsider)
        {
            IWebElement dropdownElement = Driver.FindElement(dropdownLocator);
            dropdownElement.Click();

            var selectElement = new SelectElement(dropdownElement);
            IList<IWebElement> options = selectElement.Options;
            IList<IWebElement> filteredOptions = options
                .Where(option => optionsToConsider.Contains(option.Text.Trim()))
                .ToList();

            Random random = new Random();
            int index = random.Next(filteredOptions.Count);

            selectElement.SelectByIndex(options.IndexOf(filteredOptions[index]));

            return filteredOptions[index].Text;
        }

        public void FillOutProfileForm()
        {
            IWebElement enterPlataformName = Driver.FindElement(By.CssSelector("#platform"));
            enterPlataformName.SendKeys("Web Application");
            IWebElement enterOS = Driver.FindElement(By.CssSelector("#os"));
            enterOS.SendKeys("777");
            IWebElement enterOSVersion = Driver.FindElement(By.CssSelector("#os_build"));
            enterOSVersion.SendKeys("777.1.2");
        }

        public static class RandomSummaryTitle
        {
            private static readonly Random Random = new Random();
            private static readonly List<string> Phrases = new List<string>
            {
                "Application is experiencing intermittent issues",
                "Error in data loading process",
                "System crashes under heavy load",
                "Unexpected behavior during user login"
            };

            public static string GenerateRandomTitle()
            {
                int phraseIndex = Random.Next(Phrases.Count);
                string randomPhrase = Phrases[phraseIndex];

                var sb = new StringBuilder();
                sb.Append("[BUG] - ");
                sb.Append(randomPhrase);

                return sb.ToString();
            }
        }

        private IWebElement SummaryField => Driver.FindElement(By.CssSelector("#summary"));

        public void EnterRandomSummary()
        {
            string randomTitle = RandomSummaryTitle.GenerateRandomTitle();
            SummaryField.SendKeys(randomTitle);
        }

        public void FillOutForms()
        {                     
            // Description
            IWebElement enterDescriptionText = Driver.FindElement(By.CssSelector("#description"));
            enterDescriptionText.SendKeys("As a user\n" +
                "I want to navigate through application pages\n" +
                "So I can use all functionalities");

            // Steps to Reproduce
            IWebElement enterStepsToReproduce = Driver.FindElement(By.CssSelector("#steps_to_reproduce"));
            enterStepsToReproduce.SendKeys("Given I enter my credentials on login page\n" +
                "And I log into application\n" +
                "When I click to navigate to other pages from menu icons\n" +
                "Expected Result: Page loads and I proceed to next page\n" +
                "Actual result: The page loading is taking too long to proceed and intermittently returning timeout error");

            // Additional Information
            IWebElement enterAdditionalInformations = Driver.FindElement(By.CssSelector("#additional_info"));
            enterAdditionalInformations.SendKeys("Error occurs in different tested browsers ");

            // Attach Tags 
            IWebElement attachTags = Driver.FindElement(By.CssSelector("#tag_string"));
            attachTags.SendKeys("Os 777," + "Test Enviroment");

            IWebElement existingTagsDropdown = Driver.FindElement(By.Name("tag_select"));
            existingTagsDropdown.Click();
            var selectExistingTag = new SelectElement(existingTagsDropdown);
            selectExistingTag.SelectByText("bug");
        }        

        public void UploadIssueEvidenceFile(string filePath)
        {
            IWebElement uploadFile = Driver.FindElement(By.CssSelector(".dropzone.center.dz-clickable"));

            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)Driver;

            jsExecutor.ExecuteScript(@"
            var uploadFile = arguments[0];
            var input = document.createElement('input');
            input.type = 'file';
            input.style.display = 'block'; 
            input.id = 'file-upload';
            uploadFile.appendChild(input);
            return input;", uploadFile);

            IWebElement fileInput = Driver.FindElement(By.Id("file-upload"));

            fileInput.SendKeys(filePath);
            Thread.Sleep(5000);
        }       

        public void SubmitIssue()
        {
            IWebElement submitIssueBtn = Driver.FindElement(By.CssSelector(".btn.btn-primary"));
            submitIssueBtn.Click();
        }
    }

}
