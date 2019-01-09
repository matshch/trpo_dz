using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternationalRailwayTickets.Data
{
    public class Train
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Number { get; set; }
        public string Name { get; set; }

        public ICollection<TrainSchedule> TrainSchedules { get; } = new List<TrainSchedule>();
    }
}