using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;

namespace Webhooks.Core.Services.Contracts.Responses
{
    /// <summary>
    /// Represents a service response that encapsulates a webhook author.
    /// </summary>
    [Api("Represents a service response that encapsulates a webhook author.")]
    public class WebhookAuthorResponse : IHasResponseStatus
    {
        /// <summary>
        /// The author of the webhook event.
        /// </summary>
        [ApiMember(Description = "The author of the webhook event.")]
        public WebhookAuthor Author { get; set; }

        /// <summary>
        /// The response status of the service response.
        /// </summary>
        [ApiMember(Description = "The response status of the service response.")]
        public ResponseStatus ResponseStatus { get; set; }

        public static explicit operator WebhookAuthor(WebhookAuthorResponse response)
            => response.Author;

        public static implicit operator WebhookAuthorResponse(WebhookAuthor author)
            => new WebhookAuthorResponse { Author = author };
    }
}