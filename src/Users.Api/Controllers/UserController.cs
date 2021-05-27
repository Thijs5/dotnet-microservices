using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Users.Api.Models;
using Users.Api.Services;

namespace Users.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUserService userService,
            ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of users.
        /// </summary>
        /// <param name="pageSize">Number of users to return.</param>
        /// <returns>A list of users found. Empty list if no users found.</returns>
        [HttpGet]
        [Route("/users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<User>> GetList(uint pageSize = 10)
        {
            var users = Enumerable.Range(1, (int)pageSize)
                .Select(x => Guid.NewGuid())
                .Select(x => _userService.GetUser(x))
                .Where(x => x != null)!;
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
        public ActionResult<User> Get(Guid userId)
        {
            var user = _userService.GetUser(userId);
            if (user is null) { return NotFound(); }
            return user;
        }
    }
}
