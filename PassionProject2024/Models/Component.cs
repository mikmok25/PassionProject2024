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
        public string Name { get; set; }
        public string Type { get; set; } // e.g., CPU, GPU, RAM
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }

        public string ImagePath { get; set; }
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