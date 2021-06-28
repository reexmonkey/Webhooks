using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Subscriptions.Requests
{
    /// <summary>
    /// Specifies a service request to restore a soft-deleted webhook subscription.
    /// </summary>
    [Api("Specifies a service request to restore a soft-deleted webhook subscription.")]
    public abstract class RestoreWebhookSubscriptionBase : IPost, IReturn<WebhookSubscriptionResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook subscription.", IsRequired = true)]
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook subscription in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to restore a soft-deleted webhook subscription in a synchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Undeletions")]
    [Route("/sync/webhooks/subscriptions/{Id}/undeletions", "POST")]
    public sealed class RestoreWebhookSubscription : RestoreWebhookSubscriptionBase
    {
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook subscription in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to restore a soft-deleted webhook subscription in an asynchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Undeletions")]
    [Tag("Async")]
    [Route("/async/webhooks/subscriptions/{Id}/undeletions", "POST")]
    public sealed class RestoreWebhookSubscriptionAsync : RestoreWebhookSubscriptionBase
    {
    }
}