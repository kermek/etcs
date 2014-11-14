using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NUnit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using CSharpNUnit.PageObjects;

namespace CSharpNUnit.LocalTests
{
    class AddProblem
    {
        private IWebDriver driver;
        Stopwatch sw;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            sw = Stopwatch.StartNew(); 
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
        }

        [SetUp]
        public void TestSetUp()
        {
            driver.Navigate().GoToUrl("http://localhost:8090/#/map");
        }

        [Test]
        public void AddProblemTest()
        {
            double latitude               = 50.1;
            double longitude              = 30.1;
            String problemName            = "problemNameTest";
            String problemType            = "Загрози біорізноманіттю";
            String problemDescription     = "problemDescriptionTest";
            String problemSolution        = "problemProposeTest";
            IList<String> imageURLs       = new List<String>();
            IList<String> imageComments   = new List<String>();
            String adminEmail             = "admin@.com";
            String adminPassword          = "admin";
            //imageURLs.Add("http://i.imgur.com/HHXCVbs.jpg")
            //imageURLs.Add("http://i.imgur.com/1K6AdCH.jpg");
            // Arrays.asList("comment1", "comment2");
            
            AnyPage page = new AnyPage(driver);
            page.logIn(adminEmail, adminPassword);
            page.AddProblem(latitude, longitude, problemName, problemType, problemDescription, problemSolution,
                imageURLs, imageComments);
            page.clickAtProblemByCoordinateVisible(latitude, longitude);

            Assert.AreEqual(page.GetPageTitle(), problemName);
            page.DeleteCurrentProblem();
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            if (driver != null) driver.Close();
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                               ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
