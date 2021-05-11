namespace EasierLog
{
    internal class LogObjectStructure
    {
        public string System { get; set; }
        public string Module { get; set; }
        public string Version { get; set; }
        public string User { get; set; }
        public object Info { get; set; }
        public string InfoDescription { get; set; }
        public LogLevel Level { get; set; }
    }
}
