using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VSAS.Models
{
    public class ServiceNotificationHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotificationId { get; set; }

        
        [Required]
        public long VehicleId { get; set; }

        [Required(ErrorMessage = "NotificationMessage is required")]
        [StringLength(100, ErrorMessage = "NotificationMessage cannot be longer than 100 characters")]
        public string NotificationMessage { get; set; }

        [Required(ErrorMessage = "NotificationSentDate is required")]
        public DateTime NotificationSentDate { get; set; }

        [Required(ErrorMessage = "CreatedDate is required")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("VehicleId")]
        public VehicleDetail Vehicle { get; set; }
    }
}




