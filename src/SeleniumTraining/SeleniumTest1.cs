using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace SeleniumTraining
{
    public class SeleniumTest1
    {
        [Fact]
        public void ChromeSession()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://google.com");

            string title = driver.Title;
            Assert.Equal("Google", title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            IWebElement searchBox = driver.FindElement(By.Name("q"));
            IWebElement searchButton = driver.FindElement(By.Name("btnK"));

            searchBox.SendKeys("Selenium");
            searchButton.Click();

            searchBox = driver.FindElement(By.Name("q"));
            string value = searchBox.GetAttribute("value");
            Assert.Equal("Selenium", value);

            driver.Quit();
        }
    }
}
