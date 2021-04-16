using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to delete a batch of webhook authors.
    /// </summary>
    [Api("Specifies a service request to delete a batch of webhook authors.")]
    public abstract class BatchDeleteWebhookAuthorsBase : IDelete, IReturn<List<WebhookAuthorResponse>>
    {
        /// <summary>
        /// The name of the webhook definitions associated with the author.
        /// </summary>
        [ApiMember(Description = "The name of the webhook definitions associated with the author.", IsRequired = true)]
        public List<WebhookAuthor> Authors { get; set; }

        /// <summary>
        /// Specifies whether the webhook authors should be irreversibly erased or reversibly marked for deletion.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook authors should be irreversibly erased or reversibly marked for deletion.", IsRequired = true)]
        public bool Permanently { get; set; }
    }

    /// <summary>
    /// Represents a service request to delete a batch of webhook authors in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a batch of webhook authors in a synchronous operation.")]
    [Tag("Batch")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Sync")]
    [Route("/sync/webhooks/authors", "DELETE")]
    public sealed class BatchDeleteWebhookAuthors : BatchDeleteWebhookAuthorsBase
    {
    }

    /// <summary>
    /// Represents a service request to delete a batch of webhook authors in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a batch of webhook authors in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Async")]
    [Route("/async/webhooks/authors", "DELETE")]
    public sealed class BatchDeleteWebhookAuthorsAsync : BatchDeleteWebhookAuthorsBase
    {
    }
}