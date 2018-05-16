namespace LIMPFileReader.Helpers
{
    public class AdditionalReportData
    {
        public PlaceHolder Place { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public enum PlaceHolder
    {
        Header,
        Common,
        Footer
    }
}