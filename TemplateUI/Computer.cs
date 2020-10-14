namespace TemplateLogic
{
    class Computer
    {
        public string Name { get; set; }
        public string WinVer { get; set; }
        public string Edition { get; set; }
        public string Architecture { get; set; }

        public override string ToString()
        {
            string result = $"{Name} | {WinVer} | ";
            if (Edition != null)
                result += $"{Edition} | ";
            result += $"{Architecture}";
            return result;
        }
    }
}
