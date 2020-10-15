using System;
using System.ComponentModel;
using System.IO;

namespace TemplateUI.Logic
{
    class LogWriter
    {
        private readonly int CurrentYear;
        private readonly int CurrentMonth;
        private readonly int CurrentDay;
        private readonly string LogFileName;

        public LogWriter()
        {
            CurrentYear = DateTime.Today.Year;
            CurrentMonth = DateTime.Today.Month;
            CurrentDay = DateTime.Today.Day;
            LogFileName = $@"CaseHistory\{CurrentYear}.{CurrentMonth}.{CurrentDay}.log";

            if (!Directory.Exists("CaseHistory"))
            {
                Directory.CreateDirectory("CaseHistory");
            }
            CreateTodaysLogFile();
        }

        private void CreateTodaysLogFile()
        {
            if (!File.Exists(LogFileName))
            {
                File.Create(LogFileName);
            }
        }

        public void AddLogEntry(string logEntry)
        {
            string currentTime = DateTime.Now.ToShortTimeString();
            string result = $"---------------{currentTime}---------------\n" +
                $"{logEntry}\n";

            File.AppendAllText(LogFileName, result);
        }

    }
}
