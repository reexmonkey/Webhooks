using reexmonkey.xmisc.backbone.repositories.contracts.extensions;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Repositories.Contracts;
using Reexmonkey.Webhooks.Core.Repositories.Contracts.Joins;
using Reexmonkey.Webhooks.Core.Repositories.Ormlite.Extensions;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Reexmonkey.Webhooks.Core.Repositories.Ormlite
{
    public class WebhookSubscriptionRepository : IWebhookSubscriptionRepository
    {
        private readonly IWebhookDefinitionRepository webhookDefinitionRepository;
        private readonly ISubscriptionsDefinitionsRepository subscriptionsDefinitionsRepository;
        private readonly IDbConnectionFactory factory;

        public WebhookSubscriptionRepository(
            IWebhookDefinitionRepository webhookDefinitionRepository,
            ISubscriptionsDefinitionsRepository subscriptionsDefinitionsRepository,
            IDbConnectionFactory factory)
        {
            this.subscriptionsDefinitionsRepository = subscriptionsDefinitionsRepository ?? throw new ArgumentNullException(nameof(subscriptionsDefinitionsRepository));
            this.webhookDefinitionRepository = webhookDefinitionRepository ?? throw new ArgumentNullException(nameof(webhookDefinitionRepository));

            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public bool ContainsKey(Guid key)
        {
            using (var db = factory.OpenDbConnection())
            {
                return db.Count<WebhookSubscription>(q => q.Id == key) != 0L;
            }
        }

        public async Task<bool> ContainsKeyAsync(Guid key, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                token.ThrowIfCancellationRequested();
                return await db.CountAsync<WebhookSubscription>(q => q.Id == key) != 0L;
            }
        }

        public bool ContainsKeys(IEnumerable<Guid> keys, bool strict = true)
        {
            using (var db = factory.OpenDbConnection())
            {
                var count = db.Count<WebhookSubscription>(q => keys.Contains(q.Id));
                return strict ? count != 0L && count == keys.Count() : count != 0L;
            }
        }

        public async Task<bool> ContainsKeysAsync(IEnumerable<Guid> keys, bool strict = true, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                token.ThrowIfCancellationRequested();
                var count = await db.CountAsync<WebhookSubscription>(q => keys.Contains(q.Id), token);
                return strict ? count != 0L && count == keys.Count() : count != 0L;
            }
        }

        public bool Erase(WebhookSubscription model) => EraseByKey(model.Id);

        public long EraseAll(IEnumerable<WebhookSubscription> models) => EraseAllByKeys(models.Select(q => q.Id));

        public Task<long> EraseAllAsync(IEnumerable<WebhookSubscription> models, CancellationToken token = default)
            => EraseAllByKeysAsync(models.Select(q => q.Id), token);

        public long EraseAllByKeys(IEnumerable<Guid> keys)
        {
            var deletes = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                using (var db = factory.OpenDbConnection())
                {
                    deletes = db.DeleteByIds<WebhookSubscription>(keys);
                }
                scope.Complete();
            }
            return deletes;
        }

        public async Task<long> EraseAllByKeysAsync(IEnumerable<Guid> keys, CancellationToken token = default)
        {
            var result = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                using (var db = factory.OpenDbConnection())
                {
                    result = await db.DeleteByIdsAsync<WebhookSubscription>(keys, token: token);
                }
                scope.Complete();
            }
            return result;
        }

        public Task<bool> EraseAsync(WebhookSubscription model, CancellationToken token = default) => EraseByKeyAsync(model.Id, token);

        public bool EraseByKey(Guid key)
        {
            int deletes = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                using (var db = factory.OpenDbConnection())
                {
                    deletes = db.DeleteById<WebhookSubscription>(key);
                }
                scope.Complete();
            }
            return deletes != 0;
        }

        public async Task<bool> EraseByKeyAsync(Guid key, CancellationToken token = default)
        {
            int deletes = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                using (var db = factory.OpenDbConnection())
                {
                    deletes = await db.DeleteByIdAsync<WebhookSubscription>(key);
                }
                scope.Complete();
            }
            return deletes != 0;
        }

        public List<WebhookSubscription> FindAll(Expression<Func<WebhookSubscription, bool>> predicate, bool? references = null, int? offset = null, int? count = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookSubscription>().Where(predicate).Skip(offset).Take(count);
                var matches = db.Select(query);
                if (matches.Any() && references.HasValue && references.Value) HydrateAll(matches);
                return matches;
            }
        }

        public async Task<List<WebhookSubscription>> FindAllAsync(Expression<Func<WebhookSubscription, bool>> predicate, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookSubscription>().Where(predicate).Skip(offset).Take(count);
                var matches = await db.SelectAsync(query, token);
                if (matches.Any() && references.HasValue && references.Value) HydrateAll(matches);
                return matches;
            }
        }

        public List<WebhookSubscription> FindAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
            => FindAll(x => keys.Contains(x.Id), references, offset, count);

        public Task<List<WebhookSubscription>> FindAllByKeysAsync(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
            => FindAllAsync(x => keys.Contains(x.Id), references, offset, count, token);

        public WebhookSubscription FindByKey(Guid key, bool? references = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var match = db.SingleById<WebhookSubscription>(key);
                if (match != null && references.HasValue && references.Value) Hydrate(match);
                return match;
            }
        }

        public async Task<WebhookSubscription> FindByKeyAsync(Guid key, bool? references = null, CancellationToken token = default)
        {
            using (var db = await factory.OpenAsync(token))
            {
                var match = await db.SingleByIdAsync<WebhookSubscription>(key, token);
                if (match != null && references.HasValue && references.Value) Hydrate(match);
                return match;
            }
        }

        public List<WebhookSubscription> Get(bool? references = null, int? offset = null, int? count = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookSubscription>().Skip(offset).Take(count);
                var matches = db.Select(query);
                if (matches.Any() && references.HasValue && references.Value) HydrateAll(matches);
                return matches;
            }
        }

        public async Task<List<WebhookSubscription>> GetAsync(bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookSubscription>().Skip(offset).Take(count);
                var matches = await db.SelectAsync(query, token);
                if (matches.Any() && references.HasValue && references.Value) HydrateAll(matches);
                return matches;
            }
        }

        public List<Guid> GetKeys(int? offset = null, int? count = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookSubscription>().Select(x => x.Id).Skip(offset).Take(count);
                return db.Select<Guid>(query);
            }
        }

        public async Task<List<Guid>> GetKeysAsync(int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookSubscription>().Select(x => x.Id).Skip(offset).Take(count);
                return await db.SelectAsync<Guid>(query);
            }
        }

        public void Hydrate(WebhookSubscription model)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookSubscription>()
                    .Join<WebhookSubscription, SubscriptionsDefinitions>((author, relation) => author.Id == relation.SubscriptionId)
                    .Join<SubscriptionsDefinitions, WebhookDefinition>((relation, definition) => relation.DefinitionId == definition.Id)
                    .Where(x => x.Id == model.Id);

                var result = db.SelectMulti<WebhookSubscription, WebhookDefinition>(query).FirstOrDefault();
                if (result != null) model.Webhook = result.Item2;
            }
        }

        private static Dictionary<WebhookSubscription, WebhookDefinition> GetSubscriptionsWebhooksMap(IEnumerable<Guid> keys, IDbConnection db)
        {
            var query = db.From<WebhookSubscription>()
                .Join<WebhookSubscription, SubscriptionsDefinitions>((author, relation) => author.Id == relation.SubscriptionId)
                .Join<SubscriptionsDefinitions, WebhookDefinition>((relation, webhook) => relation.DefinitionId == webhook.Id)
                .Where(x => keys.Contains(x.Id));

            var matches = db.SelectMulti<WebhookSubscription, WebhookDefinition>(query);
            return matches.ToDictionary(x => x.Item1, x => x.Item2);
        }

        private static async Task<Dictionary<WebhookSubscription, WebhookDefinition>> GetSubscriptionsWebhooksMapAsync(
            IEnumerable<Guid> keys,
            IDbConnection db,
            CancellationToken token)
        {
            var query = db.From<WebhookSubscription>()
                .Join<WebhookSubscription, SubscriptionsDefinitions>((author, relation) => author.Id == relation.SubscriptionId)
                .Join<SubscriptionsDefinitions, WebhookDefinition>((relation, webhook) => relation.DefinitionId == webhook.Id)
                .Where(x => keys.Contains(x.Id));

            var matches = await db.SelectMultiAsync<WebhookSubscription, WebhookDefinition>(query, token);
            return matches.ToDictionary(x => x.Item1, x => x.Item2);
        }

        public void HydrateAll(IEnumerable<WebhookSubscription> models)
        {
            var keys = models.Select(x => x.Id);
            using (var db = factory.OpenDbConnection())
            {
                var map = GetSubscriptionsWebhooksMap(keys, db);
                foreach (var model in models)
                {
                    if (map.TryGetValue(model, out WebhookDefinition webhook) && webhook != null)
                    {
                        model.Webhook = webhook;
                    }
                }
            }
        }

        public async Task HydrateAllAsync(IEnumerable<WebhookSubscription> models, CancellationToken token = default)
        {
            var keys = models.Select(x => x.Id);
            using (var db = await factory.OpenAsync(token))
            {
                var map = await GetSubscriptionsWebhooksMapAsync(keys, db, token);
                foreach (var model in models)
                {
                    if (map.TryGetValue(model, out WebhookDefinition webhook) && webhook != null)
                    {
                        model.Webhook = webhook;
                    }
                }
            }
        }

        public async Task HydrateAsync(WebhookSubscription model, CancellationToken token = default)
        {
            using (var db = await factory.OpenAsync(token))
            {
                var query = db.From<WebhookSubscription>()
                    .Join<WebhookSubscription, SubscriptionsDefinitions>((author, relation) => author.Id == relation.SubscriptionId)
                    .Join<SubscriptionsDefinitions, WebhookDefinition>((relation, definition) => relation.DefinitionId == definition.Id)
                    .Where(x => x.Id == model.Id);

                var result = (await db.SelectMultiAsync<WebhookSubscription, WebhookDefinition>(query)).FirstOrDefault();
                if (result != null) model.Webhook = result.Item2;
            }
        }

        public void Restore(WebhookSubscription model, bool? references = null)
        {
            model.IsActive = false;
            if (references.HasValue
                && references.Value
                && model.Webhook != null)
            {
                webhookDefinitionRepository.Restore(model.Webhook, references);
            }
            Save(model, references);
        }

        public void RestoreAll(IEnumerable<WebhookSubscription> models, bool? references = null)
        {
            var webhooks = new List<WebhookDefinition>();
            foreach (var model in models)
            {
                model.IsActive = false;
                if (model.Webhook != null)
                    webhooks.Add(model.Webhook);
            }

            if (references.HasValue && references.Value && webhooks.Any())
            {
                webhookDefinitionRepository.RestoreAll(webhooks, references);
            }

            SaveAll(models, references);
        }

        public async Task RestoreAllAsync(IEnumerable<WebhookSubscription> models, bool? references = null, CancellationToken token = default)
        {
            var webhooks = new List<WebhookDefinition>();
            foreach (var model in models)
            {
                model.IsActive = false;
                if (model.Webhook != null)
                    webhooks.Add(model.Webhook);
            }

            if (references.HasValue && references.Value && webhooks.Any())
            {
                await webhookDefinitionRepository.RestoreAllAsync(webhooks, references, token);
            }
            await SaveAllAsync(models, references, token);
        }

        public List<WebhookSubscription> RestoreAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var matches = FindAllByKeys(keys, references, offset, count);
            if (matches.Any()) RestoreAll(matches, references);
            return matches;
        }

        public async Task<List<WebhookSubscription>> RestoreAllByKeysAsync(
            IEnumerable<Guid> keys,
            bool? references = null,
            int? offset = null,
            int? count = null,
            CancellationToken token = default)
        {
            var matches = await FindAllByKeysAsync(keys, references, offset, count, token);
            if (matches.Any()) await RestoreAllAsync(matches, references, token);
            return matches;
        }

        public async Task RestoreAsync(WebhookSubscription model, bool? references = null)
        {
            model.IsActive = false;
            if (references.HasValue
                && references.Value
                && model.Webhook != null)
            {
                await webhookDefinitionRepository.RestoreAsync(model.Webhook, references);
            }
            await SaveAsync(model, references);
        }

        public WebhookSubscription RestoreByKey(Guid key, bool? references = null)
        {
            var match = FindByKey(key, references);
            if (match != null) Restore(match, references);
            return match;
        }

        public async Task<WebhookSubscription> RestoreByKeyAsync(Guid key, bool? references = null)
        {
            var match = await FindByKeyAsync(key, references);
            if (match != null) await RestoreAsync(match, references);
            return match;
        }

        public bool Save(WebhookSubscription model, bool? references = true)
        {
            var inserted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                using (var db = factory.OpenDbConnection())
                {
                    inserted = db.Save(model, false);
                    if (references.HasValue && references.Value)
                    {
                        var filter = model.ToLeftJoinFilter();
                        if (model.Webhook != null) //reference available
                        {
                            webhookDefinitionRepository.Save(model.Webhook, references: false);

                            //manage joins
                            var join = new SubscriptionsDefinitions { SubscriptionId = model.Id, DefinitionId = model.Webhook.Id };
                            var match = subscriptionsDefinitionsRepository.FindAll(filter, references: false).FirstOrDefault();

                            if (match != null) join.Id = match.Id; //update the key if the local join is identical to the remote join
                            subscriptionsDefinitionsRepository.Save(join, references: false); //insert or update join
                        }
                        else //reference not available: delete former join
                        {
                            var match = subscriptionsDefinitionsRepository.FindAll(filter, references: false).FirstOrDefault();
                            if (match != null) subscriptionsDefinitionsRepository.EraseByKey(match.Id);
                        }
                    }
                }
                scope.Complete();
            }
            return inserted;
        }

        public long SaveAll(IEnumerable<WebhookSubscription> models, bool? references = true)
        {
            var inserts = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                using (var db = factory.OpenDbConnection())
                {
                    inserts = db.SaveAll(models);
                    if (references.HasValue && references.Value)
                    {
                        var filter = models.ToLeftJoinFilter();
                        var webhookMap = models.ToDictionary(model => model, model => model.Webhook);

                        if (webhookMap.Any()) //references available
                        {
                            var webhooks = webhookMap.Select(x => x.Value);
                            var keys = webhooks.Select(x => x.Id);
                            var matches = webhookDefinitionRepository.FindAllByKeys(keys, references: false);

                            var @in = webhooks.Except(matches); //new references
                            var same = webhooks.Intersect(matches); //same references
                            var @out = matches.Except(webhooks); //stale references

                            if (@in.Any()) webhookDefinitionRepository.SaveAll(@in, references: false); //save new references
                            if (same.Any()) webhookDefinitionRepository.SaveAll(same, references: false); //update existing references
                            if (@out.Any()) webhookDefinitionRepository.EraseAll(@out); //erase stale references

                            //manage relations
                            var local = webhookMap.Select(pair => new SubscriptionsDefinitions { SubscriptionId = pair.Key.Id, DefinitionId = pair.Value.Id });
                            var remote = subscriptionsDefinitionsRepository.FindAll(filter, references: false);

                            var incoming = local.Except(remote); // new joins
                            var existing = local.Intersect(remote); // existing joins
                            var outgoing = remote.Except(local); // stale joins

                            if (incoming.Any()) subscriptionsDefinitionsRepository.SaveAll(incoming, references: false);
                            if (existing.Any()) subscriptionsDefinitionsRepository.SaveAll(existing, references: false);
                            if (outgoing.Any()) subscriptionsDefinitionsRepository.EraseAll(outgoing);
                        }
                        else //references not available: delete all former joins
                        {
                            var remote = subscriptionsDefinitionsRepository.FindAll(filter, references: false);
                            if (remote.Any()) subscriptionsDefinitionsRepository.EraseAll(remote);
                        }
                    }
                }
                scope.Complete();
            }
            return inserts;
        }

        public async Task<long> SaveAllAsync(IEnumerable<WebhookSubscription> models, bool? references = true, CancellationToken token = default)
        {
            var inserts = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                using (var db = factory.OpenDbConnection())
                {
                    inserts = await db.SaveAllAsync(models);
                    if (references.HasValue && references.Value)
                    {
                        var filter = models.ToLeftJoinFilter();
                        var webhookMap = models.ToDictionary(model => model, model => model.Webhook);

                        if (webhookMap.Any()) //references available
                        {
                            var webhooks = webhookMap.Select(x => x.Value);
                            var keys = webhooks.Select(x => x.Id);
                            var matches = await webhookDefinitionRepository.FindAllByKeysAsync(keys, references: false);

                            var @in = matches.Except(webhooks); //new references
                            var same = webhooks.Intersect(matches); //same references
                            var @out = webhooks.Except(matches); //stale references

                            if (@in.Any()) await webhookDefinitionRepository.SaveAllAsync(@in, references: false); //save new references
                            if (same.Any()) await webhookDefinitionRepository.SaveAllAsync(same, references: false); //update existing references
                            if (@out.Any()) await webhookDefinitionRepository.EraseAllAsync(@out); //erase stale references

                            //manage relations
                            var local = webhookMap.Select(pair => new SubscriptionsDefinitions { SubscriptionId = pair.Key.Id, DefinitionId = pair.Value.Id });
                            var remote = await subscriptionsDefinitionsRepository.FindAllAsync(filter, references: false);

                            var incoming = local.Except(remote); // new joins
                            var existing = local.Intersect(remote); // existing joins
                            var outgoing = remote.Except(local); // stale joins

                            if (incoming.Any()) await subscriptionsDefinitionsRepository.SaveAllAsync(incoming, references: false);
                            if (existing.Any()) await subscriptionsDefinitionsRepository.SaveAllAsync(existing, references: false);
                            if (outgoing.Any()) await subscriptionsDefinitionsRepository.EraseAllAsync(outgoing);
                        }
                        else //references not available: delete all former joins
                        {
                            var remote = await subscriptionsDefinitionsRepository.FindAllAsync(filter, references: false);
                            if (remote.Any()) await subscriptionsDefinitionsRepository.EraseAllAsync(remote);
                        }
                    }
                }
                scope.Complete();
            }
            return inserts;
        }

        public async Task<bool> SaveAsync(WebhookSubscription model, bool? references = true, CancellationToken token = default)
        {
            var inserted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                using (var db = factory.OpenDbConnection())
                {
                    inserted = await db.SaveAsync(model, references.Value);
                    if (references.HasValue && references.Value)
                    {
                        var filter = model.ToLeftJoinFilter();
                        if (model.Webhook != null) //references available
                        {
                            webhookDefinitionRepository.Save(model.Webhook, references: false);

                            //manage joins
                            var join = new SubscriptionsDefinitions { SubscriptionId = model.Id, DefinitionId = model.Webhook.Id };
                            var match = (await subscriptionsDefinitionsRepository.FindAllAsync(filter, references: false, token: token)).FirstOrDefault();

                            if (match != null) join.Id = match.Id; //update the key if the local join is identical to the remote join
                            await subscriptionsDefinitionsRepository.SaveAsync(join, references: false); //insert or update join
                        }
                        else //references not available: delete all former joins
                        {
                            var match = (await subscriptionsDefinitionsRepository.FindAllAsync(filter, references: false)).FirstOrDefault();
                            if (match != null) await subscriptionsDefinitionsRepository.EraseByKeyAsync(match.Id);
                        }
                    }
                }
                scope.Complete();
            }
            return inserted;
        }

        public void Trash(WebhookSubscription model, bool? references = null)
        {
            model.IsActive = true;
            if (references.HasValue
                && references.Value
                && model.Webhook != null)
            {
                webhookDefinitionRepository.Trash(model.Webhook, references);
            }
            Save(model, references);
        }

        public void TrashAll(IEnumerable<WebhookSubscription> models, bool? references = null)
        {
            if (references.HasValue && references.Value)
            {
                var webhooks = new List<WebhookDefinition>();
                foreach (var model in models)
                {
                    model.IsActive = true;
                    if (model.Webhook != null) webhooks.Add(model.Webhook);
                }
                if (webhooks.Any()) webhookDefinitionRepository.TrashAll(webhooks, references);
            }
            SaveAll(models, references);
        }

        public async Task TrashAllAsync(IEnumerable<WebhookSubscription> models, bool? references = null, CancellationToken token = default)
        {
            if (references.HasValue && references.Value)
            {
                var webhooks = new List<WebhookDefinition>();
                foreach (var model in models)
                {
                    model.IsActive = true;
                    if (model.Webhook != null) webhooks.Add(model.Webhook);
                }
                if (webhooks.Any()) await webhookDefinitionRepository.TrashAllAsync(webhooks, references, token);
            }
            await SaveAllAsync(models, references, token);
        }

        public List<WebhookSubscription> TrashAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var matches = FindAllByKeys(keys, references, offset, count);
            if (matches.Any()) TrashAll(matches, references);
            return matches;
        }

        public async Task<List<WebhookSubscription>> TrashAllByKeysAsync(
            IEnumerable<Guid> keys,
            bool? references = null,
            int? offset = null,
            int? count = null,
            CancellationToken token = default)
        {
            var matches = await FindAllByKeysAsync(keys, references, offset, count);
            if (matches.Any()) await TrashAllAsync(matches, references);
            return matches;
        }

        public async Task TrashAsync(WebhookSubscription model, bool? references = null)
        {
            model.IsActive = true;
            if (references.HasValue && references.Value && model.Webhook != null)
            {
                await webhookDefinitionRepository.TrashAsync(model.Webhook, references);
            }
            await SaveAsync(model, references);
        }

        public WebhookSubscription TrashByKey(Guid key, bool? references = null)
        {
            var match = FindByKey(key, references);
            if (match != null) Trash(match, references);
            return match;
        }

        public async Task<WebhookSubscription> TrashByKeyAsync(Guid key, bool? references = null, CancellationToken token = default)
        {
            var match = await FindByKeyAsync(key, references).ConfigureAwait(false);
            if (match != null) await TrashAsync(match, references).ConfigureAwait(false);
            return match;
        }
    }
}