using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace CSharpNUnit.Utility
{
    public abstract class ExcelFileWriter<T>
    {
        private Microsoft.Office.Interop.Excel.Application _excelApplication = null;
        private Microsoft.Office.Interop.Excel.Workbooks _workBooks = null;
        private Microsoft.Office.Interop.Excel._Workbook _workBook = null;
        private object _value = Missing.Value;
        private Microsoft.Office.Interop.Excel.Sheets _excelSheets = null;
        private Microsoft.Office.Interop.Excel._Worksheet _excelSheet = null;
        private Microsoft.Office.Interop.Excel.Range _excelRange = null;
        private Microsoft.Office.Interop.Excel.Font _excelFont = null;
        protected int colNumber, rowNumber;
        /// <summary>
        /// User have to set the names of header in the derived class
        /// </summary>
        public abstract object[] Headers { get; set; }
        /// <summary>
        /// user have to parse the data from the list and pass each data along with the
        /// column and row name to the base fun, FillExcelWithData().
        /// </summary>
        /// <param name="list"></param>
        public abstract void FillRowData(List<T> list);

        /// <summary>
        /// get the data of object which will be saved to the excel sheet
        /// </summary>
        public abstract object[,] ExcelData { get; }
        /// <summary>
        /// get the no of columns
        /// </summary>
        public int ColumnCount { get { return colNumber; } }
        /// <summary>
        /// get the now of rows to fill
        /// </summary>
        public int RowCount { get { return rowNumber; } }

        /// <summary>
        /// user can override this to make the headers not be in bold.
        /// by default it is true
        /// </summary>
        protected virtual bool BoldHeaders
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// api through which data from the list can be write to an excel
        /// kind of a Template Method Pattern is used here
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="holdingsList"></param>
        public void WriteDataToExcel(string fileName, List<T> list, string startColumnOfHeader, string endColumnOfHeader, string startColumnOfData)
        {
            this.ActivateExcel();
            this.FillRowData(list);
            this.FillExcelWithData(startColumnOfData);
            this.FillHeaderColumn(Headers, startColumnOfHeader, endColumnOfHeader);
            ///this.Open();
            //_excelApplication.Workbooks.Open(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            this.SaveExcel(fileName);
            //Start(fileName);

        }


        /// <summary>
        /// activate the excel application
        /// </summary>
        /// 
        protected virtual void ActivateExcel()
        {
            _excelApplication = new Microsoft.Office.Interop.Excel.Application();
            _workBooks = (Microsoft.Office.Interop.Excel.Workbooks)_excelApplication.Workbooks;
            _workBook = (Microsoft.Office.Interop.Excel._Workbook)(_workBooks.Add(_value));
            _excelSheets = (Microsoft.Office.Interop.Excel.Sheets)_workBook.Worksheets;
            _excelSheet = (Microsoft.Office.Interop.Excel._Worksheet)(_excelSheets.get_Item(1));
        }

        /// <summary>
        /// fill the header columns for the range specified and make it bold if specified
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="startColumn"></param>
        /// <param name="endColumn"></param>
        protected void FillHeaderColumn(object[] headers, string startColumn, string endColumn)
        {
            _excelRange = _excelSheet.get_Range(startColumn, endColumn);
            _excelRange.set_Value(_value, headers);
            if (BoldHeaders == true)
            {

                this.BoldRow(startColumn, endColumn);
            }
            _excelRange.EntireColumn.AutoFit();
        }
        /// <summary>
        /// Fill the excel sheet with data along with the position specified
        /// </summary>
        /// <param name="columnrow"></param>
        /// <param name="data"></param>
        private void FillExcelWithData(string startColumn)
        {
            _excelRange = _excelSheet.get_Range(startColumn, _value);
            _excelRange = _excelRange.get_Resize(RowCount + 1, ColumnCount);
            _excelRange.set_Value(Missing.Value, ExcelData);
            _excelRange.EntireColumn.AutoFit();
        }
        /// <summary>
        /// save the excel sheet to the location with file name
        /// </summary>
        /// <param name="fileName"></param>
        protected virtual void SaveExcel(string fileName)
        {
            _workBook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, _value,
                _value, _value, _value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                _value, _value, _value, _value, null);
            _workBook.Close(false, _value, _value);
            _excelApplication.Quit();
        }
        /// <summary>
        /// make the range of rows bold
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        private void BoldRow(string row1, string row2)
        {
            _excelRange = _excelSheet.get_Range(row1, row2);

            _excelFont = _excelRange.Font;
            _excelFont.Bold = true;
        }

        protected virtual void Merge(int col1, int row1, int col2, int row2)
        {
            _excelRange = _excelSheet.get_Range(_excelSheet.Cells[col1, row1], _excelSheet.Cells[col1, row2]);
            _excelRange.Merge(true);
        }
    }
}
