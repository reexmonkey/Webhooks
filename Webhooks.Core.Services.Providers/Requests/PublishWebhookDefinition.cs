﻿using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to publish a webhook definition.
    /// </summary>
    [Api("Specifies a service request to publish a webhook definition.")]
    public class PublishWebhookDefinitionsBase : IPost, IReturn<WebhookDefinitionResponse>
    {
        /// <summary>
        /// The unique identifier of the publisher, who created the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the publisher, who created the webhook definition.", IsRequired = true)]
        public Guid PublisherId { get; set; }

        /// <summary>
        /// The webhook to publish
        /// </summary>
        [ApiMember(Description = "The webhook to publish", IsRequired = true)]
        public WebhookDefinition Webhook { get; set; }

        /// <summary>
        /// The password to authorize the publish operation.
        /// </summary>
        [ApiMember(Description = "The password to authorize the publish operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to publish a webhook definition in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to publish a webhook definition in a synchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Publications")]
    [Route("/webhooks/publishers/{PublisherId}/publications", "POST")]
    public sealed class PublishWebhookDefinition : PublishWebhookDefinitionsBase
    {
    }

    /// <summary>
    /// Represents a service request to publish a webhook definition in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to publish a webhook definition in an asynchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Publications")]
    [Tag("Async")]
    [Route("/async/webhooks/publishers/{PublisherId}/publications", "POST")]
    public sealed class PublishWebhookDefinitionAsync : PublishWebhookDefinitionsBase
    {
    }
}