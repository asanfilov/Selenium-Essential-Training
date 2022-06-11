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
            const int timeoutInSeconds = 5;
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
            /* In this scenario, the ImplicitWait strategy will not help:
            harder to sync buttons are already in the DOM, but they are
            initially disabled when the page loads. So the WebDriver will locate
            and click them all without any waits. If you run the test:
             dotnet test --logger "console;verbosity=detailed" --filter Harder_To_Sync_buttons_click_exercise
            you will see no delay between Clicks:
             2022-06-06 12:53:22Z|Set ImplicitWait to 10 seconds.
             2022-06-06 12:53:23Z|button01.Click()
             2022-06-06 12:53:23Z|button02.Click()
             2022-06-06 12:53:24Z|button03.Click()
             2022-06-06 12:53:24Z|Asserting... (Assert.Equal will fail)
             */
            const int timeoutInSeconds = 10;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            Log($"Set ImplicitWait to {timeoutInSeconds} seconds.");

            IWebElement button00 = driver.FindElement(HarderToSyncButton.Start);
            button00.Click();

            IWebElement button01 = driver.FindElement(HarderToSyncButton.One);
            button01.Click();//works even if the button is disabled
            Log("button01.Click()");

            IWebElement button02 = driver.FindElement(HarderToSyncButton.Two);
            button02.Click();
            Log("button02.Click()");

            IWebElement button03 = driver.FindElement(HarderToSyncButton.Three);
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

            ClickHarderToSyncButton(wait, HarderToSyncButton.Start);
            ClickHarderToSyncButton(wait, HarderToSyncButton.One);
            ClickHarderToSyncButton(wait, HarderToSyncButton.Two);
            ClickHarderToSyncButton(wait, HarderToSyncButton.Three);

            Log("Asserting...");
            Assert.Equal("All Buttons Clicked", FindElementById("buttonmessage").Text);
        }

        private void ClickHarderToSyncButton(WebDriverWait wait, By locator)
        {
            IWebElement button = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            button.Click();//works even if the button is disabled
            Log($"{locator.Criteria}.Click()");
        }

        [Fact]
        public void SlowLoadableComponent_class_demo()
        {// Example: WebDriver synchronized components
            GoToPage(SyncholePages.Buttons);

            HarderToSyncButton startButton = new HarderToSyncButton(driver, HarderToSyncButton.Start);
            startButton.Load();
            startButton.Click();

            HarderToSyncButton buttonOne = new HarderToSyncButton(driver, HarderToSyncButton.One);
            buttonOne.Load();
            buttonOne.Click();

            HarderToSyncButton buttonTwo = new HarderToSyncButton(driver, HarderToSyncButton.Two);
            buttonTwo.Load();
            buttonTwo.Click();

            HarderToSyncButton buttonThree = new HarderToSyncButton(driver, HarderToSyncButton.Three);
            buttonThree.Load();
            buttonThree.Click();

            Assert.Equal("All Buttons Clicked", FindElementById("buttonmessage").Text);
        }
    }

    /// <summary>
    /// Extends
    /// <see href="https://www.selenium.dev/selenium/docs/api/dotnet/html/T_OpenQA_Selenium_Support_UI_SlowLoadableComponent_1.htm">SlowLoadableComponent</see>
    /// from namespace OpenQA.Selenium.Support.UI
    /// <para>
    /// Benefit: all the work—synchronization, locating, built-in waiting—is done in the button component itself.
    /// </para>
    /// </summary>
    internal class HarderToSyncButton : SlowLoadableComponent<HarderToSyncButton>
    {
        private readonly IWebDriver driver;
        private readonly By locator;

        public static readonly TimeSpan TimeOut = TimeSpan.FromSeconds(10);

        public static readonly By Start = By.Id("button00");
        public static readonly By One = By.Id("button01");
        public static readonly By Two = By.Id("button02");
        public static readonly By Three = By.Id("button03");

        public HarderToSyncButton(IWebDriver driver, By locator) : base(TimeOut)
        {
            this.driver = driver;
            this.locator = locator;
            SleepInterval = TimeSpan.FromMilliseconds(1000);//how often EvaluateLoadedStatus will be called
        }

        public void Click() => driver.FindElement(locator).Click();

        protected override bool EvaluateLoadedStatus()
        {
            IWebElement elem = driver.FindElement(locator);
            return elem.Displayed && elem.Enabled;
        }

        /* No neeed to implement the Load() method:
            harder to sync buttons are already in the DOM, but they are
            initially disabled when the page loads. */

        protected override void ExecuteLoad()
        { }
    }
}
