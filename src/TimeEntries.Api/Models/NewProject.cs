using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeEntries.Api.Models
{
    public class NewTimeEntry : IValidatableObject
    {
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
        public DateTime From { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Timestamp until.
        /// </summary>
        [Required]
        public DateTime Until { get; set; } = DateTime.MinValue;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (From >= Until)
            {
                yield return new ValidationResult(
                    $"{nameof(From)} cannot be after {nameof(Until)}.",
                    new[] { nameof(From), nameof(Until) });
            }
        }
    }
}