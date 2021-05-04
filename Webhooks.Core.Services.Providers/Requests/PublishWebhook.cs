using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to publish a webhook definition.
    /// </summary>
    [Api("Specifies a service request to publish a webhook definition.")]
    public class PublishWebhookDefinitionsBase : IPost, IReturn<WebhookDefinitionResponse>
    {
        /// <summary>
        /// The unique identifier of the provider, who created the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the provider, who created the webhook definition.", IsRequired = true)]
        public Guid ProviderId { get; set; }

        /// <summary>
        /// The webhook to publish
        /// </summary>
        [ApiMember(Description = "The webhook to publish", IsRequired = true)]
        public WebhookDefinition Webhook { get; set; }

        /// <summary>
        /// The password to providerize the publish operation.
        /// </summary>
        [ApiMember(Description = "The password to providerize the publish operation.", IsRequired = true)]
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
    [Route("/sync/webhooks/providers/{ProviderId}/publications", "POST")]
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
    [Route("/async/webhooks/providers/{ProviderId}/publications", "POST")]
    public sealed class PublishWebhookDefinitionAsync : PublishWebhookDefinitionsBase
    {
    }
}