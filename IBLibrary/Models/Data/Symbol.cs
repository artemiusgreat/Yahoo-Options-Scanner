namespace IBLibrary.Models.Data
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("Symbol")]
  public partial class Symbol
  {
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; }

    [StringLength(255)]
    public string Alias { get; set; }
  }
}
