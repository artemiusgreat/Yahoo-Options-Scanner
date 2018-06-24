using System.Collections.Generic;

namespace IBService.Areas.Service.Models
{
  public class Response<T>
  {
    public List<T> Items;
    public List<string> Errors;
    public List<string> Messages;
  }
}
