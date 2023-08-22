using RestaurantFinalAPI.Application.DTOs.TokenDTOs;
using RestaurantFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance.AuthenticationServices
{
    public interface IInternalAuthentications
    {
        Task<Response<TokenDTO>> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime, int refreshTokenMoreLife);
        Task<Response<TokenDTO>> LoginWithRefreshTokenAsync(string refreshToken, int accessTokenLifeTime, int refreshTokenMoreLife);
        Task<Response<bool>> LogOut(string userNameOrEmail);

        public Task<string> PasswordResetAsnyc(string email);
        public Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
    }
}
