using System;
using System.Diagnostics;
using System.IO;

namespace TemplateUI.Logic
{
    static class LogWriter
    {
        public static string TodaysLog = $@"CaseHistory\{DateTime.Today.Year}.{DateTime.Today.Month}.{DateTime.Today.Day}.log";
        public static void CreateTodaysLog()
        {
            if (!Directory.Exists("CaseHistory"))
            {
                Directory.CreateDirectory("CaseHistory");
            }
            if (!File.Exists(TodaysLog))
            {
                File.Create(TodaysLog);
            }
        }
        public static void AddLogEntry(string logEntry)
        {
            string result = $"---------------{DateTime.Now.ToShortTimeString()}---------------\n" +
                $"{logEntry}\n\n";

            File.AppendAllText(TodaysLog, result);
        }

        public static int LaunchTodaysLogFile()
        {
            string todaysLog = $@"CaseHistory\{DateTime.Today.Year}.{DateTime.Today.Month}.{DateTime.Today.Day}.log";
            if (!File.Exists(todaysLog))
            {
                return 0;
            }
            else
            {
                Process.Start("notepad.exe", todaysLog);
            }
            return 1;
        }

        public static int LaunchPreviousDayLogFile()
        {
            DateTime previousDaysLog = DateTime.Now.AddDays(-1);

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                previousDaysLog = DateTime.Now.AddDays(-3);
            }
            string yesterdaysLog = $@"CaseHistory\{previousDaysLog.Year}.{previousDaysLog.Month}.{previousDaysLog.Day}.log";
            if (!File.Exists(yesterdaysLog))
            {
                return 0;
            }
            else
            {
                Process.Start("notepad.exe", yesterdaysLog);
            }
            return 1;
        }
        public static int OpenLogFolder()
        {
            if (!Directory.Exists("CaseHistory"))
            {
                return 0;
            }
            else
            {
                Process.Start("explorer.exe", $@"{Directory.GetCurrentDirectory()}\CaseHistory");
            }
            return 1;
        }

    }
}
