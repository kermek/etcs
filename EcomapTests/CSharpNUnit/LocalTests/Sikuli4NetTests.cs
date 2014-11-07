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
        public APILauncher launcher           = new APILauncher(true);
        public Pattern pattern_ChromeIcon = new Pattern("http://s25.postimg.org/m87zkwrhn/pattern_Chrome_Icon.png");

        [TestFixtureSetUp]
        public void TestsSetup() { 
            launcher = new APILauncher(true);

            launcher.Start();
        }

        [Test]
        public void Test() {
            Screen scrn = new Screen();
            scrn.Find(pattern_ChromeIcon);
        }

        [TestFixtureTearDown]
        public void TestsTearDown() {
            launcher.Stop();
        }
    }
}
