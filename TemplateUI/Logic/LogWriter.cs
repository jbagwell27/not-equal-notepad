using System;
using System.IO;

namespace TemplateUI.Logic
{
    class LogWriter
    {
        public LogWriter()
        {
            if (!Directory.Exists("CaseHistory"))
            {
                Directory.CreateDirectory("CaseHistory");
            }
        }

        private void CreateTodaysLogFile()
        {
        }
    }
}
