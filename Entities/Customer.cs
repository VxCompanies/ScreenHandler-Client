using Newtonsoft.Json;

namespace Client.Entities;

internal class Customer : EntityBase
{
    [JsonProperty(Order = 2)]
    public string FirstName { get; set; } = null!;

    [JsonProperty(Order = 3)]
    public string LastName { get; set; } = null!;

    [JsonProperty(Order = 4)]
    public int Age { get; set; }

    [JsonProperty(Order = 5)]
    public string? EmailAddress { get; set; }
}
