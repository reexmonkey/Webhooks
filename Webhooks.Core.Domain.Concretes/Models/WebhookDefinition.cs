using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Domain.Concretes.Models
{
    /// <summary>
    /// Represents a webhook.
    /// </summary>
    [Api("Represents a webhook.")]
    public class WebhookDefinition : IHasId<Guid>
    {
        /// <summary>
        /// The unique identifier of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook definition.", IsRequired = false)]
        [AutoId]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique identifier of the webhook definition provider.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook definition provider.", IsRequired = false)]
        [References(typeof(WebhookPublisher))]
        public Guid WebhookPublisherId { get; set; }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookDefinition"/> class.
        /// </summary>
        public WebhookDefinition()
        {
            Tags = new List<string>();
        }
    }
}