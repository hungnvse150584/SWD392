using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingSolution.ViewModels.System.Users
{
    public class RegisterRequest
    {
        //public int UserId { get; set; }
        //public string? UserName { get; set; }
        //public string? Password { get; set; }
        //public string? FullName { get; set; }
        //public string? Email { get; set; }
        //public string? PhoneNumber { get; set; }
        //public string? AvatarUrl { get; set; }
        //public DateOnly? CreatedDate { get; set; }
        //public string? Address { get; set; }
        //public int? Role { get; set; }
        //public string? Status { get; set; }
        [Display(Name = "Tên")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        public string LastName { get; set; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        //[Display(Name = "Full Name")]
        //public string FullName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        //[Display(Name = "Address")]
        //public string Address { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}