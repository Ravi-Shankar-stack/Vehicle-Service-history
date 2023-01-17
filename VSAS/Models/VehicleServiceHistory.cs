using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace VSAS.Models
{
    public class VehicleServiceHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ServiceHistoryId { get; set; }

        
        [Required]
        public long VehicleId { get; set; }

        [Required]
        public int OdometerReading { get; set; }

        [Required]
        public DateTime ServiceDoneDate { get; set; }

        [Required]
        [StringLength(100)]
        public string ServiceDetails { get; set; }

        [Required]
        [StringLength(100)]
        public string ServiceDealerDetails { get; set; }

        [Required]
        public DateTime NextServiceDueDate { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("VehicleId")]
        public VehicleDetail Vehicle { get; set; }
    }
}
