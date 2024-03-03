using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookingSolution.ViewModels.System.Users
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        //[Display(Name = "User Name")]
        //public string UserName { get; set; }

        //[Display(Name = "Full Name")]
        //public string FullName { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }   

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Day Of Birth")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        //[Display(Name = "Address")]
        //public string Address { get; set; }
    }
}
