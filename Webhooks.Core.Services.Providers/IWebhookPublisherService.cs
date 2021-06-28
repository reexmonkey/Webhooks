using Reexmonkey.Webhooks.Core.Services.Publishers.Requests;
using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Contracts
{
    public interface IWebhookPublisherService
    {
        #region publisher profile

        WebhookPublisherResponse Post(CreateWebhookPublisher request);

        WebhookPublisherResponse Post(UpdateWebhookPublisher request);

        WebhookPublisherResponse Delete(DeleteWebhookPublisher request);

        List<WebhookPublisherResponse> Delete(BatchDeleteWebhookPublishers request);

        WebhookPublisherResponse Post(RestoreWebhookPublisher request);

        List<WebhookPublisherResponse> Post(BatchRestoreWebhookPublishers request);

        WebhookPublisherResponse Get(GetWebhookPublisher request);

        List<WebhookPublisherResponse> Get(QueryWebhookPublishers request);

        Task<WebhookPublisherResponse> Post(CreateWebhookPublisherAsync request);

        Task<WebhookPublisherResponse> Put(UpdateWebhookPublisherAsync request);

        Task<WebhookPublisherResponse> Delete(DeleteWebhookPublisherAsync request);

        Task<List<WebhookPublisherResponse>> Delete(BatchDeleteWebhookPublishersAsync request);

        Task<WebhookPublisherResponse> Post(RestoreWebhookPublisherAsync request);

        Task<List<WebhookPublisherResponse>> Post(BatchRestoreWebhookPublishersAsync request);

        Task<WebhookPublisherResponse> Get(GetWebhookPublisherAsync request);

        Task<List<WebhookPublisherResponse>> Get(QueryWebhookPublishersAsync request);

        #endregion publisher profile

        #region webhook definition

        WebhookDefinitionResponse Post(CreateWebhookDefinition request);

        WebhookDefinitionResponse Put(UpdateWebhookDefinition request);

        WebhookDefinitionResponse Get(GetWebhookDefinition request);

        List<WebhookDefinitionResponse> Get(QueryWebhookDefinitions request);

        WebhookDefinitionResponse Delete(EraseWebhookDefinition request);

        List<WebhookDefinitionResponse> Delete(BatchEraseWebhookDefinitions request);

        WebhookDefinitionResponse Post(PublishWebhookDefinition request);

        List<WebhookDefinitionResponse> Post(BatchPublishWebhookDefinitions request);

        WebhookDefinitionResponse Post(WithdrawWebhookDefinition request);

        List<WebhookDefinitionResponse> Post(BatchWithdrawWebhookDefinitions request);

        Task<WebhookDefinitionResponse> Post(CreateWebhookDefinitionAsync request);

        Task<WebhookDefinitionResponse> Put(UpdateWebhookDefinitionAsync request);

        Task<WebhookDefinitionResponse> Get(GetWebhookDefinitionAsync request);

        Task<List<WebhookDefinitionResponse>> Get(QueryWebhookDefinitionsAsync request);

        Task<WebhookDefinitionResponse> Delete(EraseWebhookDefinitionAsync request);

        Task<List<WebhookDefinitionResponse>> Delete(BatchEraseWebhookDefinitionsAsync request);

        Task<WebhookDefinitionResponse> Post(PublishWebhookDefinitionAsync request);

        Task<List<WebhookDefinitionResponse>> Post(BatchPublishWebhookDefinitionsAsync request);

        Task<WebhookDefinitionResponse> Post(WithdrawWebhookDefinitionAsync request);

        Task<List<WebhookDefinitionResponse>> Post(BatchWithdrawWebhookDefinitionsAsync request);

        #endregion webhook definition

        #region webhook event

        WebhookEventResponse Post(RaiseWebhookEvent request);

        Task<WebhookEventResponse> Post(RaiseWebhookEventAsync request);

        #endregion webhook event
    }
}