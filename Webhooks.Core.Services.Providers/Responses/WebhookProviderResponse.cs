using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Responses
{
    /// <summary>
    /// Represents a service response that encapsulates a webhook provider.
    /// </summary>
    [Api("Represents a service response that encapsulates a webhook provider.")]
    public class WebhookProviderResponse : IHasResponseStatus
    {
        /// <summary>
        /// The provider of the webhook event.
        /// </summary>
        [ApiMember(Description = "The provider of the webhook event.")]
        public WebhookProvider Provider { get; set; }

        /// <summary>
        /// The response status of the service response.
        /// </summary>
        [ApiMember(Description = "The response status of the service response.")]
        public ResponseStatus ResponseStatus { get; set; }

        public static explicit operator WebhookProvider(WebhookProviderResponse response)
            => response.Provider;

        public static implicit operator WebhookProviderResponse(WebhookProvider provider)
            => new WebhookProviderResponse { Provider = provider };
    }
}