using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingSolution.ViewModels.System.Users
{
    public class UserVm
    {

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "User Id")]
        public Guid Id { get; set; }

        //[Display(Name = "Full Name")]
        //public string FullName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime Dob { get; set; }

        //[Display(Name = "Address")]
        //public string Address { get; set; }

        //[Display(Name = "Password")]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Display(Name = "ConfirmPassword")]
        //[DataType(DataType.Password)]
        //public string ConfirmPassword { get; set; }
    }
}