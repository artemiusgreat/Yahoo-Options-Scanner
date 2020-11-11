using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Classes
{
  public interface IContainer<T> : IBaseMongoRepository
  {
    void RemovePartition<TDocument>(string partitionKey);
    IMongoCollection<TDocument> Query<TDocument>(string partitionKey) where TDocument : IDocument;
    Task AddPartitionAsync<TDocument>(IEnumerable<TDocument> documents, string partitionKey) where TDocument : IDocument;
  }

  public class CContainer<T> : BaseMongoRepository, IContainer<T>
  {
    public CContainer() : base(
      ConfigurationManager.AppSettings["MongoConnection"],
      ConfigurationManager.AppSettings["MongoStorage"])
    {
    }

    /// <summary>
    /// Drop collection by name
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <param name="partitionKey"></param>
    public void RemovePartition<TDocument>(string partitionKey)
    {
      MongoDbContext.DropCollection<TDocument>(partitionKey);
    }

    /// <summary>
    /// Add multiple records at once to Mongo DB with an ability to define collection name
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <param name="documents"></param>
    /// <param name="partitionKey"></param>
    /// <returns></returns>
    public async Task AddPartitionAsync<TDocument>(IEnumerable<TDocument> documents, string partitionKey) where TDocument : IDocument
    {
      RemovePartition<TDocument>(partitionKey);

      if (documents.Any())
      {
        documents.ForEach(o => FormatDocument(o));
        await GetCollection<TDocument>(partitionKey).InsertManyAsync(documents);
      }
    }

    /// <summary>
    /// Get records by criteria
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <param name="partitionKey"></param>
    /// <returns></returns>
    public IMongoCollection<TDocument> Query<TDocument>(string partitionKey) where TDocument : IDocument
    {
      return GetCollection<TDocument>(partitionKey);
    }
  }
}
