using MongoDB.Bson;
using MongoDB.Driver;
using Service.Classes;
using Service.Models.Data;
using Service.Models.Message;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Components.Combinations
{
  public class CShortCall : CCombination, ICombination
  {
    /// <summary>
    /// Get all options from local DB
    /// </summary>
    /// <param name="container"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public override async Task<IServiceMessage<IGroup>> GetGroupsAsync(IContainer<IGroup> container, IScannerMessage message)
    {
      var order = Builders<IGroup>.Sort;
      var selector = Builders<IGroup>.Filter;

      var orders = order.Combine
      (
        order.Descending("Option.Strike")
      );

      var selectors = selector.And
      (
        new BsonDocument(new BsonDocument("$where", new BsonJavaScript("this.Option.Strike > this.Quote.Ask"))),
        selector.Eq("Option.Right", 1),
        selector.Gt("Option.Bid", 0)
      );

      var query = container
        .Query<IGroup>(message.Symbol)
        .Find(selectors);

      return new CServiceMessage<IGroup>
      {
        Count = await query.CountAsync(),
        Items = await query
          .Sort(orders)
          .Skip((message.Page - 1) * message.Limit)
          .Limit(message.Limit)
          .ToListAsync()
      };
    }

    /// <summary>
    /// Get all possible combinations of specific option strategy
    /// </summary>
    /// <param name="groups"></param>
    /// <returns></returns>
    public override IEnumerable<IScore> GetScores(IEnumerable<IGroup> groups)
    {
      return groups.Select(group => GetScore(new[] { group }));
    }

    /// <summary>
    /// Convert combo model to simplified score model used in UI
    /// </summary>
    /// <param name="groups"></param>
    /// <returns></returns>
    public IScore GetScore(IEnumerable<IGroup> groups)
    {
      var quote = groups.ElementAt(0).Quote;
      var option = groups.ElementAt(0).Option;

      option.Direction = EDirection.Short;

      return new CScore
      {
        Debit = CType.ToMoneyStr(0),
        Credit = CType.ToMoneyStr(option.Bid),
        Symbol = quote.Symbol,
        Distance = CType.ToMoneyStr(option.Strike - quote.Ask),
        Position = CType.ToPositionStr(option),
        Expiration = CType.ToDateStr(option.Expiration)
      };
    }
  }
}
