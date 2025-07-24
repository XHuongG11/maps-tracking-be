using MapsTracking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MapsTracking.Configurations
{
    public class DeviceConfig : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasMany(d => d.TrackingEvents)
                   .WithOne(dl => dl.Device)
                   .HasForeignKey(dl => dl.DeviceID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
