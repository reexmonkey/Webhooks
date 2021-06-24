using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to publish a webhook definition.
    /// </summary>
    [Api("Specifies a service request to publish a webhook definition.")]
    public class UnpublishWebhookDefinitionsBase : IDelete, IReturn<WebhookDefinitionResponse>
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
        /// The password to publisherize the publish operation.
        /// </summary>
        [ApiMember(Description = "The password to publisherize the publish operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to publish a webhook definition in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to publish a webhook definition in a synchronous operation.")]
    [Tag("Unpublish")]
    [Tag("Webhook")]
    [Tag("Definition")]
    [Route("/webhooks/publishers/{PublisherId}/definitions", "DELETE")]
    public sealed class UnpublishWebhookDefinition : UnpublishWebhookDefinitionsBase
    {
    }

    /// <summary>
    /// Represents a service request to publish a webhook definition in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to publish a webhook definition in an asynchronous operation.")]
    [Tag("Unpublish")]
    [Tag("Webhook")]
    [Tag("Definition")]
    [Tag("Async")]
    [Route("/async/webhooks/publishers/{PublisherId}/definitions", "DELETE")]
    public sealed class UnpublishWebhookDefinitionAsync : UnpublishWebhookDefinitionsBase
    {
    }
}