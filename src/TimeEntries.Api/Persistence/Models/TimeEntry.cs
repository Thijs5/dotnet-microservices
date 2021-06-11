using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TimeEntries.Api.Persistence.Models
{
    [Table("TimeEntries")]
    public class TimeEntry
    {
        /// <summary>
        /// Unique id of the entry.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Comment("Unique id of the entry.")]
        public int Id { get; set; } = 0;

        /// <summary>
        /// Id of the linked project.
        /// </summary>
        [Required]
        [Comment("Id of the linked project.")]
        public int ProjectId { get; set; } = 0;

        /// <summary>
        /// Id of the linked user.
        /// </summary>
        [Required]
        [Comment("Id of the linked user.")]
        public int UserId { get; set; } = 0;

        /// <summary>
        /// Timestamp from.
        /// </summary>
        [Required]
        [Comment("Timestamp from.")]
        public DateTime From { get; set; }

        /// <summary>
        /// Timestamp until.
        /// </summary>
        [Required]
        [Comment("Timestamp until.")]
        public DateTime Until { get; set; }
    }
}