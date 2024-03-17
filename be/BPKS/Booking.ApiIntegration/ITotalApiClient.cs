using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApiIntegration
{
    public interface ITotalApiClient
    {
        public Task<double> GetTotalCash();
        public Task<int> GetTotalPartyBooked();
        public Task<int> GetTotalUser();
    }

}
