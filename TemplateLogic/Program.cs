namespace TemplateLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductReader pr = new ProductReader();
            foreach (var item in pr.ImagingVersions)
            {
                System.Console.Write("{0}|", item);
            }
            System.Console.WriteLine();
            foreach (var item in pr.PMSVersions)
            {
                System.Console.Write("{0}|", item);
            }
            System.Console.WriteLine();
            foreach (var item in pr.BridgeVersions)
            {
                System.Console.Write("{0}|", item);
            }
            System.Console.WriteLine();
            foreach (var item in pr.Devices)
            {
                System.Console.Write("{0}|", item);
            }
            System.Console.WriteLine();
            foreach (var item in pr.DriverVersions)
            {
                System.Console.Write("{0}|", item);
            }
        }
    }
}
