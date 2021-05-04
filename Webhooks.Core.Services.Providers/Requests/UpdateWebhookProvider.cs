using ServiceStack;
using System;
using Webhooks.Core.Services.Providers.Contracts.Responses;

namespace Reexmonkey.Webhooks.Core.Services.Providers.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to update a webhook provider.
    /// </summary>
    [Api("Specifies a service request to update a webhook provider.")]
    public abstract class UpdateWebhookProviderBase : IPut, IReturn<WebhookProviderResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook provider.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook provider.", IsRequired = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique name of the webhook provider.
        /// </summary>
        [ApiMember(Description = "The name of the webhook provider.", IsRequired = true)]
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
        /// The current password to authenticate the provider.
        /// </summary>
        [ApiMember(Description = "The current password to authenticate the provider.", IsRequired = false)]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// The password to authenticate the provider.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the provider.", IsRequired = false)]
        public string NewPassword { get; set; }

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
    /// Represents a service request to to update a webhook provider in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to to update a webhook provider in a synchronous operation.")]
    [Tag("Update")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Sync")]
    [Route("/sync/webhooks/providers/{Id}", "PUT")]
    public sealed class UpdateWebhookProvider : UpdateWebhookProviderBase
    {
    }

    /// <summary>
    /// Represents a service request to to update a webhook provider in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to to update a webhook provider in an asynchronous operation.")]
    [Tag("Update")]
    [Tag("Webhooks")]
    [Tag("Providers")]
    [Tag("Async")]
    [Route("/async/webhooks/providers/{Id}", "PUT")]
    public sealed class UpdateWebhookProviderAsync : UpdateWebhookProviderBase
    {
    }
}