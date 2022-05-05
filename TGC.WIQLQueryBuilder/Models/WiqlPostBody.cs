using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace TGC.WIQLQueryBuilder.Models;
public class WiqlPostBody
{
    public WiqlPostBody(string wiql)
    {
        Query = wiql;
    }

    public string Query { get; }

    public StringContent GetAsRequestBody()
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        var jsonBody = JsonSerializer.Serialize(this, options);
        return new StringContent(jsonBody, Encoding.UTF8, "application/json");
    }
}