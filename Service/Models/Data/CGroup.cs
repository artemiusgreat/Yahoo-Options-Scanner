using MongoDbGenericRepository.Models;

namespace Service.Models.Data
{
  public interface IGroup : IDocument
  {
    IQuote Quote { get; set; }
    IOption Option { get; set; }
  }

  public class CGroup : Document, IGroup
  {
    public IQuote Quote { get; set; }
    public IOption Option { get; set; }
  }
}
