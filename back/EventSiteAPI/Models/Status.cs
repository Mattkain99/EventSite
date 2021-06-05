using System.Text.Json.Serialization;

namespace EventSiteAPI.Models
{
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Draft,
        Open,
        Closed,
        Ongoing,
        Done,
        Canceled,
        Archived
    }
}