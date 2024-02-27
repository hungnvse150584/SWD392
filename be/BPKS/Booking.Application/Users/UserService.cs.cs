using Booking.Data.Entities;
using BookingSolution.ViewModels.System.Users;
using Google.Api.Ads.Common.Lib;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Users
{
    public class ParentService : IUserService
    {
        private readonly UserManager<Account> parentManager;

        public ParentService(UserManager<Account> parentService)
        {

        }
        public Task<bool> Authencate(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
