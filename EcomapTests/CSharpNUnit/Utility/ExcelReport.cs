using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNUnit.Utility
{
    class ExcelReport
    {
        Microsoft.Office.Interop.Excel.Application excelApp;

        public void AddTestTime(String TimeString)
        { 
            excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Workbooks.Open(System.IO.Directory.GetCurrentDirectory()
                + "\\Resources\\TestResults.xlsx", 0, false, 5, "", "", false, 
                Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                true, false, 0, true, false, false);
            int rowIndex = GetLastRow(); //int colIndex = 2;
            excelApp.Cells[rowIndex, 1] = rowIndex - 1;
            excelApp.Cells[rowIndex, 2] = TimeString;
            // show the Excel sheet in which information is written
            excelApp.Visible = true;
            excelApp.ActiveWorkbook.Save();
            excelApp.ActiveWorkbook.Close();
            Marshal.ReleaseComObject(excelApp);
        }
        private int GetLastRow() {
            int row = 1;
            while (excelApp.Cells[row, 1].Value2 != null) {
                row++;
            }
            return row;
        }
    }
}