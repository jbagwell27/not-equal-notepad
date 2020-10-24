using System.IO;

namespace TemplateUI.Logic
{
    static class ProductReader
    {
        public static string[] GetImaging()
        {
            try
            {
                return File.ReadAllText(@"Resources\Imaging.csv").Split(',');
            }
            catch (System.Exception)
            {
                if (!Directory.Exists("Resources"))
                {
                    Directory.CreateDirectory("Resources");
                }
                File.AppendAllText(@"Resources\Imaging.csv", Properties.Resources.Imaging);
                return Properties.Resources.Imaging.Split(',');
            }
        }
        public static string[] GetPMS()
        {
            try
            {
                return File.ReadAllText(@"Resources\PMS.csv").Split(',');
            }
            catch (System.Exception)
            {
                if (!Directory.Exists("Resources"))
                {
                    Directory.CreateDirectory("Resources");
                }
                File.AppendAllText(@"Resources\PMS.csv", Properties.Resources.PMS);
                return Properties.Resources.PMS.Split(',');
            }
        }
        public static string[] GetBridges()
        {
            try
            {
                return File.ReadAllText(@"Resources\Bridges.csv").Split(',');
            }
            catch (System.Exception)
            {
                if (!Directory.Exists("Resources"))
                {
                    Directory.CreateDirectory("Resources");
                }
                File.AppendAllText(@"Resources\Bridges.csv", Properties.Resources.Bridges);
                return Properties.Resources.Bridges.Split(',');
            }
        }
        public static string[] GetDevices()
        {
            try
            {
                return File.ReadAllText(@"Resources\Devices.csv").Split(',');
            }
            catch (System.Exception)
            {
                if (!Directory.Exists("Resources"))
                {
                    Directory.CreateDirectory("Resources");
                }
                File.AppendAllText(@"Resources\Devices.csv", Properties.Resources.Devices);
                return Properties.Resources.Devices.Split(',');
            }
        }

        public static string[] GetDrivers()
        {
            try
            {
                return File.ReadAllText(@"Resources\Drivers.csv").Split(',');
            }
            catch (System.Exception)
            {
                File.AppendAllText(@"Resources\Drivers.csv", Properties.Resources.Drivers);
                return Properties.Resources.Drivers.Split(',');
            }
        }

        public static string[] GetProductsList(string product)
        {
            try
            {
                return File.ReadAllText($@"Resources\{product}.csv").Split(',');
            }
            catch (System.Exception)
            {
                return Properties.Resources.Drivers.Split(',');
            }
        }
        public static void SetProductsList(string[] products, string productType)
        {
            FileStream fileStream = File.Open($@"Resources\{productType}.csv", FileMode.Open);
            fileStream.SetLength(0);
            fileStream.Close();
            File.AppendAllText($@"Resources\{productType}.csv", $"{string.Join(',', products)}");
        }

        public static void AddEntry(string product, string value)
        {
            File.AppendAllText($@"Resources\{product}.csv", $"{value},");
        }

        public static void Rebuild()
        {
            try
            {
                File.Delete(@"Resources\Imaging.csv");
                File.Delete(@"Resources\PMS.csv");
                File.Delete(@"Resources\Bridges.csv");
                File.Delete(@"Resources\Devices.csv");
                File.Delete(@"Resources\Drivers.csv");
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory("Resources");
            }

            File.AppendAllText(@"Resources\Imaging.csv", Properties.Resources.Imaging);
            File.AppendAllText(@"Resources\PMS.csv", Properties.Resources.PMS);
            File.AppendAllText(@"Resources\Bridges.csv", Properties.Resources.Bridges);
            File.AppendAllText(@"Resources\Devices.csv", Properties.Resources.Devices);
            File.AppendAllText(@"Resources\Drivers.csv", Properties.Resources.Drivers);
        }
    }
}
