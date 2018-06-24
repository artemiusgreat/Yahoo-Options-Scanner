using MongoDB.Bson;
using MongoDB.Driver;

namespace IBLibrary.Classes
{
  public partial class MongoContext
  {
    protected static IMongoClient Client { get; set; }
    protected static IMongoDatabase Storage { get; set; }

    public MongoContext()
    {
      Client = new MongoClient(Map.Data["mongo.connection"]);
      Storage = Client.GetDatabase(Map.Data["mongo.storage"]);

      var collection = Storage.GetCollection<BsonDocument>(Map.Data["mongo.collections.options"]);

      collection.InsertOneAsync(new BsonDocument("Name", "Jack")).Wait();

      var list = collection.Find(new BsonDocument("Name", "Jack")).ToList();
    }
  }
}
