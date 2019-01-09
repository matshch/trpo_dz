using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternationalRailwayTickets.Data
{
    public class Car
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string ServiceClass { get; set; }

        public ICollection<TrainCar> TrainCar { get; } = new List<TrainCar>();
        public ICollection<Place> Places { get; } = new List<Place>();

        public CarInstance CreateInstance()
        {
            var instance = new CarInstance {
                ServiceClass = ServiceClass
            };

            foreach (var place in Places)
            {
                instance.Places.Add(place.CreateInstance());
            }

            return instance;
        }
    }
}