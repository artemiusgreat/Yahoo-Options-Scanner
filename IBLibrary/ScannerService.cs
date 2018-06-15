using IBApi;
using IBLibrary.Classes;
using IBLibrary.Messages;
using IBLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBLibrary
{
  public class ScannerService : BaseService
  {
    public Task<List<Scanner>> GetScanner(Scanner query)
    {
      if (query == null)
      {
        query = new Scanner();
      }

      var process = Task.Run(() =>
      {
        var done = false;
        var id = new Random(DateTime.Now.Millisecond).Next();

        var subscription = new ScannerSubscription
        {
          Instrument = "STK",
          LocationCode = "STK.US.MAJOR",
          ScanCode = "HOT_BY_VOLUME"
        };

        var contracts = new List<Scanner>();

        Action<ErrorMessage> errorMessage = null;
        Action<ScannerMessage> scannerMessage = null;
        Action<ScannerEndMessage> scannerEndMessage = null;

        scannerMessage = (ScannerMessage data) =>
        {
          contracts.Add(new Scanner
          {
            Symbol = data.ContractDetails.Contract.Symbol
          });
        };

        scannerEndMessage = (ScannerEndMessage data) =>
        {
          done = true;
        };

        errorMessage = (ErrorMessage data) =>
        {
          var notifications = new List<int>
          {
            (int) ErrorCode.MarketDataFarmConnectionIsOK,
            (int) ErrorCode.HmdsDataFarmConnectionIsOK
          };

          if (notifications.Contains(data.ErrorCode) == false)
          {
            done = true;
          }
        };

        Sender.ErrorEvent += errorMessage;
        Sender.StockScannerEvent += scannerMessage;
        Sender.StockScannerEndEvent += scannerEndMessage;
        Sender.Socket.reqScannerSubscription(id, subscription, null);

        while (done == false) ;

        Sender.ErrorEvent -= errorMessage;
        Sender.StockScannerEvent -= scannerMessage;
        Sender.StockScannerEndEvent -= scannerEndMessage;

        return contracts;
      });

      return process;
    }
  }
}
