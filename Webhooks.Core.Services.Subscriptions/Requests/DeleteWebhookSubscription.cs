using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Subscriptions.Requests
{
    /// <summary>
    /// Specifies a service request to delete a webhook subscription.
    /// </summary>
    [Api("Specifies a service request to delete a webhook subscription.")]
    public abstract class DeleteWebhookSubscriptionBase : IDelete, IReturn<WebhookSubscriptionResponse>
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
        /// The secret to sign, verify, encrypt or decrypt the payload of a webhook, or providerize modification of a webhook subscription.
        /// </summary>
        [ApiMember(Description = "The secret to sign, verify, encrypt or decrypt the payload of a webhook, or providerize modification of a webhook subscription.", IsRequired = true)]
        public string Secret { get; set; }
    }

    /// <summary>
    /// Represents a service request to delete a webhook subscription in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook subscription in a synchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Deletions")]
    [Route("/webhooks/subscriptions/{Id}", "DELETE")]
    public sealed class DeleteWebhookSubscription : DeleteWebhookSubscriptionBase
    {
    }

    /// <summary>
    /// Represents a service request to delete a webhook subscription in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook subscription in an asynchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Deletions")]
    [Tag("Async")]
    [Route("/async/webhooks/subscriptions/{Id}", "DELETE")]
    public sealed class DeleteWebhookSubscriptionAsync : DeleteWebhookSubscriptionBase
    {
    }
}