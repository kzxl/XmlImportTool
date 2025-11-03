namespace XmlImportTool.Models
{
    public class ImportResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int RowsImported { get; set; }
    }
}
