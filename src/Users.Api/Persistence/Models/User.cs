using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Users.Api.Persistence.Models
{
    [Table("Users")]
    public class User
    {
        /// <summary>
        /// Internal Id used for joins etc.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Comment("Internal id (only used for joins, etc.).")]
        public int Id { get; set; }

        /// <summary>
        /// First name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        [Comment("First name of the user.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        [Comment("Last name of the user.")]
        public string LastName { get; set; }
    }
}