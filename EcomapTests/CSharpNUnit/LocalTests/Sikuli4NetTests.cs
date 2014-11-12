using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sikuli4Net.sikuli_REST;
using Sikuli4Net.sikuli_UTIL;
using System.Drawing;

namespace CSharpNUnit.LocalTests
{
    public class Sikuli4NetTests
    {
        public APILauncher launcher        = new APILauncher(true);
        public Pattern pattern_ChromeIcon  = new Pattern("C:\\Users\\Yermek\\etcs\\EcomapTests\\resources\\patterns\\pattern_ChromeIcon.png");
        public Pattern pattern_CalcIcon    = new Pattern(System.IO.Directory.GetCurrentDirectory() + "\\Resources\\pattern_CalcIcon.png");
        public Pattern pattern_CalcIconWeb = new Pattern("http://s25.postimg.org/f9aimk1t7/pattern_Calc_Icon.png");

        [TestFixtureSetUp]
        public void TestsSetup() { 
            launcher = new APILauncher(true);

            launcher.Start();
        }

        [Test]
        public void Test() {
            Screen scrn = new Screen();
            scrn.Find(pattern_CalcIcon);
            scrn.Click(pattern_CalcIcon);
            Assert.IsTrue(true);
            
        }

        [TestFixtureTearDown]
        public void TestsTearDown() {
            launcher.Stop();
        }
    }
}
