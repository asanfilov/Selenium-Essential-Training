using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Xunit;

namespace SeleniumTraining
{
    public class FormyTests : BaseSeleniumTest
    {
        [Fact]
        [Trait("Exercise", "Shows how to click a button in alert https://www.linkedin.com/learning/selenium-essential-training/switch-to-alert")]
        public void SwitchToAlert()
        {
            driver.Navigate().GoToUrl("https://formy-project.herokuapp.com/switch-window");
            IWebElement openAlert = FindElementById("alert-button");
            Sleep(1_000);
            openAlert.Click();

            Sleep(2_000);//add a pause to see what the test does
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        [Fact]
        [Trait("Exercise", "Shows how to close a modal https://www.linkedin.com/learning/selenium-essential-training/executing-javascript-commands")]
        public void ExecuteJavaScript()
        {
            driver.Navigate().GoToUrl("https://formy-project.herokuapp.com/modal");

            IWebElement modalButton = FindElementById("modal-button");
            Sleep(1_000);
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
            Sleep(1_000);
            nameInput.Clear();
            nameInput.SendKeys("John Doe");

            IWebElement button = FindElementById("button");
            button.Click();

            nameInput = FindElementById("name");
            Assert.Equal("", nameInput.Text);
            Assert.Equal("John Doe", nameInput.GetAttribute("value"));
        }

        [Fact]
        [Trait("Exercise", "https://www.linkedin.com/learning/selenium-essential-training/scroll")]
        public void ScrollToElement()
        {
            driver.Navigate().GoToUrl("https://formy-project.herokuapp.com/scroll");
            IWebElement nameInput = FindElementById("name");
            IWebElement dateInput = FindElementById("date");
            Sleep(2_000);

            Actions actions = new Actions(driver);
            actions.MoveToElement(nameInput);
            nameInput.SendKeys("John Doe");
            dateInput.SendKeys("12/12/2021");

            Assert.Equal("12/12/2021", dateInput.GetAttribute("value"));
        }

        [Fact]
        [Trait("Exercise", "https://www.linkedin.com/learning/selenium-essential-training/date-pickers")]
        public void DatepickerTest()
        {
            driver.Navigate().GoToUrl("https://formy-project.herokuapp.com/datepicker");
            IWebElement dateInput = FindElementById("datepicker");
            string today = DateTime.Today.ToString("MM/dd/yyyy");
            dateInput.SendKeys(today);
            dateInput.SendKeys(Keys.Return);//Keys.Enter also works

            Assert.Equal(today, dateInput.GetAttribute("value"));
        }

        [Fact]
        [Trait("Exercise", "https://www.linkedin.com/learning/selenium-essential-training/dropdown-menus")]
        public void Dropdown_list_item_click_test()
        {
            driver.Navigate().GoToUrl("https://formy-project.herokuapp.com/dropdown");
            IWebElement dropDown = FindElementById("dropdownMenuButton");

            dropDown.Click();
            //Watch 'Using wildcards' exercise at 0:45
            var options = driver.FindElements(By.CssSelector("div.dropdown-menu.show a"));
            Assert.Equal(15, options.Count);
            IWebElement item = options.First(o => o.Text.Equals("Key and Mouse Press"));
            item.Click();

            Assert.Equal("https://formy-project.herokuapp.com/keypress", driver.Url);
        }
    }
}
