using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
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
        /// The name of the publisher of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The name of the publisher of the webhook definition.", IsRequired = false)]
        public string PublisherName { get; set; }

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
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Queries")]
    [Route("/webhooks/definitions", "GET")]
    public sealed class QueryWebhookDefinitions : QueryWebhookDefinitionsBase
    {
    }

    /// <summary>
    /// Represents a service request to query for webhook definitions in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to query for webhook definitions in an asynchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Queries")]
    [Tag("Async")]
    [Route("/async/webhooks/definitions", "GET")]
    public sealed class QueryWebhookDefinitionsAsync : QueryWebhookDefinitionsBase
    {
    }
}