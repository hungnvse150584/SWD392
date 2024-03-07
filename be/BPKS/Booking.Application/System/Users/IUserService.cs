using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Users;
using GetUserPagingRequest = BookingSolution.ViewModels.System.Users.GetUserPagingRequest;
using LoginRequest = BookingSolution.ViewModels.System.Users.LoginRequest;
using RegisterRequest = BookingSolution.ViewModels.System.Users.RegisterRequest;
using UserUpdateRequest = BookingSolution.ViewModels.System.Users.UserUpdateRequest;
using UserVm = BookingSolution.ViewModels.System.Users.UserVm;

namespace Booking.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);

        Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request);
        Task<ApiResult<UserVm>> GetById(Guid id);
        Task<ApiResult<bool>> Delete(Guid id);
        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}
