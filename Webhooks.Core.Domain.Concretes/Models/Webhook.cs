using ServiceStack.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reexmonkey.Webhooks.Core.Domain.Concretes.Models
{
    public class Webhook: IHasId<string>
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }
    }
}
