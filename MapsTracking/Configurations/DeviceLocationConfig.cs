using Microsoft.EntityFrameworkCore;
using MapsTracking.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MapsTracking.Configurations
{
    public class DeviceLocationConfig : IEntityTypeConfiguration<DeviceLocation>
    {

        public void Configure(EntityTypeBuilder<DeviceLocation> builder)
        {
           
        }
    }
}
