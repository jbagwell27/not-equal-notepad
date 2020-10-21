using System.IO;

namespace TemplateUI.Logic
{
    static class ProductReader
    {
        public static string[] GetImaging()
        {
            try
            {
                return File.ReadAllText(@"Logic\Imaging.csv").Split(',');
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }
        public static string[] GetPMS()
        {
            try
            {
                return File.ReadAllText(@"Logic\PMS.csv").Split(',');
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }
        public static string[] GetBridges()
        {
            try
            {
                return File.ReadAllText(@"Logic\Bridges.csv").Split(',');
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }
        public static string[] GetDevices()
        {
            try
            {
                return File.ReadAllText(@"Logic\Devices.csv").Split(',');
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }
        public static string[] GetDrivers()
        {
            try
            {
                return File.ReadAllText(@"Logic\Drivers.csv").Split(',');
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }
        public static void AddEntry(string product, string value)
        {
            if (!File.Exists($@"Logic\{product}.csv"))
            {
                File.Create($@"Logic\{product}.csv");
            }
            File.AppendAllText($@"Logic\{product}.csv", $"{value},");
        }
    }
}
