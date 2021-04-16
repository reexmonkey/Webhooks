using ServiceStack;
using System;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to query for webhook authors.
    /// </summary>
    [Api("Specifies a service request to query for webhook authors.")]
    public abstract class QueryWebhookAuthorsBase : IGet, IReturn<List<WebhookAuthorResponse>>
    {
        /// <summary>
        /// The unique identifier of the webhook author.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook author.", IsRequired = false)]
        public Guid Id { get; set; }

        [ApiMember(Description = "The unique name of the webhook author.", IsRequired = false)]
        public string Name { get; set; }

        /// <summary>
        /// The description of the webhook author.
        /// </summary>
        [ApiMember(Description = "The description of the webhook author.", IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// The contact address of the author.
        /// </summary>
        [ApiMember(Description = "The contact address of the author.", IsRequired = false)]
        public string Address { get; set; }

        /// <summary>
        /// The webhook definitions associated with this author.
        /// </summary>
        [ApiMember(Description = "The webhook definitions associated with this author.", IsRequired = false)]
        public List<string> WebhookNames { get; set; }

        /// <summary>
        /// The time (in UTC) when the profile of the webhook author was created.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) when the profile of the webhook author was created.", IsRequired = false)]
        public DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// The time (in UTC) when the profile of the webhook was last modified.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) when the profile of the webhook author was last modified.", IsRequired = false)]
        public DateTime LastModificationTimeUtc { get; set; }

        /// <summary>
        /// Whether the webhook event has been temporarily deleted (soft-delete).
        /// <para/> true if the event has been temporarily deleted; otherwise false.
        /// </summary>
        [ApiMember(Description = "Whether the webhook event has been temporarily deleted (soft-delete)." +
            " true if the event has been temporarily deleted; otherwise false.", IsRequired = false)]
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// Represents a service request to query for webhook authors in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to query for webhook authors in a synchronous operation.")]
    [Tag("Query")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Sync")]
    [Route("/sync/webhooks/authors", "GET")]
    public sealed class QueryWebhookAuthors : QueryWebhookAuthorsBase
    {
    }

    /// <summary>
    /// Represents a service request to query for webhook authors in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to query for webhook authors in an asynchronous operation.")]
    [Tag("Query")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Async")]
    [Route("/async/webhooks/authors", "GET")]
    public sealed class QueryWebhookAuthorsAsync : QueryWebhookAuthorsBase
    {
    }
}