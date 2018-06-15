using System.Collections.Generic;

namespace IBService.Areas.Service.Models
{
  public class Query
  {
    public string Action;
    public List<Order> Orders;
    public List<Selector> Selectors;
  }
}
