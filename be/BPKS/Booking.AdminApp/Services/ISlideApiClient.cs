using BookingSolution.ViewModels.Utilities.Slides;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking.AdminApp.Services
{
    public interface ISlideApiClient
    {
        Task<List<SlideVm>> GetAll();
    }
}