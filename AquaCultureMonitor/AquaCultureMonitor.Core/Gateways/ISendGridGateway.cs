using System.Threading.Tasks;
using SendGrid;

namespace AquaCultureMonitor.Core.Gateways
{
    public interface ISendGridGateway
    {
        void Send(SendGridMessage message);
    }
}