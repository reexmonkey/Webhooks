using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Reexmonkey.Webhooks.Core.Repositories.Contracts.Joins
{
    public class AuthorsDefinitions : IEquatable<AuthorsDefinitions>
    {
        [AutoIncrement]
        public long Id { get; set; }

        [References(typeof(WebhookAuthor))]
        public Guid AuthorId { get; set; }

        [References(typeof(WebhookDefinition))]
        public Guid DefinitionId { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as AuthorsDefinitions);
        }

        public bool Equals(AuthorsDefinitions other)
        {
            return other != null &&
                   AuthorId.Equals(other.AuthorId) &&
                   DefinitionId.Equals(other.DefinitionId);
        }

        public override int GetHashCode()
        {
            int hashCode = -217596032;
            hashCode = hashCode * -1521134295 + AuthorId.GetHashCode();
            hashCode = hashCode * -1521134295 + DefinitionId.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(AuthorsDefinitions left, AuthorsDefinitions right)
        {
            return EqualityComparer<AuthorsDefinitions>.Default.Equals(left, right);
        }

        public static bool operator !=(AuthorsDefinitions left, AuthorsDefinitions right)
        {
            return !(left == right);
        }
    }
}