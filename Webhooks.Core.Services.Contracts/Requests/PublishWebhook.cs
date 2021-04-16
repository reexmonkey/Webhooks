using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to publish a webhook definition.
    /// </summary>
    [Api("Specifies a service request to publish a webhook definition.")]
    public class PublishWebhookDefinitionsBase : IPost, IReturn<WebhookDefinitionResponse>
    {
        /// <summary>
        /// The unique identifier of the author, who created the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the author, who created the webhook definition.", IsRequired = true)]
        public Guid AuthorId { get; set; }

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
    [Tag("Publish")]
    [Tag("Webhook")]
    [Tag("Definition")]
    [Tag("Sync")]
    [Route("/sync/webhooks/authors/{AuthorId}/publications", "POST")]
    public sealed class PublishWebhook : PublishWebhookDefinitionsBase
    {
    }

    /// <summary>
    /// Represents a service request to publish a webhook definition in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to publish a webhook definition in an asynchronous operation.")]
    [Tag("Publish")]
    [Tag("Webhook")]
    [Tag("Definition")]
    [Tag("Async")]
    [Route("/async/webhooks/authors/{AuthorId}/publications", "POST")]
    public sealed class PublishWebhookDefinitionAsync : PublishWebhookDefinitionsBase
    {
    }
}