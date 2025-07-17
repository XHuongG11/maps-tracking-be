using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MapsTracking.Models
{
    [Table("DeviceLocation")]
    public class DeviceLocation
    {
        [Key]
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public virtual Device Device { get; set; }
    }
}
