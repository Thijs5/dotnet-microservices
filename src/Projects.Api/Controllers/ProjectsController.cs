using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projects.Api.Mappers;
using Projects.Api.Models;
using Projects.Api.Persistence;

namespace Projects.Api.Controllers
{
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectsDbContext _context;
        private readonly IProjectMapper _mapper;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(
            ProjectsDbContext context,
            IProjectMapper mapper,
            ILogger<ProjectsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        
        /// <summary>
        /// Create a new project.
        /// </summary>
        /// <param name="newProject">Project info to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        [HttpPost]
        [Route("/projects")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Project>> CreateAsync(NewProject newProject, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var project = new Persistence.Models.Project
            {
                Name = newProject.Name,
            };
            await _context.Projects.AddAsync(project, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(GetById), new { projectId = project.Id }, project);
        }

        /// <summary>
        /// Get a list of projects.
        /// </summary>
        /// <returns>A list of projects found. Empty list if no projects found.</returns>
        [HttpGet]
        [Route("/projects")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Project>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Project>> GetAll()
        {
            var projects = _context.Projects
                .Select(x => _mapper.Map(x))
                .ToList();
            return Ok(projects);
        }

        /// <summary>
        /// Get a single project by its id.
        /// </summary>
        /// <param name="projectId">The id of the project.</param>
        /// <returns>Returns a project if a project is found.</returns>
        [HttpGet]
        [Route("/projects/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Project))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Project> GetById(int projectId)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == projectId);
            if (project is null) { return NotFound(); }
            return Ok(project);
        }

        /// <summary>
        /// Update a project.
        /// </summary>
        /// <param name="projectId">Id of the project to update.</param>
        /// <param name="editProject">New data for the project.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        [HttpPut]
        [Route("/projects/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Project))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Project>> UpdateAsync(int projectId, EditProject editProject, CancellationToken cancellationToken)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == projectId);
            if (project is null)
            {
                ModelState.AddModelError(nameof(projectId), $"Could not find project with id '{projectId}'");
            }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            project!.Name = editProject.Name;

            await _context.SaveChangesAsync(cancellationToken);
            return Ok(_mapper.Map(project));
        }

        /// <summary>
        /// Delete a project.
        /// </summary>
        /// <param name="projectId">Id of the project to delete.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpDelete]
        [Route("/projects/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Project))]
        public async Task<ActionResult<Project>> DeleteAsync(int projectId, CancellationToken cancellationToken = default)
        {
            var project = _context.Projects.SingleOrDefault(u => u.Id == projectId);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return Ok(_mapper.Map(project));
        }
    }
}
