using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;

namespace Webhooks.Core.Services.Contracts.Responses
{
    /// <summary>
    /// Represents a service response that encapsulates a webhook subscription.
    /// </summary>
    [Api("Represents a service response that encapsulates a webhook subscription.")]
    public class WebhookSubscriptionResponse : IHasResponseStatus
    {
        /// <summary>
        /// The subscription of the webhook event.
        /// </summary>
        [ApiMember(Description = "The subscription of the webhook event.")]
        public WebhookSubscription Subscription { get; set; }

        /// <summary>
        /// The response status of the service response.
        /// </summary>
        [ApiMember(Description = "The response status of the service response.")]
        public ResponseStatus ResponseStatus { get; set; }

        public static explicit operator WebhookSubscription(WebhookSubscriptionResponse response)
            => response.Subscription;

        public static implicit operator WebhookSubscriptionResponse(WebhookSubscription subscription)
            => new WebhookSubscriptionResponse { Subscription = subscription };
    }
}