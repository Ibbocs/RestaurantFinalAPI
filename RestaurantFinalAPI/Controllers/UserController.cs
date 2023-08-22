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

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto model)
        {
            var data = await userService.CreateAsync(model);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword(string userId, string token, string newPassword)
        {
            userService.UpdatePasswordAsync(userId, token, newPassword);
            return Ok();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var data = await userService.GetAllUsersAsync();
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("get-roles-to-user/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> GetRolesToUser(string Id)
        {
            var data = await userService.GetRolesToUserAsync(Id);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> AssignRoleToUser(string UserId, string[] Roles)
        {
            //olan rollari silib tezesin yazir usere
            var data = await userService.AssignRoleToUserAsnyc(UserId, Roles);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin, User")]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO model)
        {
            var data = await userService.UpdateUserAsync(model);
            return StatusCode(data.StatusCode, data);
        }

        [HttpDelete("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin, User")]
        public async Task<IActionResult> DeleteToUser(string UserIdOrName)
        {
            var data = await userService.DeleteUserAsync(UserIdOrName);
            return StatusCode(data.StatusCode, data);
        }
    }
}

