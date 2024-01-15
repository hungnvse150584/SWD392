using System;

namespace BPKS.Models
{
    public class UsersDetail
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
