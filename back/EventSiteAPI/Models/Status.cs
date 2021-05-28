using System.Text.Json.Serialization;

namespace EventSiteAPI.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Created,
        Open,
        Closed,
        Ongoing,
        Done,
        Canceled
    }
}