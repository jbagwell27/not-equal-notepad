using System;

namespace ConsoleTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericInfo gi = new GenericInfo();
            gi.ContactName = "Josh";
            gi.PhoneNumber = "7706859926";
            gi.EmailAddress = "fu@fukyou.com";
            gi.CaseNumber = "CASE-23456";
            gi.Problem = "Main issue";
            gi.ProblemDetails = "More information about issue";
            gi.TroubleShootingSteps = "What I did to troubleshoot the issue";
            gi.ResolutionDetails = "What the solution was";

            Console.WriteLine(gi.ToString());
        }
    }
}
