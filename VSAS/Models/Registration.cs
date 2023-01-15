using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VSAS.Models
{
    public class Registration
    {
        [Required(ErrorMessage ="First Name is required")]
        [StringLength(25,ErrorMessage = "First Name Cannot be longer than 25 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(25, ErrorMessage = "Last Name Cannot be longer than 25 characters")]
        public string LastName { get; set; }

        [Key]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage ="contact number must be a 10 digit number")]
        public string ContactNumber { get; set; }


        [Required(ErrorMessage = "Passcode is required")]
        [StringLength(100,ErrorMessage ="Passcode must be atleast 8 characters", MinimumLength = 8)]
        public string PassCode { get; set; }
    }
}
