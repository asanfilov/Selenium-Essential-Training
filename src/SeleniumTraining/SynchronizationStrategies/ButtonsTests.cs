using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Xunit;
using Xunit.Abstractions;

namespace SeleniumTraining.SynchronizationStrategies
{
    public class ButtonsTests : BaseSeleniumTestWithConsoleLog
    {
        public ButtonsTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Easy_To_Sync_buttons_click_exercise()
        {
            GoToPage(SyncholePages.Buttons);
            //Guess a timeout that is high enough for the test to pass:
            int timeoutInSeconds = 5;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            Log($"Set ImplicitWait to {timeoutInSeconds} seconds.");

            /*A click adds a new button programmatically: document.createElement("button")
            so the ImplicitWait strategy helps to locate new buttons.*/

            IWebElement button00 = FindElementById("easy00");
            button00.Click();
            Log("button00.Click()");

            IWebElement button01 = FindElementById("easy01");
            button01.Click();
            Log("button01.Click()");

            IWebElement button02 = FindElementById("easy02");
            button02.Click();
            Log("button02.Click()");

            IWebElement button03 = FindElementById("easy03");
            button03.Click();
            Log("button03.Click()");

            Assert.Equal("All Buttons Clicked", FindElementById("easybuttonmessage").Text);
            Log("End");
        }

        [Fact]
        public void Harder_To_Sync_buttons_click_exercise_test_fails()
        {
            GoToPage(SyncholePages.Buttons);
            /*In this scenario, the ImplicitWait strategy will not help: the buttons are
             in the DOM already, but they are initially disabled. So the WebDriver will
             locate and click them all without any waits. If you run the test:
             dotnet test --logger "console;verbosity=detailed" --filter Harder_To_Sync_buttons_click_exercise
             you will see no delay between Clicks:
             2022-06-06 12:53:22Z|Set ImplicitWait to 10 seconds.
             2022-06-06 12:53:23Z|button01.Click()
             2022-06-06 12:53:23Z|button02.Click()
             2022-06-06 12:53:24Z|button03.Click()
             2022-06-06 12:53:24Z|Asserting... (Assert.Equal will fail)
             */
            int timeoutInSeconds = 10;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            Log($"Set ImplicitWait to {timeoutInSeconds} seconds.");

            IWebElement button00 = FindElementById("button00");
            button00.Click();

            IWebElement button01 = FindElementById("button01");
            button01.Click();//works even if the button is disabled
            Log("button01.Click()");

            IWebElement button02 = FindElementById("button02");
            button02.Click();
            Log("button02.Click()");

            IWebElement button03 = FindElementById("button03");
            button03.Click();
            Log("button03.Click()");

            Log("Asserting...");
            Assert.Equal("All Buttons Clicked", FindElementById("easybuttonmessage").Text);
        }

        [Fact]
        public void Harder_To_Sync_buttons_click_exercise_test_passes()
        {
            GoToPage(SyncholePages.Buttons);
            /* Set the highest timeout as seen in the JavaScript code of this page
                to give the WebDriverWait enough time:  */
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(6000));
            Log("Begin");

            ClickHarderToSyncButton(wait, By.Id("button00"));
            ClickHarderToSyncButton(wait, By.Id("button01"));
            ClickHarderToSyncButton(wait, By.Id("button02"));
            ClickHarderToSyncButton(wait, By.Id("button03"));

            Log("Asserting...");
            Assert.Equal("All Buttons Clicked", FindElementById("buttonmessage").Text);
        }

        private void ClickHarderToSyncButton(WebDriverWait wait, By locator)
        {
            IWebElement button = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            button.Click();//works even if the button is disabled
            Log($"{locator.Criteria}.Click()");
        }
    }
}
