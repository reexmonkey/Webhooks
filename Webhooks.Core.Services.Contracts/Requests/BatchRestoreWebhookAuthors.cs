using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to restore soft-deleted webhook authors.
    /// </summary>
    [Api("Specifies a service request to restore soft-deleted webhook authors.")]
    public abstract class BatchRestoreWebhookAuthorsBase : IPost, IReturn<List<WebhookAuthorResponse>>
    {
        /// <summary>
        /// The soft-deleted webhook authors to restore.
        /// </summary>
        [ApiMember(Description = "The soft-deleted webhook authors to restore.", IsRequired = true)]
        public List<WebhookAuthor> Authors { get; set; }
    }

    /// <summary>
    /// Represents a service request to restore soft-deleted webhook authors in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to restore soft-deleted webhook authors in a synchronous operation.")]
    [Tag("Batch")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Sync")]
    [Route("/sync/webhooks/undeletions/authors", "POST")]
    public sealed class BatchRestoreWebhookAuthors : BatchRestoreWebhookAuthorsBase
    {
    }

    /// <summary>
    /// Represents a service request to restore soft-deleted webhook authors in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to restore soft-deleted webhook authors in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Async")]
    [Route("/async/webhooks/undeletions/authors", "POST")]
    public sealed class BatchRestoreWebhookAuthorsAsync : BatchRestoreWebhookAuthorsBase
    {
    }
}