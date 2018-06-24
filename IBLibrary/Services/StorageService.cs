using IBLibrary.Classes;
using IBLibrary.Models.Data;
using System.Linq;

namespace IBLibrary.Services
{
  public class StorageService : BaseService
  {
    public int Item()
    {
      using (var context = new EntityContext())
      {
        var option = new Option() { Strike = 135 };
        var symbol = new Symbol() { Name = "DM", Alias = "Demo" };
      }

      return 1;
    }
  }
}
