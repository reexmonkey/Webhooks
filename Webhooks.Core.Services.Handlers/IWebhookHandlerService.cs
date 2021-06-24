using Reexmonkey.Webhooks.Core.Services.Handlers.Requests;
using System.Threading.Tasks;

namespace Webhooks.Core.Services.Contracts
{
    public interface IWebhookHandlerService
    {
        void Post(Callback request);

        Task Post(CallbackAsync request);
    }
}
