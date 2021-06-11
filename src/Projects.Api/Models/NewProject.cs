using System.ComponentModel.DataAnnotations;

namespace Projects.Api.Models
{
    public class NewProject
    {
        /// <summary>
        /// Projectname.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; } = string.Empty;
    }
}