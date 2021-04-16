using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to restore a soft-deleted webhook definition.
    /// </summary>
    [Api("Specifies a service request to restore a soft-deleted webhook definition.")]
    public abstract class RestoreWebhookDefinitionBase : IPost, IReturn<WebhookDefinitionResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook definition.", IsRequired = true)]
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook definition in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to restore a soft-deleted webhook definition in a synchronous operation.")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Sync")]
    [Route("/sync/webhooks/undeletions/definitions/{Id}", "POST")]
    public sealed class RestoreWebhookDefinition : RestoreWebhookDefinitionBase
    {
    }

    /// <summary>
    /// Represents a service request to restore a soft-deleted webhook definition in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to restore a soft-deleted webhook definition in an asynchronous operation.")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Async")]
    [Route("/async/webhooks/undeletions/definitions/{Id}/undeletions", "POST")]
    public sealed class RestoreWebhookDefinitionAsync : RestoreWebhookDefinitionBase
    {
    }
}