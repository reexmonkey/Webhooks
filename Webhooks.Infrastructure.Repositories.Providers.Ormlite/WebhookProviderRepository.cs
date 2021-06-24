using reexmonkey.xmisc.backbone.repositories.contracts.extensions;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Repositories.Contracts;
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
    public class WebhookProviderRepository : IWebhookProviderRepository
    {
        private readonly IWebhookDefinitionRepository webhookDefinitionRepository;
        private readonly IDbConnectionFactory factory;

        public WebhookProviderRepository(
            IWebhookDefinitionRepository webhookDefinitionRepository,
            IDbConnectionFactory factory)
        {
            this.webhookDefinitionRepository = webhookDefinitionRepository ?? throw new ArgumentNullException(nameof(webhookDefinitionRepository));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public bool ContainsKey(Guid key)
        {
            using (var db = factory.OpenDbConnection())
            {
                return db.Count<WebhookPublisher>(q => q.Id == key) != 0L;
            }
        }

        public async Task<bool> ContainsKeyAsync(Guid key, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                token.ThrowIfCancellationRequested();
                return await db.CountAsync<WebhookPublisher>(q => q.Id == key) != 0L;
            }
        }

        public bool ContainsKeys(IEnumerable<Guid> keys, bool strict = true)
        {
            using (var db = factory.OpenDbConnection())
            {
                var count = db.Count<WebhookPublisher>(q => keys.Contains(q.Id));
                return strict ? count != 0L && count == keys.Count() : count != 0L;
            }
        }

        public async Task<bool> ContainsKeysAsync(IEnumerable<Guid> keys, bool strict = true, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                token.ThrowIfCancellationRequested();
                var count = await db.CountAsync<WebhookPublisher>(q => keys.Contains(q.Id), token);
                return strict ? count != 0L && count == keys.Count() : count != 0L;
            }
        }

        public bool Erase(WebhookPublisher model) => EraseByKey(model.Id);

        public long EraseAll(IEnumerable<WebhookPublisher> models) => EraseAllByKeys(models.Select(q => q.Id));

        public Task<long> EraseAllAsync(IEnumerable<WebhookPublisher> models, CancellationToken token = default)
            => EraseAllByKeysAsync(models.Select(q => q.Id), token);

        public long EraseAllByKeys(IEnumerable<Guid> keys)
        {
            var deletes = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                using (var db = factory.OpenDbConnection())
                {
                    deletes = db.DeleteByIds<WebhookPublisher>(keys);
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
                    result = await db.DeleteByIdsAsync<WebhookPublisher>(keys, token: token);
                }
                scope.Complete();
            }
            return result;
        }

        public Task<bool> EraseAsync(WebhookPublisher model, CancellationToken token = default) => EraseByKeyAsync(model.Id, token);

        public bool EraseByKey(Guid key)
        {
            int deletes = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                using (var db = factory.OpenDbConnection())
                {
                    deletes = db.DeleteById<WebhookPublisher>(key);
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
                    deletes = await db.DeleteByIdAsync<WebhookPublisher>(key);
                }
                scope.Complete();
            }
            return deletes != 0;
        }

        public List<WebhookPublisher> FindAll(Expression<Func<WebhookPublisher, bool>> predicate, bool? references = null, int? offset = null, int? count = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookPublisher>().Where(predicate).Skip(offset).Take(count);
                var matches = db.Select(query);
                if (matches.Any() && references.HasValue && references.Value) HydrateAll(matches);
                return matches;
            }
        }

        public async Task<List<WebhookPublisher>> FindAllAsync(Expression<Func<WebhookPublisher, bool>> predicate, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookPublisher>().Where(predicate).Skip(offset).Take(count);
                var matches = await db.SelectAsync(query, token);
                if (matches.Any() && references.HasValue && references.Value) HydrateAll(matches);
                return matches;
            }
        }

        public List<WebhookPublisher> FindAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
            => FindAll(x => keys.Contains(x.Id), references, offset, count);

        public Task<List<WebhookPublisher>> FindAllByKeysAsync(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
            => FindAllAsync(x => keys.Contains(x.Id), references, offset, count, token);

        public WebhookPublisher FindByKey(Guid key, bool? references = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var match = db.SingleById<WebhookPublisher>(key);
                if (match != null && references.HasValue && references.Value) Hydrate(match);
                return match;
            }
        }

        public async Task<WebhookPublisher> FindByKeyAsync(Guid key, bool? references = null, CancellationToken token = default)
        {
            using (var db = await factory.OpenAsync(token))
            {
                var match = await db.SingleByIdAsync<WebhookPublisher>(key, token);
                if (match != null && references.HasValue && references.Value) Hydrate(match);
                return match;
            }
        }

        public List<WebhookPublisher> Get(bool? references = null, int? offset = null, int? count = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookPublisher>().Skip(offset).Take(count);
                var matches = db.Select(query);
                if (matches.Any() && references.HasValue && references.Value) HydrateAll(matches);
                return matches;
            }
        }

        public async Task<List<WebhookPublisher>> GetAsync(bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookPublisher>().Skip(offset).Take(count);
                var matches = await db.SelectAsync(query, token);
                if (matches.Any() && references.HasValue && references.Value) HydrateAll(matches);
                return matches;
            }
        }

        public List<Guid> GetKeys(int? offset = null, int? count = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookPublisher>().Select(x => x.Id).Skip(offset).Take(count);
                return db.Select<Guid>(query);
            }
        }

        public async Task<List<Guid>> GetKeysAsync(int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookPublisher>().Select(x => x.Id).Skip(offset).Take(count);
                return await db.SelectAsync<Guid>(query);
            }
        }

        public void Hydrate(WebhookPublisher model)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookPublisher>()
                    .Join<WebhookPublisher, WebhookDefinition>((provider, definition) => provider.Id == definition.WebhookPublisherId)
                    .Where(x => x.Id == model.Id);

                var webhooks = db.SelectMulti<WebhookPublisher, WebhookDefinition>(query).Select(x => x.Item2);

                if (webhooks.Any())
                {
                    if (model.Webhooks == null) model.Webhooks = new List<WebhookDefinition>();
                    if (model.Webhooks.Any())
                    {
                        var incoming = webhooks.Except(model.Webhooks);
                        model.Webhooks.AddRange(incoming);
                    }
                    else model.Webhooks.AddRange(webhooks);
                }
            }
        }

        private static Dictionary<WebhookPublisher, List<WebhookDefinition>> GetProvidersWebhooksMap(IEnumerable<Guid> keys, IDbConnection db)
        {
            var query = db.From<WebhookPublisher>()
                .Join<WebhookPublisher, WebhookDefinition>((provider, definition) => provider.Id == definition.WebhookPublisherId)
                .Where(x => keys.Contains(x.Id));

            var matches = db.SelectMulti<WebhookPublisher, WebhookDefinition>(query);
            return matches.GroupBy(x => x.Item1).ToDictionary(g => g.Key, g => g.Select(tuple => tuple.Item2).ToList());
        }

        private static async Task<Dictionary<WebhookPublisher, List<WebhookDefinition>>> GetProvidersWebhooksMapAsync(IEnumerable<Guid> keys, IDbConnection db, CancellationToken token)
        {
            var query = db.From<WebhookPublisher>()
                .Join<WebhookPublisher, WebhookDefinition>((provider, definition) => provider.Id == definition.WebhookPublisherId)
                .Where(x => keys.Contains(x.Id));

            var matches = await db.SelectMultiAsync<WebhookPublisher, WebhookDefinition>(query, token);
            return matches.GroupBy(x => x.Item1).ToDictionary(g => g.Key, g => g.Select(tuple => tuple.Item2).ToList());
        }

        public void HydrateAll(IEnumerable<WebhookPublisher> models)
        {
            var keys = models.Select(x => x.Id);
            using (var db = factory.OpenDbConnection())
            {
                var map = GetProvidersWebhooksMap(keys, db);
                foreach (var model in models)
                {
                    if (model.Webhooks == null) model.Webhooks = new List<WebhookDefinition>();
                    if (map.TryGetValue(model, out List<WebhookDefinition> webhooks))
                    {
                        if (!webhooks.Any()) continue;
                        if (model.Webhooks.Any())
                        {
                            var incoming = webhooks.Except(model.Webhooks);
                            model.Webhooks.AddRange(incoming);
                        }
                        else model.Webhooks.AddRange(webhooks);
                    }
                }
            }
        }

        public async Task HydrateAllAsync(IEnumerable<WebhookPublisher> models, CancellationToken token = default)
        {
            var keys = models.Select(x => x.Id);
            using (var db = await factory.OpenAsync(token))
            {
                var map = await GetProvidersWebhooksMapAsync(keys, db, token);
                foreach (var model in models)
                {
                    if (model.Webhooks == null) model.Webhooks = new List<WebhookDefinition>();
                    if (map.TryGetValue(model, out List<WebhookDefinition> webhooks))
                    {
                        if (!webhooks.Any()) continue;
                        if (model.Webhooks.Any())
                        {
                            var incoming = webhooks.Except(model.Webhooks);
                            model.Webhooks.AddRange(incoming);
                        }
                        else model.Webhooks.AddRange(webhooks);
                    }
                }
            }
        }

        public async Task HydrateAsync(WebhookPublisher model, CancellationToken token = default)
        {
            using (var db = await factory.OpenAsync(token))
            {
                var query = db.From<WebhookPublisher>()
                    .Join<WebhookPublisher, WebhookDefinition>((provider, definition) => provider.Id == definition.WebhookPublisherId)
                    .Where(x => x.Id == model.Id);

                var webhooks = (await db.SelectMultiAsync<WebhookPublisher, WebhookDefinition>(query)).Select(x => x.Item2);

                if (webhooks.Any())
                {
                    if (model.Webhooks == null) model.Webhooks = new List<WebhookDefinition>();
                    if (model.Webhooks.Any())
                    {
                        var incoming = webhooks.Except(model.Webhooks);
                        model.Webhooks.AddRange(incoming);
                    }
                    else model.Webhooks.AddRange(webhooks);
                }
            }
        }

        public void Restore(WebhookPublisher model, bool? references = null)
        {
            model.IsDeleted = false;
            if (references.HasValue
                && references.Value
                && model.Webhooks != null
                && model.Webhooks.Any())
            {
                webhookDefinitionRepository.RestoreAll(model.Webhooks, references);
            }
            Save(model, references);
        }

        public void RestoreAll(IEnumerable<WebhookPublisher> models, bool? references = null)
        {
            if (references.HasValue && references.Value)
            {
                var webhooks = new List<WebhookDefinition>();
                foreach (var model in models)
                {
                    model.IsDeleted = false;
                    if (model.Webhooks != null && model.Webhooks.Any())
                        webhooks.AddRange(model.Webhooks);
                }
                if (webhooks.Any()) webhookDefinitionRepository.RestoreAll(webhooks, references);
            }
            SaveAll(models, references);
        }

        public async Task RestoreAllAsync(IEnumerable<WebhookPublisher> models, bool? references = null, CancellationToken token = default)
        {
            if (references.HasValue && references.Value)
            {
                var webhooks = new List<WebhookDefinition>();
                foreach (var model in models)
                {
                    model.IsDeleted = false;
                    if (model.Webhooks != null && model.Webhooks.Any())
                        webhooks.AddRange(model.Webhooks);
                }
                if (webhooks.Any()) await webhookDefinitionRepository.RestoreAllAsync(webhooks, references, token);
            }
            await SaveAllAsync(models, references, token);
        }

        public List<WebhookPublisher> RestoreAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var matches = FindAllByKeys(keys, references, offset, count);
            if (matches.Any()) RestoreAll(matches, references);
            return matches;
        }

        public async Task<List<WebhookPublisher>> RestoreAllByKeysAsync(
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

        public async Task RestoreAsync(WebhookPublisher model, bool? references = null)
        {
            model.IsDeleted = false;
            if (references.HasValue
                && references.Value
                && model.Webhooks != null
                && model.Webhooks.Any())
            {
                await webhookDefinitionRepository.RestoreAllAsync(model.Webhooks, references);
            }
            await SaveAsync(model, references);
        }

        public WebhookPublisher RestoreByKey(Guid key, bool? references = null)
        {
            var match = FindByKey(key, references);
            if (match != null) Restore(match, references);
            return match;
        }

        public async Task<WebhookPublisher> RestoreByKeyAsync(Guid key, bool? references = null)
        {
            var match = await FindByKeyAsync(key, references);
            if (match != null) await RestoreAsync(match, references);
            return match;
        }

        public bool Save(WebhookPublisher model, bool? references = true)
        {
            var inserted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                using (var db = factory.OpenDbConnection())
                {
                    inserted = db.Save(model, references ?? false);
                }
                scope.Complete();
            }
            return inserted;
        }

        public long SaveAll(IEnumerable<WebhookPublisher> models, bool? references = true)
        {
            var inserts = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                using (var db = factory.OpenDbConnection())
                {
                    inserts = db.SaveAll(models);
                    if (references.HasValue && references.Value)
                    {
                        var map = models.ToDictionary(model => model, model => model.Webhooks);
                        if (map.Any()) //references available
                        {
                            var definitions = map.SelectMany(x => x.Value);
                            var keys = definitions.Select(x => x.Id);
                            var matches = webhookDefinitionRepository.FindAllByKeys(keys, references: false);

                            var @in = definitions.Except(matches); //new references
                            var same = definitions.Intersect(matches); //same references
                            var @out = matches.Except(definitions); //stale references

                            if (@in.Any()) webhookDefinitionRepository.SaveAll(@in, references: false); //save new references
                            if (same.Any()) webhookDefinitionRepository.SaveAll(same, references: false); //update existing references
                            if (@out.Any()) webhookDefinitionRepository.EraseAll(@out); //erase stale references
                        }
                        else //references not available: delete all former joins
                        {
                            var keys = models.Select(x => x.Id);
                            var query = db
                                .From<WebhookDefinition>()
                                .Join<WebhookPublisher, WebhookDefinition>((provider, definition) => provider.Id == definition.WebhookPublisherId)
                                .Where(x => keys.Contains(x.WebhookPublisherId));
                            var remote = db.Select(query);
                            if (remote.Any()) webhookDefinitionRepository.EraseAll(remote);
                        }
                    }
                }
                scope.Complete();
            }
            return inserts;
        }

        public async Task<long> SaveAllAsync(IEnumerable<WebhookPublisher> models, bool? references = true, CancellationToken token = default)
        {
            var inserts = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                using (var db = factory.OpenDbConnection())
                {
                    inserts = await db.SaveAllAsync(models);
                    if (references.HasValue && references.Value)
                    {
                        var map = models.ToDictionary(model => model, model => model.Webhooks);

                        if (map.Any()) //references available
                        {
                            var webhooks = map.SelectMany(x => x.Value);
                            var keys = webhooks.Select(x => x.Id);
                            var matches = await webhookDefinitionRepository.FindAllByKeysAsync(keys, references: false);

                            var @in = webhooks.Except(matches); //new references
                            var same = webhooks.Intersect(matches); //same references
                            var @out = matches.Except(webhooks); //stale references

                            if (@in.Any()) await webhookDefinitionRepository.SaveAllAsync(@in, references: false); //save new references
                            if (same.Any()) await webhookDefinitionRepository.SaveAllAsync(same, references: false); //update existing references
                            if (@out.Any()) await webhookDefinitionRepository.EraseAllAsync(@out); //erase stale references
                        }
                        else //references not available: delete all former joins
                        {
                            var keys = models.Select(x => x.Id);
                            var query = db
                                .From<WebhookDefinition>()
                                .Join<WebhookPublisher, WebhookDefinition>((provider, definition) => provider.Id == definition.WebhookPublisherId)
                                .Where(x => keys.Contains(x.WebhookPublisherId));
                            var remote = db.Select(query);
                            if (remote.Any()) webhookDefinitionRepository.EraseAll(remote);
                        }
                    }
                }
                scope.Complete();
            }
            return inserts;
        }

        public async Task<bool> SaveAsync(WebhookPublisher model, bool? references = true, CancellationToken token = default)
        {
            var inserted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                using (var db = factory.OpenDbConnection())
                {
                    inserted = await db.SaveAsync(model, references ?? false);
                }
                scope.Complete();
            }
            return inserted;
        }

        public void Trash(WebhookPublisher model, bool? references = null)
        {
            model.IsDeleted = true;
            if (references.HasValue
                && references.Value
                && model.Webhooks != null
                && model.Webhooks.Any())
            {
                webhookDefinitionRepository.TrashAll(model.Webhooks, references);
            }
            Save(model, references);
        }

        public void TrashAll(IEnumerable<WebhookPublisher> models, bool? references = null)
        {
            if (references.HasValue && references.Value)
            {
                var webhooks = new List<WebhookDefinition>();
                foreach (var model in models)
                {
                    model.IsDeleted = true;
                    if (model.Webhooks != null && model.Webhooks.Any())
                        webhooks.AddRange(model.Webhooks);
                }
                if (webhooks.Any()) webhookDefinitionRepository.TrashAll(webhooks, references);
            }
            SaveAll(models, references);
        }

        public async Task TrashAllAsync(IEnumerable<WebhookPublisher> models, bool? references = null, CancellationToken token = default)
        {
            if (references.HasValue && references.Value)
            {
                var webhooks = new List<WebhookDefinition>();
                foreach (var model in models)
                {
                    model.IsDeleted = true;
                    if (model.Webhooks != null && model.Webhooks.Any())
                        webhooks.AddRange(model.Webhooks);
                }
                if (webhooks.Any()) await webhookDefinitionRepository.TrashAllAsync(webhooks, references, token);
            }
            await SaveAllAsync(models, references, token);
        }

        public List<WebhookPublisher> TrashAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var matches = FindAllByKeys(keys, references, offset, count);
            if (matches.Any()) TrashAll(matches, references);
            return matches;
        }

        public async Task<List<WebhookPublisher>> TrashAllByKeysAsync(
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

        public async Task TrashAsync(WebhookPublisher model, bool? references = null)
        {
            model.IsDeleted = true;
            if (references.HasValue
                && references.Value
                && model.Webhooks != null
                && model.Webhooks.Any())
            {
                await webhookDefinitionRepository.TrashAllAsync(model.Webhooks, references);
            }
            await SaveAsync(model, references);
        }

        public WebhookPublisher TrashByKey(Guid key, bool? references = null)
        {
            var match = FindByKey(key, references);
            if (match != null) Trash(match, references);
            return match;
        }

        public async Task<WebhookPublisher> TrashByKeyAsync(Guid key, bool? references = null, CancellationToken token = default)
        {
            var match = await FindByKeyAsync(key, references).ConfigureAwait(false);
            if (match != null) await TrashAsync(match, references).ConfigureAwait(false);
            return match;
        }
    }
}