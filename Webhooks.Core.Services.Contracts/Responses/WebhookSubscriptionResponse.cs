using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;

namespace Webhooks.Core.Services.Contracts.Responses
{
    public class WebhookSubscriptionResponse : IHasResponseStatus
    {
        public WebhookSubscription Subscription { get; set; }

        public ResponseStatus ResponseStatus { get; set; }

        public static explicit operator WebhookSubscription(WebhookSubscriptionResponse response)
            => response.Subscription;

        public static implicit operator WebhookSubscriptionResponse(WebhookSubscription subscription)
            => new WebhookSubscriptionResponse { Subscription = subscription };
    }
}