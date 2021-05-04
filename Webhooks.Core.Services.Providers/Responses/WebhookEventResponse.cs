using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;

namespace Webhooks.Core.Services.Contracts.Responses
{
    /// <summary>
    /// Represents a service response that encapsulates a webhook event.
    /// </summary>
    [Api("Represents a service response that encapsulates a webhook event.")]
    public class WebhookEventResponse : IHasResponseStatus
    {
        /// <summary>
        /// The webhook event of the service response.
        /// </summary>
        [ApiMember(Description = "The webhook event of the service response.")]
        public WebhookEvent Event { get; set; }

        /// <summary>
        /// The response status of the service response.
        /// </summary>
        [ApiMember(Description = "The response status of the service response.")]
        public ResponseStatus ResponseStatus { get; set; }

        public static explicit operator WebhookEvent(WebhookEventResponse response)
        => response.Event;

        public static implicit operator WebhookEventResponse(WebhookEvent @event)
            => new WebhookEventResponse { Event = @event };
    }
}