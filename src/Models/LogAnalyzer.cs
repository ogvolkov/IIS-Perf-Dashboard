using System;
using System.IO;

namespace IISPerfDashboard.Models
{
    /// <summary>
    /// Analyzes IIS logs
    /// </summary>
    public class LogAnalyzer
    {
        private readonly string _folderPath;

        private LogLineFormat _lineFormat;

        private readonly LogStats _logStats = new LogStats();

        private DateTime _from;

        private DateTime _to;

        public LogStats LogStats
        {
            get { return _logStats; }
        }

        public LogAnalyzer(string folderPath)
        {
            _folderPath = folderPath;
        }

        public void Analyze(DateTime from, DateTime to)
        {
            _from = from;
            _to = to;

            foreach (var fileName in Directory.GetFiles(_folderPath))
            {
                ProcessFile(fileName);
            }
        }

        private void ProcessFile(string fileName)
        {
            // fileshare parameter is required to read IIS logs while it opens them for writing
            using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(file))
            {
                for (;;)
                {
                    string line = reader.ReadLine();
                    if (line == null) break;

                    if (line[0] == '#')
                    {
                        ProcessAuxLine(line);
                    }
                    else
                    {
                        ProcessLogLine(line);
                    }
                }
            }
        }

        private void ProcessLogLine(string line)
        {
            if (_lineFormat == null) return;
            
            var logRecord = new LogRecord(line, _lineFormat);
            if (logRecord.IsValid)
            {
                if (logRecord.DateTime >= _from && logRecord.DateTime <= _to)
                {
                    // here you can add any custom url pre-processing (filtering, grouping) you want
                    _logStats.Add(logRecord);
                }
            }
        }        

        private void ProcessAuxLine(string line)
        {
            var lineParts = line.Split(' ');
            if (lineParts[0] == "#Fields:")
            {
                _lineFormat = new LogLineFormat(lineParts);                
            }
        }
    }
}