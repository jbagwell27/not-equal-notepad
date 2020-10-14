using System;

namespace ConsoleTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            Computers comp = new Computers();
            comp.Name = "HomeLaptop";
            comp.WinVer = "Windows Server 2019";
            //comp.Edition = "Home";
            comp.Architecture = "x86";

            Console.WriteLine(comp);

        }
    }
}
