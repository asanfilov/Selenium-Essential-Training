//https://www.linkedin.com/learning/advanced-selenium-3-synchronization-strategies/webdriver-built-in-waits
using OpenQA.Selenium;
using Xunit;

namespace SeleniumTraining.SynchronizationStrategies
{
    public class L02WebDriverBuiltinWaits : TestsBase
    {
        private const string baseUrl = "https://eviltester.github.io/synchole/";

        private void GoToPage(string page)
        {
            driver.Navigate().GoToUrl(baseUrl + page);
        }

        [Fact]
        public void No_need_to_wait_for_DOM_contents_on_page_load()
        {
            GoToPage("collapseable.html");
            //WebDriver automatically waits for the page to load
            Assert.Equal("SyncHole", driver.FindElement(By.CssSelector("h2")).Text);
        }

        [Fact]
        public void Form_submit_will_wait_for_page_to_load()
        {
            GoToPage("form.html");

            IWebElement username = driver.FindElement(By.Name("username"));
            username.SendKeys("Bob");
            username.Submit();

            Assert.Equal("Thanks For Your Submission", driver.FindElement(By.Id("thanks")).Text);
        }

        [Fact]
        public void Does_not_sync_on_JavaScript_that_populates_username()
        {
            GoToPage("form.html");

            IWebElement username = driver.FindElement(By.Name("username"));
            username.SendKeys("Bob");
            username.Submit();

            Assert.Equal("Thanks For Your Submission",
                         driver.FindElement(By.Id("thanks")).Text);

            /* Throws OpenQA.Selenium.NoSuchElementException Unable to locate element:
             * {"method":"css selector","selector":"li[data-name='username']"} unless this is present:
            Sleep();
             */
            Assert.Throws<NoSuchElementException>(() => driver
            .FindElement(By.CssSelector("li[data-name='username']"))
            .GetAttribute("data-value"));
        }

        [Fact]
        public void ReadyState_is_prior_to_the_JavaScript_onLoad_event_firing()
        {
            GoToPage("results.html?username=bob&submitButton=submit");

            string readyState = (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState");
            Assert.Equal("complete", readyState);

            /* Throws OpenQA.Selenium.NoSuchElementException Unable to locate element:
             * {"method":"css selector","selector":"li[data-name='username']"} unless this is present:
            Sleep();
             */
            Assert.Throws<NoSuchElementException>(() => driver
            .FindElement(By.CssSelector("li[data-name='username']"))
            .GetAttribute("data-value"));
        }
    }
}
