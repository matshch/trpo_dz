using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Ticket Ticket { get; set; }
        // Required to setup dependent side
        public Guid TicketId { get; set; }
    }
}