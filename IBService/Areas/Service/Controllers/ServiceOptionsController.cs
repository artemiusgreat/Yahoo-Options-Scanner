using IBApi;
using IBLibrary.Models.Data;
using IBLibrary.Services;
using IBService.Areas.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace IBService.Areas.Service.Controllers
{
  public class ServiceOptionsController : ServiceBaseController
  {
    [AcceptVerbs("POST")]
    public List<Option> Options([FromBody] dynamic data)
    {
      return new List<Option>();
    }

    [AcceptVerbs("POST")]
    public Response<Option> DownloadOptions([FromBody] dynamic data)
    {
      var response = new Response<Option>();
      var contracts = new List<Contract>();
      var selectors = data.ToObject<OptionSelector>();
      var symbols = (List<string>) selectors.Symbols;
      var dates = (List<string>) selectors.Dates;

      symbols.ForEach(symbol =>
      {
        dates.ForEach(date =>
        {
          contracts.Add(new Contract
          {
            Symbol = symbol,
            SecType = "OPT",
            Exchange = "SMART",
            Currency = "USD",
            LastTradeDateOrContractMonth = date
          });
        });
      });

      var optionService = new OptionService();
      var optionProcesses = optionService.GetContracts(contracts);
      var optionContracts = Task.WhenAll(optionProcesses).Result.SelectMany(o => o).ToList();
      var optionDetailsProcesses = optionService.GetOptionDetails(optionContracts);
      response.Items = Task.WhenAll(optionDetailsProcesses).Result.SelectMany(o => o).ToList();

      optionService.Dispose();

      return response;
    }
  }
}
