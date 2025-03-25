using System.Text.Json.Nodes;

namespace Accounting.Imports;

public class ImportRecordItem
{
    public ImportRecordItem()
    {
        this.CreatedTime = DateTimeOffset.UtcNow;
    }

    public ImportRecordItem(string id, JsonObject data) : this()
    {
        this.Id = id;
        this.Data = data;
    }

    public long ImportId { get; set; }
    public string Id { get; set; } = default!;
    public JsonObject? Data { get; set; } = default!;

    public DateTimeOffset CreatedTime { get; set; }
}