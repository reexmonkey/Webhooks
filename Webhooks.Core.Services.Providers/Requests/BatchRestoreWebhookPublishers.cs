using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to restore soft-deleted webhook publishers.
    /// </summary>
    [Api("Specifies a service request to restore soft-deleted webhook publishers.")]
    public abstract class BatchRestoreWebhookPublishersBase : IPost, IReturn<List<WebhookPublisherResponse>>
    {
        /// <summary>
        /// The soft-deleted webhook publishers to restore.
        /// </summary>
        [ApiMember(Description = "The soft-deleted webhook publishers to restore.", IsRequired = true)]
        public List<WebhookPublisher> Publishers { get; set; }
    }

    /// <summary>
    /// Represents a service request to restore soft-deleted webhook publishers in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to restore soft-deleted webhook publishers in a synchronous operation.")]
    [Tag("Batch")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Tag("Undeletions")]
    [Route("/webhooks/publishers/undeletions", "POST")]
    public sealed class BatchRestoreWebhookPublishers : BatchRestoreWebhookPublishersBase
    {
    }

    /// <summary>
    /// Represents a service request to restore soft-deleted webhook publishers in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to restore soft-deleted webhook publishers in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Tag("Undeletions")]
    [Tag("Async")]
    [Route("/async/webhooks/publishers/undeletions", "POST")]
    public sealed class BatchRestoreWebhookPublishersAsync : BatchRestoreWebhookPublishersBase
    {
    }
}