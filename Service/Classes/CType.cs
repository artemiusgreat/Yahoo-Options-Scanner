using Service.Models.Data;
using System;
using System.Linq;

namespace Service.Classes
{
  /// <summary>
  /// Class used to format values and convert types
  /// </summary>
  public static class CType
  {
    static readonly DateTime UnixDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    static readonly double UnixSeconds = (DateTime.MaxValue - UnixDate).TotalSeconds;

    /// <summary>
    /// Convert string to double
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static double ToDouble(string value)
    {
      double.TryParse(value, out double data);
      return data;
    }

    /// <summary>
    /// Convert string to date time
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static long ToTime(string value)
    {
      DateTime.TryParse(value, out DateTime data);
      return data.Ticks;
    }

    /// <summary>
    /// Convert string to date time
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTime ToDate(long value)
    {
      return (value > UnixSeconds ? UnixDate.AddMilliseconds(value) : UnixDate.AddSeconds(value));
    }

    /// <summary>
    /// Convert date time to timestamp measured in seconds
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static double ToSeconds(DateTime value)
    {
      return (value - UnixDate).TotalSeconds;
    }

    /// <summary>
    /// Convert date time to string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToDateStr(long value)
    {
      return ToDate(value).ToString("yyyy-MM-dd");
    }

    /// <summary>
    /// Convert double to string money-like format
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToMoneyStr(double value)
    {
      return value.ToString("0.00");
    }

    /// <summary>
    /// Convert double to string money-like format
    /// </summary>
    /// <param name="arguments"></param>
    /// <returns></returns>
    public static string ToPositionStr(params IOption[] arguments)
    {
      var items = arguments.Select(option =>
      {
        var position = CType.ToMoneyStr(option.Strike);

        if (Equals(option.Direction, EDirection.Long))
        {
          position = "+" + position;
        }

        if (Equals(option.Direction, EDirection.Short))
        {
          position = "-" + position;
        }

        if (Equals(option.Right, ERight.Call))
        {
          position += "C";
        }

        if (Equals(option.Right, ERight.Put))
        {
          position += "P";
        }

        return position;
      });

      return string.Join(" / ", items);
    }
  }
}
