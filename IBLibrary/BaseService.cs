using IBApi;
using IBLibrary.Classes;
using IBLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IBLibrary
{
  public class BaseService : IDisposable
  {
    protected Client Sender { get; set; }
    protected EReader Receiver { get; set; }

    public BaseService()
    {
      Sender = new Client();
      Sender.Socket.eConnect("127.0.0.1", 7496, 0);
      Receiver = new EReader(Sender.Socket, Sender.Signal);
      Receiver.Start();

      var process = new Thread(() =>
      {
        while (Sender.Socket.IsConnected())
        {
          Sender.Signal.waitForSignal();
          Receiver.processMsgs();
        }
      })
      {
        IsBackground = true
      };

      process.Start();
    }

    public void Dispose()
    {
      Sender.Socket.eDisconnect();
    }

    public List<Task<List<Contract>>> GetContracts(List<Contract> dataContracts)
    {
      var contracts = new List<Contract>();
      var processes = new List<Task<List<Contract>>>();

      dataContracts.ForEach(contract =>
      {
        processes.Add(Task.Run(() =>
        {
          var completion = new TaskCompletionSource<bool>();
          var id = new Random(DateTime.Now.Millisecond).Next();

          Action<ErrorMessage> errorMessage = null;
          Action<ContractDetailsMessage> contractMessage = null;
          Action<ContractDetailsEndMessage> contractEndMessage = null;

          contractMessage = (ContractDetailsMessage data) =>
          {
            if (id == data.RequestId)
            {
              contracts.Add(data.ContractDetails.Contract);
            }
          };

          contractEndMessage = (ContractDetailsEndMessage data) =>
          {
            if (id == data.RequestId)
            {
              completion.SetResult(true);
            }
          };

          errorMessage = (ErrorMessage data) =>
          {
            if (id == data.RequestId || data.ErrorCode == (int)ErrorCode.NotConnected)
            {
              id = new Random(DateTime.Now.Millisecond).Next();
              completion.SetResult(false);
            }
          };

          Sender.ErrorEvent += errorMessage;
          Sender.ContractDetailsEvent += contractMessage;
          Sender.ContractDetailsEndEvent += contractEndMessage;
          Sender.Socket.reqContractDetails(id, contract);

          var complete = completion.Task.Result;

          Sender.ErrorEvent -= errorMessage;
          Sender.ContractDetailsEvent -= contractMessage;
          Sender.ContractDetailsEndEvent -= contractEndMessage;

          return contracts;
        }));
      });

      return processes;
    }
  }
}
