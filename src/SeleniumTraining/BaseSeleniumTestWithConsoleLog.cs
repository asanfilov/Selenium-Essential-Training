using System;
using Xunit.Abstractions;

namespace SeleniumTraining
{
    public abstract class BaseSeleniumTestWithConsoleLog : BaseSeleniumTest

    {
        protected readonly ITestOutputHelper output;

        public BaseSeleniumTestWithConsoleLog(ITestOutputHelper output)

        {
            this.output = output;
        }

        protected void Log(string message)
        {
            output.WriteLine($"{DateTime.Now.ToString("u")}|{message}");
        }
    }
}
