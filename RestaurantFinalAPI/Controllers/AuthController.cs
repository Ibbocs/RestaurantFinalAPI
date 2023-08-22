using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance.IUserServices;

namespace RestaurantFinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthoService authoService;

        public AuthController(IAuthoService _authoService)
        {
            this.authoService = _authoService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(string userNameOrEmail, string password, int accesTokenLifeTime, int refreshTokenMoreLife)
        {
            var data = await authoService.LoginAsync(userNameOrEmail, password, accesTokenLifeTime, refreshTokenMoreLife);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin(string refreshToken, int accesTokenLifeTime, int refreshTokenMoreLife)
        {
            var data = await authoService.LoginWithRefreshTokenAsync(refreshToken, accesTokenLifeTime,refreshTokenMoreLife);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin,User")]
        public async Task<IActionResult> LogOut(string userNameOrEmail)
        {
            var data = await authoService.LogOut(userNameOrEmail);
            return StatusCode(data.StatusCode, data);
        }


        [HttpPost("password-reset-token")]
        //[Authorize(AuthenticationSchemes = "Admin", Roles = "Admin,User")]
        public async Task<IActionResult> PasswordReset(string email)
        {
            var data = await authoService.PasswordResetAsnyc(email);
            return Ok(data);
        }

        [HttpGet("verify-reset-token")]
        //[Authorize(AuthenticationSchemes = "Admin", Roles = "")]
        public async Task<IActionResult> VerifyResetToken(string token, string userId)
        {
            var response = await authoService.VerifyResetTokenAsync(token, userId);
            return Ok(response);
        }
    }
}
