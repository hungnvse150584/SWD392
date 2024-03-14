using BookingSolution.ViewModels.Catalog.Parties;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.System.Services
{
    public class PartyDetailsUpdateRequest
    {
        public int PartyId { get; set; }
        //public Guid? PartyHostId { get; set; }
        public string? PartyName { get; set; }
        public string? Description { get; set; }
        public string? PhoneContact { get; set; }
        public string? Place { get; set; }
        public double? Rate { get; set; }
       // public IFormFile? ThumbnailUrl { get; set; }
        public DateTime? DayStart { get; set; }
        public DateTime? DayEnd { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public string? PartyStatus { get; set; }
        
        public List<RQ> roomUserViews { get; set; }
    }
    public class RQ
    {
        public int RoomId { get; set; }
        public int ProductId { get; set; }
    }}
