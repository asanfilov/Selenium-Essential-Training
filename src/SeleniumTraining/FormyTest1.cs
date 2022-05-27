using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace SeleniumTraining
{
    public class FormyTest1
    {
        [Fact]
        [Trait("Exercise", "https://www.linkedin.com/learning/selenium-essential-training/switch-to-alert")]
        public void SwitchToAlert()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://formy-project.herokuapp.com/switch-window");
            IWebElement openAlert = driver.FindElement(By.Id("alert-button"));
            System.Threading.Thread.Sleep(1_000);
            openAlert.Click();

            System.Threading.Thread.Sleep(2_000);//add a pause to see what the test does
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            System.Threading.Thread.Sleep(2_000);
            driver.Quit();
        }
    }
}
