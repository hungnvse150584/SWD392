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
        private readonly RoleManager<AppNetRole> _roleManager;
        private readonly IConfiguration _config;


        public UserService(UserManager<AspNetUser> userManager, SignInManager<AspNetUser> signInManager, RoleManager<AppNetRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Đăng nhập không đúng");
            }
            var roles = await _userManager.GetRolesAsync(user);
            //login success
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                //new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role, string.Join(",", roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };

            //link fix bug :https://stackoverflow.com/questions/47279947/idx10603-the-algorithm-hs256-requires-the-securitykey-keysize-to-be-greater
            //Điều đó đã giải quyết được vấn đề của tôi vì số HmacSha256trong dòng SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)phải lớn hơn 128 bit.Tóm lại, chỉ cần sử dụng một chuỗi dài làm khóa.
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var token = new JwtSecurityToken(_config["this is my custom Secret key for authentication"],
            //    _config["this is my custom Secret key for authentication"],
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
            
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

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
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }
            user = new AspNetUser()
            {
                FirstName =request.FirstName,
                LastName =request.LastName,
                Dob = request.Dob,
                Email = request.Email,
                UserName = request.UserName.Trim(),
                PhoneNumber = request.PhoneNumber,
                //Address = request.Address
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
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
                //Roles = roles
            };
            return new ApiSuccessResult<UserVm>(userVm);
        }

    }
}
