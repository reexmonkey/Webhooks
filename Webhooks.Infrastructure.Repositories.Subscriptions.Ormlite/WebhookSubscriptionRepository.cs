using reexmonkey.xmisc.backbone.repositories.contracts.extensions;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Repositories.Contracts;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Reexmonkey.Webhooks.Core.Repositories.Ormlite
{
    public class WebhookSubscriptionRepository : IWebhookSubscriptionRepository
    {
        private readonly IDbConnectionFactory factory;

        public WebhookSubscriptionRepository(IDbConnectionFactory factory)
        {
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
                return db.Select(query);
            }
        }

        public async Task<List<WebhookSubscription>> FindAllAsync(Expression<Func<WebhookSubscription, bool>> predicate, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookSubscription>().Where(predicate).Skip(offset).Take(count);
                return await db.SelectAsync(query, token);
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
                return db.SingleById<WebhookSubscription>(key);
            }
        }

        public async Task<WebhookSubscription> FindByKeyAsync(Guid key, bool? references = null, CancellationToken token = default)
        {
            using (var db = await factory.OpenAsync(token))
            {
                return await db.SingleByIdAsync<WebhookSubscription>(key, token);
            }
        }

        public List<WebhookSubscription> Get(bool? references = null, int? offset = null, int? count = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookSubscription>().Skip(offset).Take(count);
                return db.Select(query);
            }
        }

        public async Task<List<WebhookSubscription>> GetAsync(bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookSubscription>().Skip(offset).Take(count);
                return await db.SelectAsync(query, token);
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

        public void Restore(WebhookSubscription model, bool? references = null)
        {
            model.IsActive = true;
            Save(model, references);
        }

        public void RestoreAll(IEnumerable<WebhookSubscription> models, bool? references = null)
        {
            foreach (var model in models)
            {
                model.IsActive = true;
            }
            SaveAll(models, references);
        }

        public async Task RestoreAllAsync(IEnumerable<WebhookSubscription> models, bool? references = null, CancellationToken token = default)
        {
            foreach (var model in models)
            {
                model.IsActive = true;
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
            model.IsActive = true;
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
                }
                scope.Complete();
            }
            return inserted;
        }

        public void Trash(WebhookSubscription model, bool? references = null)
        {
            model.IsActive = false;
            Save(model, references);
        }

        public void TrashAll(IEnumerable<WebhookSubscription> models, bool? references = null)
        {
            foreach (var model in models)
            {
                model.IsActive = false;
            }
            SaveAll(models, references);
        }

        public async Task TrashAllAsync(IEnumerable<WebhookSubscription> models, bool? references = null, CancellationToken token = default)
        {
            foreach (var model in models)
            {
                model.IsActive = false;
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