namespace IISPerfDashboard.Models
{
    public class UrlModel
    {
        public string Url { get; set; }

        public int Count { get; set; }

        public long TotalTimeTaken { get; set; }

        public double AverageTimeTaken { get; set; }

        public double Percentage { get; set; }
    }
}