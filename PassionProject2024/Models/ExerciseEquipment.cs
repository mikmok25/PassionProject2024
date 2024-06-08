using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject2024.Models
{
    public class ExerciseEquipment
    {
        //What are some things that define the exercise equipment?

        [Key]
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int EquipmentWeight { get; set; }

        public decimal EquipmentCost { get; set; }

        // A piece of equipment has one category

        [ForeignKey("Category")]

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        // A category can include many pieces of equipment

        public ICollection<Gym> Gyms { get; set; }

        // Data Transfer Object (DTO) allows us to package the information for each model.

        public class ExerciseEquipmentDto
        {
            public int EquipmentId { get; set; }
            public string EquipmentName { get; set; }
                

            public decimal EquipmentPrice { get; set; }

            public string CategoryName { get; set; }

            // A category can include many pieces of equipment

            // A piece of equipment can be at many gyms
            public ICollection <Gym> Gyms { get; set; }


        }
    }
}