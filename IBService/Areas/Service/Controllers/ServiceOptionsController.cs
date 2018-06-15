using IBApi;
using IBLibrary;
using IBLibrary.Models;
using IBService.Areas.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace IBService.Areas.Service.Controllers
{
  public class ServiceOptionsController : BaseServiceController
  {
    [AcceptVerbs("POST")]
    public List<Option> Options([FromBody] dynamic data)
    {
      return new List<Option>();
    }

    [AcceptVerbs("POST")]
    public Response<Option> ScheduleOptions([FromBody] dynamic data)
    {
      var items = new List<Option>();
      var query = data.ToObject<Query>();
      var response = new Response<Option>
      {
        Items = items
      };

      if (query.Action == null ||
          query.Selectors == null ||
          query.Selectors.Count == 0)
      {
        response.Errors = new List<string>
        {
          "No parameters found"
        };

        return response;
      }

      List<Selector> selectors = query.Selectors;

      var symbols = selectors.Find(o => o.Name == "Symbol");
      var expirations = selectors.Find(o => o.Name == "Expiration");

      if (symbols == null ||
          expirations == null)
      {
        response.Errors = new List<string>
        {
          "Symbol or Expiration is missing"
        };

        return response;
      }

      var contracts = new List<Contract>();
      var dateRange = expirations.Value.LastOrDefault().Split(':');
      var dateStart = DateTime.Parse(dateRange.FirstOrDefault());
      var dateEnd = DateTime.Parse(dateRange.LastOrDefault());

      if (dateEnd == null || dateStart == null || dateStart > dateEnd)
      {
        response.Errors = new List<string>
        {
          "Symbol or Expiration is missing"
        };

        return response;
      }

      var dateStop = dateStart.AddMonths(5);

      symbols.Value.ForEach(symbol =>
      {
        var dateCurrent = dateStart;

        while (dateCurrent < dateEnd && dateCurrent < dateStop)
        {
          contracts.Add(new Contract
          {
            Symbol = symbol,
            SecType = "OPT",
            Exchange = "SMART",
            Currency = "USD",
            LastTradeDateOrContractMonth = dateCurrent.ToString("yyyyMM")
          });

          dateCurrent = dateCurrent.AddMonths(1);
        }
      });

      var optionService = new OptionService();
      var optionProcesses = optionService.GetContracts(contracts);
      var optionContracts = Task.WhenAll(optionProcesses).Result.SelectMany(o => o).Take(10).ToList();
      var optionDetailsProcesses = optionService.GetOptionDetails("SMART", optionContracts);

      response.Items = Task.WhenAll(optionDetailsProcesses).Result.SelectMany(o => o).Take(10).ToList();
      optionService.Dispose();

      return response;
    }
  }
}
