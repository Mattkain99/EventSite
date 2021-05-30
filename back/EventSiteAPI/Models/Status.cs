using System.Text.Json.Serialization;

namespace EventSiteAPI.Models
{
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