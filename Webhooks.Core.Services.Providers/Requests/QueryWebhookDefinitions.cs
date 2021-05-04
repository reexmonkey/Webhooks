using ServiceStack;
using System;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to query for webhook definitions.
    /// </summary>
    [Api("Specifies a service request to query for webhook definitions.")]
    public abstract class QueryWebhookDefinitionsBase : IGet, IReturn<List<WebhookDefinitionResponse>>
    {
        /// <summary>
        /// The unique identifier of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook definition.", IsRequired = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the provider of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The name of the provider of the webhook definition.", IsRequired = false)]
        public string ProviderName { get; set; }

        /// <summary>
        /// The unique name of the webhook definition.
        /// <para/>
        /// Examples: response.available
        /// </summary>
        [ApiMember(Description = "The unique name of the webhook definition." +
            " Examples: response.available", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// The display name of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The display name of the webhook definition.", IsRequired = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// The description of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The description of the webhook definition.", IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// Keywords to mark or identify the webhook definition.
        /// </summary>
        [ApiMember(Description = "Keywords to mark or identify the webhook definition.", IsRequired = false)]
        public List<string> Tags { get; set; }
    }

    /// <summary>
    /// Represents a service request to query for webhook definitions in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to query for webhook definitions in a synchronous operation.")]
    [Tag("Query")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Sync")]
    [Route("/sync/webhook definitions", "GET")]
    public sealed class QueryWebhookDefinitions : QueryWebhookDefinitionsBase
    {
    }

    /// <summary>
    /// Represents a service request to query for webhook definitions in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to query for webhook definitions in an asynchronous operation.")]
    [Tag("Query")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Async")]
    [Route("/async/webhook definitions", "GET")]
    public sealed class QueryWebhookDefinitionsAsync : QueryWebhookDefinitionsBase
    {
    }
}