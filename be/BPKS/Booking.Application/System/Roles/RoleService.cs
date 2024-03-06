using Booking.Data.Entities;
using BookingSolution.ViewModels.System.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.System.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AspNetRole> _roleManager;

        public RoleService(RoleManager<AspNetRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<RoleVm>> GetAll()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    //Description = x.Description
                }).ToListAsync();

            return roles;
        }
    }
}
