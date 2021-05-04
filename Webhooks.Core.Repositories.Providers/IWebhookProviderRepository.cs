using reexmonkey.xmisc.backbone.repositories.contracts;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using System;

namespace Reexmonkey.Webhooks.Core.Repositories.Contracts
{
    public interface IWebhookProviderRepository :
        IReadRepository<Guid, WebhookProvider>,
        IWriteRepository<Guid, WebhookProvider>,
        ITrashRepository<Guid, WebhookProvider>,
        IEraseRepository<Guid, WebhookProvider>
    {
    }
}