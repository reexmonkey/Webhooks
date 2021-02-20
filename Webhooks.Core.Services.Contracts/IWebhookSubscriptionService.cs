using System.Collections.Generic;
using System.Threading.Tasks;
using Webhooks.Core.Services.Contracts.Requests;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts
{
    public interface IWebhookSubscriptionService
    {
        WebhookSubscriptionResponse Post(CreateWebhookSubscription request);

        WebhookSubscriptionResponse Post(UpdateWebhookSubscription request);

        WebhookSubscriptionResponse Post(DeleteWebhookSubscription request);

        List<WebhookSubscriptionResponse> Post(BatchDeleteWebhookSubscription request);

        WebhookSubscriptionResponse Post(RestoreWebhookSubscription request);

        WebhookSubscriptionResponse Get(GetWebhookSubscription request);

        List<WebhookSubscriptionResponse> Post(SearchWebhookSubscription request);

        Task<WebhookSubscriptionResponse> Post(CreateWebhookSubscriptionAsync request);

        Task<WebhookSubscriptionResponse> Post(UpdateWebhookSubscriptionAsync request);

        Task<WebhookSubscriptionResponse> Post(DeleteWebhookSubscriptionAsync request);

        Task<List<WebhookSubscriptionResponse>> Post(BatchDeleteWebhookSubscriptionAsync request);

        Task<WebhookSubscriptionResponse> Post(RestoreWebhookSubscriptionAsync request);

        Task<WebhookSubscriptionResponse> Get(GetWebhookSubscriptionAsync request);

        Task<List<WebhookSubscriptionResponse>> Post(SearchWebhookSubscriptionAsync request);
    }
}