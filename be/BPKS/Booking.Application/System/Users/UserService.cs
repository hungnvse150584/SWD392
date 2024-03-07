using Booking.Data.Entities;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Booking.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly RoleManager<AspNetRole> _roleManager;
        private readonly IConfiguration _config;


        public UserService(UserManager<AspNetUser> userManager, SignInManager<AspNetUser> signInManager, RoleManager<AspNetRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return new ApiErrorResult<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Đăng nhập không đúng");
            }

            // Lấy vai trò của người dùng
            var roles = await _userManager.GetRolesAsync(user);

            // Tạo các claim bao gồm vai trò của người dùng
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, request.UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Tạo mã thông báo (token) JWT bao gồm các claim và ký tắt
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            // Trả về mã thông báo JWT đã tạo
            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if(!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword)
                || x.PhoneNumber.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserVm()
                {
                    UserName = x.UserName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    Id = x.Id,

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UserVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserVm>>(pagedResult);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }

            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }

            user = new AspNetUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dob = request.Dob,
                Email = request.Email,
                UserName = request.UserName.Trim(),
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                // Thêm người dùng vào vai trò được chỉ định
                var addToRoleResult = await _userManager.AddToRoleAsync(user, request.RoleName);

                if (addToRoleResult.Succeeded)
                {
                    return new ApiSuccessResult<bool>();
                }
                else
                {
                    // Nếu không thêm được vào vai trò, xóa người dùng đã được tạo
                    await _userManager.DeleteAsync(user);
                    return new ApiErrorResult<bool>("Không thể thêm người dùng vào vai trò được chọn");
                }
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

        public async Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id))
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Dob = request.Dob;


            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }

        public async Task<ApiResult<UserVm>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserVm>("User không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserVm()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dob = user.Dob,
                Roles = roles
            };
            return new ApiSuccessResult<UserVm>(userVm);
        }
        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("User không tồn tại");
            }
            var reult = await _userManager.DeleteAsync(user);
            if (reult.Succeeded)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return new ApiSuccessResult<bool>();
        }

        
    }
}
