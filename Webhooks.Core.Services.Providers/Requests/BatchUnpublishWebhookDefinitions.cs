using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to unpublish a batch of webhook definitions associated with a publisher.
    /// </summary>
    [Api("Specifies a service request to unpublish a batch of webhook definitions associated with a publisher.")]
    public abstract class BatchUnpublishWebhookDefinitionsBase : IDelete, IReturn<List<WebhookDefinitionResponse>>
    {
        /// <summary>
        /// The name of the webhook definitions associated with the publisher.
        /// </summary>
        [ApiMember(Description = "The name of the webhook definitions associated with the publisher.", IsRequired = true)]
        public List<WebhookDefinition> Definitions { get; set; }

        /// <summary>
        /// The unique identifier of the publisher, who created the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the publisher, who created the webhook definition.", IsRequired = true)]
        public Guid PublisherId { get; set; }

        /// <summary>
        /// The password to publisherize the publish operation.
        /// </summary>
        [ApiMember(Description = "The password to publisherize the publish operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to unpublish a batch of webhook definitions in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to unpublish a batch of webhook definitions in a synchronous operation.")]
    [Tag("Batch")]
    [Tag("Unpublish")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Route("/webhooks/definitions", "DELETE")]
    public sealed class BatchUnpublishWebhookDefinitions : BatchUnpublishWebhookDefinitionsBase
    {
    }

    /// <summary>
    /// Represents a service request to unpublish a batch of webhook definitions in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to unpublish a batch of webhook definitions in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Unpublish")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Async")]
    [Route("/async/webhooks/definitions", "DELETE")]
    public sealed class BatchUnpublishWebhookDefinitionsAsync : BatchUnpublishWebhookDefinitionsBase
    {
    }
}