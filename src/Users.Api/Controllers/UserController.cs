using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Users.Api.Mappers;
using Users.Api.Models;
using Users.Api.Persistence;

namespace Users.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersDbContext _context;
        private readonly IUserMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(
            UsersDbContext context,
            IUserMapper mapper,
            ILogger<UserController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="newUser">User info to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        [HttpPost]
        [Route("/users")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> CreateAsync(NewUser newUser, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var dbUser = _mapper.Map(newUser);
            await _context.Users.AddAsync(dbUser, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(GetById), new { userId = dbUser.Id }, dbUser);
        }

        /// <summary>
        /// Get a list of users.
        /// </summary>
        /// <returns>A list of users found. Empty list if no users found.</returns>
        [HttpGet]
        [Route("/users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _context.Users
                .Select(u => _mapper.Map(u))
                .ToList();
            return Ok(users);
        }

        /// <summary>
        /// Get a single user by its id.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>Returns a user if a user is found.</returns>
        [HttpGet]
        [Route("/users/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetById(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            if (user is null) { return NotFound(); }
            return Ok(user);
        }

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="userId">Id of the user to update.</param>
        /// <param name="editUser">New data for the user.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        [HttpPut]
        [Route("/users/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> UpdateAsync(int userId, EditUser editUser, CancellationToken cancellationToken)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            if (user is null)
            {
                ModelState.AddModelError(nameof(userId), $"Could not find user with id '{userId}'");
            }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            user!.FirstName = editUser.FirstName;
            user.LastName = editUser.FamilyName;

            await _context.SaveChangesAsync(cancellationToken);
            return Ok(_mapper.Map(user));
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="userId">Id of the user to delete.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpDelete]
        [Route("/users/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<ActionResult<User>> DeleteAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return Ok(_mapper.Map(user));
        }
    }
}
