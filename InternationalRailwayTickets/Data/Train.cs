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

        public TrainInstance CreateInstance(DateTime date)
        {
            date = date.Date;

            TrainSchedule schedule = null;
            foreach (var s in TrainSchedules)
            {
                if (s.FromDate <= date && date <= s.ToDate)
                {
                    schedule = s;
                    break;
                }
            }

            if (schedule == null)
            {
                throw new ArgumentOutOfRangeException("date", "No schedule for this date");
            }

            var instance = new TrainInstance
            {
                Number = Number,
                Name = Name,
                StartDate = date,
                Route = schedule.Route
            };

            foreach (var car in schedule.TrainCars)
            {
                instance.TrainCars.Add(car.CreateInstance());
            }

            return instance;
        }

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
    }
}