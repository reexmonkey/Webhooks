using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to retrieve a webhook provider.
    /// </summary>
    [Api("Specifies a service request to retrieve a webhook provider.")]
    public abstract class GetWebhookProviderBase : IGet, IReturn<WebhookProviderResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook provider.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook provider.", IsRequired = true)]
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents a service request to retrieve a webhook provider in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to retrieve a webhook provider in a synchronous operation.")]
    [Tag("Get")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Sync")]
    [Route("/sync/webhooks/providers/{Id}", "GET")]
    public sealed class GetWebhookProvider : GetWebhookProviderBase
    {
    }

    /// <summary>
    /// Represents a service request to retrieve a webhook provider in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to retrieve a webhook provider in an asynchronous operation.")]
    [Tag("Get")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Async")]
    [Route("/async/webhooks/providers/{Id}", "GET")]
    public sealed class GetWebhookProviderAsync : GetWebhookProviderBase
    {
    }
}