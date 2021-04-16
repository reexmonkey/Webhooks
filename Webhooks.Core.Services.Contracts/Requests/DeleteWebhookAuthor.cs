using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to delete a webhook author.
    /// </summary>
    [Api("Specifies a service request to delete a webhook author.")]
    public abstract class DeleteWebhookAuthorBase : IDelete, IReturn<WebhookAuthorResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook author.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook author.", IsRequired = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Specifies whether the webhook author should be irreversibly erased (hard-delete) or reversibly marked (soft-delete) for deletion.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook author should be irreversibly erased (hard-delete) or reversibly marked (soft-delete) for deletion.", IsRequired = true)]
        public bool Permanently { get; set; }

        /// <summary>
        /// The password to authenticate the webhook author and authorize the delete operation.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the webhook author and authorize the delete operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to delete a webhook author in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook author in a synchronous operation.")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Sync")]
    [Route("/sync/webhooks/authors/{Id}", "DELETE")]
    public sealed class DeleteWebhookAuthor : DeleteWebhookAuthorBase
    {
    }

    /// <summary>
    /// Represents a service request to delete a webhook author in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook author in an asynchronous operation.")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Async")]
    [Route("/async/webhooks/authors/{Id}", "DELETE")]
    public sealed class DeleteWebhookAuthorAsync : DeleteWebhookAuthorBase
    {
    }
}