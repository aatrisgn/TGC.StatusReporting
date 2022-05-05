using Microsoft.Extensions.DependencyInjection;
using TGC.StatusReporting.Tool;
using TGC.WIQLQueryBuilder;
using TGC.WIQLQueryBuilder.Models;
using TGC.WIQLQueryBuilder.Services;

var serviceProvider = IoCContainer.CreateIoC();

var azureClientService = serviceProvider.GetService<AzureClientService>();
var azureWorkItemsService = serviceProvider.GetService<AzureWorkItemsService>();

var wiqlTestQuery = QueryBuilder.BuildWiqlQuery()
    .Select(WIQLReferences.SystemParent)
    .Where(WIQLReferences.TeamProject, WiqlComparison.Equal, "@project")
    .And(WIQLReferences.SystemIterationPath, WiqlComparison.Equal, "@currentiteration")
    .And(WIQLReferences.SystemAssignedTo, WiqlComparison.Equal, "@me")
    .And(WIQLReferences.SystemWorkItemType, WiqlComparison.Equal, "\"Task\"")
    .Raw("AND ([System.State] <> \"Closed\" OR [system.ChangedDate] > '2/20/2022')")
    .BuildQuery();

var relevantTaskIds = await azureWorkItemsService.GetWorkItemIdsByWIQL(wiqlTestQuery);

var relevantParentIdsDisticntWithRemainingWork = await azureWorkItemsService.GetWorkItemParentIdsByWorkItemIdsWithRemainingWork(relevantTaskIds);

var relevantParentIdsDisticnt = relevantParentIdsDisticntWithRemainingWork.Select(r => r.Fields.SystemParent);

var queryFields = new List<string> {
    WIQLReferences.SystemId,
    WIQLReferences.SystemWorkItemType,
    WIQLReferences.SystemTitle,
    WIQLReferences.SystemState,
    WIQLReferences.SystemIterationPath,
    WIQLReferences.SystemChangedDate,
    WIQLReferences.RemainingWork,
    WIQLReferences.StoryPoints
}.ToArray();

var relevantUserStories = await azureClientService.GetWorkItemsByIdWithFields(relevantParentIdsDisticnt.ToArray(), queryFields);

foreach(var story in relevantUserStories.AzureWorkItems)
{
    var remainingWork = relevantParentIdsDisticntWithRemainingWork.First(r => r.Fields.SystemParent == story.id).Fields.RemainingWork;
    Console.WriteLine($"[{story.id}] {story.Fields.SystemTitle} ({story.Fields.SystemState}) ({remainingWork}/{story.Fields.StoryPoints}) SP");
}

Console.ReadKey();