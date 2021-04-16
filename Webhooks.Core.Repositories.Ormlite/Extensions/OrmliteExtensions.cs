using reexmonkey.xmisc.core.linq.extensions;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Repositories.Contracts.Joins;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Reexmonkey.Webhooks.Core.Repositories.Ormlite.Extensions
{
    public static class OrmliteExtensions
    {

        public static Expression<Func<AuthorsDefinitions, bool>> ToLeftJoinFilter(this WebhookAuthor model) 
            => x => x.AuthorId == model.Id;

        public static Expression<Func<AuthorsDefinitions, bool>> ToRightJoinFilter(this WebhookAuthor model)
        {
            var filter = (Expression<Func<AuthorsDefinitions, bool>>)(x => true);
            foreach (var webhook in model.Webhooks)
            {
                filter = filter.AndAlso(x => x.DefinitionId == webhook.Id);
            }
            return filter;
        }

        public static Expression<Func<AuthorsDefinitions, bool>> ToJoinFilter(this WebhookAuthor model)
        {
            var filter = (Expression<Func<AuthorsDefinitions, bool>>)(x => true);
            foreach (var webhook in model.Webhooks)
            {
                filter = filter
                    .AndAlso(x => x.AuthorId == model.Id)
                    .AndAlso(x => x.DefinitionId == webhook.Id);
            }
            return filter;
        }
        private static Expression<Func<AuthorsDefinitions, bool>> ToFilter(
            this IEnumerable<WebhookAuthor> models, Func<WebhookAuthor, Expression<Func<AuthorsDefinitions, bool>>> func)
        {
            var filter = (Expression<Func<AuthorsDefinitions, bool>>)(x => true);
            foreach (var model in models)
            {
                filter = filter.OrElse(func(model));
            }
            return filter;
        }
        public static Expression<Func<AuthorsDefinitions, bool>> ToLeftJoinFilter(this IEnumerable<WebhookAuthor> models) 
            => models.ToFilter(ToLeftJoinFilter);

        public static Expression<Func<AuthorsDefinitions, bool>> ToRightJoinFilter(this IEnumerable<WebhookAuthor> models) 
            => models.ToFilter(ToRightJoinFilter);

        public static Expression<Func<AuthorsDefinitions, bool>> ToJoinFilter(this IEnumerable<WebhookAuthor> models) 
            => models.ToFilter(ToJoinFilter);


        public static Expression<Func<SubscriptionsDefinitions, bool>> ToLeftJoinFilter(this WebhookSubscription model)
            => x => x.SubscriptionId == model.Id;

        public static Expression<Func<SubscriptionsDefinitions, bool>> ToRightJoinFilter(this WebhookSubscription model)
        {
            var filter = (Expression<Func<SubscriptionsDefinitions, bool>>)(x => true);
            return model.Webhook != null
                ? filter.AndAlso(x => x.DefinitionId == model.Webhook.Id)
                : filter;
        }

        public static Expression<Func<SubscriptionsDefinitions, bool>> ToJoinFilter(this WebhookSubscription model)
        {
            var filter = (Expression<Func<SubscriptionsDefinitions, bool>>)(x => true);
            if (model.Webhook != null)
            {
                filter = filter
                    .AndAlso(x => x.SubscriptionId == model.Id)
                    .AndAlso(x => x.DefinitionId == model.Webhook.Id); 
            }
            return filter;
        }
        private static Expression<Func<SubscriptionsDefinitions, bool>> ToFilter(
            this IEnumerable<WebhookSubscription> models, Func<WebhookSubscription, Expression<Func<SubscriptionsDefinitions, bool>>> func)
        {
            var filter = (Expression<Func<SubscriptionsDefinitions, bool>>)(x => true);
            foreach (var model in models)
            {
                filter = filter.OrElse(func(model));
            }
            return filter;
        }
        public static Expression<Func<SubscriptionsDefinitions, bool>> ToLeftJoinFilter(this IEnumerable<WebhookSubscription> models)
            => models.ToFilter(ToLeftJoinFilter);

        public static Expression<Func<SubscriptionsDefinitions, bool>> ToRightJoinFilter(this IEnumerable<WebhookSubscription> models)
            => models.ToFilter(ToRightJoinFilter);

        public static Expression<Func<SubscriptionsDefinitions, bool>> ToJoinFilter(this IEnumerable<WebhookSubscription> models)
            => models.ToFilter(ToJoinFilter);
    }
}
