using System;
using System.Net;
using System.Net.Mail;
using RPA_Onliner_Bot.Service.Abstract;

namespace RPA_Onliner_Bot.Service
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient smtpClient;

        public EmailService(SmtpClient smtpClient)
        {
            this.smtpClient = smtpClient;
        }

        public bool SendMessage(string path, string address)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            MailAddress from = new MailAddress(((NetworkCredential)this.smtpClient.Credentials).UserName);

            MailAddress to = new MailAddress(address);

            MailMessage message = new MailMessage(from, to);

            message.Attachments.Add(new Attachment(path));

            this.smtpClient.EnableSsl = true;
            this.smtpClient.Send(message);

            return true;
        }
    }
}
