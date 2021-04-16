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
    public class SubscriptionsDefinitionsRepository : ISubscriptionsDefinitionsRepository
    {
        private readonly IMongoCollection<SubscriptionsDefinitions> collection;

        public SubscriptionsDefinitionsRepository(IMongoDatabase database, MongoCollectionSettings settings = null)
        {
            if (database is null) throw new ArgumentNullException(nameof(database));
            collection = database.GetCollection<SubscriptionsDefinitions>(nameof(SubscriptionsDefinitions), settings);
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
            var filter = Builders<SubscriptionsDefinitions>.Filter.In(x => x.Id, keys);
            var results = collection.Find(filter).ToList();
            return strict ? results.Count == keys.Count() : results.Any();
        }

        public async Task<bool> ContainsKeysAsync(IEnumerable<long> keys, bool strict = true, CancellationToken token = default)
        {
            var filter = Builders<SubscriptionsDefinitions>.Filter.In(x => x.Id, keys);
            var results = await (await collection.FindAsync(filter, cancellationToken: token)).ToListAsync(token);
            return strict ? results.Count == keys.Count() : results.Any();
        }

        public bool Erase(SubscriptionsDefinitions model)
        {
            return EraseByKey(model.Id);
        }

        public long EraseAll(IEnumerable<SubscriptionsDefinitions> models)
        {
            return EraseAllByKeys(models.Select(x => x.Id));
        }

        public Task<long> EraseAllAsync(IEnumerable<SubscriptionsDefinitions> models, CancellationToken token = default)
        {
            return EraseAllByKeysAsync(models.Select(x => x.Id), token);
        }

        public long EraseAllByKeys(IEnumerable<long> keys)
        {
            var deleted = 0L;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var filter = Builders<SubscriptionsDefinitions>.Filter.In(x => x.Id, keys);
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
                var filter = Builders<SubscriptionsDefinitions>.Filter.In(x => x.Id, keys);
                var result = await collection.DeleteManyAsync(filter, token);
                deleted = result.IsAcknowledged ? result.DeletedCount : 0L;
                scope.Complete();
            }
            return deleted;
        }

        public Task<bool> EraseAsync(SubscriptionsDefinitions model, CancellationToken token = default)
        {
            return EraseByKeyAsync(model.Id, token);
        }

        public bool EraseByKey(long key)
        {
            var deleted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var filter = Builders<SubscriptionsDefinitions>.Filter.Eq(x => x.Id, key);
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
                var filter = Builders<SubscriptionsDefinitions>.Filter.Eq(x => x.Id, key);
                var result = await collection.DeleteOneAsync(filter, token);

                deleted = result.IsAcknowledged && result.DeletedCount > 0;
                scope.Complete();
            }
            return deleted;
        }

        public List<SubscriptionsDefinitions> FindAll(Expression<Func<SubscriptionsDefinitions, bool>> predicate, bool? references = null, int? offset = null, int? count = null)
        {
            return collection.Find(predicate).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<SubscriptionsDefinitions>> FindAllAsync(Expression<Func<SubscriptionsDefinitions, bool>> predicate, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<SubscriptionsDefinitions>() { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(predicate, options, token))
                return await cursor.ToListAsync(token);
        }

        public List<SubscriptionsDefinitions> FindAllByKeys(IEnumerable<long> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var filter = Builders<SubscriptionsDefinitions>.Filter.In(x => x.Id, keys);
            return collection.Find(filter).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<SubscriptionsDefinitions>> FindAllByKeysAsync(IEnumerable<long> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var filter = Builders<SubscriptionsDefinitions>.Filter.In(x => x.Id, keys);
            var options = new FindOptions<SubscriptionsDefinitions>() { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(filter, options, token))
                return await cursor.ToListAsync(token);
        }

        public SubscriptionsDefinitions FindByKey(long key, bool? references = null)
        {
            return collection.Find(x => x.Id == key).FirstOrDefault();
        }

        public async Task<SubscriptionsDefinitions> FindByKeyAsync(long key, bool? references = null, CancellationToken token = default)
        {
            using (var cursor = await collection.FindAsync(x => x.Id == key, cancellationToken: token))
                return cursor.FirstOrDefault(token);
        }

        public List<SubscriptionsDefinitions> Get(bool? references = null, int? offset = null, int? count = null)
        {
            return collection.Find(Builders<SubscriptionsDefinitions>.Filter.Empty).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<SubscriptionsDefinitions>> GetAsync(bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<SubscriptionsDefinitions> { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(Builders<SubscriptionsDefinitions>.Filter.Empty, options, token))
                return cursor.ToList(token);
        }

        public List<long> GetKeys(int? offset = null, int? count = null)
        {
            return collection
                .Find(Builders<SubscriptionsDefinitions>.Filter.Empty)
                .Project(Builders<SubscriptionsDefinitions>.Projection.Expression(x => x.Id))
                .Skip(offset)
                .Limit(count)
                .ToList();
        }

        public async Task<List<long>> GetKeysAsync(int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<SubscriptionsDefinitions, long>
            {
                Projection = Builders<SubscriptionsDefinitions>.Projection.Expression(x => x.Id),
                Skip = offset,
                Limit = count
            };
            using (var cursor = await collection.FindAsync(Builders<SubscriptionsDefinitions>.Filter.Empty, options, token))
                return await cursor.ToListAsync();
        }

        public bool Save(SubscriptionsDefinitions model, bool? references = true)
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

        public long SaveAll(IEnumerable<SubscriptionsDefinitions> models, bool? references = true)
        {
            var saves = 0L;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var keys = models.Where(model => model != null).Select(x => x.Id);
                var matches = FindAllByKeys(keys, references: references);

                var similar = models.Intersect(matches);
                var different = matches.Any() ? models.Except(matches) : models;

                var requests = new List<WriteModel<SubscriptionsDefinitions>>();
                if (similar.Any())
                {
                    foreach (var model in similar)
                        requests.Add(new ReplaceOneModel<SubscriptionsDefinitions>(Builders<SubscriptionsDefinitions>.Filter.Where(x => x.Id == model.Id), model));
                    var result = collection.BulkWrite(requests);
                    saves += result.IsAcknowledged ? result.MatchedCount : 0L;
                }

                if (different.Any())
                {
                    requests.Clear();
                    foreach (var model in different)
                        requests.Add(new InsertOneModel<SubscriptionsDefinitions>(model));
                    var results = collection.BulkWrite(requests);
                    saves += results.IsAcknowledged ? results.InsertedCount : 0L;
                }
                scope.Complete();
            }
            return saves;
        }

        public async Task<long> SaveAllAsync(IEnumerable<SubscriptionsDefinitions> models, bool? references = true, CancellationToken token = default)
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
                    var requests = new List<WriteModel<SubscriptionsDefinitions>>();
                    foreach (var model in similar)
                    {
                        token.ThrowIfCancellationRequested();
                        requests.Add(new ReplaceOneModel<SubscriptionsDefinitions>(Builders<SubscriptionsDefinitions>.Filter.Where(x => x.Id == model.Id), model));
                    }
                    var result = await collection.BulkWriteAsync(requests, cancellationToken: token);
                    saves += result.IsAcknowledged ? (int)result.MatchedCount : 0;
                }

                if (different.Any())
                {
                    var requests = new List<WriteModel<SubscriptionsDefinitions>>();
                    foreach (var model in different)
                    {
                        token.ThrowIfCancellationRequested();
                        requests.Add(new InsertOneModel<SubscriptionsDefinitions>(model));
                    }
                    var result = await collection.BulkWriteAsync(requests, cancellationToken: token);
                    saves += result.IsAcknowledged ? (int)result.InsertedCount : 0;
                }
                scope.Complete();
            }
            return saves;
        }

        public async Task<bool> SaveAsync(SubscriptionsDefinitions model, bool? references = true, CancellationToken token = default)
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