using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
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

        /// <summary>
        /// The secret to sign, verify, encrypt or decrypt the payload of a webhook, or authorize modification of a webhook subscription.
        /// </summary>
        [ApiMember(Description = "The secret to sign, verify, encrypt or decrypt the payload of a webhook, or authorize modification of a webhook subscription.", IsRequired = true)]
        public string Secret { get; set; }
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook subscription in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to restore a soft-deleted webhook subscription in a synchronous operation.")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Sync")]
    [Route("/sync/webhooks/subscriptions/{Id}/restore", "POST")]
    public sealed class RestoreWebhookSubscription : RestoreWebhookSubscriptionBase
    {
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook subscription in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to restore a soft-deleted webhook subscription in an asynchronous operation.")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Async")]
    [Route("/sync/webhooks/subscriptions/{Id}/restore", "POST")]
    public sealed class RestoreWebhookSubscriptionAsync : RestoreWebhookSubscriptionBase
    {
    }
}