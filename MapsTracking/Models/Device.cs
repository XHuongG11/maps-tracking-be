using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapsTracking.Models
{
    [Table("GPS_Device")]
    public class Device
    {
        [Key]
        [Column("DeviceID")]
        public string DeviceID { get; set; }

        public string Name { get; set; } = string.Empty;

        public Device()
        {
            TrackingEvents = new List<DeviceLocation>();
        }

        public ICollection<DeviceLocation> TrackingEvents { get; set; }
    }
}
