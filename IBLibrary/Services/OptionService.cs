using IBApi;
using IBLibrary.Classes;
using IBLibrary.Messages;
using IBLibrary.Models.Data;
using IBLibrary.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IBLibrary.Classes.Client;

namespace IBLibrary.Services
{
  public class OptionService : BaseService
  {
    public List<Task<List<Option>>> GetOptionDetails(List<Contract> dataContracts)
    {
      var options = new List<Option>();
      var processes = new List<Task<List<Option>>>();
      var generator = new Random(DateTime.Now.Millisecond);

      dataContracts.ForEach(ctr =>
      {
        processes.Add(Task.Run(() =>
        {
          var id = ctr.ConId;
          var completion = new TaskCompletionSource<Description>();

          var option = new Option
          {
            Type = ctr.Right,
            Symbol = ctr.Symbol,
            Strike = ctr.Strike,
            Name = ctr.LocalSymbol,
            Expiration = ctr.LastTradeDateOrContractMonth
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
                case TICK_BID_SIZE: option.BidSize = Math.Max(data.Size, 0); break;
                case TICK_ASK_SIZE: option.AskSize = Math.Max(data.Size, 0); break;
              }
            }
          };

          tickPriceMessage = (TickPriceMessage data) =>
          {
            if (id == data.RequestId)
            {
              switch (data.Field)
              {
                case TICK_BID_PRICE: option.Bid = Math.Max(data.Price, 0); break;
                case TICK_ASK_PRICE: option.Ask = Math.Max(data.Price, 0); break;
                case TICK_CLOSE_PRICE: option.ClosePrice = Math.Max(data.Price, 0); break;
              }
            }
          };

          tickSnapshotEndMessage = (TickSnapshotEndMessage data) =>
          {
            if (id == data.TickerId)
            {
              completion.TrySetResult(new Description());
            }
          };

          errorMessage = (ErrorMessage data) =>
          {
            if (id == data.RequestId || data.ErrorCode == (int) ErrorCode.NotConnected)
            {
              completion.TrySetResult(new Description
              {
                Code = data.ErrorCode,
                Message = data.Message
              });
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
          var completion = new TaskCompletionSource<Description>();

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
              completion.TrySetResult(new Description());
            }
          };

          errorMessage = (ErrorMessage data) =>
          {
            if (id == data.RequestId || data.ErrorCode == (int) ErrorCode.NotConnected)
            {
              completion.TrySetResult(new Description
              {
                Code = data.ErrorCode,
                Message = data.Message
              });
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
