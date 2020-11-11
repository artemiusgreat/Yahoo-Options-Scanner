using System;
using System.Collections.Generic;

namespace Service.Models.Message
{
  public interface IScannerMessage
  {
    int Page { get; set; }
    int Limit { get; set; }
    int Cache { get; set; }
    string Combo { get; set; }
    string Symbol { get; set; }
    DateTime? Stop { get; set; }
    DateTime? Start { get; set; }
    IEnumerable<string> Symbols { get; }
  }

  /// <summary>
  /// Model that defines input parameters for the scanner
  /// </summary>
  public class CScannerMessage : IScannerMessage
  {
    public int Page { get; set; }
    public int Limit { get; set; }
    public int Cache { get; set; }
    public string Combo { get; set; }
    public string Symbol { get; set; }
    public DateTime? Stop { get; set; }
    public DateTime? Start { get; set; }

    /// <summary>
    /// Split comma separated symbols into array
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> Symbols
    {
      get
      {
        return Symbol.Split(',');
      }
    }

    /// <summary>
    /// Validate correctness of the model
    /// </summary>
    /// <returns></returns>
    public List<string> GetErrors()
    {
      var errors = new List<string>();

      if (string.IsNullOrEmpty(Symbol))
      {
        errors.Add("Symbol is not defined");
      }

      if (Start == null)
      {
        errors.Add("Start date is not defined");
      }

      if (Stop == null)
      {
        errors.Add("End date is not defined");
      }

      return errors;
    }
  }
}
