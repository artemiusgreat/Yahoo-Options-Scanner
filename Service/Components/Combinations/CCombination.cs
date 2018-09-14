using Service.Classes;
using Service.Models.Data;
using Service.Models.Message;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Components
{
  public enum ECombination
  {
    ShortPut,
    ShortCall,
    LongCondor,
    ShortCondor,
    LongStrangle,
    ShortStrangle,
    LongPutSpread,
    LongCallSpread,
    ShortPutSpread,
    ShortCallSpread
  };

  public interface ICombination
  {
    IEnumerable<IScore> GetScores(IEnumerable<IGroup> groups);
    Task<IServiceMessage<IGroup>> GetGroupsAsync(IContainer<IGroup> container, IScannerMessage message);
  }

  public class CCombination : ICombination, IDisposable
  {
    /// <summary>
    /// Get list of plain options from DB
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public virtual Task<IServiceMessage<IGroup>> GetGroupsAsync(IContainer<IGroup> container, IScannerMessage message)
    {
      return null;
    }

    /// <summary>
    /// Convert list of options into a list of possible positions
    /// </summary>
    /// <param name="groups"></param>
    /// <returns></returns>
    public virtual IEnumerable<IScore> GetScores(IEnumerable<IGroup> groups)
    {
      return null;
    }

    /// <summary>
    /// Clean up
    /// </summary>
    public void Dispose()
    {
    }
  }
}
