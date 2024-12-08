using System.Net;
using System.Net.Mail;
using Civix.App.Core.Dtos.Otp;

namespace Civix.App.Core.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            // smtp
            var client = new SmtpClient("smtp.gmail.com", 587); // Mail Server 
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("adelhana895@gmail.com", "dkuiknodbhkactkb");

            client.Send("adelhana895@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
