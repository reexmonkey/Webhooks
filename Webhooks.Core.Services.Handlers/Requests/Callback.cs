using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;

namespace Reexmonkey.Webhooks.Core.Services.Handlers.Requests
{
    /// <summary>
    /// Specifies a service request handler that helps a webhook subscription react to a published webhook event.
    /// </summary>
    [Api("Specifies a service request handler that helps a webhook subscription react to a published webhook event.")]
    public abstract class CallbackBase : IPost, IReturnVoid
    {
        /// <summary>
        /// </summary>
        public WebhookEvent Event { get; set; }

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
    [Tag("Callbacks")]
    [Route("/webhooks/events/handlers/{EndPointId}")]
    public sealed class Callback : CallbackBase
    {
    }

    /// <summary>
    /// Represents a service request handler in an asynchronous operation that helps a webhook subscription react to a published webhook event.
    /// </summary>
    [Api("Represents a service request handler in an asynchronous operation that helps a webhook subscription react to a published webhook event.")]
    [Tag("Webhooks")]
    [Tag("Callbacks")]
    [Tag("Async")]
    [Route("/async/webhooks/events/handlers/{EndPointId}")]
    public sealed class CallbackAsync : CallbackBase
    {
    }
}