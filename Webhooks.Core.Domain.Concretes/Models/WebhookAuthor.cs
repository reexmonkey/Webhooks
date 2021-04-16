using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Domain.Concretes.Models
{
    /// <summary>
    /// Represents a author of webhook authors.
    /// </summary>
    [Api("Represents a author of webhook authors.")]
    public class WebhookAuthor : IHasId<Guid>, IEquatable<WebhookAuthor>
    {
        /// <summary>
        /// The unique identifier of the webhook author.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook author.", IsRequired = false)]
        [AutoId]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique name of the webhook author.
        /// </summary>
        [ApiMember(Description = "The unique name of the webhook author.", IsRequired = true)]
        [Index(Unique = true)]
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
        public string Contact { get; set; }

        /// <summary>
        /// The password to authenticate the author.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the author.", IsRequired = false)]
        public string Password { get; set; }

        /// <summary>
        /// The webhook definitions associated with this author.
        /// </summary>
        [ApiMember(Description = "The webhook definitions associated with this author.", IsRequired = true)]
        [Reference]
        public List<WebhookDefinition> Webhooks { get; set; }

        /// <summary>
        /// The time (in UTC) when the profile of the webhook author was created.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) when the profile of the webhook author was created.", IsRequired = false)]
        public DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// The time (in UTC) when the profile of the webhook was last modified.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) when the profile of the webhook author was last modified.", IsRequired = false)]
        public DateTime LastModificationTimeUtc { get; set; }

        /// <summary>
        /// Whether the webhook author has been temporarily deleted (soft-delete).
        /// <para/> true if the author has been temporarily deleted; otherwise false.
        /// </summary>
        [ApiMember(Description = "Whether the webhook author has been temporarily deleted (soft-delete)." +
            " true if the author has been temporarily deleted; otherwise false.", IsRequired = false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookAuthor"/> class.
        /// </summary>
        public WebhookAuthor()
        {
            Webhooks = new List<WebhookDefinition>();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as WebhookAuthor);
        }

        public bool Equals(WebhookAuthor other)
        {
            return other != null &&
                   Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public static bool operator ==(WebhookAuthor left, WebhookAuthor right)
        {
            return EqualityComparer<WebhookAuthor>.Default.Equals(left, right);
        }

        public static bool operator !=(WebhookAuthor left, WebhookAuthor right)
        {
            return !(left == right);
        }
    }
}