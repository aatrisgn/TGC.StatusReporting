using System.Text.Json.Serialization;

namespace TGC.WIQLQueryBuilder.Models;
public class AzureWorkItemList
{
    public int count { get; set; }
    [JsonPropertyName("value")]
    public List<AzureWorkItem> AzureWorkItems { get; set; }
}
