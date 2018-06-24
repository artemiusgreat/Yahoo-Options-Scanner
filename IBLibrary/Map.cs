using System.Collections.Generic;

namespace IBLibrary
{
  public class Map
  {
    public static readonly Dictionary<string, string> Data = new Dictionary<string, string>
    {
      { "entity.connection", "data source=SQLEXPRESS;initial catalog=scanner;persist security info=True;user id=scanner;password=Matrix1985;multipleactiveresultsets=True;application name=EntityFramework" },
      { "mongo.connection", "mongodb://localhost:6000" },
      { "mongo.storage", "interactive-brokers" },
      { "mongo.collections.options", "options" }
    };
  }
}
