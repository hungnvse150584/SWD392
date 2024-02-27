using BookingSolution.ViewModels.System.Users;

namespace Booking.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);
    }
}
