using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to delete a batch of webhook publishers.
    /// </summary>
    [Api("Specifies a service request to delete a batch of webhook publishers.")]
    public abstract class BatchDeleteWebhookPublishersBase : IDelete, IReturn<List<WebhookPublisherResponse>>
    {
        /// <summary>
        /// The name of the webhook definitions associated with the publisher.
        /// </summary>
        [ApiMember(Description = "The name of the webhook definitions associated with the publisher.", IsRequired = true)]
        public List<WebhookPublisher> Publishers { get; set; }

        /// <summary>
        /// Specifies whether the webhook publishers should be irreversibly erased or reversibly marked for deletion.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook publishers should be irreversibly erased or reversibly marked for deletion.", IsRequired = true)]
        public bool Permanently { get; set; }
    }

    /// <summary>
    /// Represents a service request to delete a batch of webhook publishers in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a batch of webhook publishers in a synchronous operation.")]
    [Tag("Batch")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Route("/webhooks/publishers", "DELETE")]
    public sealed class BatchDeleteWebhookPublishers : BatchDeleteWebhookPublishersBase
    {
    }

    /// <summary>
    /// Represents a service request to delete a batch of webhook publishers in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a batch of webhook publishers in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Tag("Async")]
    [Route("/async/webhooks/publishers", "DELETE")]
    public sealed class BatchDeleteWebhookPublishersAsync : BatchDeleteWebhookPublishersBase
    {
    }
}