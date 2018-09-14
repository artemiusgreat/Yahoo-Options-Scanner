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
  public class CLongCondor : CCombination, ICombination
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
            var groupShortUp = group.ElementAt(i);

            for (var ii = i + 1; ii < count; ii++)
            {
              var groupLongUp = group.ElementAt(ii);

              for (var iii = ii + 1; iii < count; iii++)
              {
                var groupLongDown = group.ElementAt(iii);

                for (var iiii = iii + 1; iiii < count; iiii++)
                {
                  var groupShortDown = group.ElementAt(iiii);
                  var conditions = new []
                  {
                    Equals(groupLongUp.Option.Right, ERight.Call),
                    Equals(groupShortUp.Option.Right, ERight.Call),
                    Equals(groupLongDown.Option.Right, ERight.Put),
                    Equals(groupShortDown.Option.Right, ERight.Put),
                    Equals(groupShortUp.Option.Strike.CompareTo(groupLongUp.Option.Strike), 1),
                    Equals(groupShortDown.Option.Strike.CompareTo(groupLongDown.Option.Strike), -1),
                    Equals(groupLongUp.Option.Strike.CompareTo(groupLongDown.Option.Strike), 1)
                  };

                  if (conditions.All(condition => condition))
                  {
                    combinations.Add(GetScore(new List<IGroup>
                    {
                      groupLongUp,
                      groupShortUp,
                      groupLongDown,
                      groupShortDown
                    }));
                  }
                }
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
      var optionLongUp = groups.ElementAt(0).Option;
      var optionShortUp = groups.ElementAt(1).Option;
      var optionLongDown = groups.ElementAt(2).Option;
      var optionShortDown = groups.ElementAt(3).Option;

      optionLongUp.Direction = EDirection.Long;
      optionShortUp.Direction = EDirection.Short;
      optionLongDown.Direction = EDirection.Long;
      optionShortDown.Direction = EDirection.Short;

      return new CScore
      {
        Debit = CType.ToMoneyStr(optionLongUp.Ask - optionShortUp.Bid),
        Credit = CType.ToMoneyStr(0),
        Symbol = quote.Symbol,
        Distance = CType.ToMoneyStr(optionShortUp.Strike - optionLongUp.Strike),
        Position = CType.ToPositionStr(optionLongUp, optionShortUp, optionLongDown, optionShortDown),
        Expiration = CType.ToDateStr(optionLongUp.Expiration)
      };
    }
  }
}
