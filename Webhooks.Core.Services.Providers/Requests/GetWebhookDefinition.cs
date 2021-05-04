using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
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
    [Tag("Get")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Sync")]
    [Route("/sync/webhooks/definitions/{Id}", "GET")]
    public sealed class GetWebhookDefinition : GetWebhookDefinitionBase
    {
    }

    /// <summary>
    /// Represents a service request to retrieve a webhook in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to retrieve a webhook in an asynchronous operation.")]
    [Tag("Get")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Async")]
    [Route("/async/webhooks/definitions/{Id}", "GET")]
    public sealed class GetWebhookDefinitionAsync : GetWebhookDefinitionBase
    {
    }
}