using System.Collections.Generic;
using System.Threading.Tasks;
using Webhooks.Core.Services.Contracts.Requests;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts
{
    public interface IWebhookAuthorService
    {
        WebhookAuthorResponse Post(CreateWebhookAuthor request);

        WebhookAuthorResponse Post(UpdateWebhookAuthor request);

        WebhookAuthorResponse Delete(DeleteWebhookAuthor request);

        List<WebhookAuthorResponse> Delete(BatchDeleteWebhookAuthors request);

        WebhookAuthorResponse Post(RestoreWebhookAuthor request);

        List<WebhookAuthorResponse> Post(BatchRestoreWebhookAuthors request);

        WebhookAuthorResponse Get(GetWebhookAuthor request);

        List<WebhookAuthorResponse> Get(QueryWebhookAuthors request);

        WebhookEventResponse Post(RaiseWebhookEvent request);

        List<WebhookDefinitionResponse> Post(BatchPublishWebhooks request);

        Task<WebhookAuthorResponse> Post(CreateWebhookAuthorAsync request);

        Task<WebhookAuthorResponse> Put(UpdateWebhookAuthorAsync request);

        Task<WebhookAuthorResponse> Delete(DeleteWebhookAuthorAsync request);

        Task<List<WebhookAuthorResponse>> Delete(BatchDeleteWebhookAuthorsAsync request);

        Task<WebhookAuthorResponse> Post(RestoreWebhookAuthorAsync request);

        Task<List<WebhookAuthorResponse>> Post(BatchRestoreWebhookAuthorsAsync request);

        Task<WebhookAuthorResponse> Get(GetWebhookAuthorAsync request);

        Task<List<WebhookAuthorResponse>> Get(QueryWebhookAuthorsAsync request);

        Task<WebhookEventResponse> Post(RaiseWebhookEventAsync request);

        Task<List<WebhookDefinitionResponse>> Post(BatchPublishWebhooksAsync request);
    }
}