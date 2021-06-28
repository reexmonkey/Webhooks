using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to restore a soft-deleted webhook publisher.
    /// </summary>
    [Api("Specifies a service request to restore a soft-deleted webhook publisher.")]
    public abstract class RestoreWebhookPublisherBase : IPost, IReturn<WebhookPublisherResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook publisher.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook publisher.", IsRequired = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// The password of the publisher to authenticate the restore operation.
        /// </summary>
        [ApiMember(Description = "The password of the publisher to authenticate the restore operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook publisher in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to restore the soft-deleted profile of a webhook publisher.")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Tag("Undeletions")]
    [Route("/webhooks/publishers/{Id}/undeletions", "POST")]
    public sealed class RestoreWebhookPublisher : RestoreWebhookPublisherBase
    {
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook publisher in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to restore a soft-deleted webhook publisher in an asynchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Tag("Undeletions")]
    [Tag("Async")]
    [Route("/async/webhooks/publishers/{Id}/undeletions", "POST")]
    public sealed class RestoreWebhookPublisherAsync : RestoreWebhookPublisherBase
    {
    }
}