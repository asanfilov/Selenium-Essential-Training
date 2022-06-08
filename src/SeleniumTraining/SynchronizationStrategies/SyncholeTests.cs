using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;//WebDriverWait
using SeleniumExtras.WaitHelpers;//ExpectedConditions. https://github.com/DotNetSeleniumTools/DotNetSeleniumExtras/blob/master/src/WaitHelpers/ExpectedConditions.cs
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

        [Fact]
        public void Collapseable_div_expand_and_click_link_using_custom_ExpectedCondition()
        {
            driver.Navigate().GoToUrl("https://eviltester.github.io/synchole/collapseable.html");
            By expandingSection = By.CssSelector("section.condense");
            IWebElement section = driver.FindElement(expandingSection);
            section.Click();

            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(CustomExpectedConditions.ElementFinishedExpanding(expandingSection));
            By linkBy = By.CssSelector("a#aboutlink");
            driver.FindElement(linkBy).Click();

            Assert.EndsWith("/about.html", driver.Url);
        }

        [Fact]
        public void Collapseable_div_change_heading_text_using_JavaScriptExecutor()
        {
            driver.Navigate().GoToUrl("https://eviltester.github.io/synchole/collapseable.html");
            By headingBy = By.CssSelector("section.synchole > h2");
            IWebElement heading = driver.FindElement(headingBy);
            Assert.Equal("SyncHole", heading.Text);

            const string newText = "My new heading text";
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].innerText=arguments[1];",
                              heading, newText);

            heading = driver.FindElement(headingBy);
            Assert.Equal(newText, heading.Text);
        }

        [Fact]
        public void Messages_waiting_example_using_ExecuteScript_and_ExpectedConditions()
        {
            driver.Navigate().GoToUrl("https://eviltester.github.io/synchole/messages.html");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            /* First, will wait for the page internal script to finish processing - keep inspecting JS variables:
             * if they are publicly accessible from JS, they can be used in combination with JavaScriptExecutor
             * to help synchronize the state.   */
            string js = "return window.totalMessagesReceived > 0 && window.renderingQueueCount == 0 ? 'true' : 'false'";
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            wait.Until(d => jse.ExecuteScript(js).Equals("true"));

            //Second, wait for the page element to obtain desired state
            const string expected = "Message Count: 0 : 0";
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(
                            By.Id("messagecount"), expected));

            Assert.Equal(expected, FindElementById("messagecount").Text);
        }

        [Fact]
        public void Messages_waiting_example_using_ExecuteScript_and_ExpectedConditions_refactored()
        {
            driver.Navigate().GoToUrl("https://eviltester.github.io/synchole/messages.html");

            /*Use a custom ForJavaScriptToEvaluateTo method to catch exceptions caused by invalid JavaScript code:
                string invalidJS = "window; > 0 ";
            */
            string validJS = "window.totalMessagesReceived > 0 && window.renderingQueueCount == 0;";
            GenericJSWait jsWait = new GenericJSWait(driver, TimeSpan.FromSeconds(20));
            jsWait.ForJavaScriptToEvaluateTo(validJS, true);

            //Second, wait for the page element to obtain desired state
            const string expected = "Message Count: 0 : 0";
            new WebDriverWait(driver, TimeSpan.FromSeconds(20))
                .Until(ExpectedConditions.TextToBePresentInElementLocated(
                            By.Id("messagecount"), expected));

            Assert.Equal(expected, FindElementById("messagecount").Text);
        }
    }
}
