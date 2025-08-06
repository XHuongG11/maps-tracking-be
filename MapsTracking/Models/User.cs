using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapsTracking.Models
{
    [Table("GPS_MapUser")]
    public class User
    {
        [Key] [Required]
        public string Username {  get; set; }
        public string PIN { get; set; }

    }
}
