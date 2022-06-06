using System;
using OpenQA.Selenium;

namespace SeleniumTraining.SynchronizationStrategies
{
    public class CustomExpectedConditions
    {
        public static Func<IWebDriver, bool> ElementFinishedExpanding(By locator)
        {
            int lastHeight = 0;
            return (driver) =>
            {
                int newHeight = driver.FindElement(locator).Size.Height;

                if (newHeight > lastHeight)
                {
                    lastHeight = newHeight;
                    return false;
                }
                else
                {
                    return true;
                }
            };
        }
    }
}
