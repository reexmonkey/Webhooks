using reexmonkey.xmisc.backbone.repositories.contracts;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using System;

namespace Reexmonkey.Webhooks.Core.Repositories.Contracts
{
    public interface IWebhookSubscriptionRepository: 
        IReadRepository<Guid, WebhookSubscription>,
        IWriteRepository<Guid, WebhookSubscription>,
        ITrashRepository<Guid, WebhookSubscription>,
        IEraseRepository<Guid, WebhookSubscription>
    {

    }
}
