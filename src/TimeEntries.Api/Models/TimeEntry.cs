using System.ComponentModel.DataAnnotations;
using System;

namespace TimeEntries.Api.Models
{
    public class TimeEntry
    {
        /// <summary>
        /// Unique id of the project.
        /// </summary>
        [Required]
        public int Id { get; init; } = 0;

        /// <summary>
        /// Id of the linked project.
        /// </summary>
        [Required]
        public int ProjectId { get; set; } = 0;

        /// <summary>
        /// Id of the linked user.
        /// </summary>
        [Required]
        public int UserId { get; set; } = 0;

        /// <summary>
        /// Timestamp from.
        /// </summary>
        [Required]
        public DateTime From { get; set; }

        /// <summary>
        /// Timestamp until.
        /// </summary>
        [Required]
        public DateTime Until { get; set; }

    }
}