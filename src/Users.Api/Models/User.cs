using System.ComponentModel.DataAnnotations;

namespace Users.Api.Models
{
    public class User
    {
        /// <summary>
        /// Unique id of the user.
        /// </summary>
        [Required]
        public int Id { get; init; }

        /// <summary>
        /// First name.
        /// </summary>
        [Required]
        public string FirstName { get; init; } = string.Empty;

        /// <summary>
        /// Family name.
        /// </summary>
        [Required]
        public string FamilyName { get; init; } = string.Empty;
    }
}