using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;//WebDriverWait
using SeleniumExtras.WaitHelpers;//ExpectedConditions
using Xunit;

namespace SeleniumTraining.SynchronizationStrategies
{
    public class SyncholeTests : BaseSeleniumTest
    {
        [Fact]
        public void Collapseable_div_expand_and_click_link_using_WebDriverWait()
        {
            driver.Navigate().GoToUrl("https://eviltester.github.io/synchole/collapseable.html");
            IWebElement section = driver.FindElement(By.CssSelector("section.condense"));
            section.Click();

            /* Note: failure to correctly specify the selector will result in
            OpenQA.Selenium.WebDriverTimeoutException : Timed out after 10 seconds
            OpenQA.Selenium.NoSuchElementException  */
            var linkBy = By.CssSelector("a#aboutlink");

            /* Note: unless there is a delay, the test throws OpenQA.Selenium.ElementNotInteractableException
             * but neither of these two approaches are recommended:
            Sleep();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
             * WebDriverWait (explicit wait) is a better approach:
             */
            IWebElement sectionLink = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(ExpectedConditions.ElementToBeClickable(linkBy));
            /* Note: ExpectedConditions class no longer belongs to the OpenQA.Selenium.Support.UI namespace
            https://www.selenium.dev/selenium/docs/api/dotnet/html/T_OpenQA_Selenium_Support_UI_ExpectedConditions.htm
            so you need to add a new NuGet pkg: dotnet add package DotNetSeleniumExtras.WaitHelpers
             */

            sectionLink.Click();
            Assert.EndsWith("/about.html", driver.Url);
        }
    }
}
