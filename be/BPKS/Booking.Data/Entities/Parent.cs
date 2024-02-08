using System;
using System.ComponentModel.DataAnnotations;

namespace BPKS.Entities
{
    public class Parent
    {
        public int ParentId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }
}
