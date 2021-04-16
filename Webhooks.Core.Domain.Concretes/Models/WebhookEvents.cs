using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

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
        /// <para/> Examples: response.available
        /// </summary>
        [ApiMember(Description = "The unique name of the webhook event." +
            " Examples: response.available", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// The payload of the webhook event.
        /// </summary>
        [ApiMember(Description = "The payload of the webhook event.", IsRequired = true)]
        public object Data { get; set; }

        /// <summary>
        /// The time at which the webhook event was created.
        /// </summary>
        [ApiMember(Description = "The time at which the webhook event was created.", IsRequired = false)]
        public DateTime CreationTimeUtc { get; set; }
    }
}