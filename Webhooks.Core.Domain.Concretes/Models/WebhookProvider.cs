using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Domain.Concretes.Models
{
    /// <summary>
    /// Represents a provider of webhook providers.
    /// </summary>
    [Api("Represents a provider of webhook providers.")]
    public class WebhookProvider : IHasId<Guid>, IEquatable<WebhookProvider>
    {
        /// <summary>
        /// The unique identifier of the webhook provider.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook provider.", IsRequired = false)]
        [AutoId]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique name of the webhook provider.
        /// </summary>
        [ApiMember(Description = "The unique name of the webhook provider.", IsRequired = true)]
        [Index(Unique = true)]
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
        public string Contact { get; set; }

        /// <summary>
        /// The password to authenticate the provider.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the provider.", IsRequired = false)]
        public string Password { get; set; }

        /// <summary>
        /// The webhook definitions associated with this provider.
        /// </summary>
        [ApiMember(Description = "The webhook definitions associated with this provider.", IsRequired = true)]
        [Reference]
        public List<WebhookDefinition> Webhooks { get; set; }

        /// <summary>
        /// The time (in UTC) when the profile of the webhook provider was created.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) when the profile of the webhook provider was created.", IsRequired = false)]
        public DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// The time (in UTC) when the profile of the webhook was last modified.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) when the profile of the webhook provider was last modified.", IsRequired = false)]
        public DateTime LastModificationTimeUtc { get; set; }

        /// <summary>
        /// Whether the webhook provider has been temporarily deleted (soft-delete).
        /// <para/>
        /// true if the provider has been temporarily deleted; otherwise false.
        /// </summary>
        [ApiMember(Description = "Whether the webhook provider has been temporarily deleted (soft-delete)." +
            " true if the provider has been temporarily deleted; otherwise false.", IsRequired = false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookProvider"/> class.
        /// </summary>
        public WebhookProvider()
        {
            Webhooks = new List<WebhookDefinition>();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as WebhookProvider);
        }

        public bool Equals(WebhookProvider other)
        {
            return other != null &&
                   Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public static bool operator ==(WebhookProvider left, WebhookProvider right)
        {
            return EqualityComparer<WebhookProvider>.Default.Equals(left, right);
        }

        public static bool operator !=(WebhookProvider left, WebhookProvider right)
        {
            return !(left == right);
        }
    }
}