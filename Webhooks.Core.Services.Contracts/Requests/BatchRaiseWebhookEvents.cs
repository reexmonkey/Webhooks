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
        /// The unique identifier of the author, who raises the webhook events.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the author, who raises the webhook events.", IsRequired = true)]
        public Guid AuthorId { get; set; }

        /// <summary>
        /// The events to raise.
        /// </summary>
        [ApiMember(Description = "The events to raise.", IsRequired = true)]
        public List<WebhookEvent> Events { get; set; }

        /// <summary>
        /// The password to authenticate the author and authorize the operation.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the author and authorize the operation.", IsRequired = true)]
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
    [Route("/sync/webhooks/authors/events", "POST")]
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
    [Route("/async/webhooks/authors/events", "POST")]
    public sealed class BatchRaiseWebhookEventsAsync : BatchRaiseWebhookEventsBase
    {
    }
}