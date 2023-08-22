using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Infrastructure.TokenServıces;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance.IUserServices;
using RestaurantFinalAPI.Application.DTOs.TokenDTOs;
using RestaurantFinalAPI.Application.Exceptions;
using RestaurantFinalAPI.Application.Exceptions.UserExceptions;
using RestaurantFinalAPI.Application.Models.ResponseModels;
using RestaurantFinalAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Implementation.Services.UserServices
{
    public class AuthService : IAuthoService
    {
        //bu service useri login edib token verir ya da rft ile login edir
        readonly UserManager<AppUser> userManager;
        readonly SignInManager<AppUser> signInManager;
        readonly ITokenHandler tokenHandler;
        readonly IUserService UserService2; //todo bunu isledib vaxtlari appsettingden verersen?
        readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager, ITokenHandler _tokenHandler, IUserService _UserService2, IHttpContextAccessor httpContextAccessor)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            this.tokenHandler = _tokenHandler;
            this.UserService2 = _UserService2;
            _httpContextAccessor = httpContextAccessor;
        }

        //todo burda LoginUserRespon islede bilerdim,amma bizim bir responsemiz olmalidi o da generic yaratdigim
        public async Task<Response<TokenDTO>> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime, int refreshTokenMoreLife)
        {

            AppUser user = await userManager.FindByNameAsync(userNameOrEmail);
            if (user == null)
                user = await userManager.FindByEmailAsync(userNameOrEmail);
            //yuxaridaki sertlerden birinden dogru kececek Email ve ya ad vermeyine gore eger duzduse verdiyi :)
            //buda else ile yaza bilerem
            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await signInManager.CheckPasswordSignInAsync(user, password, false);


            if (result.Succeeded) //Autontication ugurlu
            {
                //Autorizasiya edilmelidi
                TokenDTO tokenDTO = await tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await UserService2.UpdateRefreshToken(tokenDTO.RefreshToken, user, tokenDTO.Expiration, accessTokenLifeTime);
                return new()
                {
                    Data = tokenDTO,
                    StatusCode = 200,
                };
            }
            else
                return new() { Data = null, StatusCode = 401 };

            throw new AuthenticationErrorException();
        }

        public async Task<Response<TokenDTO>> LoginWithRefreshTokenAsync(string refreshToken, int accessTokenLifeTime, int refreshTokenMoreLife)
        {
            AppUser? user = await userManager.Users.FirstOrDefaultAsync(rf => rf.RefreshToken == refreshToken);

            //rft vaxti bitmeyibse login ola biler
            if (user != null && user?.RefreshTokenEndTime > DateTime.UtcNow)
            {
                //token yaradiriq hem de rft update edirik
                TokenDTO token = await tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await UserService2.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, refreshTokenMoreLife);
                return new()
                {
                    Data = token,
                    StatusCode = 200,
                };
            }
            else
            {
                return new()
                {
                    Data = null,
                    StatusCode = 401,
                };
            }

            throw new NotFoundUserException();
        }

        public async Task<Response<bool>> LogOut(string userNameOrEmail)
        {
            AppUser user = await userManager.FindByNameAsync(userNameOrEmail);
            if (user == null)
                user = await userManager.FindByEmailAsync(userNameOrEmail);
            //yuxaridaki sertlerden birinden dogru kececek Email ve ya ad vermeyine gore eger duzduse verdiyi :)
            //buda else ile yaza bilerem
            if (user == null)
                throw new NotFoundUserException();

           // if (_httpContextAccessor.HttpContext?.User.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            

                user.RefreshTokenEndTime = null;
                user.RefreshToken = null;

                var result = await userManager.UpdateAsync(user);
                await signInManager.SignOutAsync();

                if (result.Succeeded)
                {
                    return new()
                    {
                        Data = true,
                        StatusCode = 200,
                    };
                }
                else
                {
                    return new()
                    {
                        Data = false,
                        StatusCode = 400
                    };
                }

            throw new AuthenticationErrorException();
        }

        public async Task<string> PasswordResetAsnyc(string email)
        {
            AppUser user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);

                byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
                resetToken = WebEncoders.Base64UrlEncode(tokenBytes);
                return resetToken;
            }

            return null;
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
                resetToken = Encoding.UTF8.GetString(tokenBytes);

                return await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
            }
            return false;
        }
    }
}