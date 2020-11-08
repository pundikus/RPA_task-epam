using RPA_Onliner_Bot.Service;

namespace RPA_Onliner_Bot.Console
{
    internal class Program
    {
        private static void Main()
        {
            var seleniumService = new SeleniumService(1000);

            var microwaveList = seleniumService.GetData();
        }
    }
}
