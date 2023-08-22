using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance;
using RestaurantFinalAPI.Application.Models.ResponseModels;
using RestaurantFinalAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Implementation.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Response<bool>> CreateRole(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name });
            if (result.Succeeded)
            {
                return new() { Data = result.Succeeded, StatusCode = 201 };
            }
            else
            {
                return new() { Data = result.Succeeded, StatusCode = 400 };
            }

        }

        public async Task<Response<bool>> DeleteRole(string id)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id);
            IdentityResult result = await _roleManager.DeleteAsync(appRole);

            if (result.Succeeded)
            {
                return new() { Data = result.Succeeded, StatusCode = 200 };
            }
            else
            {
                return new() { Data = result.Succeeded, StatusCode = 400 };
            }
        }

        public async Task<Response<object>> GetAllRoles()
        {
            var data = await _roleManager.Roles.ToListAsync();
            if (data == null)
                return new()
                {
                    Data = null,
                    StatusCode = 400
                };
            return new()
            {
                Data = data,
                StatusCode = 200
            };
        }

        public async Task<Response<object>> GetRoleById(string id)
        {
            var data = await _roleManager.FindByIdAsync(id);


            if (data == null)
                return new()
                {
                    Data = null,
                    StatusCode = 400
                };
            return new()
            {
                Data = data,
                StatusCode = 200
            };
        }

        public async Task<Response<bool>> UpdateRole(string id, string name)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            role.Name = name;
            IdentityResult result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return new() { Data = result.Succeeded, StatusCode = 200 };
            }
            else
            {
                return new() { Data = result.Succeeded, StatusCode = 400 };
            }
        }
    }
}
