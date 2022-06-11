using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace SeleniumTraining
{
    public class SeleniumWebDriverExamples : BaseSeleniumTest
    {
        [Fact]
        public void ChromeSession()
        {
            GoToPage("https://google.com");

            string title = driver.Title;
            Assert.Equal("Google", title);
            /*Implicit wait tells WebDriver to poll the DOM for a certain amount
            of time when trying to find an element if it's not immediately available.
            The default is 0/disabled. When set, it's active for the session.*/
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            IWebElement searchBox = driver.FindElement(By.Name("q"));
            IWebElement searchButton = driver.FindElement(By.Name("btnK"));

            searchBox.SendKeys("Selenium");
            searchButton.Click();

            searchBox = driver.FindElement(By.Name("q"));
            string value = searchBox.GetAttribute("value");
            Assert.Equal("Selenium", value);
        }

        [Fact]
        public void Explicit_wait_test()
        {
            GoToPage("https://www.google.com");
            driver.FindElement(By.Name("q")).SendKeys("cheese" + Keys.Enter);
            /* Explicit waits are available to Selenium clients for imperative,
            procedural languages. They allow your code to halt program execution,
            or freeze the thread, until the condition you pass it resolves.
            The condition is called with a certain frequency until the timeout
            of the wait is elapsed. This means that for as long as the condition
            returns a falsy value, it will keep trying and waiting.*/
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement firstResult = wait.Until(e => e.FindElement(By.XPath("//a/h3")));
            firstResult.Click();

            Assert.False(driver.Url == "https://www.google.com");
        }
    }
}
