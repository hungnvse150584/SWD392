using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.ApiIntegration
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request);
        Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest);
        Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);
        Task<ApiResult<UserVm>> GetById(Guid id);
    }
}
