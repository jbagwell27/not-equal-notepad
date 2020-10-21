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
                File.AppendAllText(@"Logic\Imaging.csv", Properties.Resources.Imaging);
                return Properties.Resources.Imaging.Split(',');
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
                File.AppendAllText(@"Logic\PMS.csv", Properties.Resources.PMS);
                return Properties.Resources.PMS.Split(',');
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
                File.AppendAllText(@"Logic\Bridges.csv", Properties.Resources.Bridges);
                return Properties.Resources.Bridges.Split(',');
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
                File.AppendAllText(@"Logic\Devices.csv", Properties.Resources.Devices);
                return Properties.Resources.Devices.Split(',');
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
                File.AppendAllText(@"Logic\Drivers.csv", Properties.Resources.Drivers);
                return Properties.Resources.Drivers.Split(',');
            }
        }
        public static void AddEntry(string product, string value)
        {
            File.AppendAllText($@"Logic\{product}.csv", $"{value},");
        }
    }
}
