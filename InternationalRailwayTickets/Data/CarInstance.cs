using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternationalRailwayTickets.Data
{
    public class CarInstance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string ServiceClass { get; set; }

        public TrainCarInstance TrainCar { get; set; }
        public ICollection<PlaceInstance> Places { get; } = new List<PlaceInstance>();
    }
}