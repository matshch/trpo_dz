using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternationalRailwayTickets.Data
{
    public class Place
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public uint Number { get; set; }
        public uint Level { get; set; }
        public uint Floor { get; set; }

        [Required]
        public Car Car { get; set; }

        public PlaceInstance CreateInstance()
        {
            return new PlaceInstance
            {
                Number = Number,
                Level = Level,
                Floor = Floor
            };
        }

        public string GetDescription()
        {
            return Floor + " этаж, " + (Level == 0 ? "сидячее" : Level + " полка");
        }
    }
}