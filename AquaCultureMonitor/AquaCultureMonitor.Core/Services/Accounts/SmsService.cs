using System.Threading.Tasks;
using AquaCultureMonitor.Core.Gateways;
using Microsoft.AspNet.Identity;

namespace AquaCultureMonitor.Core.Services.Accounts
{
	public class SmsService : IIdentityMessageService
	{
		public Task SendAsync(IdentityMessage message)
		{
			var twilio = new TwilioGateway();
			twilio.SendSms(message.Destination, message.Body);

			return Task.FromResult(0);
		}
	}
}
