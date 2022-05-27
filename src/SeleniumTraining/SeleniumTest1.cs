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
            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://google.com");

            var title = driver.Title;
            Assert.Equal("Google", title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var searchBox = driver.FindElement(By.Name("q"));
            var searchButton = driver.FindElement(By.Name("btnK"));

            searchBox.SendKeys("Selenium");
            searchButton.Click();

            searchBox = driver.FindElement(By.Name("q"));
            var value = searchBox.GetAttribute("value");
            Assert.Equal("Selenium", value);

            driver.Quit();
        }
    }
}
