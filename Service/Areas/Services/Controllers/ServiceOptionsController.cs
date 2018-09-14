using MongoDB.Driver;
using Service.Components;
using Service.Models.Data;
using Service.Models.Message;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Service.Areas.Service.Controllers
{
  public class ServiceOptionsController : ServiceBaseController
  {
    /// <summary>
    /// Process all URLs for specified symbols
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [AcceptVerbs("GET")]
    public async Task<IServiceMessage<IScore>> ScanOptions([FromUri] CScannerMessage data)
    {
      var errors = data.GetErrors();

      if (errors.Any())
      {
        return new CServiceMessage<IScore>
        {
          Errors = errors
        };
      }

      var combos = new CCombinations(data.Combo);

      if (Equals(data.Cache, 1))
      {
        await combos.GetOptionsAsync(data);
      }

      return await combos.GetCombinationsAsync(data);
    }
  }
}
