namespace AquaCultureMonitor.Core.Gateways
{
    public interface ITwilioGateway
    {
        string SendSms(string phoneNumber, string message);
    }
}