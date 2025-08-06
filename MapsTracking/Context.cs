using Microsoft.EntityFrameworkCore;
using MapsTracking.Configurations;
using MapsTracking.Models;

namespace MapsTracking
{
    public class Context :DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceLocation> DeviceLocations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DeviceConfig());
            modelBuilder.ApplyConfiguration(new DeviceLocationConfig());
        }
    }
}
