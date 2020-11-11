using MongoDbGenericRepository.Models;

namespace Service.Models.Data
{
  public enum ERight
  {
    Put,
    Call
  };

  public enum EDirection
  {
    Long,
    Short
  };

  public interface IOption : IDocument
  {
    long Expiration { get; set; }
    long Volume { get; set; }
    long OpenInterest { get; set; }
    double Bid { get; set; }
    double Ask { get; set; }
    double Strike { get; set; }
    double LastPrice { get; set; }
    string Symbol { get; set; }
    string Currency { get; set; }
    ERight Right { get; set; }
    EDirection Direction { get; set; }
  }

  public class COption : Document, IOption
  {
    public long Expiration { get; set; }
    public long Volume { get; set; }
    public long OpenInterest { get; set; }
    public double Bid { get; set; }
    public double Ask { get; set; }
    public double Strike { get; set; }
    public double LastPrice { get; set; }
    public string Symbol { get; set; }
    public string Currency { get; set; }
    public ERight Right { get; set; }
    public EDirection Direction { get; set; }
  }
}
