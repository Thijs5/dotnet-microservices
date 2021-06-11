using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projects.Api.Persistence.Models
{
    [Table("Projects")]
    public class Project
    {
        /// <summary>
        /// Unique id of the project.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Comment("Unique id of the project.")]
        public int Id { get; set; } = 0;

        /// <summary>
        /// Projectname.
        /// </summary>
        [Required]
        [MaxLength(255)]
        [Comment("Projectname.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// List of contributors of the project.
        /// </summary>
        public ICollection<Contributor> Contributors { get; set; } = null!;
    }
}