using ServiceStack.Model;
using System;

namespace Reexmonkey.Webhooks.Core.Domain.Concretes.Models
{
    public abstract class WebhookPayloadBase : IHasId<Guid>
    {
        public Guid Id { get; set; }

        public string Event { get; set; }

        public DateTime CreationTimeUtc { get; set; }
    }

    public class WebhookPayloads<TData>
        where TData: class
    {
        public TData Data { get; set; }

    }

    public class TextualWebhookPayload: WebhookPayloads<string> { }

    public class BinaryWebhookPayload: WebhookPayloads<byte[]> { }
}