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
  public class CLongStrangle : CCombination, ICombination
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

      var selectorsForCalls = selector.And
      (
        new BsonDocument(new BsonDocument("$where", new BsonJavaScript("this.Option.Strike > this.Quote.Ask"))),
        selector.Eq("Option.Right", 1),
        selector.Gt("Option.Bid", 0)
      );

      var selectorsForPuts = selector.And
      (
        new BsonDocument(new BsonDocument("$where", new BsonJavaScript("this.Option.Strike < this.Quote.Bid"))),
        selector.Eq("Option.Right", 0),
        selector.Gt("Option.Bid", 0)
      );

      var selectors = selector.Or
      (
        selectorsForPuts,
        selectorsForCalls
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
      var scores = groups
        .GroupBy(group => group.Option.Expiration)
        .SelectMany(group =>
        {
          var count = group.Count();
          var combinations = new List<IScore>();

          for (var i = 0; i < count; i++)
          {
            var groupUp = group.ElementAt(i);

            for (var ii = i + 1; ii < count; ii++)
            {
              var groupDown = group.ElementAt(ii);
              var conditions = new []
              {
                Equals(groupUp.Option.Right, ERight.Call),
                Equals(groupDown.Option.Right, ERight.Put)
              };

              if (conditions.All(condition => condition))
              {
                combinations.Add(GetScore(new List<IGroup>
                {
                  groupUp,
                  groupDown
                }));
              }
            }
          }

          return combinations;
        });

      return scores;
    }

    /// <summary>
    /// Convert combo model to simplified score model used in UI
    /// </summary>
    /// <param name="groups"></param>
    /// <returns></returns>
    public IScore GetScore(IEnumerable<IGroup> groups)
    {
      var quote = groups.ElementAt(0).Quote;
      var optionUp = groups.ElementAt(0).Option;
      var optionDown = groups.ElementAt(1).Option;

      optionUp.Direction = EDirection.Long;
      optionDown.Direction = EDirection.Long;

      return new CScore
      {
        Debit = CType.ToMoneyStr(optionUp.Ask + optionDown.Ask),
        Credit = CType.ToMoneyStr(0),
        Symbol = quote.Symbol,
        Distance = CType.ToMoneyStr(optionUp.Strike - optionDown.Strike),
        Position = CType.ToPositionStr(optionUp, optionDown),
        Expiration = CType.ToDateStr(optionUp.Expiration)
      };
    }
  }
}
