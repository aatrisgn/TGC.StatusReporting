using System.Text.Json.Serialization;

namespace TGC.WIQLQueryBuilder.Models;
public class AzureFields
{
    [JsonPropertyName("System.Parent")]
    public int SystemParent { get; set; }
    [JsonPropertyName("System.IterationPath")]
    public string SystemIterationPath { get; set; }
    [JsonPropertyName("System.WorkItemType")]
    public string SystemWorkItemType { get; set; }
    [JsonPropertyName("System.State")]
    public string SystemState { get; set; }
    [JsonPropertyName("System.ChangedDate")]
    public string SystemChangedDate { get; set; }
    [JsonPropertyName("System.Title")]
    public string SystemTitle { get; set; }
    [JsonPropertyName("Microsoft.VSTS.Scheduling.RemainingWork")]
    public double RemainingWork { get; set; }
    [JsonPropertyName("Microsoft.VSTS.Scheduling.StoryPoints")]
    public double StoryPoints { get; set; }
}