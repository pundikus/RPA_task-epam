using RPA_Onliner_Bot.Service;

namespace RPA_Onliner_Bot.Console
{
    internal class Program
    {
        private const string Path = @"D:\Test.xlsx";
        private const string Address = "nik.pundis@mail.ru";

        private static void Main()
        {
            var seleniumService = new SeleniumService(1000);
            var excelService = new ExcelService(Path);
            var emailService = new EmailService();

            var microwaveList = seleniumService.GetData();

            bool result = excelService.WriteData(microwaveList);

            if (emailService.SendMessage(result, Path, Address))
            {
                System.Console.WriteLine("Message sent!");
            }
        }
    }
}
