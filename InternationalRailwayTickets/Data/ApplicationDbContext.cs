using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternationalRailwayTickets.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarInstance> CarInstances { get; set; }

        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceInstance> PlaceInstances { get; set; }

        public DbSet<Route> Routes { get; set; }
        public DbSet<RoutePoint> RoutePoints { get; set; }

        public DbSet<Station> Stations { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Train> Trains { get; set; }
        public DbSet<TrainInstance> TrainInstances { get; set; }

        public DbSet<TrainCar> TrainCars { get; set; }
        public DbSet<TrainCarInstance> TrainCarInstances { get; set; }

        public DbSet<TrainSchedule> TrainSchedules { get; set; }
        public DbSet<DailySchedule> DailySchedules { get; set; }
        public DbSet<WeeklySchedule> WeeklySchedules { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Train>().HasIndex(e => e.Number).IsUnique();
        }
    }
}
