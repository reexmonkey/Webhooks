﻿using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Subscriptions.Requests
{
    /// <summary>
    /// Specifies a service request to retrieve a webhook subscription.
    /// </summary>
    [Api("Specifies a service request to retrieve a webhook subscription.")]
    public abstract class GetWebhookSubscriptionBase : IGet, IReturn<WebhookSubscriptionResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook subscription.", IsRequired = true)]
        public Guid Id { get; set; }

    }

    /// <summary>
    /// Represents a service request to retrieve a webhook subscription in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to retrieve a webhook subscription in a synchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Retrievals")]
    [Route("/webhooks/subscriptions/{Id}", "GET")]
    public sealed class GetWebhookSubscription : GetWebhookSubscriptionBase
    {
    }

    /// <summary>
    /// Represents a service request to retrieve a webhook subscription in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to retrieve a webhook subscription in an asynchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Retrievals")]
    [Tag("Async")]
    [Route("/async/webhooks/subscriptions/{Id}", "GET")]
    public sealed class GetWebhookSubscriptionAsync : GetWebhookSubscriptionBase
    {
    }
}