using reexmonkey.xmisc.backbone.repositories.contracts;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using System;

namespace Reexmonkey.Webhooks.Core.Repositories.Contracts
{
    public interface IBinaryWebhookEventRepository :
        IReadRepository<Guid, BinaryWebEvent>,
        IWriteRepository<Guid, BinaryWebEvent>,
        IEraseRepository<Guid, BinaryWebEvent>
    {
    }
}