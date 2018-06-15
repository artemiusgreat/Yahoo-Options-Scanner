namespace IBLibrary.Models
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("Option")]
  public partial class Option
  {
    public int OptionId { get; set; }

    [StringLength(255)]
    public string OptionStock { get; set; }

    [StringLength(255)]
    public string OptionName { get; set; }

    [StringLength(255)]
    public string OptionType { get; set; }

    [StringLength(255)]
    public string OptionExpiration { get; set; }

    [Column(TypeName = "float")]
    public double? OptionStrike { get; set; }

    [Column(TypeName = "float")]
    public double? OptionAsk { get; set; }

    [Column(TypeName = "float")]
    public double? OptionBid { get; set; }

    [Column(TypeName = "float")]
    public double? OptionAskSize { get; set; }

    [Column(TypeName = "float")]
    public double? OptionBidSize { get; set; }
  }
}
