using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapsTracking.Models
{
    [Table("Device")]
    public class Device
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public Device()
        {
            Locations = new List<DeviceLocation>();
        }

        public ICollection<DeviceLocation> Locations { get; set; }
    }
}
