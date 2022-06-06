using System;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace SeleniumTraining.SynchronizationStrategies
{
    public class L03ImplicitWaitTest : BaseSeleniumTestWithConsoleLog
    {
        public L03ImplicitWaitTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Easy_To_Sync_buttons_click_exercise()
        {
            driver.Navigate().GoToUrl("https://eviltester.github.io/synchole/buttons.html");
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
        public void Harder_To_Sync_buttons_click_exercise()
        {
            driver.Navigate().GoToUrl("https://eviltester.github.io/synchole/buttons.html");
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
    }
}
