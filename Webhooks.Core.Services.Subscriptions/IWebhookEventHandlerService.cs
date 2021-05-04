using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webhooks.Core.Services.Contracts.Requests;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts
{
    public interface IWebhookEventHandlerService
    {
        void Post(WebhookEventHandler request);

        Task Post(WebhookEventHandlerAsync request);
    }
}
