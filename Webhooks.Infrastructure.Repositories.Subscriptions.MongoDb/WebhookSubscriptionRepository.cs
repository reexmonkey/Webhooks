using MongoDB.Driver;
using reexmonkey.xmisc.backbone.repositories.contracts.extensions;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Reexmonkey.Webhooks.Core.Repositories.MongoDb
{
    public class WebhookSubscriptionRepository : IWebhookSubscriptionRepository
    {
        private readonly IMongoDatabase database;
        private readonly MongoCollectionSettings settings;
        private readonly IMongoCollection<WebhookSubscription> collection;

        public WebhookSubscriptionRepository(IMongoDatabase database, MongoCollectionSettings settings)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            collection = database.GetCollection<WebhookSubscription>(nameof(WebhookSubscription), settings);
        }

        public bool ContainsKey(Guid key)
        {
            return collection.Find(x => x.Id == key).FirstOrDefault() != null;
        }

        public async Task<bool> ContainsKeyAsync(Guid key, CancellationToken token = default)
        {
            using (var cursor = await collection.FindAsync(x => x.Id == key, cancellationToken: token))
            {
                return cursor.FirstOrDefault() != null;
            }
        }

        public bool ContainsKeys(IEnumerable<Guid> keys, bool strict = true)
        {
            var filter = Builders<WebhookSubscription>.Filter.In(x => x.Id, keys);
            var results = collection.Find(filter).ToList();
            return strict ? results.Count == keys.Count() : results.Any();
        }

        public async Task<bool> ContainsKeysAsync(IEnumerable<Guid> keys, bool strict = true, CancellationToken token = default)
        {
            var filter = Builders<WebhookSubscription>.Filter.In(x => x.Id, keys);
            using (var cursor = await collection.FindAsync(filter, cancellationToken: token))
            {
                var results = await cursor.ToListAsync(token);
                return strict ? results.Count == keys.Count() : results.Any();
            }
        }

        public bool Erase(WebhookSubscription model)
        {
            return EraseByKey(model.Id);
        }

        public long EraseAll(IEnumerable<WebhookSubscription> models)
        {
            return EraseAllByKeys(models.Select(x => x.Id));
        }

        public Task<long> EraseAllAsync(IEnumerable<WebhookSubscription> models, CancellationToken token = default)
        {
            return EraseAllByKeysAsync(models.Select(x => x.Id), token);
        }

        public long EraseAllByKeys(IEnumerable<Guid> keys)
        {
            var deleted = 0L;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var filter = Builders<WebhookSubscription>.Filter.In(x => x.Id, keys);
                var result = collection.DeleteMany(filter);
                deleted = result.IsAcknowledged ? result.DeletedCount : 0L;
                scope.Complete();
            }
            return deleted;
        }

        public async Task<long> EraseAllByKeysAsync(IEnumerable<Guid> keys, CancellationToken token = default)
        {
            var deleted = 0L;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                var filter = Builders<WebhookSubscription>.Filter.In(x => x.Id, keys);
                var result = await collection.DeleteManyAsync(filter, token);
                deleted = result.IsAcknowledged ? result.DeletedCount : 0L;
                scope.Complete();
            }
            return deleted;
        }

        public Task<bool> EraseAsync(WebhookSubscription model, CancellationToken token = default)
        {
            return EraseByKeyAsync(model.Id, token);
        }

        public bool EraseByKey(Guid key)
        {
            var deleted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var filter = Builders<WebhookSubscription>.Filter.Eq(x => x.Id, key);
                var result = collection.DeleteOne(filter);
                deleted = result.IsAcknowledged && result.DeletedCount > 0;
                scope.Complete();
            }
            return deleted;
        }

        public async Task<bool> EraseByKeyAsync(Guid key, CancellationToken token = default)
        {
            var deleted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                var filter = Builders<WebhookSubscription>.Filter.Eq(x => x.Id, key);
                var result = await collection.DeleteOneAsync(filter, token);

                deleted = result.IsAcknowledged && result.DeletedCount > 0;
                scope.Complete();
            }
            return deleted;
        }

        public List<WebhookSubscription> FindAll(Expression<Func<WebhookSubscription, bool>> predicate, bool? references = null, int? offset = null, int? count = null)
        {
            return collection.Find(predicate).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<WebhookSubscription>> FindAllAsync(Expression<Func<WebhookSubscription, bool>> predicate, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<WebhookSubscription>() { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(predicate, options, token))
            {
                return await cursor.ToListAsync(token);
            }
        }

        public List<WebhookSubscription> FindAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var filter = Builders<WebhookSubscription>.Filter.In(x => x.Id, keys);
            return collection.Find(filter).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<WebhookSubscription>> FindAllByKeysAsync(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var filter = Builders<WebhookSubscription>.Filter.In(x => x.Id, keys);
            var options = new FindOptions<WebhookSubscription>() { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(filter, options, token))
            {
                return await cursor.ToListAsync(token);
            }
        }

        public WebhookSubscription FindByKey(Guid key, bool? references = null)
        {
            return collection.Find(x => x.Id == key).FirstOrDefault();
        }

        public async Task<WebhookSubscription> FindByKeyAsync(Guid key, bool? references = null, CancellationToken token = default)
        {
            using (var cursor = await collection.FindAsync(x => x.Id == key, cancellationToken: token))
            {
                return cursor.FirstOrDefault(token);
            }
        }

        public List<WebhookSubscription> Get(bool? references = null, int? offset = null, int? count = null)
        {
            return collection.Find(Builders<WebhookSubscription>.Filter.Empty).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<WebhookSubscription>> GetAsync(bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<WebhookSubscription> { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(Builders<WebhookSubscription>.Filter.Empty, options, token))
            {
                return cursor.ToList(token);
            }
        }

        public List<Guid> GetKeys(int? offset = null, int? count = null)
        {
            return collection
                .Find(Builders<WebhookSubscription>.Filter.Empty)
                .Project(Builders<WebhookSubscription>.Projection.Expression(x => x.Id))
                .Skip(offset)
                .Limit(count)
                .ToList();
        }

        public async Task<List<Guid>> GetKeysAsync(int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<WebhookSubscription, Guid>
            {
                Projection = Builders<WebhookSubscription>.Projection.Expression(x => x.Id),
                Skip = offset,
                Limit = count
            };

            using (var cursor = await collection.FindAsync(Builders<WebhookSubscription>.Filter.Empty, options, token))
                return await cursor.ToListAsync();
        }

        public void Restore(WebhookSubscription model, bool? references = null)
        {
            model.IsActive = false;
            Save(model, references);
        }

        public void RestoreAll(IEnumerable<WebhookSubscription> models, bool? references = null)
        {
            if (references.HasValue && references.Value)
            {
                foreach (var model in models)
                {
                    model.IsActive = false;
                }
            }
            SaveAll(models, references);
        }

        public async Task RestoreAllAsync(IEnumerable<WebhookSubscription> models, bool? references = null, CancellationToken token = default)
        {
            if (references.HasValue && references.Value)
            {
                var webhooks = new List<WebhookSubscription>();
                foreach (var model in models)
                {
                    model.IsActive = false;
                }
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

        public Task RestoreAsync(WebhookSubscription model, bool? references = null)
        {
            model.IsActive = false;
            return SaveAsync(model, references);
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
            var persisted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var result = collection.ReplaceOne(x => x.Id == model.Id, model, new ReplaceOptions { IsUpsert = true });
                persisted = result.IsAcknowledged;
                scope.Complete();
            }
            return persisted;
        }

        public long SaveAll(IEnumerable<WebhookSubscription> models, bool? references = true)
        {
            var saves = 0L;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var keys = new List<Guid>();
                foreach (var model in models)
                {
                    keys.Add(model.Id);
                }

                var matches = FindAllByKeys(keys, references: references);
                var similar = models.Intersect(matches);
                var different = matches.Any() ? models.Except(matches) : models;

                var requests = new List<WriteModel<WebhookSubscription>>();
                if (similar.Any())
                {
                    foreach (var model in similar)
                        requests.Add(new ReplaceOneModel<WebhookSubscription>(Builders<WebhookSubscription>.Filter.Where(x => x.Id == model.Id), model));
                    var result = collection.BulkWrite(requests);
                    saves += result.IsAcknowledged ? result.MatchedCount : 0L;
                }

                if (different.Any())
                {
                    requests.Clear();
                    foreach (var model in different)
                        requests.Add(new InsertOneModel<WebhookSubscription>(model));
                    var results = collection.BulkWrite(requests);
                    saves += results.IsAcknowledged ? results.InsertedCount : 0L;
                }
                scope.Complete();
            }
            return saves;
        }

        public async Task<long> SaveAllAsync(IEnumerable<WebhookSubscription> models, bool? references = true, CancellationToken token = default)
        {
            var saves = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                var keys = new List<Guid>();
                foreach (var model in models)
                {
                    keys.Add(model.Id);
                }
                var matches = await FindAllByKeysAsync(keys, references: references, token: token);
                var similar = models.Intersect(matches);
                var different = matches.Any() ? models.Except(matches) : models;

                if (similar.Any())
                {
                    var requests = new List<WriteModel<WebhookSubscription>>();
                    foreach (var model in similar)
                    {
                        token.ThrowIfCancellationRequested();
                        requests.Add(new ReplaceOneModel<WebhookSubscription>(Builders<WebhookSubscription>.Filter.Where(x => x.Id == model.Id), model));
                    }
                    var result = await collection.BulkWriteAsync(requests, cancellationToken: token);
                    saves += result.IsAcknowledged ? (int)result.MatchedCount : 0;
                }

                if (different.Any())
                {
                    var requests = new List<WriteModel<WebhookSubscription>>();
                    foreach (var model in different)
                    {
                        token.ThrowIfCancellationRequested();
                        requests.Add(new InsertOneModel<WebhookSubscription>(model));
                    }
                    var result = await collection.BulkWriteAsync(requests, cancellationToken: token);
                    saves += result.IsAcknowledged ? (int)result.InsertedCount : 0;
                }
                scope.Complete();
            }
            return saves;
        }

        public async Task<bool> SaveAsync(WebhookSubscription model, bool? references = true, CancellationToken token = default)
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

        public void Trash(WebhookSubscription model, bool? references = null)
        {
            model.IsActive = true;
            Save(model, references);
        }

        public void TrashAll(IEnumerable<WebhookSubscription> models, bool? references = null)
        {
            if (references.HasValue && references.Value)
            {
                foreach (var model in models)
                {
                    model.IsActive = true;
                }
            }
            SaveAll(models, references);
        }

        public async Task TrashAllAsync(IEnumerable<WebhookSubscription> models, bool? references = null, CancellationToken token = default)
        {
            if (references.HasValue && references.Value)
            {
                foreach (var model in models)
                {
                    model.IsActive = true;
                }
            }
            await SaveAllAsync(models, references, token);
        }

        public List<WebhookSubscription> TrashAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var matches = FindAllByKeys(keys, references, offset, count);
            if (matches.Any()) TrashAll(matches, references);
            return matches;
        }

        public async Task<List<WebhookSubscription>> TrashAllByKeysAsync(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var matches = await FindAllByKeysAsync(keys, references, offset, count, token);
            if (matches.Any()) await TrashAllAsync(matches, references, token);
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
            var match = await FindByKeyAsync(key, references, token);
            if (match != null) await TrashAsync(match, references);
            return match;
        }
    }
}
