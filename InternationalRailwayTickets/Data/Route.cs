using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternationalRailwayTickets.Data
{
    public class Route
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        public ICollection<RoutePoint> Points { get; } = new List<RoutePoint>();

        public ICollection<TrainSchedule> TrainSchedules { get; } = new List<TrainSchedule>();
        public ICollection<TrainInstance> TrainInstances { get; } = new List<TrainInstance>();
    }
}