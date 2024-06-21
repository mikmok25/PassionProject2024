using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject2024.Models
{
    public class Component
    {
        [Key]
        public int ComponentID { get; set; }
        public string Name { get; set; } // Name of the component, e.g., "Intel i7 CPU"
        public string Type { get; set; } // Type of the component, e.g., "CPU", "GPU", "RAM"
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }

        public string ImagePath { get; set; } // Path to the image file or URL of the component's image

    }

    public class ComponentDto
    {
        public int ComponentID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }

    }
}