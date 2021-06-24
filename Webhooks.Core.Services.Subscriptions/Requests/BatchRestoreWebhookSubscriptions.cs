using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Subscriptions.Requests
{
    /// <summary>
    /// Specifies a service request to restore soft-deleted webhook subscriptions.
    /// </summary>
    [Api("Specifies a service request to restore soft-deleted webhook subscriptions.")]
    public abstract class BatchRestoreWebhookSubscriptionsBase : IPost, IReturn<List<WebhookSubscriptionResponse>>
    {
        /// <summary>
        /// The soft-deleted webhook subscriptions to restore.
        /// </summary>
        [ApiMember(Description = "The soft-deleted webhook subscriptions to restore.", IsRequired = true)]
        public List<WebhookSubscription> Subscriptions { get; set; }
    }

    /// <summary>
    /// Represents a service request to restore soft-deleted webhook subscriptions in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to restore soft-deleted webhook subscriptions in a synchronous operation.")]
    [Tag("Batch")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Sync")]
    [Route("/sync/webhooks/undeletions/subscriptions", "POST")]
    public sealed class BatchRestoreWebhookSubscriptions : BatchRestoreWebhookSubscriptionsBase
    {
    }

    /// <summary>
    /// Represents a service request to restore soft-deleted webhook subscriptions in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to restore soft-deleted webhook subscriptions in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Async")]
    [Route("/async/webhooks/undeletions/subscriptions", "POST")]
    public sealed class BatchRestoreWebhookSubscriptionsAsync : BatchRestoreWebhookSubscriptionsBase
    {
    }
}