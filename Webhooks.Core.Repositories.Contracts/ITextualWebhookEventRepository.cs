using reexmonkey.xmisc.backbone.repositories.contracts;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reexmonkey.Webhooks.Core.Repositories.Contracts
{
    public interface ITextualWebhookEventRepository:
        IReadRepository<Guid, TextualWebEvent>,
        IWriteRepository<Guid, TextualWebEvent>,
        IEraseRepository<Guid, TextualWebEvent>
    {

    }
}
