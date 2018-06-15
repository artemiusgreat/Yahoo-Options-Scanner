namespace IBLibrary.Classes
{
  using IBLibrary.Models;
  using System.Data.Entity;

  public partial class DataContext : DbContext
  {
    public DataContext() : base(@"data source=.\SQLEXPRESS;initial catalog=scanner;persist security info=True;user id=scanner;password=Matrix1985;multipleactiveresultsets=True;application name=EntityFramework")
    {
    }

    public virtual DbSet<Option> Options { get; set; }
    public virtual DbSet<Symbol> Symbols { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
    }
  }
}
