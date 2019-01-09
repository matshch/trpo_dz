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
        public ICollection<DateTime> IncludedDates { get; } = new List<DateTime>();
        public ICollection<DateTime> ExcludedDates { get; } = new List<DateTime>();

        public abstract IEnumerable<DateTime> GetDates(DateTime from);
    }
}