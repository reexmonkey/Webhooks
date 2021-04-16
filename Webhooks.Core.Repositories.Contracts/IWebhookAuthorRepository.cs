using reexmonkey.xmisc.backbone.repositories.contracts;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using System;

namespace Reexmonkey.Webhooks.Core.Repositories.Contracts
{
    public interface IWebhookAuthorRepository :
        IReadRepository<Guid, WebhookAuthor>,
        IWriteRepository<Guid, WebhookAuthor>,
        ITrashRepository<Guid, WebhookAuthor>,
        IEraseRepository<Guid, WebhookAuthor>
    {
    }
}