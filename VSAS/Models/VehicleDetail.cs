using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VSAS.Models
{
    public class VehicleDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long VehicleId { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required(ErrorMessage = "VehicleRegNumber is required")]
        [StringLength(50, ErrorMessage = "VehicleRegNumber cannot be longer than 50 characters")]
        //[RegularExpression(@"^[A-Z]{2}\d{2}[A-Z]{2}\d{4}$", ErrorMessage = "Vehicle registration number must be in format of AZ09AZ1234")]
        public string VehicleRegNumber { get; set; }

        [Required(ErrorMessage = "ChassisNumber is required")]
        [StringLength(50, ErrorMessage = "ChassisNumber cannot be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Chassis number must be combination of alphabets and numbers only")]
        public string ChassisNumber { get; set; }

        [Required(ErrorMessage = "EngineNumber is required")]
        [StringLength(50, ErrorMessage = "EngineNumber cannot be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Engine number must be combination of alphabets and numbers only")]
        public string EngineNumber { get; set; }

        [Required(ErrorMessage = "Make is required")]
        [StringLength(50, ErrorMessage = "Make cannot be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Make must be combination of alphabets and/or numbers only")]
        public string Make { get; set; }

        [Required(ErrorMessage = "MakeMonthYear is required")]
        public int MakeMonthYear { get; set; }

        [Required(ErrorMessage = "PurchaseDate is required")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "CurrentOdometerReading is required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "CurrentOdometerReading should be a number")]
        public int CurrentOdometerReading { get; set; }

        [Required(ErrorMessage = "CreatedDate is required")]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("UserId")]
        public UserDetails UserDetail { get; set; }
    }
}
