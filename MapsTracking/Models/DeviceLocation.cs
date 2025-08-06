using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MapsTracking.Models
{
    [Table("GPS_TrackingEvents")]
    public class DeviceLocation
    {
        [Key]
        [Column("Oid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Oid { get; set; }

        [Column("DeviceID")]
        public string DeviceID { get; set; }

        public string Title { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [Column("RecordDate")]
        public DateTime RecordDate { get; set; }

        public string UserName { get; set; }
        public int Type {  get; set; }
        public string LinkInfo { get; set; }

        [JsonIgnore]
        public virtual Device Device { get; set; }
    }
}
