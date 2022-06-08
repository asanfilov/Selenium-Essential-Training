using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;//WebDriverWait

namespace SeleniumTraining.SynchronizationStrategies
{
    public class GenericJSWait
    {
        private readonly WebDriverWait wait;

        /// <summary>
        /// Provides custom condition that can be waited for using <see cref="WebDriverWait"/>.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="timeSpan"></param>
        public GenericJSWait(IWebDriver driver, TimeSpan timeSpan)
        {
            wait = new WebDriverWait(driver, timeSpan);
        }

        /// <summary>
        /// Provides the ability to wait for JavaScript code execution using <see cref="IJavaScriptExecutor.ExecuteScript"/>.
        /// <para>
        /// Throws when input <paramref name="jsCode"/> is invalid JavaScript.
        /// </para>
        /// </summary>
        /// <param name="jsCode"></param>
        /// <param name="value"></param>
        /// <example>
        /// <code>
        /// GenericJSWait jsWait = new GenericJSWait(driver, TimeSpan.FromSeconds(3))
        /// jsWait.ForJavaScriptToEvaluateTo(validJS, true);
        /// </code>
        /// </example>
        ///<exception cref="OpenQA.Selenium.JavaScriptException">
        ///</exception>
        public void ForJavaScriptToEvaluateTo(string jsCode, bool value)
        {
            wait.Until(ConditionIsEqualTo(jsCode, value));
        }

        private Func<IWebDriver, bool> ConditionIsEqualTo(string js, bool expected)
        {
            return (driver) =>
            {
                try
                {
                    IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                    bool result = (bool)jse.ExecuteScript("return " + js);
                    return result == expected;
                }
                catch (JavaScriptException ex) { throw; }
            };
        }
    }
}
