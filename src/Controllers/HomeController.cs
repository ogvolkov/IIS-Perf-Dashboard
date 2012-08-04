using System;
using System.Linq;
using System.Web.Mvc;

using IISPerfDashboard.Models;
using IISPerfDashboard.Properties;

namespace IISPerfDashboard.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var analyzer = new LogAnalyzer(Settings.Default.LogsPath);

            // by default, analyze logs only for today - so older data doesn't skew live performance overview
            analyzer.Analyze(DateTime.Now.Date, DateTime.MaxValue);
            var logStats = analyzer.LogStats;

            var totalTime = logStats.TotalTime;

            var model = new UrlStatsPageModel();
            model.TotalRequests = logStats.TotalRequests;
            model.RequestsPerSecond = logStats.RequestsPerSecond;
            model.AverageResponse = logStats.AverageResponse;
            model.AverageLoad = logStats.AverageLoad;
            model.MinDate = logStats.MinDate;
            model.MaxDate = logStats.MaxDate;

            model.Urls = logStats.UrlStats.OrderByDescending(u => u.TotalTime)
                .Select(u => new UrlModel
                    {
                        Url = u.Url,
                        Count = u.Count,
                        AverageTimeTaken = u.AverageTimeTaken,
                        TotalTimeTaken = u.TotalTime,
                        Percentage = (totalTime != 0) ? 100 * u.TotalTime / totalTime : 0
                    }).ToList();

            return View(model);
        }
    }
}
