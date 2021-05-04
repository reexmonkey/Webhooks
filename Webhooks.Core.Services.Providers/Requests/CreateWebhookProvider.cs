using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to create a webhook provider.
    /// </summary>
    [Api("Specifies a service request to create a webhook provider.")]
    public abstract class CreateWebhookProviderBase : IPost, IReturn<WebhookProviderResponse>
    {
        /// <summary>
        /// The unique name of the webhook provider.
        /// </summary>
        [ApiMember(Description = "The unique name of the webhook provider.", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// The description of the webhook provider.
        /// </summary>
        [ApiMember(Description = "The description of the webhook provider.", IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// The contact address of the provider.
        /// </summary>
        [ApiMember(Description = "The contact address of the provider.", IsRequired = false)]
        public string Address { get; set; }

        /// <summary>
        /// The password to authenticate the provider.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the provider.", IsRequired = true)]
        public string Password { get; set; }

        /// <summary>
        /// The webhook definitions associated with this provider.
        /// </summary>
        [ApiMember(Description = "The webhook definitions associated with this provider.", IsRequired = false)]
        public List<WebhookDefinition> Webhooks { get; set; }

        /// <summary>
        /// The public key to encrypt the secret of the webhook provider.
        /// <para/>
        /// The public key can either be in the PEM (base64) or the XML (W3C or .NET) format.
        /// <para/>
        /// As owner of the public and private keys, please use the private key and the RSA algorithm with the OAEP-SHA256 padding to decrypt the encrypted secret.
        /// </summary>
        [ApiMember(Description = "The public key to encrypt the secret of the webhook provider." +
            " The public key can either be in the PEM (base64) or the XML (W3C or .NET) format." +
            " As owner of the public and private key, please use the private key and the RSA algorithm with the OAEP-SHA256 padding to decrypt the encrypted secret.", IsRequired = true)]
        public string PublicKey { get; set; }
    }

    /// <summary>
    /// Represents a service request to to create a webhook provider in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to to create a webhook provider in a synchronous operation.")]
    [Tag("Create")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Sync")]
    [Route("/sync/webhooks/providers", "POST")]
    public sealed class CreateWebhookProvider : CreateWebhookProviderBase
    {
    }

    /// <summary>
    /// Represents a service request to to create a webhook provider in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to to create a webhook provider in an asynchronous operation.")]
    [Tag("Create")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Async")]
    [Route("/async/webhooks/providers", "POST")]
    public sealed class CreateWebhookProviderAsync : CreateWebhookProviderBase
    {
    }
}