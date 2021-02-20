using ServiceStack;
using System;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to search for webhook subscriptions.
    /// </summary>
    [Api("pecifies a service request to search for webhook subscriptions.")]
    public abstract class SearchWebhookSubscriptionBase : IPost, IReturn<List<WebhookSubscriptionResponse>>
    {
        /// <summary>
        /// The unique identifier of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook subscription.", IsRequired = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique identifier of the webhook subscriber.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook subscriber.", IsRequired = true)]
        public string SubscriberId { get; set; }

        /// <summary>
        /// The display name of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The display name of the webhook subscription.", IsRequired = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// The name of the webhook definitions associated with the subscription.
        /// </summary>
        [ApiMember(Description = "The name of the webhook definitions associated with the subscription.", IsRequired = false)]
        public List<string> Webhooks { get; set; }

        /// <summary>
        /// The endpoint URL of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The endpoint URL of the webhook subscription.", IsRequired = true)]
        public Uri Endpoint { get; set; }

    }

    /// <summary>
    /// Represents a service request to search for webhook subscriptions in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to search for webhook subscriptions in a synchronous operation.")]
    [Tag("Search")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Sync")]
    [Route("/sync/webhooks/subscriptions/{Id}/search", "POST")]
    public sealed class SearchWebhookSubscription : SearchWebhookSubscriptionBase
    {
    }

    /// <summary>
    /// Represents a service request to search for webhook subscriptions in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to search for webhook subscriptions in an asynchronous operation.")]
    [Tag("Search")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Async")]
    [Route("/sync/webhooks/subscriptions/{Id}/search", "POST")]
    public sealed class SearchWebhookSubscriptionAsync : SearchWebhookSubscriptionBase
    {
    }
}