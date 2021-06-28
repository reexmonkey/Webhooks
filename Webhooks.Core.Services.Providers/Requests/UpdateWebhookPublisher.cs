using Reexmonkey.Webhooks.Core.Services.Publishers.Responses;
using ServiceStack;
using System;

namespace Reexmonkey.Webhooks.Core.Services.Publishers.Requests
{
    /// <summary>
    /// Specifies a service request to update a webhook publisher.
    /// </summary>
    [Api("Specifies a service request to update a webhook publisher.")]
    public abstract class UpdateWebhookPublisherBase : IPut, IReturn<WebhookPublisherResponse>
    {
        /// <summary>
        /// The unique identifier of the webhook publisher.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook publisher.", IsRequired = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique name of the webhook publisher.
        /// </summary>
        [ApiMember(Description = "The name of the webhook publisher.", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// The description of the webhook publisher.
        /// </summary>
        [ApiMember(Description = "The description of the webhook publisher.", IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// The contact address of the publisher.
        /// </summary>
        [ApiMember(Description = "The contact address of the publisher.", IsRequired = false)]
        public string Address { get; set; }

        /// <summary>
        /// The current password to authenticate the publisher.
        /// </summary>
        [ApiMember(Description = "The current password to authenticate the publisher.", IsRequired = false)]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// The password to authenticate the publisher.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the publisher.", IsRequired = false)]
        public string NewPassword { get; set; }

        /// <summary>
        /// The public key to encrypt the secret of the webhook publisher.
        /// <para/>
        /// The public key can either be in the PEM (base64) or the XML (W3C or .NET) format.
        /// <para/>
        /// As owner of the public and private keys, please use the private key and the RSA algorithm with the OAEP-SHA256 padding to decrypt the encrypted secret.
        /// </summary>
        [ApiMember(Description = "The public key to encrypt the secret of the webhook publisher." +
            " The public key can either be in the PEM (base64) or the XML (W3C or .NET) format." +
            " As owner of the public and private key, please use the private key and the RSA algorithm with the OAEP-SHA256 padding to decrypt the encrypted secret.", IsRequired = true)]
        public string PublicKey { get; set; }
    }

    /// <summary>
    /// Represents a service request to to update a webhook publisher in a synchronous operation.
    /// </summary>
    [Api("Represents a service request to to update a webhook publisher in a synchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Tag("Updates")]
    [Route("/webhooks/publishers/{Id}", "PUT")]
    public sealed class UpdateWebhookPublisher : UpdateWebhookPublisherBase
    {
    }

    /// <summary>
    /// Represents a service request to to update a webhook publisher in an asynchronous operation.
    /// </summary>
    [Api("Represents a service request to to update a webhook publisher in an asynchronous operation.")]
    [Tag("Webhooks")]
    [Tag("Publishers")]
    [Tag("Updates")]
    [Tag("Async")]
    [Route("/async/webhooks/publishers/{Id}", "PUT")]
    public sealed class UpdateWebhookPublisherAsync : UpdateWebhookPublisherBase
    {
    }
}