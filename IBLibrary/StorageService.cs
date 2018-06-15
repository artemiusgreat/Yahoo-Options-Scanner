using IBLibrary.Classes;
using IBLibrary.Models;
using System.Linq;

namespace IBLibrary
{
  public class StorageService : BaseService
  {
    public int Item()
    {
      using (var context = new DataContext())
      {
        var option = new Option() { OptionStrike = 135 };
        var symbol = new Symbol() { SymbolName = "DM", SymbolAlias = "Demo" };
      }

      return 1;
    }
  }
}
