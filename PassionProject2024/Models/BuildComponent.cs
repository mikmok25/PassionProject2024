using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject2024.Models
{
    public class BuildComponent
    {
        [Key]
        public int BuildComponentID { get; set; }  // Primary key for the BuildComponent table

        [ForeignKey("Build")]
        public int BuildID { get; set; } // Foreign key to the Build table
        public virtual Build Build { get; set; } // Navigation property to the Build

        [ForeignKey("Component")]
        public int ComponentID { get; set; } // Foreign key to the Component table
        public virtual Component Component { get; set; } // Navigation property to the Component
    }
}
