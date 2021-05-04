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
    public class WebhookDefinitionRepository : IWebhookDefinitionRepository
    {
        private readonly IMongoDatabase database;
        private readonly MongoCollectionSettings settings;
        private readonly IMongoCollection<WebhookDefinition> collection;

        public WebhookDefinitionRepository(IMongoDatabase database, MongoCollectionSettings settings)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            collection = database.GetCollection<WebhookDefinition>(nameof(WebhookDefinition), settings);
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
            var filter = Builders<WebhookDefinition>.Filter.In(x => x.Id, keys);
            var results = collection.Find(filter).ToList();
            return strict ? results.Count == keys.Count() : results.Any();
        }

        public async Task<bool> ContainsKeysAsync(IEnumerable<Guid> keys, bool strict = true, CancellationToken token = default)
        {
            var filter = Builders<WebhookDefinition>.Filter.In(x => x.Id, keys);
            using (var cursor = await collection.FindAsync(filter, cancellationToken: token))
            {
                var results = await cursor.ToListAsync(token);
                return strict ? results.Count == keys.Count() : results.Any();
            }
        }

        public bool Erase(WebhookDefinition model)
        {
            return EraseByKey(model.Id);
        }

        public long EraseAll(IEnumerable<WebhookDefinition> models)
        {
            return EraseAllByKeys(models.Select(x => x.Id));
        }

        public Task<long> EraseAllAsync(IEnumerable<WebhookDefinition> models, CancellationToken token = default)
        {
            return EraseAllByKeysAsync(models.Select(x => x.Id), token);
        }

        public long EraseAllByKeys(IEnumerable<Guid> keys)
        {
            var deleted = 0L;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var filter = Builders<WebhookDefinition>.Filter.In(x => x.Id, keys);
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
                var filter = Builders<WebhookDefinition>.Filter.In(x => x.Id, keys);
                var result = await collection.DeleteManyAsync(filter, token);
                deleted = result.IsAcknowledged ? result.DeletedCount : 0L;
                scope.Complete();
            }
            return deleted;
        }

        public Task<bool> EraseAsync(WebhookDefinition model, CancellationToken token = default)
        {
            return EraseByKeyAsync(model.Id, token);
        }

        public bool EraseByKey(Guid key)
        {
            var deleted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var filter = Builders<WebhookDefinition>.Filter.Eq(x => x.Id, key);
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
                var filter = Builders<WebhookDefinition>.Filter.Eq(x => x.Id, key);
                var result = await collection.DeleteOneAsync(filter, token);

                deleted = result.IsAcknowledged && result.DeletedCount > 0;
                scope.Complete();
            }
            return deleted;
        }

        public List<WebhookDefinition> FindAll(Expression<Func<WebhookDefinition, bool>> predicate, bool? references = null, int? offset = null, int? count = null)
        {
            return collection.Find(predicate).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<WebhookDefinition>> FindAllAsync(Expression<Func<WebhookDefinition, bool>> predicate, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<WebhookDefinition>() { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(predicate, options, token))
            {
                return await cursor.ToListAsync(token);
            }
        }

        public List<WebhookDefinition> FindAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var filter = Builders<WebhookDefinition>.Filter.In(x => x.Id, keys);
            return collection.Find(filter).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<WebhookDefinition>> FindAllByKeysAsync(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var filter = Builders<WebhookDefinition>.Filter.In(x => x.Id, keys);
            var options = new FindOptions<WebhookDefinition>() { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(filter, options, token))
            {
                return await cursor.ToListAsync(token);
            }
        }

        public WebhookDefinition FindByKey(Guid key, bool? references = null)
        {
            return collection.Find(x => x.Id == key).FirstOrDefault();
        }

        public async Task<WebhookDefinition> FindByKeyAsync(Guid key, bool? references = null, CancellationToken token = default)
        {
            using (var cursor = await collection.FindAsync(x => x.Id == key, cancellationToken: token))
            {
                return cursor.FirstOrDefault(token);
            }
        }

        public List<WebhookDefinition> Get(bool? references = null, int? offset = null, int? count = null)
        {
            return collection.Find(Builders<WebhookDefinition>.Filter.Empty).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<WebhookDefinition>> GetAsync(bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<WebhookDefinition> { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(Builders<WebhookDefinition>.Filter.Empty, options, token))
            {
                return cursor.ToList(token);
            }
        }

        public List<Guid> GetKeys(int? offset = null, int? count = null)
        {
            return collection
                .Find(Builders<WebhookDefinition>.Filter.Empty)
                .Project(Builders<WebhookDefinition>.Projection.Expression(x => x.Id))
                .Skip(offset)
                .Limit(count)
                .ToList();
        }

        public async Task<List<Guid>> GetKeysAsync(int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<WebhookDefinition, Guid>
            {
                Projection = Builders<WebhookDefinition>.Projection.Expression(x => x.Id),
                Skip = offset,
                Limit = count
            };

            using (var cursor = await collection.FindAsync(Builders<WebhookDefinition>.Filter.Empty, options, token))
                return await cursor.ToListAsync();
        }

        public void Restore(WebhookDefinition model, bool? references = null)
        {
            model.IsDeleted = false;
            Save(model, references);
        }

        public void RestoreAll(IEnumerable<WebhookDefinition> models, bool? references = null)
        {
            if (references.HasValue && references.Value)
            {
                foreach (var model in models)
                {
                    model.IsDeleted = false;
                }
            }
            SaveAll(models, references);
        }

        public async Task RestoreAllAsync(IEnumerable<WebhookDefinition> models, bool? references = null, CancellationToken token = default)
        {
            if (references.HasValue && references.Value)
            {
                var webhooks = new List<WebhookDefinition>();
                foreach (var model in models)
                {
                    model.IsDeleted = false;
                }
            }
            await SaveAllAsync(models, references, token);
        }

        public List<WebhookDefinition> RestoreAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var matches = FindAllByKeys(keys, references, offset, count);
            if (matches.Any()) RestoreAll(matches, references);
            return matches;
        }

        public async Task<List<WebhookDefinition>> RestoreAllByKeysAsync(
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

        public Task RestoreAsync(WebhookDefinition model, bool? references = null)
        {
            model.IsDeleted = false;
            return SaveAsync(model, references);
        }

        public WebhookDefinition RestoreByKey(Guid key, bool? references = null)
        {
            var match = FindByKey(key, references);
            if (match != null) Restore(match, references);
            return match;
        }

        public async Task<WebhookDefinition> RestoreByKeyAsync(Guid key, bool? references = null)
        {
            var match = await FindByKeyAsync(key, references);
            if (match != null) await RestoreAsync(match, references);
            return match;
        }

        public bool Save(WebhookDefinition model, bool? references = true)
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

        public long SaveAll(IEnumerable<WebhookDefinition> models, bool? references = true)
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

                var requests = new List<WriteModel<WebhookDefinition>>();
                if (similar.Any())
                {
                    foreach (var model in similar)
                        requests.Add(new ReplaceOneModel<WebhookDefinition>(Builders<WebhookDefinition>.Filter.Where(x => x.Id == model.Id), model));
                    var result = collection.BulkWrite(requests);
                    saves += result.IsAcknowledged ? result.MatchedCount : 0L;
                }

                if (different.Any())
                {
                    requests.Clear();
                    foreach (var model in different)
                        requests.Add(new InsertOneModel<WebhookDefinition>(model));
                    var results = collection.BulkWrite(requests);
                    saves += results.IsAcknowledged ? results.InsertedCount : 0L;
                }
                scope.Complete();
            }
            return saves;
        }

        public async Task<long> SaveAllAsync(IEnumerable<WebhookDefinition> models, bool? references = true, CancellationToken token = default)
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
                    var requests = new List<WriteModel<WebhookDefinition>>();
                    foreach (var model in similar)
                    {
                        token.ThrowIfCancellationRequested();
                        requests.Add(new ReplaceOneModel<WebhookDefinition>(Builders<WebhookDefinition>.Filter.Where(x => x.Id == model.Id), model));
                    }
                    var result = await collection.BulkWriteAsync(requests, cancellationToken: token);
                    saves += result.IsAcknowledged ? (int)result.MatchedCount : 0;
                }

                if (different.Any())
                {
                    var requests = new List<WriteModel<WebhookDefinition>>();
                    foreach (var model in different)
                    {
                        token.ThrowIfCancellationRequested();
                        requests.Add(new InsertOneModel<WebhookDefinition>(model));
                    }
                    var result = await collection.BulkWriteAsync(requests, cancellationToken: token);
                    saves += result.IsAcknowledged ? (int)result.InsertedCount : 0;
                }
                scope.Complete();
            }
            return saves;
        }

        public async Task<bool> SaveAsync(WebhookDefinition model, bool? references = true, CancellationToken token = default)
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

        public void Trash(WebhookDefinition model, bool? references = null)
        {
            model.IsDeleted = true;
            Save(model, references);
        }

        public void TrashAll(IEnumerable<WebhookDefinition> models, bool? references = null)
        {
            if (references.HasValue && references.Value)
            {
                foreach (var model in models)
                {
                    model.IsDeleted = true;
                }
            }
            SaveAll(models, references);
        }

        public async Task TrashAllAsync(IEnumerable<WebhookDefinition> models, bool? references = null, CancellationToken token = default)
        {
            if (references.HasValue && references.Value)
            {
                foreach (var model in models)
                {
                    model.IsDeleted = true;
                }
            }
            await SaveAllAsync(models, references, token);
        }

        public List<WebhookDefinition> TrashAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var matches = FindAllByKeys(keys, references, offset, count);
            if (matches.Any()) TrashAll(matches, references);
            return matches;
        }

        public async Task<List<WebhookDefinition>> TrashAllByKeysAsync(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var matches = await FindAllByKeysAsync(keys, references, offset, count, token);
            if (matches.Any()) await TrashAllAsync(matches, references, token);
            return matches;
        }

        public async Task TrashAsync(WebhookDefinition model, bool? references = null)
        {
            model.IsDeleted = true;
            await SaveAsync(model, references);
        }

        public WebhookDefinition TrashByKey(Guid key, bool? references = null)
        {
            var match = FindByKey(key, references);
            if (match != null) Trash(match, references);
            return match;
        }

        public async Task<WebhookDefinition> TrashByKeyAsync(Guid key, bool? references = null, CancellationToken token = default)
        {
            var match = await FindByKeyAsync(key, references, token);
            if (match != null) await TrashAsync(match, references);
            return match;
        }
    }
}
