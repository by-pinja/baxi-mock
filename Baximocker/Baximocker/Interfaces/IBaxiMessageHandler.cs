using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Baximocker.Interfaces
{
    public interface IBaxiMessageHandler
    {
        void SetPaymentAsSuccessful(long terminalID);

    }
}
