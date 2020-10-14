using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace TemplateLogic
{
    class ProductReader
    {
        public List<string> ImagingVersions { get; }
        public List<string> PMSVersions { get; }
        public List<string> BridgeVersions { get; }
        public List<string> Devices { get; }
        public List<string> DriverVersions { get; }
        private readonly string TemplateFile;
        public ProductReader()
        {
            TemplateFile = new StreamReader("Products.json").ReadToEnd();
            ImagingVersions = new List<string>();
            PMSVersions = new List<string>();
            BridgeVersions = new List<string>();
            Devices = new List<string>();
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
            {
                ImagingVersions.Add(item);
            }
        }
        private void FillPMS()
        {
            JToken token = JToken.Parse(TemplateFile);
            string list = token.SelectToken("PMS").ToString();
            dynamic array = JsonConvert.DeserializeObject(list);
            foreach (string item in array)
            {
                PMSVersions.Add(item);
            }
        } private void FillBridge()
        {
            JToken token = JToken.Parse(TemplateFile);
            string list = token.SelectToken("Bridge").ToString();
            dynamic array = JsonConvert.DeserializeObject(list);
            foreach (string item in array)
            {
                BridgeVersions.Add(item);
            }
        } private void FillDevice()
        {
            JToken token = JToken.Parse(TemplateFile);
            string list = token.SelectToken("Device").ToString();
            dynamic array = JsonConvert.DeserializeObject(list);
            foreach (string item in array)
            {
                Devices.Add(item);
            }
        } private void FillDriver()
        {
            JToken token = JToken.Parse(TemplateFile);
            string list = token.SelectToken("Driver").ToString();
            dynamic array = JsonConvert.DeserializeObject(list);
            foreach (string item in array)
            {
                DriverVersions.Add(item);
            }
        }
    }

}
