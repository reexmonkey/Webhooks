using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to delete a webhook definition.
    /// </summary>
    [Api("Specifies a service request to delete a webhook definition.")]
    public abstract class DeleteWebhookDefinitionBase : IDelete, IReturn<WebhookDefinitionResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook definition.", IsRequired = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique identifier of the author, who created the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the author, who created the webhook definition.", IsRequired = true)]
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Specifies whether the webhook definition should be irreversibly erased (hard-delete) or reversibly marked (soft-delete) for deletion.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook definition should be irreversibly erased (hard-delete) or reversibly marked (soft-delete) for deletion.", IsRequired = true)]
        public bool Permanently { get; set; }

        /// <summary>
        /// The password to authenticate the author and authorize the publish operation.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the author and authorize the publish operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to delete a webhook definition in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook definition in a synchronous operation.")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Sync")]
    [Route("/sync/webhooks/definitions/{Id}", "DELETE")]
    public sealed class DeleteWebhookDefinition : DeleteWebhookDefinitionBase
    {
    }

    /// <summary>
    /// Represents a service request to delete a webhook definition in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook definition in an asynchronous operation.")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Async")]
    [Route("/async/webhooks/definitions/{Id}", "DELETE")]
    public sealed class DeleteWebhookDefinitionAsync : DeleteWebhookDefinitionBase
    {
    }
}