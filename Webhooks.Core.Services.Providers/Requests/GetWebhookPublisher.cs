using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to retrieve a webhook publisher.
    /// </summary>
    [Api("Specifies a service request to retrieve a webhook publisher.")]
    public abstract class GetWebhookPublisherBase : IGet, IReturn<WebhookPublisherResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook publisher.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook publisher.", IsRequired = true)]
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents a service request to retrieve a webhook publisher in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to retrieve a webhook publisher in a synchronous operation.")]
    [Tag("Get")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Route("/webhooks/publishers/{Id}", "GET")]
    public sealed class GetWebhookPublisher : GetWebhookPublisherBase
    {
    }

    /// <summary>
    /// Represents a service request to retrieve a webhook publisher in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to retrieve a webhook publisher in an asynchronous operation.")]
    [Tag("Get")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Tag("Async")]
    [Route("/async/webhooks/publishers/{Id}", "GET")]
    public sealed class GetWebhookPublisherAsync : GetWebhookPublisherBase
    {
    }
}