using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTraining
{
    public class TestsBase : IDisposable
    {
        protected readonly IWebDriver driver;

        public TestsBase()
        {
            driver = new ChromeDriver();
        }

        public void Dispose()
        {
            Sleep();
            driver.Quit();
        }

        protected void Sleep(int milliseconds = 2_000)
        {
            System.Threading.Thread.Sleep(milliseconds);
        }

        protected IWebElement FindElementById(string id)
        {
            return driver.FindElement(By.Id(id));
        }
    }
}
