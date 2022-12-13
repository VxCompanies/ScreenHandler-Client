using Newtonsoft.Json;

namespace Client.Entities;

public class EntityBase
{
    [JsonProperty(Order = 1)]
    public int Id { get; set; }
}
