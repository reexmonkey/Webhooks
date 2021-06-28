using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to publish a webhook event.
    /// </summary>
    [Api("Specifies a service request to raise a webhook event.")]
    public class RaiseWebhookEventBase : IPost, IReturn<WebhookEventResponse>
    {
        /// <summary>
        /// The unique identifier of the publisher, who raises the webhook event.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the publisher, who raises the webhook event.", IsRequired = true)]
        public Guid PublisherId { get; set; }

        /// <summary>
        /// The webhook, for which the event is raised.
        /// </summary>
        [ApiMember(Description = "The webhook, for which the event is raised.", IsRequired = true)]
        public WebhookDefinition Webhook { get; set; }

        /// <summary>
        /// The payload of the webhook event.
        /// </summary>
        [ApiMember(Description = "The payload of the webhook event.", IsRequired = true)]
        public Dictionary<string, object> Data { get; set; }

        /// <summary>
        /// The password to authenticate the publisher and authorize the publish operation.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the publisher and authorize the publish operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to raise a webhook event in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to raise a webhook event in a synchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Events")]
    [Route("/webhooks/publishers/{PublisherId}/events", "POST")]
    public sealed class RaiseWebhookEvent : RaiseWebhookEventBase
    {
    }

    /// <summary>
    /// Represents a service request to raise a webhook event in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to raise a webhook event in an asynchronous operation.")]
    [Tag("Publish")]
    [Tag("Webhook")]
    [Tag("Event")]
    [Tag("Async")]
    [Route("/async/webhooks/publishers/{PublisherId}/events", "POST")]
    public sealed class RaiseWebhookEventAsync : RaiseWebhookEventBase
    {
    }
}