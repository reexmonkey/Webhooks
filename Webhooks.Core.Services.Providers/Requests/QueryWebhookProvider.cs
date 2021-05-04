using ServiceStack;
using System;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to query for webhook providers.
    /// </summary>
    [Api("Specifies a service request to query for webhook providers.")]
    public abstract class QueryWebhookProvidersBase : IGet, IReturn<List<WebhookProviderResponse>>
    {
        /// <summary>
        /// The unique identifier of the webhook provider.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook provider.", IsRequired = false)]
        public Guid Id { get; set; }

        [ApiMember(Description = "The unique name of the webhook provider.", IsRequired = false)]
        public string Name { get; set; }

        /// <summary>
        /// The description of the webhook provider.
        /// </summary>
        [ApiMember(Description = "The description of the webhook provider.", IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// The contact address of the provider.
        /// </summary>
        [ApiMember(Description = "The contact address of the provider.", IsRequired = false)]
        public string Address { get; set; }

        /// <summary>
        /// The webhook definitions associated with this provider.
        /// </summary>
        [ApiMember(Description = "The webhook definitions associated with this provider.", IsRequired = false)]
        public List<string> WebhookNames { get; set; }

        /// <summary>
        /// The time (in UTC) when the profile of the webhook provider was created.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) when the profile of the webhook provider was created.", IsRequired = false)]
        public DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// The time (in UTC) when the profile of the webhook was last modified.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) when the profile of the webhook provider was last modified.", IsRequired = false)]
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
    /// Represents a service request to query for webhook providers in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to query for webhook providers in a synchronous operation.")]
    [Tag("Query")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Sync")]
    [Route("/sync/webhooks/providers", "GET")]
    public sealed class QueryWebhookProviders : QueryWebhookProvidersBase
    {
    }

    /// <summary>
    /// Represents a service request to query for webhook providers in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to query for webhook providers in an asynchronous operation.")]
    [Tag("Query")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Async")]
    [Route("/async/webhooks/providers", "GET")]
    public sealed class QueryWebhookProvidersAsync : QueryWebhookProvidersBase
    {
    }
}