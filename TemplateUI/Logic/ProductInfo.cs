namespace TemplateUI.Logic
{
    class ProductInfo
    {
        public string Imaging { get; set; }
        public string PMS { get; set; }
        public string Bridge { get; set; }
        public string DatabasePath { get; set; }
        public string DeviceType { get; set; }
        public string SerialNumber { get; set; }
        public string Driver { get; set; }

        public ProductInfo()
        {
            Clear();
        }
        public void Clear()
        {
            Imaging = null;
            PMS = null;
            Bridge = null;
            DatabasePath = null;
            DeviceType = null;
            SerialNumber = null;
            Driver = null;
        }

        public override string ToString()
        {
            string result = "-----Environment-----\n\n";

            result += Imaging != null && Imaging != string.Empty ? $"Imaging Version:  {Imaging}\n" : null;
            result += PMS != null && PMS != string.Empty ? $"PMS Version:  {PMS}\n" : null;
            result += Bridge != null && Bridge != string.Empty ? $"Integrator Version:  {Bridge}\n" : null;
            result += DatabasePath != null ? $"Database Path:  {DatabasePath}\n" : null;
            result += DeviceType != null && DeviceType != string.Empty ? $"Device:  {DeviceType}\n" : null;
            result += SerialNumber != null ? $"SN:  {SerialNumber}\n" : null;
            result += Driver != null && Driver != string.Empty ? $"Driver:  {Driver}\n" : null;

            return result;
        }
    }
}
