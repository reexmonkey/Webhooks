using ServiceStack;
using System;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to create a webhook subscription.
    /// </summary>
    [Api("pecifies a service request to create a webhook subscription.")]
    public abstract class CreateWebhookSubscriptionBase : IPost, IReturn<WebhookSubscriptionResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook subscriber.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook subscriber.", IsRequired = true)]
        public string SubscriberId { get; set; }

        /// <summary>
        /// The display name of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The display name of the webhook subscription.", IsRequired = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// The name of the webhook definitions associated with the subscription.
        /// </summary>
        [ApiMember(Description = "The name of the webhook definitions associated with the subscription.", IsRequired = true)]
        public List<string> Webhooks { get; set; }

        /// <summary>
        /// The endpoint URL of the webhook subscription.
        /// </summary>
        [ApiMember(Description = "The endpoint URL of the webhook subscription.", IsRequired = true)]
        public Uri Endpoint { get; set; }

        /// <summary>
        /// The secret to sign, verify, encrypt or decrypt the payload of a webhook, or authorize modification of a webhook subscription.
        /// </summary>
        [ApiMember(Description = "The secret to sign, verify, encrypt or decrypt the payload of a webhook, or authorize modification of a webhook subscription.", IsRequired = true)]
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

        /// <summary>
        /// Creates a new instance of the <see cref="CreateWebhookSubscriptionBase"/> class.
        /// </summary>
        public CreateWebhookSubscriptionBase()
        {
            Webhooks = new List<string>();
            Headers = new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// Represents a service request to to create a webhook subscription in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to to create a webhook subscription in a synchronous operation.")]
    [Tag("Create")]    
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Sync")]
    [Route("/sync/webhooks/subscriptions", "POST")]
    public sealed class CreateWebhookSubscription : CreateWebhookSubscriptionBase
    {
    }

    /// <summary>
    /// Represents a service request to to create a webhook subscription in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to to create a webhook subscription in an asynchronous operation.")]
    [Tag("Create")]    
    [Tag("Webhooks")]
    [Tag("Subscriptions")]
    [Tag("Async")]
    [Route("/sync/webhooks/subscriptions", "POST")]
    public sealed class CreateWebhookSubscriptionAsync : CreateWebhookSubscriptionBase
    {
    }
}