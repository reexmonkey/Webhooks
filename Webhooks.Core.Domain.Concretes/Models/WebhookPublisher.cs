using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Domain.Concretes.Models
{
    /// <summary>
    /// Represents a publisher of webhooks.
    /// </summary>
    [Api("Represents a publisher of webhooks.")]
    public class WebhookPublisher : IHasId<Guid>, IEquatable<WebhookPublisher>
    {
        /// <summary>
        /// The unique identifier of the webhook publisher.
        /// </summary>
        [ApiMember(Description = "The unique identifier of the webhook publisher.", IsRequired = false)]
        [AutoId]
        public Guid Id { get; set; }

        /// <summary>
        /// The unique name of the webhook publisher.
        /// </summary>
        [ApiMember(Description = "The unique name of the webhook publisher.", IsRequired = true)]
        [Index(Unique = true)]
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
        public string Contact { get; set; }

        /// <summary>
        /// The password to authenticate the publisher.
        /// </summary>
        [ApiMember(Description = "The password to authenticate the publisher.", IsRequired = true)]
        public string Password { get; set; }

        /// <summary>
        /// The webhook definitions associated with this publisher.
        /// </summary>
        [ApiMember(Description = "The webhook definitions associated with this publisher.", IsRequired = true)]
        [Reference]
        public List<WebhookDefinition> Definitions { get; set; }

        /// <summary>
        /// The time (in UTC) when the profile of the webhook publisher was created.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) when the profile of the webhook publisher was created.", IsRequired = false)]
        public DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// The time (in UTC) when the profile of the webhook was last modified.
        /// </summary>
        [ApiMember(Description = "The time (in UTC) when the profile of the webhook publisher was last modified.", IsRequired = false)]
        public DateTime LastModificationTimeUtc { get; set; }

        /// <summary>
        /// Whether the webhook publisher has been temporarily deleted (soft-delete).
        /// <para/>
        /// true if the publisher has been temporarily deleted; otherwise false.
        /// </summary>
        [ApiMember(Description = "Whether the webhook publisher has been temporarily deleted (soft-delete)." +
            " true if the publisher has been temporarily deleted; otherwise false.", IsRequired = false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookPublisher"/> class.
        /// </summary>
        public WebhookPublisher()
        {
            Definitions = new List<WebhookDefinition>();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as WebhookPublisher);
        }

        public bool Equals(WebhookPublisher other)
        {
            return other != null &&
                   Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public static bool operator ==(WebhookPublisher left, WebhookPublisher right)
        {
            return EqualityComparer<WebhookPublisher>.Default.Equals(left, right);
        }

        public static bool operator !=(WebhookPublisher left, WebhookPublisher right)
        {
            return !(left == right);
        }
    }
}