using System.ComponentModel.DataAnnotations;

namespace Users.Api.Models
{
    public class NewUser
    {
        /// <summary>
        /// First name.
        /// </summary>
        /// <value></value>
        [Required]
        [MaxLength(255)]
        public string FirstName { get; init; } = string.Empty;

        /// <summary>
        /// Family name.
        /// </summary>
        /// <value></value>
        [Required]
        [MaxLength(255)]
        public string FamilyName { get; init; } = string.Empty;
    }
}