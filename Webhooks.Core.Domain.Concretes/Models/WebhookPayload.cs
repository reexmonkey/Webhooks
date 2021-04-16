using ServiceStack;
using ServiceStack.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reexmonkey.Webhooks.Core.Domain.Concretes.Models
{
    public class WebhookPayload: IHasId<string>
    {

        /// <summary>
        /// The unique name of the webhook definition.
        /// <para/> Examples: response.available
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook definition." +
            " Examples: response.available", IsRequired = true)]
        public string Id { get; set; }



        /// <summary>
        /// The display name of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The display name of the webhook definition.", IsRequired = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// The description of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The display name of the webhook definition.", IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// The time at which the webhook event was created.
        /// </summary>
        [ApiMember(Description = "The time at which the webhook event was created.", IsRequired = false)]
        public DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// The time at which the webhook event was deleted.
        /// </summary>
        [ApiMember(Description = "The time at which the webhook event was created.", IsRequired = false)]
        public DateTime DeletionTimeUtc { get; set; }

        /// <summary>
        /// Whether the webhook definition has been temporarily deleted (soft-delete).
        /// <para/> true if the event has been temporarily deleted; otherwise false.
        /// </summary>
        [ApiMember(Description = "Whether the webhook definition has been temporarily deleted (soft-delete)." +
            " true if the event has been temporarily deleted; otherwise false.", IsRequired = false)]        
        public bool IsDeleted { get; set; }
    }
}
