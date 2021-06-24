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
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook publisher in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to restore the soft-deleted profile of a webhook publisher.")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Route("/webhooks/undeletions/publishers/{Id}", "POST")]
    public sealed class RestoreWebhookPublisher : RestoreWebhookPublisherBase
    {
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook publisher in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to restore a soft-deleted webhook publisher in an asynchronous operation.")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Tag("Async")]
    [Route("/async/webhooks/undeletions/publishers/{Id}", "POST")]
    public sealed class RestoreWebhookPublisherAsync : RestoreWebhookPublisherBase
    {
    }
}