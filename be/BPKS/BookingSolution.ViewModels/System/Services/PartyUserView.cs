﻿namespace BookingSolution.ViewModels.System.Services
{
    public class PartyUserView
    {
        public int PartyId { get; set; }
        //public Guid? PartyHostId { get; set; }
        public string? PartyName { get; set; }
        public string? Description { get; set; }
        public string? PhoneContact { get; set; }
        public string? Place { get; set; }
        public double? Rate { get; set; }
        public string? ThumbnailUrl { get; set; }
        public DateTime? DayStart { get; set; }
        public DateTime? DayEnd { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public string? PartyStatus { get; set; }
        public List<RoomUserView>? roomUserViews { get; set; }
       
    }
}
