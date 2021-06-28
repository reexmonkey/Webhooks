using ServiceStack;
using System;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Subscriptions.Requests
{
    /// <summary>
    /// Specifies a service request to update a webhook subscription.
    /// </summary>
    [Api("Specifies a service request to update a webhook subscription.")]
    public abstract class UpdateWebhookSubscriptionBase : IPut, IReturn<WebhookSubscriptionResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook subscription.", IsRequired = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique identifier of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook subscription.", IsRequired = false)]
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The display name of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The display name of the webhook subscription.", IsRequired = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// The name of the webhook definitions associated with the subscription.
        /// </summary>
        [ApiMember(Description = "The name of the webhook definitions associated with the subscription.", IsRequired = false)]
        public List<string> Webhooks { get; set; }

        /// <summary>
        /// The endpoint URL of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The endpoint URL of the webhook subscription.", IsRequired = false)]
        public Uri Endpoint { get; set; }

        /// <summary>
        /// The current secret to sign, verify, encrypt or decrypt the payload of a webhook.
        /// </summary>
        [ApiMember(Description = "The current secret to sign, verify, encrypt or decrypt the payload of a webhook.", IsRequired = true)]
        public string CurrentSecret { get; set; }

        /// <summary>
        /// The new secret to sign, verify, encrypt or decrypt the payload of a webhook.
        /// </summary>
        [ApiMember(Description = "The new secret to sign, verify, encrypt or decrypt the payload of a webhook.", IsRequired = false)]
        public string NewSecret { get; set; }

        /// <summary>
        /// Additional headers to send with the webhook.
        /// </summary>
        [ApiMember(Description = "Additional headers to send with the webhook.", IsRequired = false)]
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// The public key to encrypt the secret of the webhook subscription.
        /// <para/> The public key can either be in the PEM (base64) or the XML (W3C or .NET) format.
        /// <para/> As owner of the public and private keys, please use the private key and the RSA algorithm with the OAEP-SHA256 padding to decrypt the encrypted secret.
        /// </summary>
        [ApiMember(Description = "The public key to encrypt the secret of the webhook subscription." +
            " The public key can either be in the PEM (base64) or the XML (W3C or .NET) format." +
            " As owner of the public and private key, please use the private key and the RSA algorithm with the OAEP-SHA256 padding to decrypt the encrypted secret.", IsRequired = true)]
        public string PublicKey { get; set; }
    }

    /// <summary>
    /// Represents a service request to to update a webhook subscription in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to to update a webhook subscription in a synchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Updates")]
    [Route("/webhooks/subscriptions/{Id}", "PUT")]
    public sealed class UpdateWebhookSubscription : UpdateWebhookSubscriptionBase
    {
    }

    /// <summary>
    /// Represents a service request to to update a webhook subscription in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to to update a webhook subscription in an asynchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Updates")]
    [Tag("Async")]
    [Route("/async/webhooks/subscriptions/{Id}", "PUT")]
    public sealed class UpdateWebhookSubscriptionAsync : UpdateWebhookSubscriptionBase
    {
    }
}