using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

namespace CSharpNUnit
{
    class SeleniumTestExample2
    {
        private IWebDriver driver;

        //[TestFixtureSetUp]
        public void FixtureSetup()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
        }

        //[SetUp]
        public void TestSetUp()
        {
        driver.Navigate().GoToUrl("https://www.google.com");
        }

        //[Test]
        public void SearchTest() {
            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("Cheese");
            query.Submit();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return d.Title.ToLower().StartsWith("cheese"); });
            System.Console.WriteLine("Page title is: " + driver.Title);

            Assert.AreEqual("Cheese", driver.Title.Split(' ').GetValue(0));
        }

        //[TestFixtureTearDown]
        public void FixtureTearDown()
        {
        if (driver != null) driver.Close();
        }
    }
}
