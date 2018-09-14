using Service.Components.Connectors;
using Service.Models.Data;
using Service.Models.Message;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Components
{
  public enum EConnector
  {
    Yahoo
  };

  public interface IConnector : IDisposable
  {
    Task<Dictionary<string, List<IGroup>>> GetGroupsAsync(IScannerMessage message);
  };

  /// <summary>
  /// Class to convert API response into a model
  /// </summary>
  public sealed class CConnector : IDisposable
  {
    public IConnector Instance { get; set; }

    /// <summary>
    /// Factory that specifies exact connector
    /// </summary>
    /// <param name="connector"></param>
    public CConnector(EConnector connector)
    {
      switch (connector)
      {
        case EConnector.Yahoo: Instance = new CYahooConnector(); break;
      }
    }

    /// <summary>
    /// Call method that downloads options from relevant connector
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task<Dictionary<string, List<IGroup>>> GetGroupsAsync(IScannerMessage message)
    {
      return Instance.GetGroupsAsync(message);
    }

    /// <summary>
    /// Dispose connector instance
    /// </summary>
    public void Dispose()
    {
      Instance.Dispose();
    }
  }
}
