namespace IBLibrary.Models.Data
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("Option")]
  public partial class Option
  {
    public int Id { get; set; }

    [StringLength(255)]
    public string Symbol { get; set; }

    [StringLength(255)]
    public string Name { get; set; }

    [StringLength(255)]
    public string Type { get; set; }

    [StringLength(255)]
    public string Expiration { get; set; }

    [Column(TypeName = "float")]
    public double? Strike { get; set; }

    [Column(TypeName = "float")]
    public double? Ask { get; set; }

    [Column(TypeName = "float")]
    public double? AskSize { get; set; }

    [Column(TypeName = "float")]
    public double? Bid { get; set; }

    [Column(TypeName = "float")]
    public double? BidSize { get; set; }

    [Column(TypeName = "float")]
    public double? LastPrice { get; set; }

    [Column(TypeName = "float")]
    public double? LastSize { get; set; }

    [Column(TypeName = "float")]
    public double? ClosePrice { get; set; }
  }
}
