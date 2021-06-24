using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to delete a batch of webhook definitions associated with a publisher.
    /// </summary>
    [Api("Specifies a service request to delete a batch of webhook definitions associated with a publisher.")]
    public abstract class BatchEraseWebhookDefinitionsBase : IDelete, IReturn<List<WebhookDefinitionResponse>>
    {
        /// <summary>
        /// The name of the webhook definitions associated with the publisher.
        /// </summary>
        [ApiMember(Description = "The name of the webhook definitions associated with the publisher.", IsRequired = true)]
        public List<WebhookDefinition> Definitions { get; set; }

        /// <summary>
        /// Specifies whether the webhook definitions should be irreversibly erased or reversibly marked for deletion.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook definitions should be irreversibly erased or reversibly marked for deletion.", IsRequired = true)]
        public bool Permanently { get; set; }

        /// <summary>
        /// The unique identifier of the publisher, who created the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the publisher, who created the webhook definition.", IsRequired = true)]
        public Guid PublisherId { get; set; }

        /// <summary>
        /// The password to publisherize the publish operation.
        /// </summary>
        [ApiMember(Description = "The password to publisherize the publish operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to delete a batch of webhook definitions in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a batch of webhook definitions in a synchronous operation.")]
    [Tag("Batch")]
    [Tag("Erase")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Route("/webhooks/definitions", "DELETE")]
    public sealed class BatchEraseWebhookDefinitions : BatchEraseWebhookDefinitionsBase
    {
    }

    /// <summary>
    /// Represents a service request to delete a batch of webhook definitions in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a batch of webhook definitions in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Erase")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Async")]
    [Route("/async/webhooks/definitions", "DELETE")]
    public sealed class BatchEraseWebhookDefinitionsAsync : BatchEraseWebhookDefinitionsBase
    {
    }
}