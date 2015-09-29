using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SendGrid;

namespace AquaCultureMonitor.Core.Services.Accounts
{
	public class EmailService : IIdentityMessageService
	{
		public Task SendAsync(IdentityMessage message)
		{
			return configSendGridasync(message);
		}

		private async Task configSendGridasync(IdentityMessage message)
		{
			var myMessage = new SendGridMessage();
			myMessage.AddTo(message.Destination);
			myMessage.AddBcc("ross.neufeld@outlook.com");
			myMessage.From = new MailAddress("noreply@calgaryschoolbuses.com", "Account | Calgary School Buses");
			myMessage.Subject = message.Subject;
			myMessage.Text = message.Body;
			myMessage.Html = message.Body;

			var credentials = new NetworkCredential(ConfigurationManager.AppSettings["SendGridUsername"], ConfigurationManager.AppSettings["SendGridPassword"]);

			// Create a Web transport for sending email.
			var transportWeb = new Web(credentials);

			// Send the email.
			try
			{
				await transportWeb.DeliverAsync(myMessage);
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
			}
		}
	}
}
