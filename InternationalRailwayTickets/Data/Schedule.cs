using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternationalRailwayTickets.Data
{
    public abstract class Schedule
    {
        public Schedule(DateTime fromDate)
        {
            FromDate = fromDate.Date;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime FromDate { get; private set; }

        [NotMapped]
        public ICollection<DateTime> IncludedDates { get; private set; } = new List<DateTime>();
        [NotMapped]
        public ICollection<DateTime> ExcludedDates { get; private set; } = new List<DateTime>();

        public string IncludedDatesJson
        {
            get
            {
                return IncludedDates == null ? "[]" : JsonConvert.SerializeObject(IncludedDates);
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    IncludedDates.Clear();
                else
                    IncludedDates = JsonConvert.DeserializeObject<ICollection<DateTime>>(value);
            }
        }

        public string ExcludedDatesJson
        {
            get
            {
                return ExcludedDates == null ? "[]" : JsonConvert.SerializeObject(ExcludedDates);
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    ExcludedDates.Clear();
                else
                    ExcludedDates = JsonConvert.DeserializeObject<ICollection<DateTime>>(value);
            }
        }

        public abstract IEnumerable<DateTime> GetDates(DateTime from);
    }
}