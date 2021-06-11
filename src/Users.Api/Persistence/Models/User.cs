using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Users.Api.Persistence.Models
{
    [Table("Users")]
    public class User
    {
        /// <summary>
        /// Unique id of the user.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Comment("Unique id of the user.")]
        public int Id { get; set; } = 0;

        /// <summary>
        /// First name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        [Comment("First name of the user.")]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Last name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        [Comment("Last name of the user.")]
        public string LastName { get; set; } = null!;
    }
}