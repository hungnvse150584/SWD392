using BookingSolution.ViewModels.Utilities.Slides;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking.ApiIntegration
{
    public interface ISlideApiClient
    {
        Task<List<SlideVm>> GetAll();
    }
}