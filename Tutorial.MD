## Prerequisites
TODO add later

### Ensure you have the latest version of Chrome browser
On your computer, open Chrome. At the top right, click three vertical dots.
Click Help - About Google Chrome.
Click Update Google Chrome (if you can't find this button, you're on the latest version) and click Relaunch.
Note the version, you will need it later to install ChromeDriver.

## Create a test project
Open a new PowerShell prompt and use .NET command-line interface ([dotnet CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/)) commands:

    dotnet new xunit -f netcoreapp3.1 -n SeleniumTraining
    cd .\SeleniumTraining\
    dotnet add package Selenium.WebDriver
    dotnet add package Selenium.WebDriver.ChromeDriver

## Add your first Selenium-powered test
Now you are ready to [write your first Selenium script](https://www.selenium.dev/documentation/webdriver/getting_started/first_script/). Launch Visual Studio and open SeleniumTraining.csproj  
Rename UnitTest1.cs to SeleniumTest1.cs and replace its contents with this code:  
```cs
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
```
Save the file. In the PowerShell prompt, type this command:  

    dotnet test

Expected result: Chrome browser shall start, search Google for "Selenium", and close.
