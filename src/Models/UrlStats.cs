namespace IISPerfDashboard.Models
{
    public class UrlStats
    {
        public string Url { get; set; }

        public int Count { get; set; }

        public long TotalTime { get; set; }

        public double AverageTimeTaken
        {
            get
            {
                return (Count > 0) ? TotalTime/Count : 0;
            }
        }
        
        public void Add(LogRecord logRecord)
        {
            if (Url == logRecord.Url)
            {
                Count++;
                TotalTime += logRecord.TimeTaken;
            }
        }
    }
}