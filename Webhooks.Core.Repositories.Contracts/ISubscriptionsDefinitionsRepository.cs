using reexmonkey.xmisc.backbone.repositories.contracts;
using Reexmonkey.Webhooks.Core.Repositories.Contracts.Joins;
using System;

namespace Reexmonkey.Webhooks.Core.Repositories.Contracts
{
    public interface ISubscriptionsDefinitionsRepository :
        IReadRepository<long, SubscriptionsDefinitions>,
        IWriteRepository<long, SubscriptionsDefinitions>,
        IEraseRepository<long, SubscriptionsDefinitions>
    {
    }
}