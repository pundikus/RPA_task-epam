using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop.Excel;
using RPA_Onliner_Bot.DataFile;
using RPA_Onliner_Bot.Service.Abstract;
using Excel = Microsoft.Office.Interop.Excel;

namespace RPA_Onliner_Bot.Service
{
    public class ExcelService : IExcelService
    {
        private readonly string path;
        private readonly Application excel = new Excel.Application();

        public ExcelService(string path)
        {
            this.path = path;
        }

        public bool WriteData(List<MicrowaveData> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            try
            {
                this.excel.DisplayAlerts = false;

                var workbook = File.Exists(this.path) ? this.excel.Workbooks.Open(this.path) : this.excel.Workbooks.Add(1);

                var worksheets = workbook.Worksheets[1];
                worksheets.Name = "Microwave";

                for (int i = 0; i < list.Count; i++)
                {
                    worksheets.Columns[1].ColumnWidth = 25;
                    worksheets.Cells[i + 1, 1] = list[i].Name;

                    worksheets.Columns[2].ColumnWidth = 15;
                    worksheets.Cells[i + 1, 2] = list[i].Price;

                    worksheets.Columns[3].ColumnWidth = 70;
                    worksheets.Cells[i + 1, 3] = list[i].Link;
                }

                this.excel.Application.ActiveWorkbook.SaveAs(this.path);
                workbook.Close();
                this.excel.Quit();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
