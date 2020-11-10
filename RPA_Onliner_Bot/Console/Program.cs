using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using Mono.Options;
using RPA_Onliner_Bot.Service;

namespace RPA_Onliner_Bot.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = null;
            string address = null;
            var p = new OptionSet()
            {
                { "ep|excelPath=", "Path to the file to save data to.", v => path = v },
                { "ea|address=", "Email to send the result file to.", v => address = v },
            };

            try
            {
                p.Parse(args);
                if (path == null)
                {
                    System.Console.WriteLine("There is no path argument");
                    return;
                }

                if (address == null)
                {
                    System.Console.WriteLine("There is no address argument");
                    return;
                }

                var seleniumService = new SeleniumService(int.Parse(ConfigurationManager.AppSettings["UserDelay"]));
                var microwaveList = seleniumService.GetData();
                if (microwaveList == null ||
                    microwaveList.Count.Equals(0))
                {
                    System.Console.WriteLine("Unable to get data");
                    return;
                }

                var excelService = new ExcelService(path);
                var excelSaved = excelService.WriteData(microwaveList);
                if (!excelSaved)
                {
                    System.Console.WriteLine("Unable to write data to the file");
                    return;
                }

                var emailService = new EmailService(new SmtpClient(
                    ConfigurationManager.AppSettings["SmtpHost"],
                    int.Parse(ConfigurationManager.AppSettings["SmtpPort"]))
                {
                    Credentials = new NetworkCredential
                    {
                        UserName = ConfigurationManager.AppSettings["MailLogin"],
                        Password = ConfigurationManager.AppSettings["MailPassword"],
                    },
                });
                var emailSent = emailService.SendMessage(path, address);
                if (!emailSent)
                {
                    System.Console.WriteLine("Unable to send file");
                    return;
                }

                System.Console.WriteLine("Bot completed all tasks");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}