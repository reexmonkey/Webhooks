using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to delete a batch of webhook providers.
    /// </summary>
    [Api("Specifies a service request to delete a batch of webhook providers.")]
    public abstract class BatchDeleteWebhookProvidersBase : IDelete, IReturn<List<WebhookProviderResponse>>
    {
        /// <summary>
        /// The name of the webhook definitions associated with the provider.
        /// </summary>
        [ApiMember(Description = "The name of the webhook definitions associated with the provider.", IsRequired = true)]
        public List<WebhookProvider> Providers { get; set; }

        /// <summary>
        /// Specifies whether the webhook providers should be irreversibly erased or reversibly marked for deletion.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook providers should be irreversibly erased or reversibly marked for deletion.", IsRequired = true)]
        public bool Permanently { get; set; }
    }

    /// <summary>
    /// Represents a service request to delete a batch of webhook providers in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a batch of webhook providers in a synchronous operation.")]
    [Tag("Batch")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Sync")]
    [Route("/sync/webhooks/providers", "DELETE")]
    public sealed class BatchDeleteWebhookProviders : BatchDeleteWebhookProvidersBase
    {
    }

    /// <summary>
    /// Represents a service request to delete a batch of webhook providers in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a batch of webhook providers in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Async")]
    [Route("/async/webhooks/providers", "DELETE")]
    public sealed class BatchDeleteWebhookProvidersAsync : BatchDeleteWebhookProvidersBase
    {
    }
}