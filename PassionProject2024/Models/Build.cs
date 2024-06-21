using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject2024.Models
{
    public class Build
    {
        [Key]
        public int BuildID { get; set; }  
        public string BuildName { get; set; }
        public string BuildDescription { get; set; }

        // Collection of components in the build
        public virtual ICollection<BuildComponent> BuildComponents { get; set; } 
    }

    public class BuildDto
    {
        public int BuildID { get; set; }
        public string BuildName { get; set; }
        public string BuildDescription { get; set; }
        public List<ComponentDto> Components { get; set; }
    }

}