using Service.Classes;
using Service.Components.Combinations;
using Service.Models.Data;
using Service.Models.Message;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Components
{
  public class CCombinations : IDisposable
  {
    public ICombination Combination { get; set; }
    public IContainer<IGroup> Container { get; set; }

    /// <summary>
    /// Factory that specifies exact connector
    /// </summary>
    /// <param name="combo"></param>
    public CCombinations(string combo)
    {
      Container = new CContainer<IGroup>();

      switch (combo)
      {
        case nameof(ECombination.ShortPut): Combination = new CShortPut(); break;
        case nameof(ECombination.ShortCall): Combination = new CShortCall(); break;
        case nameof(ECombination.LongCondor): Combination = new CLongCondor(); break;
        case nameof(ECombination.ShortCondor): Combination = new CShortCondor(); break;
        case nameof(ECombination.LongStrangle): Combination = new CLongStrangle(); break;
        case nameof(ECombination.ShortStrangle): Combination = new CShortStrangle(); break;
        case nameof(ECombination.LongPutSpread): Combination = new CLongPutSpread(); break;
        case nameof(ECombination.ShortPutSpread): Combination = new CShortPutSpread(); break;
        case nameof(ECombination.LongCallSpread): Combination = new CLongCallSpread(); break;
        case nameof(ECombination.ShortCallSpread): Combination = new CShortCallSpread(); break;
      }
    }

    /// <summary>
    /// Get options from Yahoo API and save to local DB
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task GetOptionsAsync(IScannerMessage message)
    {
      using (var connector = new CConnector(EConnector.Yahoo))
      {
        var groups = await connector.GetGroupsAsync(message);

        foreach (var group in groups)
        {
          await Container.AddPartitionAsync(group.Value, group.Key);
        }
      }
    }

    /// <summary>
    /// Convert list of options into a list of possible positions
    /// </summary>
    /// <returns></returns>
    public async Task<IServiceMessage<IScore>> GetCombinationsAsync(IScannerMessage message)
    {
      var count = 0L;
      var scores = new List<IScore>();
      var symbols = message.Symbols;

      foreach (var symbol in symbols)
      {
        message.Symbol = symbol;

        var response = await Combination.GetGroupsAsync(Container, message);
        var combinations = Combination.GetScores(response.Items);

        count = Math.Max(count, response.Count);
        scores.AddRange(combinations);
      }

      return new CServiceMessage<IScore>
      {
        Count = count,
        Items = scores
      };
    }

    /// <summary>
    /// Clean up
    /// </summary>
    public void Dispose()
    {
    }
  }
}
