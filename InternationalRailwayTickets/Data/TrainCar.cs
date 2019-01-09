using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternationalRailwayTickets.Data
{
    public class TrainCar
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public uint Number { get; set; }

        [Required]
        public TrainSchedule TrainSchedule { get; set; }
        [Required]
        public Car Car { get; set; }
        public RoutePoint FromPoint { get; set; }
        public RoutePoint ToPoint { get; set; }
    }
}