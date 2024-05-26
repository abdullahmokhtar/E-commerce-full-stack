using System.Net.Mail;
using System.Net;

namespace Backend.API.Helper
{
    public static class EmailSettigns
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);

            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("abdullahmokhtr55@gmail.com", "rhqsgtbocklxpllx");

            client.Send("abdullahmokhtr55@gmail.com", email.To, email.Title, email.Body);
        }
    }
}
