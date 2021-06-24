using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to publish a batch of webhook definitions.
    /// </summary>
    [Api("Specifies a service request to publish a batch of webhook definitions.")]
    public abstract class BatchPublishWebhookDefinitionsBase : IPost, IReturn<List<WebhookDefinitionResponse>>
    {
        /// <summary>
        /// The webhooks to publish
        /// </summary>
        [ApiMember(Description = "The webhook definitions to publish", IsRequired = true)]
        public List<WebhookDefinition> Webhooks { get; set; }

        /// <summary>
        /// The unique identifier of the publisher, who is responsible for the webhook definitions.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the publisher, who is responsible for the webhook definitions.", IsRequired = true)]
        public Guid PublisherId { get; set; }

        /// <summary>
        /// The password to authenticate the publisher and publisherize the publish operation.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the publisher and publisherize the publish operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to publish a batch of webhooks in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to publish a batch of webhooks in a synchronous operation.")]
    [Tag("Batch")]
    [Tag("Publish")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Route("/webhooks/publishers/{PublisherId}/definitions", "POST")]
    public sealed class BatchPublishWebhookDefinitions : BatchPublishWebhookDefinitionsBase
    {
    }

    /// <summary>
    /// Represents a service request to publish a batch of webhooks in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to publish a batch of webhooks in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Publish")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Async")]
    [Route("/async/webhooks/publishers/{PublisherId}/definitions", "POST")]
    public sealed class BatchPublishWebhookDefinitionsAsync : BatchPublishWebhookDefinitionsBase
    {
    }
}