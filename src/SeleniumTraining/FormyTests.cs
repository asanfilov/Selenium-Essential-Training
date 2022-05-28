using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace SeleniumTraining
{
    public class FormyTests : IDisposable
    {
        private readonly IWebDriver driver;

        public FormyTests()
        {
            driver = new ChromeDriver();
        }

        public void Dispose()
        {
            System.Threading.Thread.Sleep(2_000);
            driver.Quit();
        }

        #region Helper methods

        private IWebElement FindElementById(string id)
        {
            return driver.FindElement(By.Id(id));
        }

        #endregion Helper methods

        [Fact]
        [Trait("Exercise", "Shows how to click a button in alert https://www.linkedin.com/learning/selenium-essential-training/switch-to-alert")]
        public void SwitchToAlert()
        {
            driver.Navigate().GoToUrl("https://formy-project.herokuapp.com/switch-window");
            IWebElement openAlert = FindElementById("alert-button");
            System.Threading.Thread.Sleep(1_000);
            openAlert.Click();

            System.Threading.Thread.Sleep(2_000);//add a pause to see what the test does
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        [Fact]
        [Trait("Exercise", "Shows how to close a modal https://www.linkedin.com/learning/selenium-essential-training/executing-javascript-commands")]
        public void ExecuteJavaScript()
        {
            driver.Navigate().GoToUrl("https://formy-project.herokuapp.com/modal");

            IWebElement modalButton = FindElementById("modal-button");
            System.Threading.Thread.Sleep(1_000);
            modalButton.Click();

            IWebElement closeButton = FindElementById("close-button");
            //closeButton.Click(); //will throw OpenQA.Selenium.ElementNotInteractableException
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", closeButton);
        }

        [Fact]
        [Trait("Exercise", "https://www.linkedin.com/learning/selenium-essential-training/keyboard-and-mouse-input")]
        public void KeyboardAndMouseInput()
        {
            driver.Navigate().GoToUrl("https://formy-project.herokuapp.com/keypress");
            IWebElement nameInput = FindElementById("name");

            nameInput.Click();
            nameInput.SendKeys("Doe, John");
            System.Threading.Thread.Sleep(1_000);
            nameInput.Clear();
            nameInput.SendKeys("John Doe");

            IWebElement button = FindElementById("button");
            button.Click();

            nameInput = FindElementById("name");
            Assert.Equal("", nameInput.Text);
            Assert.Equal("John Doe", nameInput.GetAttribute("value"));
        }
    }
}
