using Microsoft.Extensions.Options;
using Mozo.Model;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Mozo.Comun.Implement;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
}
public class EmailSender : IEmailSender
{
    private SmtpClient Cliente { get; }
    private EmailSenderModel Options { get; }

    public EmailSender(IOptions<EmailSenderModel> options)
    {
        Options = options.Value;
        Cliente = new SmtpClient()
        {
            Host = Options.Host,
            Port = Options.Port,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(Options.Email, Options.Password),
            EnableSsl = Options.EnableSsl,
        };
    }

    public Task SendEmailAsync(string email, string subject, string message)
    {
        var correo = new MailMessage(from: Options.Email, to: email, subject: subject, body: message);
        correo.IsBodyHtml = true;
        return Cliente.SendMailAsync(correo);
    }
}