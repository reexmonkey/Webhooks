using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack;
using System.Collections.Generic;
using Webhooks.Core.Services.Contracts.Responses;

namespace Webhooks.Core.Services.Contracts.Requests
{
    /// <summary>
    /// Specifies a service request to create a webhook author.
    /// </summary>
    [Api("Specifies a service request to create a webhook author.")]
    public abstract class CreateWebhookAuthorBase : IPost, IReturn<WebhookAuthorResponse>
    {
        /// <summary>
        /// The unique name of the webhook author.
        /// </summary>
        [ApiMember(Description = "The unique name of the webhook author.", IsRequired = true)]
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
        /// The password to authenticate the author.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the author.", IsRequired = true)]
        public string Password { get; set; }

        /// <summary>
        /// The webhook definitions associated with this author.
        /// </summary>
        [ApiMember(Description = "The webhook definitions associated with this author.", IsRequired = false)]
        public List<WebhookDefinition> Webhooks { get; set; }

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
    /// Represents a service request to to create a webhook author in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to to create a webhook author in a synchronous operation.")]
    [Tag("Create")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Sync")]
    [Route("/sync/webhooks/authors", "POST")]
    public sealed class CreateWebhookAuthor : CreateWebhookAuthorBase
    {
    }

    /// <summary>
    /// Represents a service request to to create a webhook author in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to to create a webhook author in an asynchronous operation.")]
    [Tag("Create")]
    [Tag("Webhooks")]
    [Tag("Authors")]
    [Tag("Async")]
    [Route("/async/webhooks/authors", "POST")]
    public sealed class CreateWebhookAuthorAsync : CreateWebhookAuthorBase
    {
    }
}