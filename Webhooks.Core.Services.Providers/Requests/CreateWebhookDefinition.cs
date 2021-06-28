using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to create a webhook definition.
    /// </summary>
    [Api("Specifies a service request to create a webhook definition.")]
    public abstract class CreateWebhookDefinitionBase : IPost, IReturn<WebhookDefinitionResponse>
    {
        /// <summary>
        /// The unique identifier of the publisher of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the publisher of the webhook definition.", IsRequired = true)]
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
        /// The password to authenticate the publisher.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the publisher.", IsRequired = true)]
        public string Password { get; set; }

        /// <summary>
        /// Keywords to mark or identify the webhook definition.
        /// </summary>
        [ApiMember(Description = "Keywords to mark or identify the webhook definition.", IsRequired = false)]
        public List<string> Tags { get; set; }
    }

    /// <summary>
    /// Represents a service request to create a webhook definition.
    /// </summary>
    [Api("Represents a service request to create a webhook definition.")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Creations")]
    [Route("/webhooks/publishers/{PublisherId}/definitions", "POST")]
    public sealed class CreateWebhookDefinition : CreateWebhookDefinitionBase
    {
    }

    /// <summary>
    /// Represents a service request to create a webhook definition.
    /// </summary>
    [Api("Represents a service request to create a webhook definition.")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Creations")]
    [Tag("Async")]
    [Route("/async/wwebhooks/publishers/{PublisherId}/definitions", "POST")]
    public sealed class CreateWebhookDefinitionAsync : CreateWebhookDefinitionBase
    {
    }
}