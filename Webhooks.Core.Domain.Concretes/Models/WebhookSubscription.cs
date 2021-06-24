using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Domain.Concretes.Models
{
    /// <summary>
    /// Represents a webhook subscription
    /// </summary>
    [Api(" Represents a webhook subscription")]
    public class WebhookSubscription : IHasId<Guid>
    {
        /// <summary>
        /// The unique identifier of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook subscription.", IsRequired = false)]
        [AutoId]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique name of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The unique name of the webhook subscription.", IsRequired = true)]
        public string Name { get; set; }

        public string UserId { get; set; }

        /// <summary>
        /// The subscribed webhook.
        /// </summary>
        [ApiMember(Description = "The names of subscribed webhooks.", IsRequired = true)]
        [Reference]
        public List<string> Webhooks { get; set; }

        /// <summary>
        /// The endpoint URI of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The endpoint URI of the webhook subscription.", IsRequired = true)]
        public Uri EndPointUri { get; set; }

        /// <summary>
        /// The secret to sign, verify, encrypt or decrypt the payload of a webhook.
        /// </summary>
        [ApiMember(Description = "The secret to sign, verify, encrypt or decrypt the payload of a webhook.", IsRequired = true)]
        public string Secret { get; set; }

        /// <summary>
        /// The time (in UTC) at which the webhook subscription was created.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) at which the webhook subscription was created.", IsRequired = true)]
        public DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// The time (in UTC) at which the the webhook subscription was last modified.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) of the webhook subscription was last modified.", IsRequired = true)]
        public DateTime LastModificationTimeUtc { get; set; }

        /// <summary>
        /// Additional headers to send with the webhook.
        /// </summary>
        [ApiMember(Description = "Additional headers to send with the webhook.", IsRequired = false)]
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Specifies whether the webhook subscription is active or not.
        /// <para/>
        /// True if the subscription is active; otherwise false.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook subscription is active or not." +
            " True if the subscription is active; otherwise false.", IsRequired = false)]
        public bool IsActive { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookSubscription"/> class.
        /// </summary>
        public WebhookSubscription()
        {
            Headers = new Dictionary<string, string>();
            Webhooks = new List<string>();
        }
    }
}