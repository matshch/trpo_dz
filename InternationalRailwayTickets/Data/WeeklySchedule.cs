using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace InternationalRailwayTickets.Data
{
    public class WeeklySchedule : Schedule
    {
        public WeeklySchedule(DateTime fromDate, ICollection<DayOfWeek> daysOfWeek) : base(fromDate)
        {
            if (!daysOfWeek.Any())
            {
                throw new ArgumentException("DaysOfWeek cannot be empty.", "daysOfWeek");
            }
            DaysOfWeek = daysOfWeek;
        }

        public WeeklySchedule(DateTime fromDate, string daysOfWeekJson) : base(fromDate)
        {
            DaysOfWeekJson = daysOfWeekJson;
        }

        [NotMapped]
        public ICollection<DayOfWeek> DaysOfWeek { get; private set; } = new List<DayOfWeek>();

        public string DaysOfWeekJson
        {
            get => DaysOfWeek == null ? "[]" : JsonConvert.SerializeObject(DaysOfWeek);

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    DaysOfWeek.Clear();
                }
                else
                {
                    DaysOfWeek = JsonConvert.DeserializeObject<ICollection<DayOfWeek>>(value);
                }
            }
        }

        public override IEnumerable<DateTime> GetDates(DateTime from)
        {
            from = from.Date;
            var included = IncludedDates.Select(e => e.Date).Where(e => e >= from);
            var excluded = ExcludedDates.Select(e => e.Date).Where(e => e >= from);
            for (var date = from; ; date = date.AddDays(1))
            {
                if (!DaysOfWeek.Contains(date.DayOfWeek))
                {
                    continue;
                }

                var maybeIncluded = included.Where(e => e < date).ToList();
                if (maybeIncluded.Any())
                {
                    maybeIncluded.Sort();
                    foreach (var maybe in maybeIncluded)
                    {
                        if (!excluded.Contains(maybe))
                        {
                            yield return maybe;
                        }
                    }
                    included = included.Except(maybeIncluded);
                    excluded = excluded.Except(maybeIncluded);
                }

                if (!excluded.Contains(date))
                {
                    yield return date;
                }
                included = included.Where(e => e > date);
                excluded = excluded.Where(e => e > date);
            }
        }
    }
}