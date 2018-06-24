namespace IBLibrary.Models.Data
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("Scanner")]
  public class Scanner
  {
    public string Symbol;
    public double Ask;
    public double Bid;
  }
}
