using System.Net.Mail;
using System.Net;

namespace ShradhaGeneralBookStores.Helpers;

public class MailHelper
{
    private IConfiguration configuration;

    public MailHelper(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public bool Send(string from, string to, string subject, string body)
    {
        try
        {
            var host = configuration["Gmail:Host"];
            var port = int.Parse(configuration["Gmail:Port"]);
            var username = configuration["Gmail:Username"];
            var password = configuration["Gmail:Password"];
            var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);
            var smtpClient = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = enable,
                Credentials = new NetworkCredential(username, password)
            };

            var mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml= true;
            smtpClient.Send(mailMessage);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
