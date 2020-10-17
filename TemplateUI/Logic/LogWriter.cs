using System;
using System.IO;

namespace TemplateUI.Logic
{
    static class LogWriter
    {
        public static void AddLogEntry(string logEntry)
        {
            int CurrentYear = DateTime.Today.Year;
            int CurrentMonth = DateTime.Today.Month;
            int CurrentDay = DateTime.Today.Day;
            string LogFileName = $@"CaseHistory\{CurrentYear}.{CurrentMonth}.{CurrentDay}.log";


            if (!Directory.Exists("CaseHistory"))
            {
                Directory.CreateDirectory("CaseHistory");
            }
            if (!File.Exists(LogFileName))
            {
                File.Create(LogFileName);
            }

            string result = $"---------------{DateTime.Now.ToShortTimeString()}---------------\n" +
                $"{logEntry}\n";

            File.AppendAllText(LogFileName, result);
        }

    }
}
