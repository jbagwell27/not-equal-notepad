using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows.Interop;

namespace TemplateUI.Logic
{
    class ProductReader
    {
        public List<string> ImagingVersions { get; }
        public List<string> PMSVersions { get; }
        public List<string> BridgeVersions { get; }
        public List<string> DeviceTypes { get; }
        public List<string> DriverVersions { get; }

        private readonly string TemplateFile;
        public ProductReader()
        {
            TemplateFile = new StreamReader(@"Logic\Products.json").ReadToEnd();
            ImagingVersions = new List<string>();
            PMSVersions = new List<string>();
            BridgeVersions = new List<string>();
            DeviceTypes = new List<string>();
            DriverVersions = new List<string>();
            FillImaging();
            FillPMS();
            FillBridge();
            FillDevice();
            FillDriver();
        }

        private void FillImaging()
        {
            JToken token = JToken.Parse(TemplateFile);
            string list = token.SelectToken("Imaging").ToString();
            dynamic array = JsonConvert.DeserializeObject(list);
            foreach (string item in array)
                ImagingVersions.Add(item);
        }
        private void FillPMS()
        {
            JToken token = JToken.Parse(TemplateFile);
            string list = token.SelectToken("PMS").ToString();
            dynamic array = JsonConvert.DeserializeObject(list);
            foreach (string item in array)
                PMSVersions.Add(item);
        }
        private void FillBridge()
        {
            JToken token = JToken.Parse(TemplateFile);
            string list = token.SelectToken("Bridge").ToString();
            dynamic array = JsonConvert.DeserializeObject(list);
            foreach (string item in array)
                BridgeVersions.Add(item);
        }
        private void FillDevice()
        {
            JToken token = JToken.Parse(TemplateFile);
            string list = token.SelectToken("Device").ToString();
            dynamic array = JsonConvert.DeserializeObject(list);
            foreach (string item in array)
                DeviceTypes.Add(item);
        }
        private void FillDriver()
        {
            JToken token = JToken.Parse(TemplateFile);
            string list = token.SelectToken("Driver").ToString();
            dynamic array = JsonConvert.DeserializeObject(list);
            foreach (string item in array)
                DriverVersions.Add(item);
        }
        
        public void AddEntry(string product, string value)
        {
            var list = JsonConvert.DeserializeObject<List<Imaging>>(TemplateFile);
            list.Add(new Imaging("Imaging", value));
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
        }
    }

    class Imaging
    {
        string Program;
        string Version;
        public Imaging(string program, string version)
        {
            Program = program;
            Version = version;
        }
    }

}
