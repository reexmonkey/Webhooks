using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack.DataAnnotations;
using System;

namespace Reexmonkey.Webhooks.Core.Repositories.Contracts.Joins
{
    public class SubscriptionsDefinitions
    {
        [AutoIncrement]
        public long Id { get; set; }

        [References(typeof(WebhookSubscription))]
        public Guid SubscriptionId { get; set; }

        [References(typeof(WebhookDefinition))]
        public Guid DefinitionId { get; set; }
    }
}