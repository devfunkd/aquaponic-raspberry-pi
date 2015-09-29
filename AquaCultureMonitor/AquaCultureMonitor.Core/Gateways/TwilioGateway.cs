using System;
using System.Web.Configuration;
using Twilio;

namespace AquaCultureMonitor.Core.Gateways
{
    public class TwilioGateway : ITwilioGateway
    {
        private readonly string _accountSid = WebConfigurationManager.AppSettings["AccountSid"];
        private readonly string _authToken = WebConfigurationManager.AppSettings["AuthToken"];
        private readonly string _twilioPhoneNumber = WebConfigurationManager.AppSettings["TwilioPhoneNumber"];
        private readonly bool _twilioDebug = Convert.ToBoolean(WebConfigurationManager.AppSettings["TwilioDebug"]);

        private readonly TwilioRestClient _twilioRestClient;

        public TwilioGateway()
        {
            _twilioRestClient = new TwilioRestClient(_accountSid, _authToken);
        }

        public string SendSms(string phoneNumber, string message)
        {
            if (_twilioDebug) return null;

            var exConfirmation = _twilioRestClient.SendMessage(_twilioPhoneNumber, phoneNumber, message);
            return exConfirmation.Sid;
        }
    }
}
