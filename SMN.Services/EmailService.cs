using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SMN.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string subject, string body)
        {
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage { Subject = subject, Body = body, IsBodyHtml = true };
            message.To.Add(to);
            client.Send(message);
            client.Dispose();
        }
    }
}
