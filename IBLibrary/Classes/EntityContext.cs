namespace IBLibrary.Classes
{
  using System.Data.Entity;

  public partial class EntityContext : DbContext
  {
    public EntityContext() : base(Map.Data["entity.connection"])
    {
    }
  }
}
