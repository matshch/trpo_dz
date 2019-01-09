using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternationalRailwayTickets.Data
{
    public class TrainSchedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public Train Train { get; set; }
        [Required]
        public Route Route { get; set; }
        [Required]
        public Schedule Schedule { get; set; }
        public ICollection<TrainCar> TrainCars { get; } = new List<TrainCar>();
    }
}