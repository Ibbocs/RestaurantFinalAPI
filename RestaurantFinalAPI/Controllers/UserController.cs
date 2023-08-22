using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance.IUserServices;
using RestaurantFinalAPI.Application.DTOs.UserDTOs;

namespace RestaurantFinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly private IUserService userService;

        public UserController(IUserService _userService)
        {
            this.userService = _userService;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <remarks>URL: POST /api/User</remarks>
        /// <param name="model">Create user infarmation</param>
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto model)
        {
            var data = await userService.CreateAsync(model);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Updates the password for a user.
        /// </summary>
        /// <remarks>
        /// URL: PUT /api/User/update-password
        /// </remarks>
        /// <param name="userId">ID of the user</param>
        /// <param name="token">Password reset token</param>
        /// <param name="newPassword">New password</param>
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword(string userId, string token, string newPassword)
        {
            userService.UpdatePasswordAsync(userId, token, newPassword);
            return Ok();
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <remarks>URL: GET /api/User</remarks>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var data = await userService.GetAllUsersAsync();
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Gets the roles assigned to a user.
        /// </summary>
        /// <param name="Id">ID of the user</param>
        /// <remarks>URL: GET /api/User/get-roles-to-user/{Id}</remarks>
        [HttpGet("get-roles-to-user/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> GetRolesToUser(string Id)
        {
            var data = await userService.GetRolesToUserAsync(Id);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Assigns roles to a user.
        /// </summary>
        /// <param name="UserId">ID of the user</param>
        /// <param name="Roles">Roles to assign</param>
        /// <remarks>URL: POST /api/User/assign-role-to-user</remarks>
        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> AssignRoleToUser(string UserId, string[] Roles)
        {
            //olan rollari silib tezesin yazir usere
            var data = await userService.AssignRoleToUserAsnyc(UserId, Roles);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Updates user information.
        /// </summary>
        /// <param name="model">Updated user information</param>
        /// <remarks>URL: PUT /api/User/update-user</remarks>
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin, User")]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO model)
        {
            var data = await userService.UpdateUserAsync(model);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="UserIdOrName">ID or name of the user to delete</param>
        /// <remarks>URL: DELETE /api/User/delete-to-user</remarks>
        [HttpDelete("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin, User")]
        public async Task<IActionResult> DeleteToUser(string UserIdOrName)
        {
            var data = await userService.DeleteUserAsync(UserIdOrName);
            return StatusCode(data.StatusCode, data);
        }
    }
}

