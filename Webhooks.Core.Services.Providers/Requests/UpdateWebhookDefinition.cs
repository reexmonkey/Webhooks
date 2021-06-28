using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
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
        /// The unique identifier of the publisher, who created the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the publisher, who created the webhook definition.", IsRequired = true)]
        public Guid PublisherId { get; set; }

        /// <summary>
        /// The unique name of the webhook definition.
        /// <para/>
        /// Examples: response.available
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
        /// The password to authenticate the publisher and authorize the publish operation.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the publisher and authorize the publish operation.", IsRequired = true)]
        public string Password { get; set; }

        /// <summary>
        /// Specifies whether the webhook definition has been published or not.
        /// <para/>
        /// True if the definition was published; otherwise false.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook definition has been published or not." +
            " True if the definition was published; otherwise false.", IsRequired = false)]
        public bool IsPublished { get; set; }

        /// <summary>
        /// Specifies whether the webhook definition has been temporarily deleted (soft-delete).
        /// <para/>
        /// true if the definition has been temporarily deleted; otherwise false.
        /// </summary>
        [ApiMember(Description = "Whether the webhook definition has been temporarily deleted (soft-delete)." +
            " true if the definition has been temporarily deleted; otherwise false.", IsRequired = false)]
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// Represents a service request to to update a webhook definition in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to to update a webhook definition in a synchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Updates")]
    [Route("/webhooks/publishers/{PublisherId}/definitions/{Id}", "PUT")]
    public sealed class UpdateWebhookDefinition : UpdateWebhookDefinitionBase
    {
    }

    /// <summary>
    /// Represents a service request to to update a webhook definition in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to to update a webhook definition in an asynchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Updates")]
    [Tag("Async")]
    [Route("/async/webhooks/publishers/{PublisherId}/definitions/{Id}", "PUT")]
    public sealed class UpdateWebhookDefinitionAsync : UpdateWebhookDefinitionBase
    {
    }
}