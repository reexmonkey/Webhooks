using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;

namespace Webhooks.Core.Services.Contracts.Responses
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
        /// The unique name of the webhook author.
        /// </summary>
        [ApiMember(Description = "The unique name of the webhook author.", IsRequired = true)]
        public string AuthorName { get; set; }




        /// <summary>
        /// The response status of the service response.
        /// </summary>
        [ApiMember(Description = "The response status of the service response.")]
        public ResponseStatus ResponseStatus { get; set; }

        public static explicit operator WebhookDefinition(WebhookDefinitionResponse response)
        => response.Definition;

        public static implicit operator WebhookDefinitionResponse(WebhookDefinition definition)
            => new WebhookDefinitionResponse { Definition = definition };
    }
}