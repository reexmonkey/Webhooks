using System;

namespace Reexmonkey.Webhooks.Core.Domain.Concretes.Models
{
    /// <summary>
    /// Specifies a webhook event to store webhook information
    /// </summary>
    public abstract class WebhookEventBase
    {
        /// <summary>
        /// The unique identifier of the webhook event.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The unique name of the webhook associated to this event.
        /// </summary>
        public string WebhookId { get; set; }

        /// <summary>
        /// The creation time of the webhook event.
        /// </summary>
        public DateTime CreationTimeUtc { get; set; }
    }


    /// <summary>
    /// Specifies a webhook event to store webhook information of type <typeparamref name="TData"/>.
    /// </summary>
    /// <typeparam name="TData">The type of information to store in the webhook event.</typeparam>
    public abstract class WebhookEvent<TData> : WebhookEventBase
        where TData : class
    {
        /// <summary>
        /// The information that the webhook event stores.
        /// </summary>
        public TData Data { get; set; }
    }

    public sealed class TextualWebEvent : WebhookEvent<string> { }

    public sealed class BinaryWebEvent : WebhookEvent<byte[]> { }
}