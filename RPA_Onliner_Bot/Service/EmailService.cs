using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using RPA_Onliner_Bot.Service.Abstract;

namespace RPA_Onliner_Bot.Service
{
    public class EmailService : IEmailService
    {
        public bool SendMessage(bool result, string path, string address)
        {
            if (path == null)
            {
                throw new ArgumentNullException();
            }

            if (address == null)
            {
                throw new ArgumentNullException();
            }

            if (!result)
            {
                return false;
            }

            MailAddress from = new MailAddress("nikita.pundis@mail.ru", "Nikita");

            MailAddress to = new MailAddress(address);

            MailMessage message = new MailMessage(from, to);

            message.Attachments.Add(new Attachment(path));

            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);

            smtp.Credentials = new NetworkCredential("nikita.pundis@mail.ru", "ybrbnrf2703");
            smtp.EnableSsl = true;
            smtp.Send(message);

            return true;
        }
    }
}
