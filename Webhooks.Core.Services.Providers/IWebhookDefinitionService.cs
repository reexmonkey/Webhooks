using Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests;
using Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts
{
    public interface IWebhookDefinitionService
    {
        WebhookDefinitionResponse Post(CreateWebhookDefinition request);

        WebhookDefinitionResponse Put(UpdateWebhookDefinition request);

        WebhookDefinitionResponse Delete(DeleteWebhookDefinition request);

        List<WebhookDefinitionResponse> Delete(BatchDeleteWebhookDefinitions request);

        WebhookDefinitionResponse Post(RestoreWebhookDefinition request);

        List<WebhookDefinitionResponse> Post(BatchRestoreWebhookDefinitions request);

        WebhookDefinitionResponse Get(GetWebhookDefinition request);

        List<WebhookDefinitionResponse> Get(QueryWebhookDefinitions request);

        Task<WebhookDefinitionResponse> Post(CreateWebhookDefinitionAsync request);

        Task<WebhookDefinitionResponse> Put(UpdateWebhookDefinitionAsync request);

        Task<WebhookDefinitionResponse> Delete(DeleteWebhookDefinitionAsync request);

        Task<List<WebhookDefinitionResponse>> Delete(BatchDeleteWebhookDefinitionsAsync request);

        Task<WebhookDefinitionResponse> Post(RestoreWebhookDefinitionAsync request);

        Task<List<WebhookDefinitionResponse>> Post(BatchRestoreWebhookDefinitionsAsync request);

        Task<WebhookDefinitionResponse> Get(GetWebhookDefinitionAsync request);

        Task<List<WebhookDefinitionResponse>> Get(QueryWebhookDefinitionsAsync request);
    }
}