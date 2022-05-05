using TGC.WIQLQueryBuilder.Models;

namespace TGC.WIQLQueryBuilder.Services;
public class AzureWorkItemsService
{
    private readonly AzureClientService _azureClientService;
    public AzureWorkItemsService(AzureClientService azureClientService)
    {
        _azureClientService = azureClientService;
    }

    public async Task<IEnumerable<int>> GetWorkItemIdsByWIQL(WiqlPostBody wiqlPostBody)
    {
        var relevantTasks = await _azureClientService.ExecuteWiqlQuery(wiqlPostBody);

        var relevantIdsDisticnt = relevantTasks.workItems.Select(w => w.id).ToList();

        return relevantIdsDisticnt;
    }

    public async Task<IEnumerable<int>> GetWorkItemParentIdsByWorkItemIds(IEnumerable<int> workItemIds)
    {
        var parentWorkItems = await _azureClientService.GetWorkItemsByIdWithFields(workItemIds.ToArray(), new List<string> { WIQLReferences.SystemParent }.ToArray());

        var relevantParentIdsDisticnt = parentWorkItems.AzureWorkItems.DistinctBy(w => w.Fields.SystemParent).Select(w => w.Fields.SystemParent).ToList();

        return relevantParentIdsDisticnt;
    }

    public async Task<IEnumerable<AzureWorkItem>> GetWorkItemParentIdsByWorkItemIdsWithRemainingWork(IEnumerable<int> workItemIds)
    {
        var parentWorkItems = await _azureClientService.GetWorkItemsByIdWithFields(workItemIds.ToArray(), new List<string> { WIQLReferences.SystemParent, WIQLReferences.RemainingWork}.ToArray());

        var relevantParentIdsDisticnt = parentWorkItems.AzureWorkItems
            .GroupBy(w => w.Fields.SystemParent)
            .Select(nw => new AzureWorkItem
            {
                Fields = new AzureFields
                {
                    RemainingWork = nw.Sum(w => w.Fields.RemainingWork),
                    SystemParent = nw.First().Fields.SystemParent
                }
            }).ToList();

        return relevantParentIdsDisticnt;
    }
}