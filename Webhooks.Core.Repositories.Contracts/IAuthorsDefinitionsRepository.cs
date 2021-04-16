using reexmonkey.xmisc.backbone.repositories.contracts;
using Reexmonkey.Webhooks.Core.Repositories.Contracts.Joins;
using System;

namespace Reexmonkey.Webhooks.Core.Repositories.Contracts
{
    public interface IAuthorsDefinitionsRepository :
        IReadRepository<long, AuthorsDefinitions>,
        IWriteRepository<long, AuthorsDefinitions>,
        IEraseRepository<long, AuthorsDefinitions>
    {
    }
}