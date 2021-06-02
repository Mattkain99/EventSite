using System.Text.Json.Serialization;

namespace EventSiteAPI.Models
{
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