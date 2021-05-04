using ServiceStack;
using System;
using Webhooks.Core.Services.Providers.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to delete a webhook provider.
    /// </summary>
    [Api("Specifies a service request to delete a webhook provider.")]
    public abstract class DeleteWebhookProviderBase : IDelete, IReturn<WebhookProviderResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook provider.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook provider.", IsRequired = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Specifies whether the webhook provider should be irreversibly erased (hard-delete) or reversibly marked (soft-delete) for deletion.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook provider should be irreversibly erased (hard-delete) or reversibly marked (soft-delete) for deletion.", IsRequired = true)]
        public bool Permanently { get; set; }

        /// <summary>
        /// The password to authenticate the webhook provider and providerize the delete operation.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the webhook provider and providerize the delete operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to delete a webhook provider in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook provider in a synchronous operation.")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Sync")]
    [Route("/sync/webhooks/providers/{Id}", "DELETE")]
    public sealed class DeleteWebhookProvider : DeleteWebhookProviderBase
    {
    }

    /// <summary>
    /// Represents a service request to delete a webhook provider in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook provider in an asynchronous operation.")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Async")]
    [Route("/async/webhooks/providers/{Id}", "DELETE")]
    public sealed class DeleteWebhookProviderAsync : DeleteWebhookProviderBase
    {
    }
}