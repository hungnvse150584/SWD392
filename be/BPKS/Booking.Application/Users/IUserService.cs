using BookingSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Users
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequest request);

        Task<bool> Register(RegisterRequest request);
    }
}
