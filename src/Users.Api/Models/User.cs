using System;
using System.ComponentModel.DataAnnotations;

namespace Users.Api.Models
{
    public class User
    {
        /// <summary>
        /// Unique identifier of the user.
        /// </summary>
        /// <value></value>
        [Required]
        public Guid Id { get; init; } = Guid.Empty;

        /// <summary>
        /// First name.
        /// </summary>
        /// <value></value>
        [Required]
        public string FirstName { get; init; } = string.Empty;

        /// <summary>
        /// Family name.
        /// </summary>
        /// <value></value>
        [Required]
        public string FamilyName { get; init; } = string.Empty;
    }
}