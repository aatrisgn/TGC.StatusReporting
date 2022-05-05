namespace TGC.WIQLQueryBuilder.Models;
public class AzureResponseBody
{
    public string queryType { get; set; }
    public string queryResultType { get; set; }
    public DateTime asOf { get; set; }
    public List<AzureResponseColumn> columns { get; set; }
    public List<AzureWorkItem> workItems { get; set; }
}