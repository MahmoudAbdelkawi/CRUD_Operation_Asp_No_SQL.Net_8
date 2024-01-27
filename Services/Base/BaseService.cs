using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Online_Survey.Models;
using Online_Survey.Models.Base;

namespace Online_Survey.Services.Base
{
    public class BaseService<TDocument> where TDocument : TEntity
    {
        private readonly IMongoCollection<TDocument> document;
        public BaseService(IOptions<DatabaseConfigurations> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);
            document = database.GetCollection<TDocument>(typeof(TDocument).Name);
        }
        public async Task<BsonDocument> GetDocumentAsync(ObjectId id) =>
            (await document.Find(x => x.Id == id).FirstOrDefaultAsync()).ToBsonDocument();

        public async Task CreateAsync(TDocument newBook) =>
            await document.InsertOneAsync(newBook);

        public async Task UpdateAsync(ObjectId id, TDocument updatedBook) =>
            await document.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(ObjectId id) =>
            await document.DeleteOneAsync(x => x.Id == id);
    }
}
