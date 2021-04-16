using MongoDB.Driver;
using reexmonkey.xmisc.backbone.repositories.contracts.extensions;
using Reexmonkey.Webhooks.Core.Repositories.Contracts;
using Reexmonkey.Webhooks.Core.Repositories.Contracts.Joins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Reexmonkey.Webhooks.Core.Repositories.MongoDb
{
    public class AuthorsDefinitionsRepository : IAuthorsDefinitionsRepository
    {
        private readonly IMongoCollection<AuthorsDefinitions> collection;

        public AuthorsDefinitionsRepository(IMongoDatabase database, MongoCollectionSettings settings = null)
        {
            if (database is null) throw new ArgumentNullException(nameof(database));
            collection = database.GetCollection<AuthorsDefinitions>(nameof(AuthorsDefinitions), settings);
        }

        public bool ContainsKey(long key)
        {
            return collection.Find(x => x.Id == key).FirstOrDefault() != null;
        }

        public async Task<bool> ContainsKeyAsync(long key, CancellationToken token = default)
        {
            return (await collection.FindAsync(x => x.Id == key, cancellationToken: token)).FirstOrDefault() != null;
        }

        public bool ContainsKeys(IEnumerable<long> keys, bool strict = true)
        {
            var filter = Builders<AuthorsDefinitions>.Filter.In(x => x.Id, keys);
            var results = collection.Find(filter).ToList();
            return strict ? results.Count == keys.Count() : results.Any();
        }

        public async Task<bool> ContainsKeysAsync(IEnumerable<long> keys, bool strict = true, CancellationToken token = default)
        {
            var filter = Builders<AuthorsDefinitions>.Filter.In(x => x.Id, keys);
            var results = await (await collection.FindAsync(filter, cancellationToken: token)).ToListAsync(token);
            return strict ? results.Count == keys.Count() : results.Any();
        }

        public bool Erase(AuthorsDefinitions model)
        {
            return EraseByKey(model.Id);
        }

        public long EraseAll(IEnumerable<AuthorsDefinitions> models)
        {
            return EraseAllByKeys(models.Select(x => x.Id));
        }

        public Task<long> EraseAllAsync(IEnumerable<AuthorsDefinitions> models, CancellationToken token = default)
        {
            return EraseAllByKeysAsync(models.Select(x => x.Id), token);
        }

        public long EraseAllByKeys(IEnumerable<long> keys)
        {
            var deleted = 0L;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var filter = Builders<AuthorsDefinitions>.Filter.In(x => x.Id, keys);
                var result = collection.DeleteMany(filter);
                deleted = result.IsAcknowledged ? result.DeletedCount : 0L;
                scope.Complete();
            }
            return deleted;
        }

        public async Task<long> EraseAllByKeysAsync(IEnumerable<long> keys, CancellationToken token = default)
        {
            var deleted = 0L;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                var filter = Builders<AuthorsDefinitions>.Filter.In(x => x.Id, keys);
                var result = await collection.DeleteManyAsync(filter, token);
                deleted = result.IsAcknowledged ? result.DeletedCount : 0L;
                scope.Complete();
            }
            return deleted;
        }

        public Task<bool> EraseAsync(AuthorsDefinitions model, CancellationToken token = default)
        {
            return EraseByKeyAsync(model.Id, token);
        }

        public bool EraseByKey(long key)
        {
            var deleted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var filter = Builders<AuthorsDefinitions>.Filter.Eq(x => x.Id, key);
                var result = collection.DeleteOne(filter);
                deleted = result.IsAcknowledged && result.DeletedCount > 0;
                scope.Complete();
            }
            return deleted;
        }

        public async Task<bool> EraseByKeyAsync(long key, CancellationToken token = default)
        {
            var deleted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                var filter = Builders<AuthorsDefinitions>.Filter.Eq(x => x.Id, key);
                var result = await collection.DeleteOneAsync(filter, token);

                deleted = result.IsAcknowledged && result.DeletedCount > 0;
                scope.Complete();
            }
            return deleted;
        }

        public List<AuthorsDefinitions> FindAll(Expression<Func<AuthorsDefinitions, bool>> predicate, bool? references = null, int? offset = null, int? count = null)
        {
            return collection.Find(predicate).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<AuthorsDefinitions>> FindAllAsync(Expression<Func<AuthorsDefinitions, bool>> predicate, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<AuthorsDefinitions>() { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(predicate, options, token))
                return await cursor.ToListAsync(token);
        }

        public List<AuthorsDefinitions> FindAllByKeys(IEnumerable<long> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var filter = Builders<AuthorsDefinitions>.Filter.In(x => x.Id, keys);
            return collection.Find(filter).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<AuthorsDefinitions>> FindAllByKeysAsync(IEnumerable<long> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var filter = Builders<AuthorsDefinitions>.Filter.In(x => x.Id, keys);
            var options = new FindOptions<AuthorsDefinitions>() { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(filter, options, token))
                return await cursor.ToListAsync(token);
        }

        public AuthorsDefinitions FindByKey(long key, bool? references = null)
        {
            return collection.Find(x => x.Id == key).FirstOrDefault();
        }

        public async Task<AuthorsDefinitions> FindByKeyAsync(long key, bool? references = null, CancellationToken token = default)
        {
            using (var cursor = await collection.FindAsync(x => x.Id == key, cancellationToken: token))
                return cursor.FirstOrDefault(token);
        }

        public List<AuthorsDefinitions> Get(bool? references = null, int? offset = null, int? count = null)
        {
            return collection.Find(Builders<AuthorsDefinitions>.Filter.Empty).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<AuthorsDefinitions>> GetAsync(bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<AuthorsDefinitions> { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(Builders<AuthorsDefinitions>.Filter.Empty, options, token))
                return cursor.ToList(token);
        }

        public List<long> GetKeys(int? offset = null, int? count = null)
        {
            return collection
                .Find(Builders<AuthorsDefinitions>.Filter.Empty)
                .Project(Builders<AuthorsDefinitions>.Projection.Expression(x => x.Id))
                .Skip(offset)
                .Limit(count)
                .ToList();
        }

        public async Task<List<long>> GetKeysAsync(int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<AuthorsDefinitions, long>
            {
                Projection = Builders<AuthorsDefinitions>.Projection.Expression(x => x.Id),
                Skip = offset,
                Limit = count
            };
            using (var cursor = await collection.FindAsync(Builders<AuthorsDefinitions>.Filter.Empty, options, token))
                return await cursor.ToListAsync();
        }

        public bool Save(AuthorsDefinitions model, bool? references = true)
        {
            var persisted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var result = collection.ReplaceOne(x => x.Id == model.Id, model, new ReplaceOptions { IsUpsert = true });
                persisted = result.IsAcknowledged;
                scope.Complete();
            }
            return persisted;
        }

        public long SaveAll(IEnumerable<AuthorsDefinitions> models, bool? references = true)
        {
            var saves = 0L;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var keys = models.Where(model => model != null).Select(x => x.Id);
                var matches = FindAllByKeys(keys, references: references);

                var similar = models.Intersect(matches);
                var different = matches.Any() ? models.Except(matches) : models;

                var requests = new List<WriteModel<AuthorsDefinitions>>();
                if (similar.Any())
                {
                    foreach (var model in similar)
                        requests.Add(new ReplaceOneModel<AuthorsDefinitions>(Builders<AuthorsDefinitions>.Filter.Where(x => x.Id == model.Id), model));
                    var result = collection.BulkWrite(requests);
                    saves += result.IsAcknowledged ? result.MatchedCount : 0L;
                }

                if (different.Any())
                {
                    requests.Clear();
                    foreach (var model in different)
                        requests.Add(new InsertOneModel<AuthorsDefinitions>(model));
                    var results = collection.BulkWrite(requests);
                    saves += results.IsAcknowledged ? results.InsertedCount : 0L;
                }
                scope.Complete();
            }
            return saves;
        }

        public async Task<long> SaveAllAsync(IEnumerable<AuthorsDefinitions> models, bool? references = true, CancellationToken token = default)
        {
            var saves = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                var keys = models.Where(model => model != null).Select(x => x.Id);
                var matches = await FindAllByKeysAsync(keys, references: references, token: token);
                var similar = models.Intersect(matches);
                var different = matches.Any() ? models.Except(matches) : models;

                if (similar.Any())
                {
                    var requests = new List<WriteModel<AuthorsDefinitions>>();
                    foreach (var model in similar)
                    {
                        token.ThrowIfCancellationRequested();
                        requests.Add(new ReplaceOneModel<AuthorsDefinitions>(Builders<AuthorsDefinitions>.Filter.Where(x => x.Id == model.Id), model));
                    }
                    var result = await collection.BulkWriteAsync(requests, cancellationToken: token);
                    saves += result.IsAcknowledged ? (int)result.MatchedCount : 0;
                }

                if (different.Any())
                {
                    var requests = new List<WriteModel<AuthorsDefinitions>>();
                    foreach (var model in different)
                    {
                        token.ThrowIfCancellationRequested();
                        requests.Add(new InsertOneModel<AuthorsDefinitions>(model));
                    }
                    var result = await collection.BulkWriteAsync(requests, cancellationToken: token);
                    saves += result.IsAcknowledged ? (int)result.InsertedCount : 0;
                }
                scope.Complete();
            }
            return saves;
        }

        public async Task<bool> SaveAsync(AuthorsDefinitions model, bool? references = true, CancellationToken token = default)
        {
            var persisted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                var result = await collection.ReplaceOneAsync(x => x.Id == model.Id, model, new ReplaceOptions { IsUpsert = true });
                persisted = result.IsAcknowledged;
                scope.Complete();
            }
            return persisted;
        }
    }
}