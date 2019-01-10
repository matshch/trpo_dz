using System;

namespace InternationalRailwayTickets.Models
{
    public class FindTrainModel
    {
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public DateTime FromDate { get; set; }
    }
}