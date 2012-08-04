using System;
using System.Collections.Generic;

namespace IISPerfDashboard.Models
{
    public class UrlStatsPageModel
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public double RequestsPerSecond { get; set; }
        public long TotalRequests { get; set; }
        public double AverageResponse { get; set; }
        public double AverageLoad { get; set; }

        public List<UrlModel> Urls { get; set; }
    }    
}