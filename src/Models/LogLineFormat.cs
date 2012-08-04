using System;

namespace IISPerfDashboard.Models
{
    public class LogLineFormat
    {
        private int _dateFieldIndex;
        private int _timeFieldIndex;
        private int _urlFieldIndex;
        private int _timeTakenIndex;

        public LogLineFormat(string[] formatLineParts)
        {
            for (int i = 0; i < formatLineParts.Length - 1; i++)
            {
                switch (formatLineParts[i + 1])
                {
                    case "date":
                        _dateFieldIndex = i;
                        break;
                    case "time":
                        _timeFieldIndex = i;
                        break;
                    case "cs-uri-stem":
                        _urlFieldIndex = i;
                        break;
                    case "time-taken":
                        _timeTakenIndex = i;
                        break;
                }
            }
        }

        public bool IsValidLine(string[] lineParts)
        {
            var length = lineParts.Length;
            return _dateFieldIndex < length || _timeFieldIndex < length || _urlFieldIndex < length || _timeTakenIndex < length;
        }

        public DateTime GetDate(string[] lineParts)
        {
            var date = DateTime.Parse(lineParts[_dateFieldIndex]);
            var time = TimeSpan.Parse(lineParts[_timeFieldIndex]);

            return date + time;
        }

        public string GetUrl(string[] lineParts)
        {
            return lineParts[_urlFieldIndex];
        }

        public int GetTimeTaken(string[] lineParts)
        {
            return int.Parse(lineParts[_timeTakenIndex]);
        }
    }
}