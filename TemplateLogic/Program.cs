using Microsoft.VisualBasic;

namespace TemplateLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericInfo gi = new GenericInfo();
            ProductInfo pi = new ProductInfo();
            Computer comp = new Computer();

            gi.ContactName = "Josh";
            gi.PhoneNumber = "7706859926";
            gi.EmailAddress = "joshinator414@gmail.com";
            gi.CaseNumber = "CAS-1234567";
            gi.Problem = "Sensor not working";
            gi.ProblemDetails = "Sensor shows as not connected on all machines";
            gi.TroubleShootingSteps = "I remote in and the sensor doesn't show in device manager.\n" +
                "I update driver and device manager shows device\n" +
                "Test the device and we get green bars\n" +
                "I repeat the steps on all machines";
            gi.ResolutionDetails = "Updated driver on all machines\n" +
                "Issue resolved";
            pi.Imaging = "9.4.4";
            pi.PMS = "Dentrix";
            pi.Bridge = "323";
            pi.Device = "Platinum";
            comp.Name = "XRAY";
            comp.WinVer = "Windows 10";
            comp.Edition = "Pro";
            comp.Architecture = "x64";


            System.Console.WriteLine("{0}\n---Environment Info---\n\n{1}\nComputers\n{2}",gi,pi,comp);
        }
    }
}
