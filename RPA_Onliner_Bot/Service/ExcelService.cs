using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using RPA_Onliner_Bot.DataFile;
using RPA_Onliner_Bot.Service.Abstract;
using _Excel = Microsoft.Office.Interop.Excel;

namespace RPA_Onliner_Bot.Service
{
    public class ExcelService : IExcelService
    {
        private string path;
        private _Application excel = new _Excel.Application();
        private Workbooks workbooks;
        private Workbook workbook;
        private Worksheet worksheets;

        public ExcelService(string path)
        {
            this.path = path;
        }

        public bool WriteData(List<MicrowaveData> list)
        {
            this.excel.DisplayAlerts = false;
            bool flag;

            if (File.Exists(this.path))
            {
                this.workbooks = this.excel.Workbooks;
                this.workbook = this.workbooks.Open(this.path);
                flag = false;
            }
            else
            {
                this.workbooks = this.excel.Workbooks;
                this.workbook = this.workbooks.Add(1);
                flag = true;
            }

            this.worksheets = this.workbook.Worksheets[1];
            this.worksheets.Name = "Microwave";

            for (int i = 0; i < list.Count; i++)
            {
                this.worksheets.Columns[1].ColumnWidth = 25;
                this.worksheets.Cells[i + 1, 1] = list[i].Name;

                this.worksheets.Columns[2].ColumnWidth = 15;
                this.worksheets.Cells[i + 1, 2] = list[i].Price;

                this.worksheets.Columns[3].ColumnWidth = 70;
                this.worksheets.Cells[i + 1, 3] = list[i].Link;
            }

            if (flag)
            {
                this.excel.Application.ActiveWorkbook.SaveAs(this.path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, _Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            else
            {
                this.workbook.Save();
            }

            this.workbook.Close();
            this.excel.Quit();

            GC.Collect();
            return true;
        }
    }
}
