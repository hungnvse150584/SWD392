using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);
        Task<PagedResult<UserVm>> GetUsersPaging(GetUserPagingRequest request);
        Task<bool> RegisterUser(RegisterRequest request);
    }
}
