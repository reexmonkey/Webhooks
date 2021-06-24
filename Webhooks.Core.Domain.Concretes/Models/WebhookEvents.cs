using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Domain.Concretes.Models
{
    /// <summary>
    /// Represents a webhook event with a payload.
    /// </summary>
    [Api("Represents a webhook event with a payload")]
    public class WebhookEvent
    {
        /// <summary>
        /// The unique identifier of the webhook event.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook event.", IsRequired = false)]
        [AutoId]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique name of the webhook event.
        /// <para/>
        /// Examples: response.available
        /// </summary>
        [ApiMember(Description = "The unique name of the webhook event." +
            " Examples: response.available", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// The payload of the webhook event.
        /// </summary>
        [ApiMember(Description = "The payload of the webhook event.", IsRequired = true)]
        public Dictionary<string, object> Data { get; set; }

        /// <summary>
        /// The time at which the webhook event was created.
        /// </summary>
        [ApiMember(Description = "The time at which the webhook event was created.", IsRequired = false)]
        public DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// Keywords to mark or identify the webhook event.
        /// </summary>
        [ApiMember(Description = "Keywords to mark or identify the webhook event.", IsRequired = false)]
        public List<string> Tags { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookEvent"/> class.
        /// </summary>
        public WebhookEvent()
        {
            Data = new Dictionary<string, object>();
            Tags = new List<string>();
        }
    }
}