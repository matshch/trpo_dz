using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternationalRailwayTickets.Data
{
    public class Ticket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string PassengerName { get; set; }
        [Required]
        public string DocumentType { get; set; }
        [Required]
        public string DocumentNumber { get; set; }
        public bool Paid { get; set; }

        [Required]
        public IdentityUser User { get; set; }
        public PlaceInstance PlaceInstance { get; set; }
        public RoutePoint FromPoint { get; set; }
        public RoutePoint ToPoint { get; set; }
    }
}