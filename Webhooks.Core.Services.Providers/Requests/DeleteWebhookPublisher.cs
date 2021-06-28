using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to delete a webhook publisher.
    /// </summary>
    [Api("Specifies a service request to delete a webhook publisher.")]
    public abstract class DeleteWebhookPublisherBase : IDelete, IReturn<WebhookPublisherResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook publisher.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook publisher.", IsRequired = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Specifies whether the webhook publisher should be irreversibly erased (hard-delete) or reversibly marked (soft-delete) for deletion.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook publisher should be irreversibly erased (hard-delete) or reversibly marked (soft-delete) for deletion.", IsRequired = true)]
        public bool Permanently { get; set; }

        /// <summary>
        /// The password to authenticate the webhook publisher and authorize the delete operation.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the webhook publisher and authorize the delete operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to delete a webhook publisher in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook publisher in a synchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Tag("Deletions")]
    [Route("/webhooks/publishers/{Id}", "DELETE")]
    public sealed class DeleteWebhookPublisher : DeleteWebhookPublisherBase
    {
    }

    /// <summary>
    /// Represents a service request to delete a webhook publisher in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook publisher in an asynchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Tag("Deletions")]
    [Tag("Async")]
    [Route("/async/webhooks/publishers/{Id}", "DELETE")]
    public sealed class DeleteWebhookPublisherAsync : DeleteWebhookPublisherBase
    {
    }
}