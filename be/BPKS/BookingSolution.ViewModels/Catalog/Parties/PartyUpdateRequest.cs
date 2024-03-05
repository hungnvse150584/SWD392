using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.Catalog.Parties
{
    public class PartyUpdateRequest
    {
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public string Description { get; set; }
        public string PhoneContact { get; set; }
        public string Place { get; set; }
        //public double Rate { get; set; }
        //public string ThumbnailUrl { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DayStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime DayEnd { get; set; }
        
        public string PartyStatus { get; set; }

        public IFormFile ThumbnailUrl { get; set; }

        
    }
}
