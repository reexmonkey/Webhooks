using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to delete a batch of webhook subscriptions.
    /// </summary>
    [Api("Specifies a service request to delete a batch of webhook subscriptions.")]
    public abstract class BatchDeleteWebhookDefinitionsBase : IDelete, IReturn<List<WebhookDefinitionResponse>>
    {
        /// <summary>
        /// The name of the webhook definitions associated with the subscription.
        /// </summary>
        [ApiMember(Description = "The name of the webhook definitions associated with the subscription.", IsRequired = true)]
        public List<WebhookDefinition> Definitions { get; set; }

        /// <summary>
        /// Specifies whether the webhook subscriptions should be irreversibly erased or reversibly marked for deletion.
        /// </summary>
        [ApiMember(Description = "Specifies whether the webhook subscriptions should be irreversibly erased or reversibly marked for deletion.", IsRequired = true)]
        public bool Permanently { get; set; }

        /// <summary>
        /// The unique identifier of the provider, who created the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the provider, who created the webhook definition.", IsRequired = true)]
        public Guid ProviderId { get; set; }

        /// <summary>
        /// The password to providerize the publish operation.
        /// </summary>
        [ApiMember(Description = "The password to providerize the publish operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to delete a batch of webhook subscriptions in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a batch of webhook subscriptions in a synchronous operation.")]
    [Tag("Batch")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Sync")]
    [Route("/sync/webhooks/subscriptions", "DELETE")]
    public sealed class BatchDeleteWebhookDefinitions : BatchDeleteWebhookDefinitionsBase
    {
    }

    /// <summary>
    /// Represents a service request to delete a batch of webhook subscriptions in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a batch of webhook subscriptions in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Delete")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Async")]
    [Route("/async/webhooks/subscriptions", "DELETE")]
    public sealed class BatchDeleteWebhookDefinitionsAsync : BatchDeleteWebhookDefinitionsBase
    {
    }
}