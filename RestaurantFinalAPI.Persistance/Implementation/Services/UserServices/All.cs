//using Microsoft.AspNetCore.Identity;
//using RestaurantFinalAPI.Application.Abstraction.Servıces.Infrastructure.TokenServıces;
//using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance.IUserServices;
//using RestaurantFinalAPI.Application.DTOs.TokenDTOs;
//using RestaurantFinalAPI.Application.Exceptions.UserExceptions;
//using RestaurantFinalAPI.Application.Exceptions;
//using RestaurantFinalAPI.Domain.Entities.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using RestaurantFinalAPI.Application.DTOs.UserDTOs;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using RestaurantFinalAPI.Application.Models.ResponseModels;
//using System.Security.Cryptography;

//namespace RestaurantFinalAPI.Persistance.Implementation.Services.UserServices
//{
//    internal class All
//    {

//    }

//    public class AuthService2 : IAuthoService
//    {
//        //bu service useri login edib token verir ya da rft ile login edir
//        readonly UserManager<AppUser> userManager;
//        readonly SignInManager<AppUser> signInManager;
//        readonly ITokenHandler tokenHandler;
//        readonly IUserService UserService2; //todo bunu isledib vaxtlari appsettingden verersen?


//        public AuthService2(SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager, ITokenHandler _tokenHandler, IUserService _UserService2)
//        {
//            signInManager = _signInManager;
//            userManager = _userManager;
//            this.tokenHandler = _tokenHandler;
//            this.UserService2 = _UserService2;
//        }

//        //todo burda LoginUserRespon islede bilerdim,amma bizim bir responsemiz olmalidi o da generic yaratdigim
//        public async Task<Response<TokenDTO>> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime)
//        {
            
//            AppUser user = await userManager.FindByNameAsync(userNameOrEmail);
//            if (user == null)
//                user = await userManager.FindByEmailAsync(userNameOrEmail);
//            //yuxaridaki sertlerden birinden dogru kececek Email ve ya ad vermeyine gore eger duzduse verdiyi :)
//            //buda else ile yaza bilerem
//            if (user == null)
//                throw new NotFoundUserException();

//            SignInResult result = await signInManager.CheckPasswordSignInAsync(user, password, false);

//            if (result.Succeeded) //Autontication ugurlu
//            {
//                //default olaraq hamiya bu rol verilecek 
//                //todo bunu appsettingden oxu
//                await userManager.AddToRoleAsync(user, "User");
//                //Autorizasiya edilmelidi
//                TokenDTO tokenDTO = await tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
//                await UserService2.UpdateRefreshToken(tokenDTO.RefreshToken, user, tokenDTO.Expiration, 59);
//                return new()
//                {
//                    Data = tokenDTO,
//                    StatusCode = 200,
//                };

//            }
//            else
//                return new() { Data = null, StatusCode = 400 };

//            throw new AuthenticationErrorException();
//        }

//        public async Task<Response<TokenDTO>> LoginWithRefreshTokenAsync(string refreshToken)//todo burda methoddan al token vaxtini
//        {
//            AppUser? user = await userManager.Users.FirstOrDefaultAsync(rf => rf.RefreshToken == refreshToken);

//            //rft vaxti bitmeyibse login ola biler
//            if (user != null && user?.RefreshTokenEndTime > DateTime.UtcNow)
//            {
//                //token yaradiriq hem de rft update edirik
//                TokenDTO token = await tokenHandler.CreateAccessToken(1, user);
//                await UserService2.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 59);
//                return new()
//                {
//                    Data = token,
//                    StatusCode = 200,
//                };
//            }
//            else
//            {
//                return new()
//                {
//                    Data = null,
//                    StatusCode = 400,
//                };
//            }

//            throw new NotFoundUserException();
//        }
//    }

//    public class UserService2 : IUserService
//    {
//        //unutma ki qeydiyatda rft vermirik, login olanda verirk
//        //bu service user yaratmaq ve rft'sini update elemek ucuundu
//        readonly UserManager<AppUser> userManager;

//        public UserService2(UserManager<AppUser> _userManager)
//        {
//            userManager = _userManager;
//        }

//        //user yaratma
//        public async Task<Response<CreateUserResponseDTO>> CreateAsync(CreateUserDto model)
//        {

//            //todo identiti user falan yaradilanda id bizim vermeyi teleb edir string verdiyimize gore app userde Id = Guid. NewGuid(). ToString() GY 38bolum 57:21
//            IdentityResult result = await userManager.CreateAsync(new()
//            {
//                Id = Guid.NewGuid().ToString(),
//                UserName = model.UserName,
//                Email = model.Email,
//                FirstName = model.FirstName,
//                LastName = model.LastName,


//            }, model.Password);


//            //CreateUserResponseDTO response = new() { Succeeded = result.Succeeded };

//            var response = new Response<CreateUserResponseDTO>
//            {
//                Data = new CreateUserResponseDTO { Succeeded = result.Succeeded },
//                StatusCode = result.Succeeded ? 200 : 400
//            };

//            if (!result.Succeeded)
//            {
//                response.Data.Message = string.Join("\n", result.Errors.Select(error => $"{error.Code} - {error.Description}"));
//            }

//            return response;

//            //throw new UserCreateFaileedException();
//        }

//        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int refreshTokenMoreLifeMinute)
//        {
//            //AppUser user = await userManager.FindByIdAsync(user.Id); //todo app user cekilede biler cekilmeyede ehtiyaca gore
//            if (user != null)
//            {
//                user.RefreshToken = refreshToken;
//                user.RefreshTokenEndTime = accessTokenDate.AddMinutes(refreshTokenMoreLifeMinute);
//                await userManager.UpdateAsync(user);
//            }
//            else
//                throw new NotFoundUserException();

//            //throw new UpdateUserException(updateResult.Errors);
//        }

//    }

//    public class TokenHandler : ITokenHandler
//    {
//        readonly IConfiguration configuration;
//        readonly UserManager<AppUser> userManager;

//        public TokenHandler(IConfiguration _configuration, UserManager<AppUser> userManager)
//        {
//            this.configuration = _configuration;
//            this.userManager = userManager;
//        }


//        public async Task<TokenDTO> CreateAccessToken(int minute, AppUser user)
//        {
//            TokenDTO tokenDTO = new TokenDTO();

//            //Security key simmetrikliyini duzeldirik
//            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));

//            //todo jwtAutoAndLogger claimsder de vermisem, bunda da belke ele edek?
//            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id),
//                new Claim(ClaimTypes.Name, user.UserName),
//                new Claim(ClaimTypes.Email, user.Email),
//            };

//            //Role verirem tokene
//            var roles = await userManager.GetRolesAsync(user);
//            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


//            //token konfigurasiyasi veririk
//            tokenDTO.Expiration = DateTime.UtcNow.AddMinutes(minute);
//            JwtSecurityToken securityToken = new(
//                audience: configuration["Token:Audience"],
//                issuer: configuration["Token:Issure"],
//                expires: tokenDTO.Expiration, //life time
//                notBefore: DateTime.UtcNow, //islemeye baslayacagi vaxt
//                signingCredentials: signingCredentials,
//                claims: claims
//                );

//            //Token Yaradiriq
//            JwtSecurityTokenHandler tokenHandler = new();
//            //todo burda creat mehodun isledib sonra bunu isletmisik jwtAutoAndLogger dersimizde
//            tokenDTO.AccessToken = tokenHandler.WriteToken(securityToken);

//            //refresh token yaradib veririk
//            tokenDTO.RefreshToken = CreateRefreshToken();

//            return tokenDTO;
//        }



//        public string CreateRefreshToken()
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(configuration["Token:RefreshTokenSecret"]); // Refresh token için gizli sifre
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            };

//            var refreshToken = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(refreshToken);
//        }

//        public string RefreshToken()
//        {
//            //xususi bir clase ile random edelerle RFT yaradiriq
//            //todo burda bu cur edeler yaradiriq deye bunu contollerde query kimi veremmerik + falan yaranacaq ve http qebul elemir olari deyisir
//            byte[] number = new byte[32];
//            using RandomNumberGenerator randomNumber = RandomNumberGenerator.Create();
//            randomNumber.GetBytes(number);
//            return Convert.ToBase64String(number);
//        }
//    }
//}
