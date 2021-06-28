using ServiceStack;
using System;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Subscriptions.Requests
{
    /// <summary>
    /// Specifies a service request to query for webhook subscriptions.
    /// </summary>
    [Api("Specifies a service request to query for webhook subscriptions.")]
    public abstract class QueryWebhookSubscriptionsBase : IGet, IReturn<List<WebhookSubscriptionResponse>>
    {
        /// <summary>
        /// The unique identifier of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook subscription.", IsRequired = false)]
        public Guid Id { get; set; }

        [ApiMember(Description = "The unique name of the webhook subscription.", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// The display name of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The description of the webhook subscription.", IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// The name of the webhook events associated with the subscription.
        /// </summary>
        [ApiMember(Description = "The name of the webhook events associated with the subscription.", IsRequired = false)]
        public List<string> WebhookNames { get; set; }

        /// <summary>
        /// The endpoint URI of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The endpoint URI of the webhook subscription.", IsRequired = true)]
        public Uri EndpointUri { get; set; }


        /// <summary>
        /// The time (in UTC) at which the webhook subscription was created.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) at which the webhook subscription was created.", IsRequired = true)]
        public DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// The time (in UTC) at which the the webhook subscription was last modified.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) of the webhook subscription was last modified.", IsRequired = true)]
        public DateTime LastModificationTimeUtc { get; set; }

        /// <summary>
        /// Specifies whether the webhook subscription is active or not.
        /// <para/> True if the subscription is active; otherwise false.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook subscription is active or not." +
            " True if the subscription is active; otherwise false.", IsRequired = false)]
        public bool IsActive { get; set; }

    }

    /// <summary>
    /// Represents a service request to query for webhook subscriptions in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to query for webhook subscriptions in a synchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Queries")]
    [Route("/webhooks/subscriptions", "GET")]
    public sealed class QueryWebhookSubscriptions : QueryWebhookSubscriptionsBase
    {
    }

    /// <summary>
    /// Represents a service request to query for webhook subscriptions in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to query for webhook subscriptions in an asynchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Queries")]
    [Tag("Async")]
    [Route("/async/webhooks/subscriptions", "GET")]
    public sealed class QueryWebhookSubscriptionsAsync : QueryWebhookSubscriptionsBase
    {
    }
}