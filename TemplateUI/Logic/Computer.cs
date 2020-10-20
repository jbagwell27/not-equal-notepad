namespace TemplateUI.Logic
{
    class Computer
    {
        public string Name { get; set; }
        public string OSVersion { get; set; }
        public string Edition { get; set; }
        public string Architecture { get; set; }

        public Computer() { }

        public Computer(string name, string osversion, string edition, string architecture)
        {
            Name = name;
            OSVersion = osversion;
            Edition = edition;
            Architecture = architecture;
        }

        public override string ToString()
        {
            string result = $"{Name} | Windows {OSVersion} | ";
            result += !string.IsNullOrEmpty(Edition) ? $"{Edition} | " : null;
            result += $"{Architecture}";
            return result;
        }
    }
}
