using System.Collections.Generic;

namespace Service.Models.Message
{
  public interface IServiceMessage<T>
  {
    long Count { get; set; }
    List<T> Items { get; set; }
    List<string> Errors { get; set; }
  }

  public class CServiceMessage<T> : IServiceMessage<T>
  {
    public long Count { get; set; }
    public List<T> Items { get; set; }
    public List<string> Errors { get; set; }

    public CServiceMessage()
    {
      Items = new List<T>();
      Errors = new List<string>();
    }
  }
}
