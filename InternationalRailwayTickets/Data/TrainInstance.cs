using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternationalRailwayTickets.Data
{
    public class TrainInstance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public string Number { get; set; }
        public string Name { get; set; }

        [Required]
        public Route Route { get; set; }
        public ICollection<TrainCarInstance> TrainCars { get; } = new List<TrainCarInstance>();
    }
}