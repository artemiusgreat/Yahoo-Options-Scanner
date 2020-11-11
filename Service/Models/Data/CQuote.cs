using MongoDbGenericRepository.Models;

namespace Service.Models.Data
{
  public interface IQuote : IDocument
  {
    double Bid { get; set; }
    double Ask { get; set; }
    long BidSize { get; set; }
    long AskSize { get; set; }
    string Name { get; set; }
    string Symbol { get; set; }
    string Currency { get; set; }
    string Exchange { get; set; }
  }

  public class CQuote : Document, IQuote
  {
    public double Bid { get; set; }
    public double Ask { get; set; }
    public long BidSize { get; set; }
    public long AskSize { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string Currency { get; set; }
    public string Exchange { get; set; }
  }
}
