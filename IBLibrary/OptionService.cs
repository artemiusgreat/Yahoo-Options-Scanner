using IBApi;
using IBLibrary.Classes;
using IBLibrary.Messages;
using IBLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IBLibrary.Classes.Client;

namespace IBLibrary
{
  public class OptionService : BaseService
  {
    public List<Task<List<Option>>> GetOptionDetails(string exchange, List<Contract> dataContracts)
    {
      var options = new List<Option>();
      var processes = new List<Task<List<Option>>>();
      var generator = new Random(DateTime.Now.Millisecond);

      dataContracts.ForEach(ctr =>
      {
        processes.Add(Task.Run(() =>
        {
          var id = generator.Next();
          var completion = new TaskCompletionSource<bool>();

          var option = new Option
          {
            OptionType = ctr.Right,
            OptionStock = ctr.Symbol,
            OptionStrike = ctr.Strike,
            OptionName = ctr.LocalSymbol,
            OptionExpiration = ctr.LastTradeDateOrContractMonth
          };

          Action<ErrorMessage> errorMessage = null;
          Action<TickSizeMessage> tickSizeMessage = null;
          Action<TickPriceMessage> tickPriceMessage = null;
          Action<TickSnapshotEndMessage> tickSnapshotEndMessage = null;

          tickSizeMessage = (TickSizeMessage data) =>
          {
            if (id == data.RequestId)
            {
              switch (data.Field)
              {
                case TICK_BID_SIZE: option.OptionBidSize = data.Size; break;
                case TICK_ASK_SIZE: option.OptionAskSize = data.Size; break;
              }
            }
          };

          tickPriceMessage = (TickPriceMessage data) =>
          {
            if (id == data.RequestId)
            {
              switch (data.Field)
              {
                case TICK_BID_PRICE: option.OptionBid = data.Price; break;
                case TICK_ASK_PRICE: option.OptionAsk = data.Price; break;
              }
            }
          };

          tickSnapshotEndMessage = (TickSnapshotEndMessage data) =>
          {
            completion.SetResult(true);
          };

          errorMessage = (ErrorMessage data) =>
          {
            if (id == data.RequestId || data.ErrorCode == (int) ErrorCode.NotConnected)
            {
              completion.SetResult(false);
            }
          };

          var contract = new Contract
          {
            LocalSymbol = ctr.LocalSymbol,
            Symbol = ctr.Symbol,
            Exchange = ctr.Exchange,
            Currency = ctr.Currency,
            SecType = ctr.SecType
          };

          Sender.ErrorEvent += errorMessage;
          Sender.TickSizeEvent += tickSizeMessage;
          Sender.TickPriceEvent += tickPriceMessage;
          Sender.TickSnapshotEndEvent += tickSnapshotEndMessage;
          Sender.Socket.reqMktData(id, contract, string.Empty, true, false, null);

          var complete = completion.Task.Result;

          Sender.ErrorEvent -= errorMessage;
          Sender.TickSizeEvent -= tickSizeMessage;
          Sender.TickPriceEvent -= tickPriceMessage;
          Sender.TickSnapshotEndEvent -= tickSnapshotEndMessage;

          options.Add(option);

          return options;
        }));
      });

      return processes;
    }

    public List<Task<List<Option>>> GetOptionParams(List<string> symbols)
    {
      var contracts = new List<Option>();
      var optionParams = new List<OptionParam>();
      var processes = new List<Task<List<Option>>>();
      var generator = new Random(DateTime.Now.Millisecond);

      symbols.ForEach(symbol =>
      {
        processes.Add(Task.Run(() =>
        {
          var id = generator.Next();
          var optionParam = new OptionParam();
          var completion = new TaskCompletionSource<bool>();

          Action<ErrorMessage> errorMessage = null;
          Action<SecurityDefinitionOptionParameterMessage> contractMessage = null;
          Action<SecurityDefinitionOptionParameterEndMessage> contractEndMessage = null;

          contractMessage = (SecurityDefinitionOptionParameterMessage data) =>
          {
            if (id == data.RequestId)
            {
              optionParam.Strikes = data.Strikes.ToList();
              optionParam.Expirations = data.Expirations.ToList();
            }
          };

          contractEndMessage = (SecurityDefinitionOptionParameterEndMessage data) =>
          {
            if (id == data.RequestId)
            {
              completion.SetResult(true);
            }
          };

          errorMessage = (ErrorMessage data) =>
          {
            if (id == data.RequestId || data.ErrorCode == (int) ErrorCode.NotConnected)
            {
              completion.SetResult(false);
            }
          };

          Sender.ErrorEvent += errorMessage;
          Sender.SecurityDefinitionEvent += contractMessage;
          Sender.SecurityDefinitionEndEvent += contractEndMessage;
          Sender.Socket.reqSecDefOptParams(id, symbol, "", "STK", 0);

          var complete = completion.Task.Result;

          Sender.ErrorEvent -= errorMessage;
          Sender.SecurityDefinitionEvent -= contractMessage;
          Sender.SecurityDefinitionEndEvent -= contractEndMessage;

          return contracts;
        }));
      });

      return processes;
    }
  }
}
