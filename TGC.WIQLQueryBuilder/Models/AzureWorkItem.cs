using System.Text.Json.Serialization;

namespace TGC.WIQLQueryBuilder.Models;
public class AzureWorkItem
{
    public int id { get; set; }
    public string url { get; set; }
    public int rev { get; set; }
    [JsonPropertyName("fields")]
    public AzureFields Fields { get; set; }
}