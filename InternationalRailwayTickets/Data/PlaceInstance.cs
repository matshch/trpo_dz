using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace InternationalRailwayTickets.Data
{
    public class PlaceInstance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public uint Number { get; set; }
        public uint Level { get; set; }
        public uint Floor { get; set; }

        [Required]
        public CarInstance Car { get; set; }
        public ICollection<Ticket> Tickets { get; } = new List<Ticket>();

        public string GetDescription()
        {
            return Floor + " этаж, " + (Level == 0 ? "сидячее" : Level + " полка");
        }

        public bool IsVacant(RoutePoint fromPoint, RoutePoint toPoint)
        {
            return !Tickets.Any(e => e.ToPoint.FromStartTime > fromPoint.FromStartTime &&
                               e.FromPoint.FromStartTime < toPoint.FromStartTime);
        }
    }
}