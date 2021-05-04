using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System.Collections.Generic;
using Webhooks.Core.Services.Providers.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to restore soft-deleted webhook providers.
    /// </summary>
    [Api("Specifies a service request to restore soft-deleted webhook providers.")]
    public abstract class BatchRestoreWebhookProvidersBase : IPost, IReturn<List<WebhookProviderResponse>>
    {
        /// <summary>
        /// The soft-deleted webhook providers to restore.
        /// </summary>
        [ApiMember(Description = "The soft-deleted webhook providers to restore.", IsRequired = true)]
        public List<WebhookProvider> Providers { get; set; }
    }

    /// <summary>
    /// Represents a service request to restore soft-deleted webhook providers in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to restore soft-deleted webhook providers in a synchronous operation.")]
    [Tag("Batch")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Sync")]
    [Route("/sync/webhooks/undeletions/providers", "POST")]
    public sealed class BatchRestoreWebhookProviders : BatchRestoreWebhookProvidersBase
    {
    }

    /// <summary>
    /// Represents a service request to restore soft-deleted webhook providers in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to restore soft-deleted webhook providers in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Async")]
    [Route("/async/webhooks/undeletions/providers", "POST")]
    public sealed class BatchRestoreWebhookProvidersAsync : BatchRestoreWebhookProvidersBase
    {
    }
}