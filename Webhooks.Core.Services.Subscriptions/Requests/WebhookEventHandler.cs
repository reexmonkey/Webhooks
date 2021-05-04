using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request handler that helps a webhook subscription react to a published webhook event.
    /// </summary>
    [Api("Specifies a service request handler that helps a webhook subscription react to a published webhook event.")]
    public abstract class WebhookEventHandlerBase : WebhookEvent, IPost, IReturnVoid
    {
        /// <summary>
        /// The unique identifier of the public endpoint that will receive the webhook event.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the public endpoint that will receive the webhook event.", IsRequired = true)]
        public string EndPointId { get; set; }
    }

    /// <summary>
    /// Represents a service request handler that helps a webhook subscription react to a published webhook event.
    /// </summary>
    [Api("Represents a service request handler that helps a webhook subscription react to a published webhook event.")]
    [Tag("Webhooks")]
    [Tag("Events")]
    [Tag("Handlers")]
    [Tag("Sync")]
    [Route("/sync/webhooks/events/handlers/{EndPointId}")]
    public sealed class WebhookEventHandler : WebhookEventHandlerBase
    {
    }

    /// <summary>
    /// Represents a service request handler in an asynchronous operation that helps a webhook subscription react to a published webhook event.
    /// </summary>
    [Api("Represents a service request handler in an asynchronous operation that helps a webhook subscription react to a published webhook event.")]
    [Tag("Webhooks")]
    [Tag("Events")]
    [Tag("Handlers")]
    [Tag("Async")]
    [Route("/async/webhooks/events/handlers/{EndPointId}")]
    public sealed class WebhookEventHandlerAsync : WebhookEventHandlerBase
    {
    }
}