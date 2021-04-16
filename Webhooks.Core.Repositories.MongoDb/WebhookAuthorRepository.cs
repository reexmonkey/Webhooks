using MongoDB.Driver;
using reexmonkey.xmisc.backbone.repositories.contracts.extensions;
using Reexmonkey.Webhooks.Core.Domain.Concretes.Models;
using Reexmonkey.Webhooks.Core.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Reexmonkey.Webhooks.Core.Repositories.MongoDb
{
    public class WebhookAuthorRepository : IWebhookAuthorRepository
    {
        private readonly IWebhookDefinitionRepository webhookDefinitionRepository;
        private readonly IAuthorsDefinitionsRepository authorsDefinitionsRepository;
        private readonly IMongoDatabase database;
        private readonly MongoCollectionSettings settings;
        private readonly IMongoCollection<WebhookAuthor> collection;

        public WebhookAuthorRepository(
            IWebhookDefinitionRepository webhookDefinitionRepository,
            IAuthorsDefinitionsRepository authorsDefinitionsRepository,
            IMongoDatabase database,
            MongoCollectionSettings settings)
        {
            this.webhookDefinitionRepository = webhookDefinitionRepository ?? throw new ArgumentNullException(nameof(webhookDefinitionRepository));
            this.authorsDefinitionsRepository = authorsDefinitionsRepository ?? throw new ArgumentNullException(nameof(authorsDefinitionsRepository));
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            collection = database.GetCollection<WebhookAuthor>(nameof(WebhookAuthor), settings);
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
            var filter = Builders<WebhookAuthor>.Filter.In(x => x.Id, keys);
            var results = collection.Find(filter).ToList();
            return strict ? results.Count == keys.Count() : results.Any();
        }

        public async Task<bool> ContainsKeysAsync(IEnumerable<Guid> keys, bool strict = true, CancellationToken token = default)
        {
            var filter = Builders<WebhookAuthor>.Filter.In(x => x.Id, keys);
            using (var cursor = await collection.FindAsync(filter, cancellationToken: token))
            {
                var results = await cursor.ToListAsync(token);
                return strict ? results.Count == keys.Count() : results.Any();
            }
        }

        public bool Erase(WebhookAuthor model)
        {
            return EraseByKey(model.Id);
        }

        public long EraseAll(IEnumerable<WebhookAuthor> models)
        {
            return EraseAllByKeys(models.Select(x => x.Id));
        }

        public Task<long> EraseAllAsync(IEnumerable<WebhookAuthor> models, CancellationToken token = default)
        {
            return EraseAllByKeysAsync(models.Select(x => x.Id), token);
        }

        public long EraseAllByKeys(IEnumerable<Guid> keys)
        {
            var deleted = 0L;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {

                var filter = Builders<WebhookAuthor>.Filter.In(x => x.Id, keys);
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

                var filter = Builders<WebhookAuthor>.Filter.In(x => x.Id, keys);
                var result = await collection.DeleteManyAsync(filter, token);
                deleted = result.IsAcknowledged ? (int)result.DeletedCount : 0L;
                scope.Complete();
            }
            return deleted;
        }

        public Task<bool> EraseAsync(WebhookAuthor model, CancellationToken token = default)
        {
            return EraseByKeyAsync(model.Id, token);
        }

        public bool EraseByKey(Guid key)
        {
            var deleted = false;
            using (var scope = TransactionScopeOption.Required.AsTransactionScope())
            {
                var filter = Builders<WebhookAuthor>.Filter.Eq(x => x.Id, key);
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
                var filter = Builders<WebhookAuthor>.Filter.Eq(x => x.Id, key);
                var result = await collection.DeleteOneAsync(filter, token);

                deleted = result.IsAcknowledged && result.DeletedCount > 0;
                scope.Complete();
            }
            return deleted;
        }

        public List<WebhookAuthor> FindAll(Expression<Func<WebhookAuthor, bool>> predicate, bool? references = null, int? offset = null, int? count = null)
        {
            return collection.Find(predicate).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<WebhookAuthor>> FindAllAsync(Expression<Func<WebhookAuthor, bool>> predicate, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<WebhookAuthor>() { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(predicate, options, token))
            {
                return await cursor.ToListAsync(token);
            }
        }

        public List<WebhookAuthor> FindAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var filter = Builders<WebhookAuthor>.Filter.In(x => x.Id, keys);
            return collection.Find(filter).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<WebhookAuthor>> FindAllByKeysAsync(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var filter = Builders<WebhookAuthor>.Filter.In(x => x.Id, keys);
            var options = new FindOptions<WebhookAuthor>() { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(filter, options, token))
            {
                return await cursor.ToListAsync(token);
            }
        }

        public WebhookAuthor FindByKey(Guid key, bool? references = null)
        {
            return collection.Find(x => x.Id == key).FirstOrDefault();
        }

        public async Task<WebhookAuthor> FindByKeyAsync(Guid key, bool? references = null, CancellationToken token = default)
        {
            using (var cursor = await collection.FindAsync(x => x.Id == key, cancellationToken: token))
            {
                return cursor.FirstOrDefault(token);
            }
        }

        public List<WebhookAuthor> Get(bool? references = null, int? offset = null, int? count = null)
        {
            return collection.Find(Builders<WebhookAuthor>.Filter.Empty).Skip(offset).Limit(count).ToList();
        }

        public async Task<List<WebhookAuthor>> GetAsync(bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<WebhookAuthor> { Skip = offset, Limit = count };
            using (var cursor = await collection.FindAsync(Builders<WebhookAuthor>.Filter.Empty, options, token))
            {
                return cursor.ToList(token);
            }
        }

        public List<Guid> GetKeys(int? offset = null, int? count = null)
        {
            return collection
                .Find(Builders<WebhookAuthor>.Filter.Empty)
                .Project(Builders<WebhookAuthor>.Projection.Expression(x => x.Id))
                .Skip(offset)
                .Limit(count)
                .ToList();
        }

        public async Task<List<Guid>> GetKeysAsync(int? offset = null, int? count = null, CancellationToken token = default)
        {
            var options = new FindOptions<WebhookAuthor, Guid>
            {
                Projection = Builders<WebhookAuthor>.Projection.Expression(x => x.Id),
                Skip = offset,
                Limit = count
            };

            using (var cursor = await collection.FindAsync(Builders<WebhookAuthor>.Filter.Empty, options, token))
                return await cursor.ToListAsync();
        }

        public void Restore(WebhookAuthor model, bool? references = null)
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

        public void RestoreAll(IEnumerable<WebhookAuthor> models, bool? references = null)
        {
            foreach(var model in models)
            {
                model.IsDeleted = false;
                if (references.HasValue && references.Value && model.Webhooks != null && model.Webhooks.Any())
                    model.Webhooks.ForEach(x => x.IsDeleted = false);
            }
            SaveAll(models, references);
        }

        public Task RestoreAllAsync(IEnumerable<WebhookAuthor> models, bool? references = null, CancellationToken token = default)
        {
            foreach (var model in models)
            {
                model.IsDeleted = false;
                if (references.HasValue && references.Value && model.Webhooks != null && model.Webhooks.Any())
                    model.Webhooks.ForEach(x => x.IsDeleted = false);
            }

            return SaveAllAsync(models, references);
        }

        public List<WebhookAuthor> RestoreAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            var matches = FindAllByKeys(keys, references, offset, count);
            if (matches.Any()) RestoreAll(matches, references);
            return matches;
        }

        public async Task<List<WebhookAuthor>> RestoreAllByKeysAsync(
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

        public Task RestoreAsync(WebhookAuthor model, bool? references = null)
        {
            model.IsDeleted = false;
            if (references.HasValue
                && references.Value
                && model.Webhooks != null
                && model.Webhooks.Any())
            {
                model.Webhooks.ForEach(x => x.IsDeleted = false);
            }
            return SaveAsync(model, references);
        }

        public WebhookAuthor RestoreByKey(Guid key, bool? references = null)
        {
            var match = FindByKey(key, references);
            if (match != null) Restore(match, references);
            return match;
        }

        public Task<WebhookAuthor> RestoreByKeyAsync(Guid key, bool? references = null)
        {
            throw new NotImplementedException();
        }

        public bool Save(WebhookAuthor model, bool? references = true)
        {
            throw new NotImplementedException();
        }

        public long SaveAll(IEnumerable<WebhookAuthor> models, bool? references = true)
        {
            throw new NotImplementedException();
        }

        public Task<long> SaveAllAsync(IEnumerable<WebhookAuthor> models, bool? references = true, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync(WebhookAuthor model, bool? references = true, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public void Trash(WebhookAuthor model, bool? references = null)
        {
            throw new NotImplementedException();
        }

        public void TrashAll(IEnumerable<WebhookAuthor> models, bool? references = null)
        {
            throw new NotImplementedException();
        }

        public Task TrashAllAsync(IEnumerable<WebhookAuthor> models, bool? references = null, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public List<WebhookAuthor> TrashAllByKeys(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<WebhookAuthor>> TrashAllByKeysAsync(IEnumerable<Guid> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task TrashAsync(WebhookAuthor model, bool? references = null)
        {
            throw new NotImplementedException();
        }

        public WebhookAuthor TrashByKey(Guid key, bool? references = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebhookAuthor> TrashByKeyAsync(Guid key, bool? references = null, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
