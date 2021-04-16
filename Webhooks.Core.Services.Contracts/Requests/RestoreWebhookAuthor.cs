using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to restore a soft-deleted webhook author.
    /// </summary>
    [Api("Specifies a service request to restore a soft-deleted webhook author.")]
    public abstract class RestoreWebhookAuthorBase : IPost, IReturn<WebhookAuthorResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook author.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook author.", IsRequired = true)]
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook author in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to restore the soft-deleted profile of a webhook author.")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Sync")]
    [Route("/sync/webhooks/undeletions/authors/{Id}", "POST")]
    public sealed class RestoreWebhookAuthor : RestoreWebhookAuthorBase
    {
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook author in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to restore a soft-deleted webhook author in an asynchronous operation.")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Async")]
    [Route("/async/webhooks/undeletions/authors/{Id}", "POST")]
    public sealed class RestoreWebhookAuthorAsync : RestoreWebhookAuthorBase
    {
    }
}