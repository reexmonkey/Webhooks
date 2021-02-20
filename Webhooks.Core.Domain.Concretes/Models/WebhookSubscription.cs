using ServiceStack;
using ServiceStack.Model;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Domain.Concretes.Models
{
    /// <summary>
    /// Represents a webhook subscription
    /// </summary>
    [Api(" Represents a webhook subscription")]
    public class WebhookSubscription: IHasId<Guid>
    {
        /// <summary>
        /// The unique identifier of the webhook subscription.
        /// <para/> To be filled by the internal system.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook subscription." +
            " To be filled by the internal system.", IsRequired = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique identifier of the webhook subscriber.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook subscriber.", IsRequired = true)]        
        public string SubscriberId { get; set; }
        
        /// <summary>
        /// The display name of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The display name of the webhook subscription.", IsRequired = false)]         
        public string DisplayName { get; set; }

        /// <summary>
        /// The name of the webhook definitions associated with the subscription.
        /// </summary>
        [ApiMember(Description = "The name of the webhook definitions associated with the subscription.", IsRequired = true)]          
        public List<string> Webhooks { get; set; }

        /// <summary>
        /// The endpoint URL of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The endpoint URL of the webhook subscription.", IsRequired = true)]          
        public Uri Endpoint { get; set; }

        /// <summary>
        /// The secret to sign or verify the payload of a webhook.
        /// </summary>
        [ApiMember(Description = "The secret to sign or verify the payload of a webhook.", IsRequired = true)]               
        public string Secret { get; set; }

        /// <summary>
        /// The creation time of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The creation time of the webhook subscription.", IsRequired = true)]         
        public DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// The last modification time of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The last modification time of the webhook subscription.", IsRequired = true)]         
        public DateTime LastModificationTimeUtc { get; set; }

        /// <summary>
        /// Additional headers to send with the webhook.
        /// </summary>
        [ApiMember(Description = "Additional headers to send with the webhook.", IsRequired = false)]
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Specifies whether the webhook subscription is active or not.
        /// <para/> True if the subscription is active; otherwise false.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook subscription is active or not." +
            " True if the subscription is active; otherwise false.", IsRequired = false)]
        public bool IsActive { get; set; }

        public WebhookSubscription()
        {
            Webhooks = new List<string>();
            Headers = new Dictionary<string, string>
            {
                {"Content-Type", "application/json" }
            };
        }

    }
}
