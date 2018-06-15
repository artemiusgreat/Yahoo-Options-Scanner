namespace IBLibrary.Models
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("Symbol")]
  public partial class Symbol
  {
    public int SymbolId { get; set; }

    [StringLength(255)]
    public string SymbolName { get; set; }

    [StringLength(255)]
    public string SymbolAlias { get; set; }
  }
}
