using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace IISPerfDashboard.Models
{
    public class LogStats
    {
        private List<UrlStats> _urlStatsCollection = new List<UrlStats>();

        private DateTime _minDate;

        private DateTime _maxDate;

        public void Add(LogRecord logRecord)
        {
            if (_minDate == DateTime.MinValue && _maxDate == DateTime.MinValue)
            {
                _minDate = logRecord.DateTime;
                _maxDate = logRecord.DateTime;
            }

            if (logRecord.DateTime < _minDate)
            {
                _minDate = logRecord.DateTime;
            }

            if (logRecord.DateTime > _maxDate)
            {
                _maxDate = logRecord.DateTime;
            }


            var urlStats = _urlStatsCollection.FirstOrDefault(s => s.Url == logRecord.Url);
            if (urlStats == null)
            {
                urlStats = new UrlStats();
                urlStats.Url = logRecord.Url;
                _urlStatsCollection.Add(urlStats);
            }

            urlStats.Add(logRecord);
        }

        public ICollection<UrlStats> UrlStats
        {
            get { return _urlStatsCollection; }
        }

        public long TotalTime
        {
            get { return _urlStatsCollection.Sum(u => u.TotalTime); }
        }

        public long TotalRequests
        {
            get { return _urlStatsCollection.Sum(u => u.Count); }
        }

        public double AverageResponse
        {
            get
            {
                var totalTime = _urlStatsCollection.Sum(u => u.TotalTime);
                var totalRequests = _urlStatsCollection.Sum(u => u.Count);
                return (totalRequests > 0) ? totalTime / totalRequests : 0;
            }
        }

        public double RequestsPerSecond
        {
            get
            {
                var secondsPassed = (_maxDate - _minDate).TotalSeconds;
                return (secondsPassed > 0) ? TotalRequests / secondsPassed : 0;
            }
        }

        public double AverageLoad
        {
            get
            {
                var timePassed = (_maxDate - _minDate).TotalMilliseconds;
                var loadTime = TotalTime;

                // get number of processor cores; assume the code is executed on the same server that generated log files
                int coreCount = 0;
                foreach (var item in new ManagementObjectSearcher("Select * from Win32_Processor").Get())
                {
                    coreCount += int.Parse(item["NumberOfCores"].ToString());
                }

                if (coreCount == 0) coreCount = 1;

                return (timePassed > 0) ? 100 * loadTime / (coreCount * timePassed) : 0;
            }
        }

        public DateTime MinDate
        {
            get { return _minDate; }
        }

        public DateTime MaxDate
        {
            get { return _maxDate; }
        }
    }
}