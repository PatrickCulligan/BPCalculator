using Microsoft.VisualStudio.TestTools.UnitTesting;

// NuGet install Selenium WebDriver package and Support Classes

using OpenQA.Selenium;

// NuGet install Chrome Driver
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

// run 2 instances of VS to do run Selenium tests against localhost
// instance 1 : run web app e.g. on IIS Express
// instance 2 : from Test Explorer run Selenium test
// or use the dotnet vstest task
// e.g. dotnet vstest seleniumtest\bin\debug\netcoreapp2.1\seleniumtest.dll /Settings:seleniumtest.runsettings

namespace NUit
{
    [TestClass]
    public class SeleniumTest
    {
        // .runsettings file contains test run parameters
        // e.g. URI for app
        // test context for this run

        private TestContext testContextInstance;

        // test harness uses this property to initliase test context
        public TestContext TestContext
        {
            get { return this.testContextInstance; }
            set { this.testContextInstance = value; }
        }

        // URI for web app being tested
        private String webAppUri;
        // .runsettings property overriden in vsts test runner 
        // release task to point to run settings file
        // also webAppUri overriden to use pipeline variable

        [TestInitialize]                // run before each unit test
        public void Setup()
        {
            // read URL from SeleniumTest.runsettings (configure run settings)
            this.webAppUri = testContextInstance.Properties["webAppUri"].ToString();

            //this.webAppUri = "https://bpcalulator-green-slot-dscydwgqh3eyfxf4.ukwest-01.azurewebsites.net/";
        }

        [TestMethod]
        public void TestBloodPressureCalculatorUI()
        {
            // Configure Chrome to run headless for CI/CD environments

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--remote-debugging-port=9222");

            using (IWebDriver driver = new ChromeDriver(options))
            {
                try
                {
                    // Navigate to URI for blood pressure calculator
                    driver.Navigate().GoToUrl(webAppUri);

                    // Locate the input fields for Systolic and Diastolic values
                    IWebElement systolicInput = driver.FindElement(By.Name("BP.Systolic"));
                    IWebElement diastolicInput = driver.FindElement(By.Name("BP.Diastolic"));

                    // Input values into Systolic and Diastolic fields
                    systolicInput.Clear();
                    systolicInput.SendKeys("120");
                    diastolicInput.Clear();
                    diastolicInput.SendKeys("80");

                    // Submit the form
                    driver.FindElement(By.CssSelector("input[type='submit']")).Click();

                    // Explicitly wait for result with "Category" item
                    IWebElement categoryElement = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                        .Until(c => c.FindElement(By.XPath("//p[contains(text(), 'Category:')]")));

                    // Get the result and verify
                    string category = categoryElement.Text;
                    StringAssert.Contains(category, "Ideal");
                }
                catch (WebDriverException e)
                {
                    Assert.Fail("WebDriverException occurred: " + e.Message);
                }
                finally
                {
                    driver.Quit();
                }
            }
        }
    }
}