using Service.Classes;
using Service.Models.Data;
using Service.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service.Components.Connectors
{
  public class Quote
  {
    public string language { get; set; }
    public string region { get; set; }
    public string quoteType { get; set; }
    public string quoteSourceName { get; set; }
    public string currency { get; set; }
    public long sharesOutstanding { get; set; }
    public double bookValue { get; set; }
    public double fiftyDayAverage { get; set; }
    public double fiftyDayAverageChange { get; set; }
    public double fiftyDayAverageChangePercent { get; set; }
    public double twoHundredDayAverage { get; set; }
    public double twoHundredDayAverageChange { get; set; }
    public double twoHundredDayAverageChangePercent { get; set; }
    public long marketCap { get; set; }
    public double forwardPE { get; set; }
    public double priceToBook { get; set; }
    public long sourceInterval { get; set; }
    public string exchangeTimezoneName { get; set; }
    public string exchangeTimezoneShortName { get; set; }
    public long gmtOffSetMilliseconds { get; set; }
    public string market { get; set; }
    public string exchange { get; set; }
    public double regularMarketPrice { get; set; }
    public long regularMarketTime { get; set; }
    public double regularMarketChange { get; set; }
    public double regularMarketOpen { get; set; }
    public double regularMarketDayHigh { get; set; }
    public double regularMarketDayLow { get; set; }
    public double postMarketChange { get; set; }
    public double regularMarketChangePercent { get; set; }
    public string regularMarketDayRange { get; set; }
    public double regularMarketPreviousClose { get; set; }
    public double bid { get; set; }
    public double ask { get; set; }
    public long bidSize { get; set; }
    public long askSize { get; set; }
    public string messageBoardId { get; set; }
    public string fullExchangeName { get; set; }
    public string longName { get; set; }
    public string financialCurrency { get; set; }
    public long averageDailyVolume3Month { get; set; }
    public long averageDailyVolume10Day { get; set; }
    public double fiftyTwoWeekLowChange { get; set; }
    public double fiftyTwoWeekLowChangePercent { get; set; }
    public string fiftyTwoWeekRange { get; set; }
    public double fiftyTwoWeekHighChange { get; set; }
    public double fiftyTwoWeekHighChangePercent { get; set; }
    public double fiftyTwoWeekLow { get; set; }
    public double fiftyTwoWeekHigh { get; set; }
    public long dividendDate { get; set; }
    public long earningsTimestamp { get; set; }
    public long earningsTimestampStart { get; set; }
    public long earningsTimestampEnd { get; set; }
    public double trailingAnnualDividendRate { get; set; }
    public double trailingPE { get; set; }
    public double trailingAnnualDividendYield { get; set; }
    public double epsTrailingTwelveMonths { get; set; }
    public double epsForward { get; set; }
    public long regularMarketVolume { get; set; }
    public long priceHint { get; set; }
    public double postMarketChangePercent { get; set; }
    public long postMarketTime { get; set; }
    public double postMarketPrice { get; set; }
    public long exchangeDataDelayedBy { get; set; }
    public string marketState { get; set; }
    public bool esgPopulated { get; set; }
    public bool tradeable { get; set; }
    public string shortName { get; set; }
    public string symbol { get; set; }
  }

  public class Call
  {
    public string contractSymbol { get; set; }
    public double strike { get; set; }
    public string currency { get; set; }
    public double lastPrice { get; set; }
    public double change { get; set; }
    public double percentChange { get; set; }
    public long volume { get; set; }
    public long openInterest { get; set; }
    public double bid { get; set; }
    public double ask { get; set; }
    public string contractSize { get; set; }
    public int expiration { get; set; }
    public int lastTradeDate { get; set; }
    public double impliedVolatility { get; set; }
    public bool inTheMoney { get; set; }
  }

  public class Put
  {
    public string contractSymbol { get; set; }
    public double strike { get; set; }
    public string currency { get; set; }
    public double lastPrice { get; set; }
    public double change { get; set; }
    public double percentChange { get; set; }
    public long volume { get; set; }
    public long openInterest { get; set; }
    public double bid { get; set; }
    public double ask { get; set; }
    public string contractSize { get; set; }
    public int expiration { get; set; }
    public int lastTradeDate { get; set; }
    public double impliedVolatility { get; set; }
    public bool inTheMoney { get; set; }
  }

  public class Option
  {
    public long expirationDate { get; set; }
    public bool hasMiniOptions { get; set; }
    public List<Call> calls { get; set; }
    public List<Put> puts { get; set; }
  }

  public class Result
  {
    public string underlyingSymbol { get; set; }
    public List<int> expirationDates { get; set; }
    public List<double> strikes { get; set; }
    public bool hasMiniOptions { get; set; }
    public Quote quote { get; set; }
    public List<Option> options { get; set; }
  }

  public class OptionChain
  {
    public List<Result> result { get; set; }
    public object error { get; set; }
  }

  public class CYahoo
  {
    public OptionChain optionChain { get; set; }
  }

  /// <summary>
  /// Class to parse options chain from Yahoo Finance API
  /// </summary>
  public class CYahooConnector : IConnector, IDisposable
  {
    public string Source { get; set; }
    public HttpClient Client { get; set; }

    public CYahooConnector()
    {
      Client = new HttpClient();
      Source = "https://query2.finance.yahoo.com/v7/finance/options";
    }

    /// <summary>
    /// Get list of records with info about quote and option
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, List<IGroup>>> GetGroupsAsync(IScannerMessage message)
    {
      var stop = CType.ToSeconds(message.Stop.Value);
      var start = CType.ToSeconds(message.Start.Value);
      var combos = new Dictionary<string, List<IGroup>>();

      foreach (var symbol in message.Symbols)
      {
        combos[symbol] = new List<IGroup>();

        var dates = await GetExpirationsAsync(symbol);

        foreach (var date in dates)
        {
          if (date >= start && date <= stop)
          {
            combos[symbol].AddRange(await GetChainAsync(symbol, date));
          }
        }
      }

      return combos;
    }

    /// <summary>
    /// Get list of expirations dates
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public async Task<List<int>> GetExpirationsAsync(string symbol)
    {
      var source = await Client.GetAsync(Source + "/" + symbol);
      var content = await source.Content.ReadAsAsync<CYahoo>();

      return content.optionChain.result.FirstOrDefault().expirationDates;
    }

    /// <summary>
    /// Get options for expiration date
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    public async Task<List<IGroup>> GetChainAsync(string symbol, int date)
    {
      var groups = new List<IGroup>();
      var source = await Client.GetAsync(Source + "/" + symbol + "?date=" + date);

      return await source.Content.ReadAsAsync<CYahoo>().ContinueWith(content =>
      {
        var chain = content.Result.optionChain.result.FirstOrDefault();
        var options = chain.options.FirstOrDefault();

        options.calls.ForEach(option =>
        {
          groups.Add(new CGroup
          {
            Quote = ToQuote(chain.quote),
            Option = ToOption(option)
          });
        });

        options.puts.ForEach(option =>
        {
          groups.Add(new CGroup
          {
            Quote = ToQuote(chain.quote),
            Option = ToOption(option)
          });
        });

        return groups;
      });
    }

    /// <summary>
    /// Clean up
    /// </summary>
    public void Dispose()
    {
      Client.Dispose();
    }

    /// <summary>
    /// Convert Yahoo's quote model to internal model
    /// </summary>
    /// <param name="quote"></param>
    /// <returns></returns>
    public static IQuote ToQuote(Quote quote)
    {
      return new CQuote
      {
        Bid = quote.bid,
        Ask = quote.ask,
        BidSize = quote.bidSize,
        AskSize = quote.askSize,
        Name = quote.longName,
        Symbol = quote.symbol,
        Currency = quote.currency,
        Exchange = quote.exchange
      };
    }

    /// <summary>
    /// Convert Yahoo's option model to internal model
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public static IOption ToOption(Call option)
    {
      return new COption
      {
        Volume = option.volume,
        Expiration = option.expiration,
        OpenInterest = option.openInterest,
        Bid = option.bid,
        Ask = option.ask,
        Strike = option.strike,
        LastPrice = option.lastPrice,
        Symbol = option.contractSymbol,
        Currency = option.currency,
        Right = ERight.Call
      };
    }

    /// <summary>
    /// Convert Yahoo's option model to internal model
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public static IOption ToOption(Put option)
    {
      return new COption
      {
        Volume = option.volume,
        Expiration = option.expiration,
        OpenInterest = option.openInterest,
        Bid = option.bid,
        Ask = option.ask,
        Strike = option.strike,
        LastPrice = option.lastPrice,
        Symbol = option.contractSymbol,
        Currency = option.currency,
        Right = ERight.Put
      };
    }
  }
}
