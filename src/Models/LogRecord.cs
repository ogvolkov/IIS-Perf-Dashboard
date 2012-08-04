using System;

namespace IISPerfDashboard.Models
{
    public class LogRecord
    {
        private readonly bool _isValid;

        private readonly DateTime _dateTime;

        private string _url;

        private readonly int _timeTaken;

        public DateTime DateTime
        {
            get { return _dateTime; }
        }

        public int TimeTaken
        {
            get { return _timeTaken; }
        }

        public bool IsValid
        {
            get { return _isValid; }
        }

        public string Url
        {
            get { return _url; }
        }
        
        public LogRecord(string line, LogLineFormat format)
        {
            var lineParts = line.Split(' ');
            _isValid = format.IsValidLine(lineParts);

            if (_isValid)
            {
                _dateTime = format.GetDate(lineParts);
                _url = format.GetUrl(lineParts);
                _timeTaken = format.GetTimeTaken(lineParts);
            }
        }    
    }
}