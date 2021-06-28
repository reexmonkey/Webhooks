using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Responses
{
    /// <summary>
    /// Represents a service response that encapsulates a webhook definition.
    /// </summary>
    [Api("Represents a service response that encapsulates a webhook definition.")]
    public class WebhookDefinitionResponse : IHasResponseStatus
    {
        /// <summary>
        /// The webhook definition of the service response.
        /// </summary>
        [ApiMember(Description = "The webhook definition of the service response.")]
        public WebhookDefinition Definition { get; set; }

        /// <summary>
        /// The response status of the service response.
        /// </summary>
        [ApiMember(Description = "The response status of the service response.")]
        public ResponseStatus ResponseStatus { get; set; }

        public static explicit operator WebhookDefinition(WebhookDefinitionResponse response)
        => response?.Definition;

        public static implicit operator WebhookDefinitionResponse(WebhookDefinition definition)
            => definition != null ? new WebhookDefinitionResponse { Definition = definition } : default;
    }
}