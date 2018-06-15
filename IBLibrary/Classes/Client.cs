using IBApi;
using IBLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IBLibrary.Classes
{
  public class Client : EWrapper
  {
    public EClientSocket Socket { get; set; }
    public EReaderSignal Signal { get; set; }

    public const int TICK_BID_SIZE = 0;
    public const int TICK_BID_PRICE = 1;
    public const int TICK_ASK_PRICE = 2;
    public const int TICK_ASK_SIZE = 3;
    public const int TICK_LAST_PRICE = 4;
    public const int TICK_LAST_SIZE = 5;
    public const int TICK_CLOSE_PRICE = 9;

    public event Action<ErrorMessage> ErrorEvent;
    public event Action<TickSizeMessage> TickSizeEvent;
    public event Action<TickPriceMessage> TickPriceEvent;
    public event Action<TickReqParamsMessage> TickParamsEvent;
    public event Action<TickSnapshotEndMessage> TickSnapshotEndEvent;
    public event Action<ContractDetailsMessage> ContractDetailsEvent;
    public event Action<ContractDetailsEndMessage> ContractDetailsEndEvent;
    public event Action<ScannerMessage> StockScannerEvent;
    public event Action<ScannerEndMessage> StockScannerEndEvent;
    public event Action<SecurityDefinitionOptionParameterMessage> SecurityDefinitionEvent;
    public event Action<SecurityDefinitionOptionParameterEndMessage> SecurityDefinitionEndEvent;

    public class OptionParam
    {
      public List<double> Strikes { get; set; }
      public List<string> Expirations { get; set; }
    }

    public Client()
    {
      Signal = new EReaderMonitorSignal();
      Socket = new EClientSocket(this, Signal);
    }

    public void ShowMessage(string message)
    {
      Console.WriteLine(message);
    }

    public string GetExecutionName()
    {
      var st = new StackTrace();
      var sf = st.GetFrame(1);

      return sf.GetMethod().Name;
    }

    public void error(string str)
    {
      ErrorEvent?.Invoke(new ErrorMessage(0, 0, str));
    }

    public void error(Exception e)
    {
      ErrorEvent?.Invoke(new ErrorMessage(0, 0, e.Message));
    }

    public void error(int id, int errorCode, string errorMsg)
    {
      ErrorEvent?.Invoke(new ErrorMessage(id, errorCode, errorMsg));
    }

    public void currentTime(long time)
    {
      ShowMessage(GetExecutionName());
    }

    public void tickPrice(int tickerId, int field, double price, TickAttrib attribs)
    {
      TickPriceEvent?.Invoke(
        new TickPriceMessage(tickerId, field, price, attribs)
      );
    }

    public void tickSize(int tickerId, int field, int size)
    {
      TickSizeEvent?.Invoke(
        new TickSizeMessage(tickerId, field, size)
      );
    }

    public void tickString(int tickerId, int field, string value)
    {
      ShowMessage(GetExecutionName());
    }

    public void tickGeneric(int tickerId, int field, double value)
    {
      ShowMessage(GetExecutionName());
    }

    public void tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
    {
      ShowMessage(GetExecutionName());
    }

    public void deltaNeutralValidation(int reqId, DeltaNeutralContract deltaNeutralContract)
    {
      ShowMessage(GetExecutionName());
    }

    public void tickOptionComputation(int tickerId, int field, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
    {
      ShowMessage(GetExecutionName());
    }

    public void tickSnapshotEnd(int tickerId)
    {
      TickSnapshotEndEvent?.Invoke(
        new TickSnapshotEndMessage(tickerId)
      );
    }

    public void nextValidId(int orderId)
    {
      ShowMessage(GetExecutionName());
    }

    public void managedAccounts(string accountsList)
    {
      ShowMessage(GetExecutionName());
    }

    public void connectionClosed()
    {
      ShowMessage(GetExecutionName());
    }

    public void accountSummary(int reqId, string account, string tag, string value, string currency)
    {
      ShowMessage(GetExecutionName());
    }

    public void accountSummaryEnd(int reqId)
    {
      ShowMessage(GetExecutionName());
    }

    public void bondContractDetails(int reqId, ContractDetails contract)
    {
      ShowMessage(GetExecutionName());
    }

    public void updateAccountValue(string key, string value, string currency, string accountName)
    {
      ShowMessage(GetExecutionName());
    }

    public void updatePortfolio(Contract contract, double position, double marketPrice, double marketValue, double averageCost, double unrealizedPNL, double realizedPNL, string accountName)
    {
      ShowMessage(GetExecutionName());
    }

    public void updateAccountTime(string timestamp)
    {
      ShowMessage(GetExecutionName());
    }

    public void accountDownloadEnd(string account)
    {
      ShowMessage(GetExecutionName());
    }

    public void orderStatus(int orderId, string status, double filled, double remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId, string whyHeld, double mktCapPrice)
    {
      ShowMessage(GetExecutionName());
    }

    public void openOrder(int orderId, Contract contract, Order order, OrderState orderState)
    {
      ShowMessage(GetExecutionName());
    }

    public void openOrderEnd()
    {
      ShowMessage(GetExecutionName());
    }

    public void contractDetails(int reqId, ContractDetails contractDetails)
    {
      ContractDetailsEvent?.Invoke(new ContractDetailsMessage(reqId, contractDetails));
    }

    public void contractDetailsEnd(int reqId)
    {
      ContractDetailsEndEvent?.Invoke(new ContractDetailsEndMessage(reqId));
    }

    public void execDetails(int reqId, Contract contract, Execution execution)
    {
      ShowMessage(GetExecutionName());
    }

    public void execDetailsEnd(int reqId)
    {
      ShowMessage(GetExecutionName());
    }

    public void commissionReport(CommissionReport commissionReport)
    {
      ShowMessage(GetExecutionName());
    }

    public void fundamentalData(int reqId, string data)
    {
      ShowMessage(GetExecutionName());
    }

    public void historicalData(int reqId, Bar bar)
    {
      ShowMessage(GetExecutionName());
    }

    public void historicalDataUpdate(int reqId, Bar bar)
    {
      ShowMessage(GetExecutionName());
    }

    public void historicalDataEnd(int reqId, string start, string end)
    {
      ShowMessage(GetExecutionName());
    }

    public void marketDataType(int reqId, int marketDataType)
    {
      ShowMessage(GetExecutionName());
    }

    public void updateMktDepth(int tickerId, int position, int operation, int side, double price, int size)
    {
      ShowMessage(GetExecutionName());
    }

    public void updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, int size)
    {
      ShowMessage(GetExecutionName());
    }

    public void updateNewsBulletin(int msgId, int msgType, string message, string origExchange)
    {
      ShowMessage(GetExecutionName());
    }

    public void position(string account, Contract contract, double pos, double avgCost)
    {
      ShowMessage(GetExecutionName());
    }

    public void positionEnd()
    {
      ShowMessage(GetExecutionName());
    }

    public void realtimeBar(int reqId, long time, double open, double high, double low, double close, long volume, double WAP, int count)
    {
      ShowMessage(GetExecutionName());
    }

    public void scannerParameters(string xml)
    {
      ShowMessage(GetExecutionName());
    }

    public void scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
    {
      StockScannerEvent?.Invoke(new ScannerMessage(reqId, rank, contractDetails, distance, benchmark, projection, legsStr));
    }

    public void scannerDataEnd(int reqId)
    {
      StockScannerEndEvent?.Invoke(new ScannerEndMessage(reqId));
    }

    public void receiveFA(int faDataType, string faXmlData)
    {
      ShowMessage(GetExecutionName());
    }

    public void verifyMessageAPI(string apiData)
    {
      ShowMessage(GetExecutionName());
    }

    public void verifyCompleted(bool isSuccessful, string errorText)
    {
      ShowMessage(GetExecutionName());
    }

    public void verifyAndAuthMessageAPI(string apiData, string xyzChallenge)
    {
      ShowMessage(GetExecutionName());
    }

    public void verifyAndAuthCompleted(bool isSuccessful, string errorText)
    {
      ShowMessage(GetExecutionName());
    }

    public void displayGroupList(int reqId, string groups)
    {
      ShowMessage(GetExecutionName());
    }

    public void displayGroupUpdated(int reqId, string contractInfo)
    {
      ShowMessage(GetExecutionName());
    }

    public void connectAck()
    {
      ShowMessage(GetExecutionName());
    }

    public void positionMulti(int requestId, string account, string modelCode, Contract contract, double pos, double avgCost)
    {
      ShowMessage(GetExecutionName());
    }

    public void positionMultiEnd(int requestId)
    {
      ShowMessage(GetExecutionName());
    }

    public void accountUpdateMulti(int requestId, string account, string modelCode, string key, string value, string currency)
    {
      ShowMessage(GetExecutionName());
    }

    public void accountUpdateMultiEnd(int requestId)
    {
      ShowMessage(GetExecutionName());
    }

    public void securityDefinitionOptionParameter(int reqId, string exchange, int underlyingConId, string tradingClass, string multiplier, HashSet<string> expirations, HashSet<double> strikes)
    {
      SecurityDefinitionEvent?.Invoke(
        new SecurityDefinitionOptionParameterMessage(
          reqId, exchange, underlyingConId, tradingClass, multiplier, expirations, strikes
        )
      );
    }

    public void securityDefinitionOptionParameterEnd(int reqId)
    {
      SecurityDefinitionEndEvent?.Invoke(
        new SecurityDefinitionOptionParameterEndMessage(reqId)
      );
    }

    public void softDollarTiers(int reqId, SoftDollarTier[] tiers)
    {
      ShowMessage(GetExecutionName());
    }

    public void familyCodes(FamilyCode[] familyCodes)
    {
      ShowMessage(GetExecutionName());
    }

    public void symbolSamples(int reqId, ContractDescription[] contractDescriptions)
    {
      ShowMessage(GetExecutionName());
    }

    public void mktDepthExchanges(DepthMktDataDescription[] depthMktDataDescriptions)
    {
      ShowMessage(GetExecutionName());
    }

    public void tickNews(int tickerId, long timeStamp, string providerCode, string articleId, string headline, string extraData)
    {
      ShowMessage(GetExecutionName());
    }

    public void smartComponents(int reqId, Dictionary<int, KeyValuePair<string, char>> theMap)
    {
      ShowMessage(GetExecutionName());
    }

    public void tickReqParams(int tickerId, double minTick, string bboExchange, int snapshotPermissions)
    {
      TickParamsEvent?.Invoke(
        new TickReqParamsMessage(tickerId, minTick, bboExchange, snapshotPermissions)
      );
    }

    public void newsProviders(NewsProvider[] newsProviders)
    {
      ShowMessage(GetExecutionName());
    }

    public void newsArticle(int requestId, int articleType, string articleText)
    {
      ShowMessage(GetExecutionName());
    }

    public void historicalNews(int requestId, string time, string providerCode, string articleId, string headline)
    {
      ShowMessage(GetExecutionName());
    }

    public void historicalNewsEnd(int requestId, bool hasMore)
    {
      ShowMessage(GetExecutionName());
    }

    public void headTimestamp(int reqId, string headTimestamp)
    {
      ShowMessage(GetExecutionName());
    }

    public void histogramData(int reqId, HistogramEntry[] data)
    {
      ShowMessage(GetExecutionName());
    }

    public void rerouteMktDataReq(int reqId, int conId, string exchange)
    {
      ShowMessage(GetExecutionName());
    }

    public void rerouteMktDepthReq(int reqId, int conId, string exchange)
    {
      ShowMessage(GetExecutionName());
    }

    public void marketRule(int marketRuleId, PriceIncrement[] priceIncrements)
    {
      ShowMessage(GetExecutionName());
    }

    public void pnl(int reqId, double dailyPnL, double unrealizedPnL, double realizedPnL)
    {
      ShowMessage(GetExecutionName());
    }

    public void pnlSingle(int reqId, int pos, double dailyPnL, double unrealizedPnL, double realizedPnL, double value)
    {
      ShowMessage(GetExecutionName());
    }

    public void historicalTicks(int reqId, HistoricalTick[] ticks, bool done)
    {
      ShowMessage(GetExecutionName());
    }

    public void historicalTicksBidAsk(int reqId, HistoricalTickBidAsk[] ticks, bool done)
    {
      ShowMessage(GetExecutionName());
    }

    public void historicalTicksLast(int reqId, HistoricalTickLast[] ticks, bool done)
    {
      ShowMessage(GetExecutionName());
    }

    public void tickByTickAllLast(int reqId, int tickType, long time, double price, int size, TickAttrib attribs, string exchange, string specialConditions)
    {
      ShowMessage(GetExecutionName());
    }

    public void tickByTickBidAsk(int reqId, long time, double bidPrice, double askPrice, int bidSize, int askSize, TickAttrib attribs)
    {
      ShowMessage(GetExecutionName());
    }

    public void tickByTickMidPoint(int reqId, long time, double midPoint)
    {
      ShowMessage(GetExecutionName());
    }
  }
}
