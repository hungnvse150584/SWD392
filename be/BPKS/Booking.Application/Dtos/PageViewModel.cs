using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Dtos
{
    public class PageViewModel<T>
    {
        List<T> Items { get; set; }
        public int TotalRecord {  get; set; }
    }
}
