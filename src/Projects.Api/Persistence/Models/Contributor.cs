using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projects.Api.Persistence.Models
{
    [Table("Contributors")]
    public class Contributor
    {
        /// <summary>
        /// Internal id of the contributor.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Comment("Internal id (only used for joins, etc.).")]
        public int Id { get; set; } = 0;

        /// <summary>
        /// User id of the contributor.
        /// </summary>
        [Comment("User id of the contributor.")]
        public int UserId { get; set; } = 0;

        /// <summary>
        /// The id of the linked project.
        /// </summary>
        [ForeignKey("Project")]
        [Comment("The id of the linked project.")]
        public int ProjectId { get; set; } = 0;

        /// <summary>
        /// The linked project.
        /// </summary>
        public Project Project { get; set; } = null!;
    }
}