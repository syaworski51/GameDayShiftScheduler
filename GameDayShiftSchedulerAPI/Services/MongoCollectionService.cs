using GameDayShiftSchedulerAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameDayShiftSchedulerAPI.Services
{
    public class MongoCollectionService<TEntity> where TEntity : IEntity
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoCollectionService(IMongoClient client, string databaseName, string collectionName)
        {
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<TEntity>(collectionName);
        }

        public async Task CreateAsync(TEntity document) =>
            await _collection.InsertOneAsync(document);

        public async Task<List<TEntity>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<TEntity> GetByIdAsync(ObjectId id) =>
            await _collection.Find(d => d.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(ObjectId id, TEntity updatedDocument) =>
            await _collection.ReplaceOneAsync(d => d.Id == id, updatedDocument);

        public async Task DeleteOneAsync(ObjectId id) =>
            await _collection.DeleteOneAsync(d => d.Id == id);

        public async Task DeleteManyAsync(FilterDefinition<TEntity> criterion) =>
            await _collection.DeleteManyAsync(criterion);
    }
}
