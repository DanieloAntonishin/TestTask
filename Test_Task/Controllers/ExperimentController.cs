using Microsoft.AspNetCore.Mvc;
using System.Text;
using Test_Task.Models;

namespace Test_Task.Controllers
{
    [ApiController]
    [Route("/experiment")]
    public class ExperimentController : Controller
    {
        IExperimentRepositories _exRepositories;
        public ExperimentController(IExperimentRepositories exRepositories)
        {
            _exRepositories = exRepositories;
        }

        [HttpGet(ExperimentName.BUTTON_COLORE_EXPERIMENT)]
        public async Task<IActionResult> GetButtonColor(int diviceToken) 
        {
            var user = new User { Id = diviceToken };
            var res = await _exRepositories.GetButtonResultAsync(user);
            return Json(new { key = "button-color", value = res });
        }

        [HttpGet(ExperimentName.PRICE_EXPERIMENT)]
        public async Task<IActionResult> GetPrice(int diviceToken)
        {
            var user = new User { Id = diviceToken };
            var res = await _exRepositories.GetPriceResultAsync(user);
            return Json(new { key = "price", value = res });
        }

        [HttpGet("statistic/"+ ExperimentName.BUTTON_COLORE_EXPERIMENT)]
        public IActionResult GetButtonStatistic()
        {
            var exStat = new StringBuilder();
            var res = _exRepositories.GetStatisticAsync(ExperimentName.BUTTON_COLORE_EXPERIMENT);
            var html = System.IO.File.ReadAllText(@"./assets/statistic.html");      // get static html file
            html = html.Replace("{RequestCount}", res.RequestCount.ToString()).Replace("{UserCount}", res.UserCount.ToString());    // replace for dynamic html doc
            foreach (var item in res.ExpStatistic)
            {
                exStat.AppendLine($"<br /> Color {item.Key} | Count {item.Value}"); // get whole button color option and count for distinct option
            }
            html = html.Replace("{ExperimentStat}", exStat.ToString());
            return Content(html,"text/html");   // send view for user
        }

        [HttpGet("statistic/" + ExperimentName.PRICE_EXPERIMENT)]
        public IActionResult GetPriceStatistic()
        {
            var exStat = new StringBuilder();
            var res = _exRepositories.GetStatisticAsync(ExperimentName.PRICE_EXPERIMENT);
            var html = System.IO.File.ReadAllText(@"./assets/statistic.html");      // get static html file
            html = html.Replace("{RequestCount}", res.RequestCount.ToString()).Replace("{UserCount}", res.UserCount.ToString());    // replace for dynamic html doc
            foreach (var item in res.ExpStatistic)
            {
                exStat.AppendLine($"<br /> Price {item.Key} | Count {item.Value}"); // get wholeprice option and count for distinct option
            }
            html = html.Replace("{ExperimentStat}", exStat.ToString());
            return Content(html, "text/html");   // send view for user
        }
    }
}