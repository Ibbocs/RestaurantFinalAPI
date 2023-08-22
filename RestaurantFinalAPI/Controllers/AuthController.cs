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

        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <remarks>URL: POST /api/Auth/Login</remarks>
        /// <param name="userNameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <param name="accessTokenLifetime">Access token lifetime in minutes</param>
        /// <param name="refreshTokenLifetime">Refresh token lifetime in minutes</param>
        /// <returns>User details and tokens</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(string userNameOrEmail, string password, int accessTokenLifetime, int refreshTokenLifetime)
        {
            
            var data = await authoService.LoginAsync(userNameOrEmail, password, accessTokenLifetime, refreshTokenLifetime);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Logs in the user using a refresh token.
        /// </summary>
        /// <remarks>URL: POST api/Auth/RefreshTokenLogin</remarks>
        /// <param name="refreshToken">Refresh token</param>
        /// <param name="accesTokenLifeTime">Access token lifetime in minutes</param>
        /// <param name="refreshTokenMoreLife">Refresh token lifetime in minutes</param>
        /// <returns>User details and tokens</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin(string refreshToken, int accesTokenLifeTime, int refreshTokenMoreLife)
        {
            var data = await authoService.LoginWithRefreshTokenAsync(refreshToken, accesTokenLifeTime,refreshTokenMoreLife);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Logs out the user.
        /// </summary>
        /// <remarks>URL: PUT api/Auth/LogOut</remarks>
        /// <param name="userNameOrEmail">Username or email of the user to log out</param>
        /// <returns>Logout status</returns>
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin,User")]
        public async Task<IActionResult> LogOut(string userNameOrEmail)
        {
            var data = await authoService.LogOut(userNameOrEmail);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Generates a password reset token.
        /// </summary>
        /// <remarks>URL: POST api/Auth/password-reset-token</remarks>
        /// <param name="email">Email</param>
        /// <returns>Password reset token</returns>
        [HttpPost("password-reset-token")]
        public async Task<IActionResult> PasswordReset(string email)
        {
            
            var data = await authoService.PasswordResetAsnyc(email);
            return Ok(data);
        }

        /// <summary>
        /// Verifies a password reset token.
        /// </summary>
        /// <remarks>URL: GET api/Auth/verify-reset-token</remarks>
        /// <param name="token">Password reset token</param>
        /// <param name="userId">User ID</param>
        /// <returns>Token verification result</returns>
        [HttpGet("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken(string token, string userId)
        {
            
            var response = await authoService.VerifyResetTokenAsync(token, userId);
            return Ok(response);
        }
    }
}
