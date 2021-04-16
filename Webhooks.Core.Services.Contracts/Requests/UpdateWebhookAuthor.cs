using ServiceStack;
using System;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to update a webhook author.
    /// </summary>
    [Api("Specifies a service request to update a webhook author.")]
    public abstract class UpdateWebhookAuthorBase : IPut, IReturn<WebhookAuthorResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook author.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook author.", IsRequired = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique name of the webhook author.
        /// </summary>
        [ApiMember(Description = "The name of the webhook author.", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// The description of the webhook author.
        /// </summary>
        [ApiMember(Description = "The description of the webhook author.", IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// The contact address of the author.
        /// </summary>
        [ApiMember(Description = "The contact address of the author.", IsRequired = false)]
        public string Address { get; set; }

        /// <summary>
        /// The current password to authenticate the author.
        /// </summary>
        [ApiMember(Description = "The current password to authenticate the author.", IsRequired = false)]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// The password to authenticate the author.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the author.", IsRequired = false)]
        public string NewPassword { get; set; }

        /// <summary>
        /// The public key to encrypt the secret of the webhook author.
        /// <para/> The public key can either be in the PEM (base64) or the XML (W3C or .NET) format.
        /// <para/> As owner of the public and private keys, please use the private key and the RSA algorithm with the OAEP-SHA256 padding to decrypt the encrypted secret.
        /// </summary>
        [ApiMember(Description = "The public key to encrypt the secret of the webhook author." +
            " The public key can either be in the PEM (base64) or the XML (W3C or .NET) format." +
            " As owner of the public and private key, please use the private key and the RSA algorithm with the OAEP-SHA256 padding to decrypt the encrypted secret.", IsRequired = true)]
        public string PublicKey { get; set; }
    }

    /// <summary>
    /// Represents a service request to to update a webhook author in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to to update a webhook author in a synchronous operation.")]
    [Tag("Update")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Sync")]
    [Route("/sync/webhooks/authors/{Id}", "PUT")]
    public sealed class UpdateWebhookAuthor : UpdateWebhookAuthorBase
    {
    }

    /// <summary>
    /// Represents a service request to to update a webhook author in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to to update a webhook author in an asynchronous operation.")]
    [Tag("Update")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Async")]
    [Route("/async/webhooks/authors/{Id}", "PUT")]
    public sealed class UpdateWebhookAuthorAsync : UpdateWebhookAuthorBase
    {
    }
}