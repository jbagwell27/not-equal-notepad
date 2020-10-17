using System;
using System.Diagnostics;
using System.IO;

namespace TemplateUI.Logic
{
    static class LogWriter
    {
        public static void CreateTodaysLog()
        {


        }
        public static void AddLogEntry(string logEntry)
        {
            string todaysLog = $@"CaseHistory\{DateTime.Today.Year}.{DateTime.Today.Month}.{DateTime.Today.Day}.log";

            if (!Directory.Exists("CaseHistory"))
            {
                Directory.CreateDirectory("CaseHistory");
            }
            if (!File.Exists(todaysLog))
            {
                File.Create(todaysLog);
            }

            string result = $"---------------{DateTime.Now.ToShortTimeString()}---------------\n" +
                $"{logEntry}\n\n";

            File.AppendAllText(todaysLog, result);
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
            DateTime yesterday = DateTime.Now.AddDays(-1);

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                yesterday = DateTime.Now.AddDays(-3);
            }
            string yesterdaysLog = $@"CaseHistory\{yesterday.Year}.{yesterday.Month}.{yesterday.Day}.log";
            if (!File.Exists(yesterdaysLog))
            {
                return 0;
            }
            else
            {
                Process.Start("", yesterdaysLog);
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
