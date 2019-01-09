using System;
using System.Collections.Generic;
using System.Linq;

namespace InternationalRailwayTickets.Data
{
    public class DailySchedule : Schedule
    {
        public DailySchedule(DateTime fromDate, uint everyNDay) : base(fromDate)
        {
            if (everyNDay < 1)
            {
                throw new ArgumentOutOfRangeException("everyNDay", "Can't create schedule every 0 days.");
            }
            EveryNDay = everyNDay;
        }

        public uint EveryNDay { get; private set; }

        public override IEnumerable<DateTime> GetDates(DateTime from)
        {
            from = from.Date;
            var included = IncludedDates.Select(e => e.Date).Where(e => e >= from);
            var excluded = ExcludedDates.Select(e => e.Date).Where(e => e >= from);
            for (var date = FromDate.Date; ; date = date.AddDays(EveryNDay))
            {
                if (date < from)
                {
                    continue;
                }

                var maybeIncluded = included.Where(e => e < date).ToList();
                if (maybeIncluded.Count > 0)
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
                else
                {
                    included = included.Except(new[] { date });
                    excluded = excluded.Except(new[] { date });
                }
            }
        }
    }
}