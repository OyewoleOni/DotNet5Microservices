using MongoDB.Driver;

namespace Play.Catalog.Service.Repositories
{
    public class ItemRepository{
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> dbCollection;
        private readonly  FilterDefinitionBuilder<Item> FilterBuilder = Builders<Item>.Filter;

        public ItemRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
        }
    }
}