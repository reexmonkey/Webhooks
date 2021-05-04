using Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests;
using Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts
{
    public interface IWebhookProviderService
    {
        WebhookProviderResponse Post(CreateWebhookProvider request);

        WebhookProviderResponse Post(UpdateWebhookProvider request);

        WebhookProviderResponse Delete(DeleteWebhookProvider request);

        List<WebhookProviderResponse> Delete(BatchDeleteWebhookProviders request);

        WebhookProviderResponse Post(RestoreWebhookProvider request);

        List<WebhookProviderResponse> Post(BatchRestoreWebhookProviders request);

        WebhookProviderResponse Get(GetWebhookProvider request);

        List<WebhookProviderResponse> Get(QueryWebhookProviders request);

        WebhookEventResponse Post(PublishWebhookEvent request);

        List<WebhookDefinitionResponse> Post(BatchPublishWebhooks request);

        Task<WebhookProviderResponse> Post(CreateWebhookProviderAsync request);

        Task<WebhookProviderResponse> Put(UpdateWebhookProviderAsync request);

        Task<WebhookProviderResponse> Delete(DeleteWebhookProviderAsync request);

        Task<List<WebhookProviderResponse>> Delete(BatchDeleteWebhookProvidersAsync request);

        Task<WebhookProviderResponse> Post(RestoreWebhookProviderAsync request);

        Task<List<WebhookProviderResponse>> Post(BatchRestoreWebhookProvidersAsync request);

        Task<WebhookProviderResponse> Get(GetWebhookProviderAsync request);

        Task<List<WebhookProviderResponse>> Get(QueryWebhookProvidersAsync request);

        Task<WebhookEventResponse> Post(RaiseWebhookEventAsync request);

        Task<List<WebhookDefinitionResponse>> Post(BatchPublishWebhooksAsync request);
    }
}