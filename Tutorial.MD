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

