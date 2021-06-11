using System.Collections.Generic;
using System.Linq;

namespace TimeEntries.Api.Mappers
{
    public interface ITimeEntryMapper
    {
        Models.TimeEntry? Map(Persistence.Models.TimeEntry? entry);
    }

    public class TimeEntryMapper : ITimeEntryMapper
    {
        public Models.TimeEntry? Map(Persistence.Models.TimeEntry? entry)
        {
            if (entry is null) { return null; }
            return new Models.TimeEntry
            {
                Id = entry.Id,
                ProjectId = entry.ProjectId,
                UserId = entry.UserId,
                From = entry.From,
                Until = entry.Until,
            };
        }
    }
}