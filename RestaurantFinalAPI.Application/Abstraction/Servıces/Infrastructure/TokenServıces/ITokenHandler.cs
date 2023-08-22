using RestaurantFinalAPI.Application.DTOs.TokenDTOs;
using RestaurantFinalAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Abstraction.Servıces.Infrastructure.TokenServıces
{
    public interface ITokenHandler
    {
        Task<TokenDTO> CreateAccessToken(int minute, AppUser user);
        string CreateRefreshToken();
    }
}
