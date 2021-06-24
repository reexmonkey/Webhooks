using MongoDB.Driver;
using reexmonkey.xmisc.backbone.repositories.contracts.extensions;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Repositories.Contracts;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Reexmonkey.Webhooks.Core.Repositories.MongoDb
{
    public class WebhookProviderRepository : IWebhookProviderRepository
    {
        private readonly IWebhookDefinitionRepository webhookDefinitionRepository;
        private readonly IMongoDatabase database;
        private readonly MongoCollectionSettings settings;
        private readonly IMongoCollection<WebhookPublisher> collection;

        public WebhookProviderRepository(
            IWebhookDefinitionRepository webhookDefinitionRepository,
            IMongoDatabase database,
            MongoCollectionSettings settings)
        {
            this.webhookDefinitionRepository = webhookDefinitionRepository ?? throw new ArgumentNullException(nameof(webhookDefinitionRepository));
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            collection = database.GetCollection<WebhookPublisher>(nameof(WebhookPublisher), settings);
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
            var filter = Builders<WebhookPublisher>.Filter.In(x => x.Id, keys);
            var results = collection.Find(filter).ToList();
            return strict ? results.Count == keys.Count() : results.Any();
        }

        public async Task<bool> ContainsKeysAsync(IEnumerable<Guid> keys, bool strict = true, CancellationToken token = default)
        {
            var filter = Builders<WebhookPublisher>.Filter.In(x => x.Id, keys);
            using (var cursor = await collection.FindAsync(filter, cancellationToken: token))
            {
                var results = await cursor.ToListAsync(token);
                return strict ? results.Count == keys.Count() : results.Any();
            }
        }

        public bool Erase(WebhookPublisher model)
        {
            return EraseByKey(model.Id);
        }

        public long EraseAll(IEnumerable<WebhookPublisher> models)
        {
            return EraseAllByKeys(models.Select(x => x.Id));
        }

        public Task<long> EraseAllAsync(IEnumerable<WebhookPublisher> models, CancellationToken token = default)
        {
            return EraseAllByKeysAsync(models.Select(x => x.Id), token);
        }

        public long EraseAllByKeys(IEnumerable<Guid> keys)
        {
            var deleted = 0L;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var filter = Builders<WebhookPublisher>.Filter.In(x => x.Id, keys);
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
                var filter = Builders<WebhookPublisher>.Filter.In(x => x.Id, keys);
                var result = await collection.DeleteManyAsync(filter, token);
                deleted = result.IsAcknowledged ? (int)result.DeletedCount : 0L;
                scope.Complete();
            }
            return deleted;
        }

        public Task<bool> EraseAsync(WebhookPublisher model, CancellationToken token = default)
        {
            return EraseByKeyAsync(model.Id, token);
        }

        public bool EraseByKey(Guid key)
        {
            var deleted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var filter = Builders<WebhookPublisher>.Filter.Eq(x => x.Id, key);
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
                var filter = Builders<WebhookPublisher>.Filter.Eq(x => x.Id, key);
                var result = await collection.DeleteOneAsync(filter, token);

                deleted = result.IsAcknowledged && result.DeletedCount > 0;
                scope.Complete();
            }
            return deleted;
        }

        public List<WebhookPublisher> FindAll(Expression<Func<WebhookPublisher, bool>> predicate, bool? references = null, int? offset = null, int? count = null)
        {
            return collection.Find(predicate).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<WebhookPublisher>> FindAllAsync(Expression<Func<WebhookPublisher, bool>> predicate, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<WebhookPublisher>() { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(predicate, options, token))
            {
                return await cursor.ToListAsync(token);
            }
        }

        public List<WebhookPublisher> FindAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var filter = Builders<WebhookPublisher>.Filter.In(x => x.Id, keys);
            return collection.Find(filter).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<WebhookPublisher>> FindAllByKeysAsync(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var filter = Builders<WebhookPublisher>.Filter.In(x => x.Id, keys);
            var options = new FindOptions<WebhookPublisher>() { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(filter, options, token))
            {
                return await cursor.ToListAsync(token);
            }
        }

        public WebhookPublisher FindByKey(Guid key, bool? references = null)
        {
            return collection.Find(x => x.Id == key).FirstOrDefault();
        }

        public async Task<WebhookPublisher> FindByKeyAsync(Guid key, bool? references = null, CancellationToken token = default)
        {
            using (var cursor = await collection.FindAsync(x => x.Id == key, cancellationToken: token))
            {
                return cursor.FirstOrDefault(token);
            }
        }

        public List<WebhookPublisher> Get(bool? references = null, int? offset = null, int? count = null)
        {
            return collection.Find(Builders<WebhookPublisher>.Filter.Empty).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<WebhookPublisher>> GetAsync(bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<WebhookPublisher> { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(Builders<WebhookPublisher>.Filter.Empty, options, token))
            {
                return cursor.ToList(token);
            }
        }

        public List<Guid> GetKeys(int? offset = null, int? count = null)
        {
            return collection
                .Find(Builders<WebhookPublisher>.Filter.Empty)
                .Project(Builders<WebhookPublisher>.Projection.Expression(x => x.Id))
                .Skip(offset)
                .Limit(count)
                .ToList();
        }

        public async Task<List<Guid>> GetKeysAsync(int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<WebhookPublisher, Guid>
            {
                Projection = Builders<WebhookPublisher>.Projection.Expression(x => x.Id),
                Skip = offset,
                Limit = count
            };

            using (var cursor = await collection.FindAsync(Builders<WebhookPublisher>.Filter.Empty, options, token))
                return await cursor.ToListAsync();
        }

        public void Restore(WebhookPublisher model, bool? references = null)
        {
            model.IsDeleted = false;
            if (references.HasValue
                && references.Value
                && model.Webhooks != null
                && model.Webhooks.Any())
            {
                model.Webhooks.ForEach(x => x.IsDeleted = false);
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

        public Task RestoreAsync(WebhookPublisher model, bool? references = null)
        {
            model.IsDeleted = false;
            if (references.HasValue
                && references.Value
                && model.Webhooks != null
                && model.Webhooks.Any())
            {
                webhookDefinitionRepository.RestoreAll(model.Webhooks, references);
            }
            return SaveAsync(model, references);
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
            var persisted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var candidate = model;
                if (!references.HasValue || !references.Value)
                {
                    candidate = model.CreateCopy();
                    candidate.Webhooks.Clear();
                }
                var result = collection.ReplaceOne(x => x.Id == model.Id, candidate, new ReplaceOptions { IsUpsert = true });
                persisted = result.IsAcknowledged;
                scope.Complete();
            }
            return persisted;
        }

        public long SaveAll(IEnumerable<WebhookPublisher> models, bool? references = true)
        {
            var saves = 0L;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var keys = new List<Guid>();
                var candidates = new List<WebhookPublisher>();
                foreach (var model in models)
                {
                    var candidate = model;
                    if (!references.HasValue || !references.Value)
                    {
                        candidate = model.CreateCopy();
                        candidate.Webhooks.Clear();
                    }
                    candidates.Add(candidate);
                    keys.Add(candidate.Id);
                }

                var matches = FindAllByKeys(keys, references: references);
                var similar = candidates.Intersect(matches);
                var different = matches.Any() ? candidates.Except(matches) : candidates;

                var requests = new List<WriteModel<WebhookPublisher>>();
                if (similar.Any())
                {
                    foreach (var model in similar)
                        requests.Add(new ReplaceOneModel<WebhookPublisher>(Builders<WebhookPublisher>.Filter.Where(x => x.Id == model.Id), model));
                    var result = collection.BulkWrite(requests);
                    saves += result.IsAcknowledged ? result.MatchedCount : 0L;
                }

                if (different.Any())
                {
                    requests.Clear();
                    foreach (var model in different)
                        requests.Add(new InsertOneModel<WebhookPublisher>(model));
                    var results = collection.BulkWrite(requests);
                    saves += results.IsAcknowledged ? results.InsertedCount : 0L;
                }
                scope.Complete();
            }
            return saves;
        }

        public async Task<long> SaveAllAsync(IEnumerable<WebhookPublisher> models, bool? references = true, CancellationToken token = default)
        {
            var saves = 0;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                var keys = new List<Guid>();
                var candidates = new List<WebhookPublisher>();
                foreach (var model in models)
                {
                    var candidate = model;
                    if (!references.HasValue || !references.Value)
                    {
                        candidate = model.CreateCopy();
                        candidate.Webhooks.Clear();
                    }
                    candidates.Add(candidate);
                    keys.Add(candidate.Id);
                }
                var matches = await FindAllByKeysAsync(keys, references: references, token: token);
                var similar = candidates.Intersect(matches);
                var different = matches.Any() ? candidates.Except(matches) : candidates;

                if (similar.Any())
                {
                    var requests = new List<WriteModel<WebhookPublisher>>();
                    foreach (var model in similar)
                    {
                        token.ThrowIfCancellationRequested();
                        requests.Add(new ReplaceOneModel<WebhookPublisher>(Builders<WebhookPublisher>.Filter.Where(x => x.Id == model.Id), model));
                    }
                    var result = await collection.BulkWriteAsync(requests, cancellationToken: token);
                    saves += result.IsAcknowledged ? (int)result.MatchedCount : 0;
                }

                if (different.Any())
                {
                    var requests = new List<WriteModel<WebhookPublisher>>();
                    foreach (var model in different)
                    {
                        token.ThrowIfCancellationRequested();
                        requests.Add(new InsertOneModel<WebhookPublisher>(model));
                    }
                    var result = await collection.BulkWriteAsync(requests, cancellationToken: token);
                    saves += result.IsAcknowledged ? (int)result.InsertedCount : 0;
                }
                scope.Complete();
            }
            return saves;
        }

        public async Task<bool> SaveAsync(WebhookPublisher model, bool? references = true, CancellationToken token = default)
        {
            var persisted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScopeFlow())
            {
                var candidate = model;
                if (!references.HasValue || !references.Value)
                {
                    candidate = model.CreateCopy();
                    candidate.Webhooks.Clear();
                }
                var result = await collection.ReplaceOneAsync(x => x.Id == model.Id, candidate, new ReplaceOptions { IsUpsert = true });
                persisted = result.IsAcknowledged;
                scope.Complete();
            }
            return persisted;
        }

        public void Trash(WebhookPublisher model, bool? references = null)
        {
            model.IsDeleted = true;
            if (references.HasValue
                && references.Value
                && model.Webhooks != null
                && model.Webhooks.Any())
            {
                webhookDefinitionRepository.TrashAll(model.Webhooks);
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

        public async Task<List<WebhookPublisher>> TrashAllByKeysAsync(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var matches = await FindAllByKeysAsync(keys, references, offset, count, token);
            if (matches.Any()) await TrashAllAsync(matches, references, token);
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
            var match = await FindByKeyAsync(key, references, token);
            if (match != null) await TrashAsync(match, references);
            return match;
        }
    }
}
