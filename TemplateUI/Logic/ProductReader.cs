using System.IO;

namespace TemplateUI.Logic
{
    static class ProductReader
    {
        public static string[] GetImaging()
        {
            return File.ReadAllText(@"Logic\Imaging.csv").Split(',');
        }
        public static string[] GetPMS()
        {
            return File.ReadAllText(@"Logic\PMS.csv").Split(',');
        }
        public static string[] GetBridges()
        {
            return File.ReadAllText(@"Logic\Bridge.csv").Split(',');
        }
        private static string[] GetDevices()
        {
            return File.ReadAllText(@"Logic\Devices.csv").Split(',');
        }
        private static string[] GetDrivers()
        {
            return File.ReadAllText(@"Logic\Drivers.csv").Split(',');
        }
        public static void AddEntry(string product, string value)
        {
            File.AppendAllText($@"Logic\{product}.csv", value);
        }
    }
}
