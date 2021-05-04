using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to restore a soft-deleted webhook provider.
    /// </summary>
    [Api("Specifies a service request to restore a soft-deleted webhook provider.")]
    public abstract class RestoreWebhookProviderBase : IPost, IReturn<WebhookProviderResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook provider.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook provider.", IsRequired = true)]
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook provider in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to restore the soft-deleted profile of a webhook provider.")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Sync")]
    [Route("/sync/webhooks/undeletions/providers/{Id}", "POST")]
    public sealed class RestoreWebhookProvider : RestoreWebhookProviderBase
    {
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook provider in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to restore a soft-deleted webhook provider in an asynchronous operation.")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Async")]
    [Route("/async/webhooks/undeletions/providers/{Id}", "POST")]
    public sealed class RestoreWebhookProviderAsync : RestoreWebhookProviderBase
    {
    }
}