using ServiceStack;
using System;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to update a webhook definition.
    /// </summary>
    [Api("Specifies a service request to update a webhook definition.")]
    public abstract class UpdateWebhookDefinitionBase : IPut, IReturn<WebhookDefinitionResponse>
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
        /// The unique name of the webhook definition.
        /// <para/> Examples: response.available
        /// </summary>
        [ApiMember(Description = "The unique name of the webhook definition." +
            " Examples: response.available", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// The display name of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The display name of the webhook definition.", IsRequired = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// The description of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The description of the webhook definition.", IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// Keywords to mark or identify the webhook definition.
        /// </summary>
        [ApiMember(Description = "Keywords to mark or identify the webhook definition.", IsRequired = false)]
        public List<string> Tags { get; set; }

        /// <summary>
        /// The password to authenticate the author and authorize the publish operation.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the author and authorize the publish operation.", IsRequired = true)]
        public string Password { get; set; }

        /// <summary>
        /// Specifies whether the webhook definition has been published or not.
        /// <para/> True if the definition was published; otherwise false.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook definition has been published or not." +
            " True if the definition was published; otherwise false.", IsRequired = false)]
        public bool IsPublished { get; set; }

        /// <summary>
        /// Specifies whether the webhook definition has been temporarily deleted (soft-delete).
        /// <para/> true if the definition has been temporarily deleted; otherwise false.
        /// </summary>
        [ApiMember(Description = "Whether the webhook definition has been temporarily deleted (soft-delete)." +
            " true if the definition has been temporarily deleted; otherwise false.", IsRequired = false)]
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// Represents a service request to to update a webhook definition in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to to update a webhook definition in a synchronous operation.")]
    [Tag("Update")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Sync")]
    [Route("/sync/webhooks/definitions/{Id}", "PUT")]
    public sealed class UpdateWebhookDefinition : UpdateWebhookDefinitionBase
    {
    }

    /// <summary>
    /// Represents a service request to to update a webhook definition in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to to update a webhook definition in an asynchronous operation.")]
    [Tag("Update")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Async")]
    [Route("/async/webhooks/definitions/{Id}", "PUT")]
    public sealed class UpdateWebhookDefinitionAsync : UpdateWebhookDefinitionBase
    {
    }
}