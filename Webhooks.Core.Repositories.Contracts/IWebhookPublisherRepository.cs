using reexmonkey.xmisc.backbone.repositories.contracts;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using System;

namespace Reexmonkey.Webhooks.Core.Repositories
{
    /// <summary>
    /// Specifies a repository for webhook publishers
    /// </summary>
    public interface IWebhookPublisherRepository :
        IReadRepository<Guid, WebhookPublisher>,
        IWriteRepository<Guid, WebhookPublisher>,
        ITrashRepository<Guid, WebhookPublisher>,
        IEraseRepository<Guid, WebhookPublisher>
    {
    }
}