using System.Collections.Generic;
using System.Threading.Tasks;
using Webhooks.Core.Services.Contracts.Requests;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts
{
    public interface IWebhookSubscriptionService
    {
        WebhookSubscriptionResponse Post(CreateWebhookSubscriptions request);

        WebhookSubscriptionResponse Post(UpdateWebhookSubscription request);

        WebhookSubscriptionResponse Post(DeleteWebhookSubscription request);

        List<WebhookSubscriptionResponse> Post(BatchDeleteWebhookSubscriptions request);

        WebhookSubscriptionResponse Post(RestoreWebhookSubscription request);

        List<WebhookSubscriptionResponse> Post(BatchRestoreWebhookSubscriptions request);

        WebhookSubscriptionResponse Get(GetWebhookSubscription request);

        List<WebhookSubscriptionResponse> Post(QueryWebhookSubscriptions request);

        Task<WebhookSubscriptionResponse> Post(CreateWebhookSubscriptionsAsync request);

        Task<WebhookSubscriptionResponse> Post(UpdateWebhookSubscriptionAsync request);

        Task<WebhookSubscriptionResponse> Post(DeleteWebhookSubscriptionAsync request);

        Task<List<WebhookSubscriptionResponse>> Post(BatchDeleteWebhookSubscriptionsAsync request);

        Task<WebhookSubscriptionResponse> Post(RestoreWebhookSubscriptionAsync request);

        Task<List<WebhookSubscriptionResponse>> Post(BatchRestoreWebhookSubscriptionsAsync request);

        Task<WebhookSubscriptionResponse> Get(GetWebhookSubscriptionAsync request);

        Task<List<WebhookSubscriptionResponse>> Post(QueryWebhookSubscriptionsAsync request);
    }
}