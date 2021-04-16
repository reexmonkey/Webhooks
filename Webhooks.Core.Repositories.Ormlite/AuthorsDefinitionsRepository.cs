using reexmonkey.xmisc.backbone.repositories.contracts.extensions;
using Reexmonkey.Webhooks.Core.Repositories.Contracts;
using Reexmonkey.Webhooks.Core.Repositories.Contracts.Joins;
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
    public class AuthorsDefinitionsRepository : IAuthorsDefinitionsRepository
    {
        private readonly IDbConnectionFactory factory;

        public AuthorsDefinitionsRepository(IDbConnectionFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public bool ContainsKey(long key)
        {
            using (var db = factory.OpenDbConnection())
            {
                return db.Count<AuthorsDefinitions>(q => q.Id == key) != 0L;
            }
        }

        public async Task<bool> ContainsKeyAsync(long key, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                token.ThrowIfCancellationRequested();
                return await db.CountAsync<AuthorsDefinitions>(q => q.Id == key) != 0L;
            }
        }

        public bool ContainsKeys(IEnumerable<long> keys, bool strict = true)
        {
            using (var db = factory.OpenDbConnection())
            {
                var count = db.Count<AuthorsDefinitions>(q => keys.Contains(q.Id));
                return strict ? count != 0L && count == keys.Count() : count != 0L;
            }
        }

        public async Task<bool> ContainsKeysAsync(IEnumerable<long> keys, bool strict = true, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                token.ThrowIfCancellationRequested();
                var count = await db.CountAsync<AuthorsDefinitions>(q => keys.Contains(q.Id), token);
                return strict ? count != 0L && count == keys.Count() : count != 0L;
            }
        }

        public bool Erase(AuthorsDefinitions model) => EraseByKey(model.Id);

        public long EraseAll(IEnumerable<AuthorsDefinitions> models) => EraseAllByKeys(models.Select(q => q.Id));

        public Task<long> EraseAllAsync(IEnumerable<AuthorsDefinitions> models, CancellationToken token = default)
            => EraseAllByKeysAsync(models.Select(q => q.Id), token);

        public long EraseAllByKeys(IEnumerable<long> keys)
        {
            var deletes = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                using (var db = factory.OpenDbConnection())
                {
                    deletes = db.DeleteByIds<AuthorsDefinitions>(keys);
                }
                scope.Complete();
            }
            return deletes;
        }

        public async Task<long> EraseAllByKeysAsync(IEnumerable<long> keys, CancellationToken token = default)
        {
            var result = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                using (var db = factory.OpenDbConnection())
                {
                    result = await db.DeleteByIdsAsync<AuthorsDefinitions>(keys, token: token);
                }
                scope.Complete();
            }
            return result;
        }

        public Task<bool> EraseAsync(AuthorsDefinitions model, CancellationToken token = default) => EraseByKeyAsync(model.Id, token);

        public bool EraseByKey(long key)
        {
            int deletes = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                using (var db = factory.OpenDbConnection())
                {
                    deletes = db.DeleteById<AuthorsDefinitions>(key);
                }
                scope.Complete();
            }
            return deletes != 0;
        }

        public async Task<bool> EraseByKeyAsync(long key, CancellationToken token = default)
        {
            int deletes = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                using (var db = factory.OpenDbConnection())
                {
                    deletes = await db.DeleteByIdAsync<AuthorsDefinitions>(key);
                }
                scope.Complete();
            }
            return deletes != 0;
        }

        public List<AuthorsDefinitions> FindAll(Expression<Func<AuthorsDefinitions, bool>> predicate, bool? references = null, int? offset = null, int? count = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<AuthorsDefinitions>().Where(predicate).Skip(offset).Take(count);
                return db.Select(query);
            }
        }

        public async Task<List<AuthorsDefinitions>> FindAllAsync(Expression<Func<AuthorsDefinitions, bool>> predicate, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<AuthorsDefinitions>().Where(predicate).Skip(offset).Take(count);
                return await db.SelectAsync(query, token);
            }
        }

        public List<AuthorsDefinitions> FindAllByKeys(IEnumerable<long> keys, bool? references = null, int? offset = null, int? count = null)
            => FindAll(x => keys.Contains(x.Id), references, offset, count);

        public Task<List<AuthorsDefinitions>> FindAllByKeysAsync(IEnumerable<long> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
            => FindAllAsync(x => keys.Contains(x.Id), references, offset, count, token);

        public AuthorsDefinitions FindByKey(long key, bool? references = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                return db.SingleById<AuthorsDefinitions>(key);
            }
        }

        public async Task<AuthorsDefinitions> FindByKeyAsync(long key, bool? references = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                return await db.SingleByIdAsync<AuthorsDefinitions>(key, token);
            }
        }

        public List<AuthorsDefinitions> Get(bool? references = null, int? offset = null, int? count = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<AuthorsDefinitions>().Skip(offset).Take(count);
                return db.Select(query);
            }
        }

        public async Task<List<AuthorsDefinitions>> GetAsync(bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<AuthorsDefinitions>().Skip(offset).Take(count);
                return await db.SelectAsync(query, token);
            }
        }

        public List<long> GetKeys(int? offset = null, int? count = null)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<AuthorsDefinitions>().Select(x => x.Id).Skip(offset).Take(count);
                return db.Select<long>(query);
            }
        }

        public async Task<List<long>> GetKeysAsync(int? offset = null, int? count = null, CancellationToken token = default)
        {
            using (var db = factory.OpenDbConnection())
            {
                var query = db.From<AuthorsDefinitions>().Select(x => x.Id).Skip(offset).Take(count);
                return await db.SelectAsync<long>(query);
            }
        }

        public bool Save(AuthorsDefinitions model, bool? references = true)
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

        public long SaveAll(IEnumerable<AuthorsDefinitions> models, bool? references = true)
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

        public async Task<long> SaveAllAsync(IEnumerable<AuthorsDefinitions> models, bool? references = true, CancellationToken token = default)
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

        public async Task<bool> SaveAsync(AuthorsDefinitions model, bool? references = true, CancellationToken token = default)
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
    }
}