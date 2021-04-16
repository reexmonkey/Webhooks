using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to retrieve a webhook author.
    /// </summary>
    [Api("Specifies a service request to retrieve a webhook author.")]
    public abstract class GetWebhookAuthorBase : IGet, IReturn<WebhookAuthorResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook author.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook author.", IsRequired = true)]
        public Guid Id { get; set; }

    }

    /// <summary>
    /// Represents a service request to retrieve a webhook author in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to retrieve a webhook author in a synchronous operation.")]
    [Tag("Get")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Sync")]
    [Route("/sync/webhooks/authors/{Id}", "GET")]
    public sealed class GetWebhookAuthor : GetWebhookAuthorBase
    {
    }

    /// <summary>
    /// Represents a service request to retrieve a webhook author in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to retrieve a webhook author in an asynchronous operation.")]
    [Tag("Get")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Async")]
    [Route("/async/webhooks/authors/{Id}", "GET")]
    public sealed class GetWebhookAuthorAsync : GetWebhookAuthorBase
    {
    }
}