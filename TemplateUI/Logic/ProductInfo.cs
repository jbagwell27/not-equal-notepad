namespace TemplateUI.Logic
{
    class ProductInfo
    {
        public string Imaging { get; set; }
        public string PMS { get; set; }
        public string Bridge { get; set; }
        public string Device { get; set; }
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
            Device = null;
            SerialNumber = null;
            Driver = null;
        }

        public override string ToString()
        {
            string result = "";

            if (Imaging != null)
                result += $"Imaging Version:  {Imaging}\n";
            if (PMS != null)
                result += $"PMS Version:  {PMS}\n";
            if (Bridge != null)
                result += $"Integrator Version:  {Bridge}\n";
            if (Device != null)
                result += $"Device:  {Device}\n";
            if (SerialNumber != null)
                result += $"SN:  {SerialNumber}\n";
            if (Driver != null)
                result += $"Driver:  {Driver}\n";

            return result;
        }
    }
}
