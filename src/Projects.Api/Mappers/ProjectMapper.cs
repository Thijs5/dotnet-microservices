using System.Collections.Generic;
using System.Linq;

namespace Projects.Api.Mappers
{
    public interface IProjectMapper
    {
        Models.Project? Map(Persistence.Models.Project? project);
    }

    public class ProjectMapper : IProjectMapper
    {
        public Models.Project? Map(Persistence.Models.Project? project)
        {
            if (project is null) { return null; }
            return new Models.Project
            {
                Id = project.Id,
                Name = project.Name,
                Contributors = project.Contributors.Select(x => x.UserId),
            };
        }
    }
}