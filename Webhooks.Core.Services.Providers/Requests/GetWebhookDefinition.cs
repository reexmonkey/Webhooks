using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to retrieve a webhook definition.
    /// </summary>
    [Api("Specifies a service request to retrieve a webhook definition.")]
    public abstract class GetWebhookDefinitionBase : IGet, IReturn<WebhookDefinitionResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook definition.", IsRequired = true)]
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents a service request to retrieve a webhook in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to retrieve a webhook in a synchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Retrievals")]
    [Route("/webhooks/definitions/{Id}", "GET")]
    public sealed class GetWebhookDefinition : GetWebhookDefinitionBase
    {
    }

    /// <summary>
    /// Represents a service request to retrieve a webhook in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to retrieve a webhook in an asynchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Retrievals")]
    [Tag("Async")]
    [Route("/async/webhooks/definitions/{Id}", "GET")]
    public sealed class GetWebhookDefinitionAsync : GetWebhookDefinitionBase
    {
    }
}