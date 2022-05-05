using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TGC.WIQLQueryBuilder.Models;

namespace TGC.WIQLQueryBuilder.Services;
public class AzureClientService
{
    private readonly IOptions<AzureSettings> _azureSettings;
    private readonly string _wiqlUrl;

    public AzureClientService(IOptions<AzureSettings> azureSettings)
    {
        _azureSettings = azureSettings ?? throw new ArgumentNullException(nameof(azureSettings));
        _wiqlUrl = _azureSettings.Value.BaseUrl + _azureSettings.Value.ProjectName + "/_apis/wit/wiql?api-version=5.1";
    }

    public async Task<AzureWorkItemList> GetWorkItemsByIdWithFields(int[] ids, string[] fields)
    {
        using (var azureClient = this.CreateClient())
        {
            var requestUrl = $"{_azureSettings.Value.BaseUrl}{_azureSettings.Value.ProjectName}/_apis/wit/workitems?ids={String.Join("%2C",ids)}&fields={String.Join("%2C", fields)}";
            var response = await azureClient.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var azureResponseBody = JsonSerializer.Deserialize<AzureWorkItemList>(responseBody);

            return azureResponseBody;
        }
    }

    public async Task<AzureResponseBody> ExecuteWiqlQuery(WiqlPostBody wiqlPostBody)
    {
        using (var azureClient = this.CreateClient())
        {
            var requestUrl = $"{_azureSettings.Value.BaseUrl}{_azureSettings.Value.ProjectName}/_apis/wit/wiql?api-version=5.1";
            var response = await azureClient.PostAsync(requestUrl, wiqlPostBody.GetAsRequestBody());

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var azureResponseBody = JsonSerializer.Deserialize<AzureResponseBody>(responseBody);

            return azureResponseBody;
        }
    }

    private AzureClient CreateClient()
    {
        AzureClient client = new AzureClient();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(
                ASCIIEncoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", "", _azureSettings.Value.PersonalAccessToken))));
        return client;
    }

}