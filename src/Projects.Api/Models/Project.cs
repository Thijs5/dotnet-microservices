using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projects.Api.Models
{
    public class Project
    {
        /// <summary>
        /// Unique id of the project.
        /// </summary>
        [Required]
        public int Id { get; init; } = 0;

        /// <summary>
        /// Projectname.
        /// </summary>
        [Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Family name.
        /// </summary>
        /// <value></value>
        [Required]
        public IEnumerable<int> Contributors { get; init; } = Enumerable.Empty<int>();

    }
}