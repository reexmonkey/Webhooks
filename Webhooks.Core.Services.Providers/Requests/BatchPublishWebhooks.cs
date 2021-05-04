using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to publish a batch of webhooks.
    /// </summary>
    [Api("Specifies a service request to publish a batch of webhooks.")]
    public abstract class BatchPublishWebhooksBase : IPost, IReturn<List<WebhookEventResponse>>
    {
        /// <summary>
        /// The webhooks to publish
        /// </summary>
        [ApiMember(Description = "The webhooks to publish", IsRequired = true)]
        public List<WebhookDefinition> Webhooks { get; set; }

        /// <summary>
        /// The unique identifier of the provider, who is responsible for the webhooks.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the provider, who is responsible for the webhooks.", IsRequired = true)]
        public Guid ProviderId { get; set; }

        /// <summary>
        /// The password to authenticate the provider and providerize the publish operation.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the provider and providerize the publish operation.", IsRequired = true)]
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
    [Tag("Sync")]
    [Route("/sync/webhooks/providers/{ProviderId}/definitions", "POST")]
    public sealed class BatchPublishWebhooks : BatchPublishWebhooksBase
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
    [Route("/async/webhooks/providers/{ProviderId}/definitions", "POST")]
    public sealed class BatchPublishWebhooksAsync : BatchPublishWebhooksBase
    {
    }
}