using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace InternationalRailwayTickets.Data
{
    public class TrainInstance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public string Number { get; set; }
        public string Name { get; set; }

        [Required]
        public Route Route { get; set; }
        public ICollection<TrainCarInstance> TrainCars { get; } = new List<TrainCarInstance>();

        public string GetFullName()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return Number;
            }
            else
            {
                return Number + " «" + Name + "»";
            }
        }

        public string GetRouteDescription()
        {
            return GetFirstPoint().Station.Name + " — " + GetLastPoint().Station.Name;
        }

        public RoutePoint GetFirstPoint()
        {
            return Route.Points.OrderBy(e => e.FromStartTime).First();
        }

        public RoutePoint GetLastPoint()
        {
            return Route.Points.OrderByDescending(e => e.FromStartTime).First();
        }

        public DateTime GetTimeAtPoint(RoutePoint point)
        {
            return StartDate.Add(Route.StartTime).Add(point.FromStartTime);
        }
    }
}