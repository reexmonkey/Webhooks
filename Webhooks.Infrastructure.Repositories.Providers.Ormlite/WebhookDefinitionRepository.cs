using reexmonkey.xmisc.backbone.repositories.contracts.extensions;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
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
    public class WebhookDefinitionRepository : IWebhookDefinitionRepository
    {
        private readonly IDbConnectionFactory factory;

        public WebhookDefinitionRepository(IDbConnectionFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public bool ContainsKey(Guid key)
        {
            using (var db = factory.OpenDbConnection())
            {
                return db.Count<WebhookDefinition>(q => q.Id == key) != 0L;
            }
        }

        public async Task<bool> ContainsKeyAsync(Guid key, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                token.ThrowIfCancellationRequested();
                return await db.CountAsync<WebhookDefinition>(q => q.Id == key) != 0L;
            }
        }

        public bool ContainsKeys(IEnumerable<Guid> keys, bool strict = true)
        {
            using (var db = factory.OpenDbConnection())
            {
                var count = db.Count<WebhookDefinition>(q => keys.Contains(q.Id));
                return strict ? count != 0L && count == keys.Count() : count != 0L;
            }
        }

        public async Task<bool> ContainsKeysAsync(IEnumerable<Guid> keys, bool strict = true, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                token.ThrowIfCancellationRequested();
                var count = await db.CountAsync<WebhookDefinition>(q => keys.Contains(q.Id), token);
                return strict ? count != 0L && count == keys.Count() : count != 0L;
            }
        }

        public bool Erase(WebhookDefinition model) => EraseByKey(model.Id);

        public long EraseAll(IEnumerable<WebhookDefinition> models) => EraseAllByKeys(models.Select(q => q.Id));

        public Task<long> EraseAllAsync(IEnumerable<WebhookDefinition> models, CancellationToken token = default)
            => EraseAllByKeysAsync(models.Select(q => q.Id), token);

        public long EraseAllByKeys(IEnumerable<Guid> keys)
        {
            var deletes = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                using (var db = factory.OpenDbConnection())
                {
                    deletes = db.DeleteByIds<WebhookDefinition>(keys);
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
                    result = await db.DeleteByIdsAsync<WebhookDefinition>(keys, token: token);
                }
                scope.Complete();
            }
            return result;
        }

        public Task<bool> EraseAsync(WebhookDefinition model, CancellationToken token = default) => EraseByKeyAsync(model.Id, token);

        public bool EraseByKey(Guid key)
        {
            int deletes = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                using (var db = factory.OpenDbConnection())
                {
                    deletes = db.DeleteById<WebhookDefinition>(key);
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
                    deletes = await db.DeleteByIdAsync<WebhookDefinition>(key);
                }
                scope.Complete();
            }
            return deletes != 0;
        }

        public List<WebhookDefinition> FindAll(Expression<Func<WebhookDefinition, bool>> predicate, bool? references = null, int? offset = null, int? count = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookDefinition>().Where(predicate).Skip(offset).Take(count);
                return db.Select(query);
            }
        }

        public async Task<List<WebhookDefinition>> FindAllAsync(Expression<Func<WebhookDefinition, bool>> predicate, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookDefinition>().Where(predicate).Skip(offset).Take(count);
                return await db.SelectAsync(query, token);
            }
        }

        public List<WebhookDefinition> FindAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
            => FindAll(x => keys.Contains(x.Id), false, offset, count);

        public Task<List<WebhookDefinition>> FindAllByKeysAsync(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
            => FindAllAsync(x => keys.Contains(x.Id), false, offset, count, token);

        public WebhookDefinition FindByKey(Guid key, bool? references = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                return db.SingleById<WebhookDefinition>(key);
            }
        }

        public async Task<WebhookDefinition> FindByKeyAsync(Guid key, bool? references = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                return await db.SingleByIdAsync<WebhookDefinition>(key, token);
            }
        }

        public List<WebhookDefinition> Get(bool? references = null, int? offset = null, int? count = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookDefinition>().Skip(offset).Take(count);
                return db.Select(query);
            }
        }

        public async Task<List<WebhookDefinition>> GetAsync(bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookDefinition>().Skip(offset).Take(count);
                return await db.SelectAsync(query, token);
            }
        }

        public List<Guid> GetKeys(int? offset = null, int? count = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookDefinition>().Select(x => x.Id).Skip(offset).Take(count);
                return db.Select<Guid>(query);
            }
        }

        public async Task<List<Guid>> GetKeysAsync(int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<WebhookDefinition>().Select(x => x.Id).Skip(offset).Take(count);
                return await db.SelectAsync<Guid>(query);
            }
        }

        public void Restore(WebhookDefinition model, bool? references = null)
        {
            model.IsDeleted = false;
            Save(model, references ?? false);
        }

        public void RestoreAll(IEnumerable<WebhookDefinition> models, bool? references = null)
        {
            foreach (var model in models)
            {
                model.IsDeleted = false;
            }
            SaveAll(models, references ?? false);
        }

        public Task RestoreAllAsync(IEnumerable<WebhookDefinition> models, bool? references = null, CancellationToken token = default)
        {
            foreach (var model in models)
            {
                model.IsDeleted = false;
            }
            return SaveAllAsync(models, references ?? false);
        }

        public List<WebhookDefinition> RestoreAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var matches = FindAllByKeys(keys, references ?? false, offset, count);
            if (matches.Any()) RestoreAll(matches, references ?? false);
            return matches;
        }

        public async Task<List<WebhookDefinition>> RestoreAllByKeysAsync(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var matches = await FindAllByKeysAsync(keys, references ?? false, offset, count, token);
            if (matches.Any()) await RestoreAllAsync(matches, false, token);
            return matches;
        }

        public Task RestoreAsync(WebhookDefinition model, bool? references = null)
        {
            model.IsDeleted = false;
            return SaveAsync(model, references ?? false);
        }

        public WebhookDefinition RestoreByKey(Guid key, bool? references = null)
        {
            var match = FindByKey(key, references ?? false);
            if (match != null) Restore(match, references ?? false);
            return match;
        }

        public async Task<WebhookDefinition> RestoreByKeyAsync(Guid key, bool? references = null)
        {
            var match = await FindByKeyAsync(key, references ?? false);
            if (match != null) await RestoreAsync(match, references ?? false);
            return match;
        }

        public bool Save(WebhookDefinition model, bool? references = true)
        {
            var inserted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                using (var db = factory.OpenDbConnection())
                {
                    inserted = db.Save(model, references.Value);
                }
                scope.Complete();
            }
            return inserted;
        }

        public long SaveAll(IEnumerable<WebhookDefinition> models, bool? references = true)
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

        public async Task<long> SaveAllAsync(IEnumerable<WebhookDefinition> models, bool? references = true, CancellationToken token = default)
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

        public async Task<bool> SaveAsync(WebhookDefinition model, bool? references = true, CancellationToken token = default)
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

        public void Trash(WebhookDefinition model, bool? references = null)
        {
            model.IsDeleted = true;
            Save(model, references ?? false);
        }

        public void TrashAll(IEnumerable<WebhookDefinition> models, bool? references = null)
        {
            foreach (var model in models)
            {
                model.IsDeleted = true;
            }
            SaveAll(models, references ?? false);
        }

        public Task TrashAllAsync(IEnumerable<WebhookDefinition> models, bool? references = null, CancellationToken token = default)
        {
            foreach (var model in models)
            {
                model.IsDeleted = true;
            }
            return SaveAllAsync(models, references ?? false);
        }

        public List<WebhookDefinition> TrashAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var matches = FindAllByKeys(keys, references ?? false, offset, count);
            if (matches.Any()) TrashAll(matches, references ?? false);
            return matches;
        }

        public async Task<List<WebhookDefinition>> TrashAllByKeysAsync(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var matches = await FindAllByKeysAsync(keys, references ?? false, offset, count, token);
            if (matches.Any()) await TrashAllAsync(matches, references ?? false, token);
            return matches;
        }

        public Task TrashAsync(WebhookDefinition model, bool? references = null)
        {
            model.IsDeleted = true;
            return SaveAsync(model, references ?? false);
        }

        public WebhookDefinition TrashByKey(Guid key, bool? references = null)
        {
            var match = FindByKey(key, references ?? false);
            if (match != null) Trash(match, references ?? false);
            return match;
        }

        public async Task<WebhookDefinition> TrashByKeyAsync(Guid key, bool? references = null, CancellationToken token = default)
        {
            var match = await FindByKeyAsync(key, references ?? false);
            if (match != null) await TrashAsync(match, references ?? false);
            return match;
        }
    }
}