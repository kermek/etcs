using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sikuli4Net.sikuli_REST;
using Sikuli4Net.sikuli_UTIL;
using System.Drawing;
using System.Threading;

namespace CSharpNUnit.LocalTests
{
    public class Sikuli4NetThread
    {
        private APILauncher launcher          = new APILauncher(true);
        private Pattern pattern_OpenButton;
        private Pattern pattern_FileNameField;
        //private Pattern pattern_OpenButton = new Pattern(System.IO.Directory.GetCurrentDirectory() + "\\Resources\\pattern_OpenButtonWindows7EN.png");
        //private Pattern pattern_FileNameField = new Pattern(System.IO.Directory.GetCurrentDirectory() + "\\Resources\\pattern_FileNameField_Windows7.png");
        
        public void ChooseFile(object FileName) {
            launcher = new APILauncher(true);

            launcher.Start();
            //System.Diagnostics.Debug.Write(System.IO.Directory.GetCurrentDirectory() + "\\Resources\\pattern_CalcIcon.png");
            if (System.Globalization.CultureInfo.InstalledUICulture.ThreeLetterISOLanguageName.Equals("rus")) {
                pattern_OpenButton = new Pattern(System.IO.Directory.GetCurrentDirectory() + "\\Resources\\pattern_OpenButtonWindows8RU.png");
                pattern_FileNameField = new Pattern(System.IO.Directory.GetCurrentDirectory() + "\\Resources\\pattern_FileNameField_Windows8.png");
            } else {
                pattern_OpenButton = new Pattern(System.IO.Directory.GetCurrentDirectory() + "\\Resources\\pattern_OpenButtonWindows7EN.png");
                pattern_FileNameField = new Pattern(System.IO.Directory.GetCurrentDirectory() + "\\Resources\\pattern_FileNameField_Windows7.png");
            }
            Screen scrn = new Screen();
            String file = (string)FileName;

            Thread thread = new Thread(() => System.Windows.Clipboard.SetText(file));
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start();
            thread.Join();
            //System.Windows.Clipboard.SetText((string)FileName);
            Thread.Sleep(1000);
            
            scrn.Click(pattern_FileNameField);
            scrn.Type(pattern_FileNameField, "v", KeyModifier.CTRL);
            Thread.Sleep(2000);
            scrn.Click(pattern_OpenButton);
            launcher.Stop();
        }
    }
}
