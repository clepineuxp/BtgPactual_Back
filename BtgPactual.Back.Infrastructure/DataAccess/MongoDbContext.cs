using BtgPactual.Back.Domain.Configurations;
using MongoDB.Driver;

namespace BtgPactual.Back.Infrastructure.DataAccess
{
    public class MongoDbContext
    {
        public MongoClient _client;

        public IMongoDatabase db;

        public MongoDbContext(DatabaseConfiguration databaseConfiguration)
        {
            _client = new MongoClient(databaseConfiguration.DatabaseConnection);
            db = _client.GetDatabase(databaseConfiguration.DatabaseName);
        }
    }
}
