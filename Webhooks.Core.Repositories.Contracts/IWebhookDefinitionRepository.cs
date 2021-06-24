using reexmonkey.xmisc.backbone.repositories.contracts;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using System;

namespace Reexmonkey.Webhooks.Core.Repositories
{
    public interface IWebhookDefinitionRepository :
        IReadRepository<Guid, WebhookDefinition>,
        IWriteRepository<Guid, WebhookDefinition>,
        IEraseRepository<Guid, WebhookDefinition>,
        ITrashRepository<Guid, WebhookDefinition>
    {
    }
}