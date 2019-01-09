using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternationalRailwayTickets.Data
{
    public class TrainCarInstance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public uint Number { get; set; }

        [Required]
        public TrainInstance Train { get; set; }
        [Required]
        public CarInstance Car { get; set; }
        // Required to setup dependent side
        public Guid CarId { get; set; }
        [Required]
        public RoutePoint FromPoint { get; set; }
        [Required]
        public RoutePoint ToPoint { get; set; }
    }
}