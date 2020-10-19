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
            string result = "";

            result += !string.IsNullOrEmpty(Imaging) ? $"Imaging Version:  {Imaging}\n" : null;
            result += !string.IsNullOrEmpty(PMS) ? $"PMS Version:  {PMS}\n" : null;
            result += !string.IsNullOrEmpty(Bridge) ? $"Integrator Version:  {Bridge}\n" : null;
            result += !string.IsNullOrEmpty(DatabasePath) ? $"Database Path:  {DatabasePath}\n" : null;
            result += !string.IsNullOrEmpty(DeviceType) ? $"Device:  {DeviceType}\n" : null;
            result += !string.IsNullOrEmpty(SerialNumber) ? $"SN:  {SerialNumber}\n" : null;
            result += !string.IsNullOrEmpty(Driver) ? $"Driver:  {Driver}\n" : null;

            return result;
        }
    }
}
