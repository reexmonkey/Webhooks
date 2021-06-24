using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Responses
{
    /// <summary>
    /// Represents a service response that encapsulates a webhook publisher.
    /// </summary>
    [Api("Represents a service response that encapsulates a webhook publisher.")]
    public class WebhookPublisherResponse : IHasResponseStatus
    {
        /// <summary>
        /// The publisher of the webhook event.
        /// </summary>
        [ApiMember(Description = "The publisher of the webhook event.")]
        public WebhookPublisher Publisher { get; set; }

        /// <summary>
        /// The response status of the service response.
        /// </summary>
        [ApiMember(Description = "The response status of the service response.")]
        public ResponseStatus ResponseStatus { get; set; }

        public static explicit operator WebhookPublisher(WebhookPublisherResponse response)
            => response.Publisher;

        public static implicit operator WebhookPublisherResponse(WebhookPublisher publisher)
            => new WebhookPublisherResponse { Publisher = publisher };
    }
}