using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance;
using System.Data;

namespace RestaurantFinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <remarks>URL: GET /api/Role</remarks>
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _roleService.GetAllRoles();
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Gets a specific role by ID.
        /// </summary>
        /// <param name="id">ID of the role</param>
        /// <remarks>URL: GET /api/Role/{id}</remarks>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoles(string id)
        {
            var result = await _roleService.GetRoleById(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="name">Name of the role</param>
        /// <remarks>URL: POST /api/Role</remarks>
        [HttpPost()]
        public async Task<IActionResult> CreateRole(string name)
        {
            var result = await _roleService.CreateRole(name);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <param name="id">ID of the role</param>
        /// <param name="name">New name for the role</param>
        /// <remarks>URL: PUT /api/Role/{id}</remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(string id, string name)
        {
            var result = await _roleService.UpdateRole(id,name);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Deletes a role.
        /// </summary>
        /// <param name="id">ID of the role to delete</param>
        /// <remarks>URL: DELETE /api/Role/{id}</remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _roleService.DeleteRole(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}

