using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SendGrid;

namespace AquaCultureMonitor.Core.Gateways
{
    public class SendGridGateway : ISendGridGateway
    {
        private readonly string _sendGridUsername = ConfigurationManager.AppSettings["SendGridUsername"];
        private readonly string _sendGridPassword = ConfigurationManager.AppSettings["SendGridPassword"];

        public void Send(SendGridMessage message)
        {
            // Create network credentials to access your SendGrid account.
            var credentials = new NetworkCredential(_sendGridUsername, _sendGridPassword);

            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Disable Unsubsribe
            message.DisableUnsubscribe();

            var email = ConfigurationManager.AppSettings["TestNotifications"];

            var testMessage = new SendGridMessage
            {
                Subject = message.Subject,
                Html = message.Html,
                Attachments = message.Attachments,
                StreamedAttachments = message.StreamedAttachments
            };

            testMessage.AddTo(email);
            testMessage.From = new MailAddress("no-reply@no-domain.com", "AquaCulture Monitor");

            transportWeb.Deliver(testMessage);

        }
    }
}
