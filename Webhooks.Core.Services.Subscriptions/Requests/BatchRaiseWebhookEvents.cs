using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to raise webhook events.
    /// </summary>
    [Api("Specifies a service request to raise webhook events.")]
    public class BatchRaiseWebhookEventsBase : IPost, IReturn<WebhookEventResponse>
    {
        /// <summary>
        /// The unique identifier of the provider, who raises the webhook events.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the provider, who raises the webhook events.", IsRequired = true)]
        public Guid ProviderId { get; set; }

        /// <summary>
        /// The events to raise.
        /// </summary>
        [ApiMember(Description = "The events to raise.", IsRequired = true)]
        public List<WebhookEvent> Events { get; set; }

        /// <summary>
        /// The password to authenticate the provider and providerize the operation.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the provider and providerize the operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to raise webhook events.
    /// </summary>
    [Api("Represents a service request to raise webhook events.")]
    [Tag("Publish")]
    [Tag("Webhooks")]
    [Tag("Events")]
    [Tag("Sync")]
    [Route("/sync/webhooks/providers/events", "POST")]
    public sealed class BatchRaiseWebhookEvents : BatchRaiseWebhookEventsBase
    {
    }

    /// <summary>
    /// Represents a service request to raise webhook events in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to raise webhook events in an asynchronous operation.")]
    [Tag("Publish")]
    [Tag("Webhooks")]
    [Tag("Events")]
    [Tag("Async")]
    [Route("/async/webhooks/providers/events", "POST")]
    public sealed class BatchRaiseWebhookEventsAsync : BatchRaiseWebhookEventsBase
    {
    }
}