using MongoDbGenericRepository.Models;

namespace Service.Models.Data
{
  public interface IScore : IDocument
  {
    string Debit { get; set; }
    string Credit { get; set; }
    string Symbol { get; set; }
    string Distance { get; set; }
    string Position { get; set; }
    string Expiration { get; set; }
  }

  public class CScore : Document, IScore
  {
    public string Debit { get; set; }
    public string Credit { get; set; }
    public string Symbol { get; set; }
    public string Distance { get; set; }
    public string Position { get; set; }
    public string Expiration { get; set; }
  }
}
