using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to query for webhook publishers.
    /// </summary>
    [Api("Specifies a service request to query for webhook publishers.")]
    public abstract class QueryWebhookPublishersBase : IGet, IReturn<List<WebhookPublisherResponse>>
    {
        /// <summary>
        /// The unique identifier of the webhook publisher.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook publisher.", IsRequired = false)]
        public Guid Id { get; set; }

        [ApiMember(Description = "The unique name of the webhook publisher.", IsRequired = false)]
        public string Name { get; set; }

        /// <summary>
        /// The description of the webhook publisher.
        /// </summary>
        [ApiMember(Description = "The description of the webhook publisher.", IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// The contact address of the publisher.
        /// </summary>
        [ApiMember(Description = "The contact address of the publisher.", IsRequired = false)]
        public string Address { get; set; }

        /// <summary>
        /// The webhook definitions associated with this publisher.
        /// </summary>
        [ApiMember(Description = "The webhook definitions associated with this publisher.", IsRequired = false)]
        public List<string> WebhookNames { get; set; }

        /// <summary>
        /// The time (in UTC) when the profile of the webhook publisher was created.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) when the profile of the webhook publisher was created.", IsRequired = false)]
        public DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// The time (in UTC) when the profile of the webhook was last modified.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) when the profile of the webhook publisher was last modified.", IsRequired = false)]
        public DateTime LastModificationTimeUtc { get; set; }

        /// <summary>
        /// Whether the webhook event has been temporarily deleted (soft-delete).
        /// <para/>
        /// true if the event has been temporarily deleted; otherwise false.
        /// </summary>
        [ApiMember(Description = "Whether the webhook event has been temporarily deleted (soft-delete)." +
            " true if the event has been temporarily deleted; otherwise false.", IsRequired = false)]
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// Represents a service request to query for webhook publishers in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to query for webhook publishers in a synchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Tag("Queries")]
    [Route("/webhooks/publishers", "GET")]
    public sealed class QueryWebhookPublishers : QueryWebhookPublishersBase
    {
    }

    /// <summary>
    /// Represents a service request to query for webhook publishers in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to query for webhook publishers in an asynchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Tag("Queries")]
    [Tag("Async")]
    [Route("/async/webhooks/publishers", "GET")]
    public sealed class QueryWebhookPublishersAsync : QueryWebhookPublishersBase
    {
    }
}