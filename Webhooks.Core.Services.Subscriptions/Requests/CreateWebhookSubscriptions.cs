using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Subscriptions.Requests
{
    /// <summary>
    /// Specifies a service request to create webhook subscriptions.
    /// </summary>
    [Api("Specifies a service request to create webhook subscriptions.")]
    public abstract class CreateWebhookSubscriptionsBase : IPost, IReturn<List<WebhookSubscriptionResponse>>
    {
        /// <summary>
        /// The unique identifier or name of the subscriber.
        /// </summary>
        [ApiMember(Description = "The unique identifier or name of the subscriber.", IsRequired = true)]
        public string SubscriberId { get; set; }

        /// <summary>
        /// The subscribed webhooks to subscribe.
        /// </summary>
        [ApiMember(Description = "The webhooks to subscribe.", IsRequired = false)]
        public List<WebhookDefinition> Webhooks { get; set; }

        /// <summary>
        /// The endpoint URI of the webhook subscriber.
        /// </summary>
        [ApiMember(Description = "The endpoint URI of the webhook subscriber.", IsRequired = true)]
        public Uri EndpointUri { get; set; }

        /// <summary>
        /// The secret to sign, verify, encrypt or decrypt the payload of a webhook.
        /// </summary>
        [ApiMember(Description = "The secret to sign, verify, encrypt or decrypt the payload of a webhook.", IsRequired = true)]
        public string Secret { get; set; }

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
    /// Represents a service request to create webhook subscriptions.
    /// </summary>
    [Api("Represents a service request to create webhook subscriptions.")]
    [Tag("Create")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Sync")]
    [Route("/sync/webhooks/subscriptions", "POST")]
    public sealed class CreateWebhookSubscriptions : CreateWebhookSubscriptionsBase
    {
    }

    /// <summary>
    /// Represents a service request to create webhook subscriptions.
    /// </summary>
    [Api("Represents a service request to create webhook subscriptions.")]
    [Tag("Create")]
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Async")]
    [Route("/async/webhooks/subscriptions", "POST")]
    public sealed class CreateWebhookSubscriptionsAsync : CreateWebhookSubscriptionsBase
    {
    }
}