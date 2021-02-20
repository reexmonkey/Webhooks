using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to delete a batch of webhook subscriptions.
    /// </summary>
    [Api("pecifies a service request to delete a batch of webhook subscriptions.")]
    public abstract class BatchDeleteWebhookSubscriptionBase : IPost, IReturn<List<WebhookSubscriptionResponse>>
    {
        /// <summary>
        /// The name of the webhook definitions associated with the subscription.
        /// </summary>
        [ApiMember(Description = "The name of the webhook definitions associated with the subscription.", IsRequired = true)]
        public List<WebhookSubscription> Subscriptions { get; set; }

        /// <summary>
        /// Specifies whether the webhook subscriptions should be irreversibly erased or reversibly marked for deletion.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook subscriptions should be irreversibly erased or reversibly marked for deletion.", IsRequired = true)]
        public bool Permanently { get; set; }
    }

    /// <summary>
    /// Represents a service request to delete a batch of webhook subscriptions in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a batch of webhook subscriptions in a synchronous operation.")]
    [Tag("Batch")]    
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Sync")]
    [Route("/sync/webhooks/subscriptions/delete", "POST")]
    public sealed class BatchDeleteWebhookSubscription : BatchDeleteWebhookSubscriptionBase
    {
    }

    /// <summary>
    /// Represents a service request to delete a batch of webhook subscriptions in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a batch of webhook subscriptions in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Async")]
    [Route("/sync/webhooks/subscriptions/delete", "POST")]
    public sealed class BatchDeleteWebhookSubscriptionAsync : BatchDeleteWebhookSubscriptionBase
    {
    }
}