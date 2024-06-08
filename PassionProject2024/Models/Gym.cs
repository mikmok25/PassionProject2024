using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Web;

namespace PassionProject2024.Models
{
    public class Gym
    {
        [Key]
        public int GymId { get; set; }
        public string GymName { get; set; }

        public string GymAddress { get; set;}

        // A gym has many pieces of equipment

        public ICollection<ExerciseEquipment> ExerciseEquipments { get; set; }


    }
}