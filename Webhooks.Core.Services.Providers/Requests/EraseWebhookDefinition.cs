using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to delete a webhook definition.
    /// </summary>
    [Api("Specifies a service request to delete a webhook definition.")]
    public abstract class EraseWebhookDefinitionBase : IDelete, IReturn<WebhookDefinitionResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook definition.", IsRequired = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique identifier of the publisher, who created the webhook definition.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the publisher, who created the webhook definition.", IsRequired = true)]
        public Guid PublisherId { get; set; }

        /// <summary>
        /// The password to authenticate the publisher and publisherize the publish operation.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the publisher and publisherize the publish operation.", IsRequired = true)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a service request to delete a webhook definition in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook definition in a synchronous operation.")]
    [Tag("Erase")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Route("/webhooks/definitions/{Id}", "DELETE")]
    public sealed class EraseWebhookDefinition : EraseWebhookDefinitionBase
    {
    }

    /// <summary>
    /// Represents a service request to delete a webhook definition in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to delete a webhook definition in an asynchronous operation.")]
    [Tag("Erase")]
    [Tag("Webhooks")]
    [Tag("Definitions")]
    [Tag("Async")]
    [Route("/async/webhooks/definitions/{Id}", "DELETE")]
    public sealed class EraseWebhookDefinitionAsync : EraseWebhookDefinitionBase
    {
    }
}