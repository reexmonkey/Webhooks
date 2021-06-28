using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// SSpecifies a service request to withdraw a batch of published webhook definitions associated with a publisher.
    /// </summary>
    [Api("Specifies a service request to withdraw a batch of published webhook definitions associated with a publisher.")]
    public abstract class BatchWithdrawWebhookDefinitionsBase : IPost, IReturn<List<WebhookDefinitionResponse>>
    {
        /// <summary>
        /// The name of the webhook definitions associated with the publisher.
        /// </summary>
        [ApiMember(Description = "The name of the webhook definitions associated with the publisher.", IsRequired = true)]
        public List<WebhookDefinition> Definitions { get; set; }

        /// <summary>
        /// The unique identifier of the publisher, who created the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the publisher, who created the webhook definition.", IsRequired = true)]
        public Guid PublisherId { get; set; }

        /// <summary>
        /// The password to authorize the withdrawal operation.
        /// </summary>
        [ApiMember(Description = "The password to authorize the withdrawal operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to withdraw a batch of webhook definitions in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to withdraw a batch of webhook definitions in a synchronous operation.")]
    [Tag("Batch")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Withdrawals")]
    [Route("/webhooks/definitions/withdrawals", "POST")]
    public sealed class BatchWithdrawWebhookDefinitions : BatchWithdrawWebhookDefinitionsBase
    {
    }

    /// <summary>
    /// Represents a service request to withdraw a batch of webhook definitions in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to withdraw a batch of webhook definitions in an asynchronous operation.")]
    [Tag("Batch")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Withdrawals")]
    [Tag("Async")]
    [Route("/async/webhooks/definitions/withdrawals", "POST")]
    public sealed class BatchWithdrawWebhookDefinitionsAsync : BatchWithdrawWebhookDefinitionsBase
    {
    }
}