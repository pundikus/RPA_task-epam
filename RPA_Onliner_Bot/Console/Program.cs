using System.Collections.Generic;
using System.IO;
using RPA_Onliner_Bot.DataFile;
using RPA_Onliner_Bot.Service;

namespace RPA_Onliner_Bot.Console
{
    internal class Program
    {
        private const string Path = @"D:\Test.xlsx";

        private static void Main()
        {
            var seleniumService = new SeleniumService(1000);
            var excelService = new ExcelService(Path);

            var microwaveList = seleniumService.GetData();

            bool result = excelService.WriteData(microwaveList);
        }
    }
}
