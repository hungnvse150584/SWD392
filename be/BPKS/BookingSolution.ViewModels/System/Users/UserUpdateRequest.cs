using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        //[Display(Name = "Address")]
        //public string Address { get; set; }
    }
}
