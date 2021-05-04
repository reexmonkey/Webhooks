using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to restore soft-deleted webhook definitions.
    /// </summary>
    [Api("Specifies a service request to restore soft-deleted webhook definitions.")]
    public abstract class BatchRestoreWebhookDefinitionsBase : IPost, IReturn<List<WebhookDefinitionResponse>>
    {
        /// <summary>
        /// The soft-deleted webhook definitions to restore.
        /// </summary>
        [ApiMember(Description = "The soft-deleted webhook definitions to restore.", IsRequired = true)]
        public List<WebhookDefinition> Definitions { get; set; }

        /// <summary>
        /// The unique identifier of the provider, who created the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the provider, who created the webhook definition.", IsRequired = true)]
        public Guid ProviderId { get; set; }

        /// <summary>
        /// The password to authenticate the provider and providerize the restore operation.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the provider and providerize the restore operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to restore soft-deleted webhook definitions in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to restore soft-deleted webhook definitions in a synchronous operation.")]
    [Tag("Batch")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Sync")]
    [Route("/sync/webhooks/undeletions/definitions", "POST")]
    public sealed class BatchRestoreWebhookDefinitions : BatchRestoreWebhookDefinitionsBase
    {
    }

    /// <summary>
    /// Represents a service request to restore soft-deleted webhook definitions in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to restore soft-deleted webhook definitions in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Restore")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Async")]
    [Route("/async/webhooks/undeletions/definitions", "POST")]
    public sealed class BatchRestoreWebhookDefinitionsAsync : BatchRestoreWebhookDefinitionsBase
    {
    }
}