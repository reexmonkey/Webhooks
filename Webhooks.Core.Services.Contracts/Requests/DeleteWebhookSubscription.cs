using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to delete a webhook subscription.
    /// </summary>
    [Api("pecifies a service request to delete a webhook subscription.")]
    public abstract class DeleteWebhookSubscriptionBase : IPost, IReturn<WebhookSubscriptionResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook subscription.", IsRequired = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Specifies whether the webhook subscription should be irreversibly erased (hard-delete) or reversibly marked (soft-delete) for deletion.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook subscription should be irreversibly erased (hard-delete) or reversibly marked (soft-delete) for deletion.", IsRequired = true)]
        public bool Permanently { get; set; }

        /// <summary>
        /// The secret to sign, verify, encrypt or decrypt the payload of a webhook, or authorize modification of a webhook subscription.
        /// </summary>
        [ApiMember(Description = "The secret to sign, verify, encrypt or decrypt the payload of a webhook, or authorize modification of a webhook subscription.", IsRequired = true)]
        public string Secret { get; set; }
    }

    /// <summary>
    /// Represents a service request to delete a webhook subscription in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook subscription in a synchronous operation.")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Sync")]
    [Route("/sync/webhooks/subscriptions/{Id}/delete", "POST")]
    public sealed class DeleteWebhookSubscription : DeleteWebhookSubscriptionBase
    {
    }

    /// <summary>
    /// Represents a service request to delete a webhook subscription in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook subscription in an asynchronous operation.")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Async")]
    [Route("/sync/webhooks/subscriptions/{Id}/delete", "POST")]
    public sealed class DeleteWebhookSubscriptionAsync : DeleteWebhookSubscriptionBase
    {
    }
}