using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeEntries.Api.Mappers;
using TimeEntries.Api.Models;
using TimeEntries.Api.Persistence;

namespace TimeEntries.Api.Controllers
{
    [ApiController]
    public class TimeEntriesController : ControllerBase
    {
        private readonly TimeEntriesDbContext _context;
        private readonly ITimeEntryMapper _mapper;
        private readonly ILogger<TimeEntriesController> _logger;

        public TimeEntriesController(
            TimeEntriesDbContext context,
            ITimeEntryMapper mapper,
            ILogger<TimeEntriesController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Create a new entry for a project/user combination.
        /// </summary>
        /// <param name="newTimeEntry">Time entry info to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        [HttpPost]
        [Route("/time-entries")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TimeEntry>> CreateAsync(NewTimeEntry newTimeEntry, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var timeEntry = new Persistence.Models.TimeEntry
            {
                ProjectId = newTimeEntry.ProjectId,
                UserId = newTimeEntry.UserId,
                From = newTimeEntry.From,
                Until = newTimeEntry.Until,
            };
            await _context.TimeEntries.AddAsync(timeEntry, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(GetById), new { timeEntryId = timeEntry.Id }, timeEntry);
        }

        /// <summary>
        /// Get a list of time entries for a project.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        [HttpGet]
        [Route("/time-entries/projects/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TimeEntry>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<TimeEntry>> GetByProject(int projectId)
        {
            var entries = _context.TimeEntries
                .Where(x => x.ProjectId == projectId)
                .OrderByDescending(x => x.From)
                .Select(x => _mapper.Map(x))
                .ToList();
            return Ok(entries);
        }


        /// <summary>
        /// Get a list of time entries for a project.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        [HttpGet]
        [Route("/time-entries/users/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TimeEntry>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<TimeEntry>> GetByUser(int userId)
        {
            var entries = _context.TimeEntries
                .Where(x => x.ProjectId == userId)
                .OrderByDescending(x => x.From)
                .Select(x => _mapper.Map(x))
                .ToList();
            return Ok(entries);
        }

        /// <summary>
        /// Get a single time entry by its id.
        /// </summary>
        /// <param name="timeEntryId">The id of the time entry.</param>
        /// <returns>Returns a time entry if a time entry is found.</returns>
        [HttpGet]
        [Route("/time-entries/{timeEntryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TimeEntry))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TimeEntry> GetById(int timeEntryId)
        {
            var timeEntry = _context.TimeEntries.SingleOrDefault(x => x.Id == timeEntryId);
            if (timeEntry is null) { return NotFound(); }
            return Ok(timeEntry);
        }

        /// <summary>
        /// Update a time entry.
        /// </summary>
        /// <param name="timeEntryId"></param>
        /// <param name="editTimeEntry">New data for the project.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        [HttpPut]
        [Route("/time-entries/{timeEntryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TimeEntry))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TimeEntry>> UpdateAsync(int timeEntryId, EditTimeEntry editTimeEntry, CancellationToken cancellationToken)
        {
            var timeEntry = _context.TimeEntries.SingleOrDefault(x => x.Id == timeEntryId);
            if (timeEntry is null)
            {
                ModelState.AddModelError(nameof(timeEntryId), $"Could not find project with id '{timeEntryId}'");
            }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            timeEntry!.ProjectId = editTimeEntry.ProjectId;
            timeEntry!.UserId = editTimeEntry.UserId;
            timeEntry!.From = editTimeEntry.From;
            timeEntry!.Until = editTimeEntry.Until;

            await _context.SaveChangesAsync(cancellationToken);
            return Ok(_mapper.Map(timeEntry));
        }

        /// <summary>
        /// Delete a time entry.
        /// </summary>
        /// <param name="timeEntryId">Id of the time entry to delete.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpDelete]
        [Route("/time-entries/{timeEntryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TimeEntry))]
        public async Task<ActionResult<TimeEntry>> DeleteAsync(int timeEntryId, CancellationToken cancellationToken = default)
        {
            var timeEntry = _context.TimeEntries.SingleOrDefault(u => u.Id == timeEntryId);
            if (timeEntry != null)
            {
                _context.TimeEntries.Remove(timeEntry);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return Ok(_mapper.Map(timeEntry));
        }
    }
}
