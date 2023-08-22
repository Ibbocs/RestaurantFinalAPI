using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance.IUserServices;
using RestaurantFinalAPI.Application.DTOs.UserDTOs;
using RestaurantFinalAPI.Application.Exceptions;
using RestaurantFinalAPI.Application.Exceptions.UserExceptions;
using RestaurantFinalAPI.Application.Models.ResponseModels;
using RestaurantFinalAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Implementation.Services.UserServices
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> userManager;
        readonly IMapper _mapper;

        public UserService(UserManager<AppUser> _userManager, IMapper mapper)
        {
            userManager = _userManager;
            _mapper = mapper;
        }

        //user yaratma
        public async Task<Response<CreateUserResponseDTO>> CreateAsync(CreateUserDto model)
        {

            var id = Guid.NewGuid().ToString();
            IdentityResult result = await userManager.CreateAsync(new()
            {
                Id = id,
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            }, model.Password);


            //CreateUserResponseDTO response = new() { Succeeded = result.Succeeded };

            var response = new Response<CreateUserResponseDTO>
            {
                Data = new CreateUserResponseDTO { Succeeded = result.Succeeded },
                StatusCode = result.Succeeded ? 200 : 400
            };

            if (!result.Succeeded)
            {
                response.Data.Message = string.Join(" \n ", result.Errors.Select(error => $"{error.Code} - {error.Description}"));
            }

            AppUser user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
                user = await userManager.FindByEmailAsync(id);
            if (user != null)
                await userManager.AddToRoleAsync(user, "User");

            return response;

            //throw new UserCreateFaileedException();
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int refreshTokenMoreLifeMinute)
        {
            //bu method rft update elemek ucunnde conntrollerlerde de vere bilerem amma vermmeisem
            //AppUser user = await userManager.FindByIdAsync(user.Id); //todo app user cekilede biler cekilmeyede ehtiyaca gore
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndTime = accessTokenDate.AddMinutes(refreshTokenMoreLifeMinute);
                await userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();

            //throw new UpdateUserException(updateResult.Errors);
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            //AppUser user = await userManager.FindByIdAsync(userId);
            //if (user != null)
            //{
            //    //resetToken = resetToken.UrlDecode();
            //    byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
            //    resetToken = Encoding.UTF8.GetString(tokenBytes);
            //    IdentityResult result = await userManager.ResetPasswordAsync(user, resetToken, newPassword);
            //    string a = "dcd";
            //    if (result.Succeeded)
            //        await userManager.UpdateSecurityStampAsync(user);
            //    else
            //        throw new SomeThingsWrongException();
            //}

            AppUser user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
                resetToken = Encoding.UTF8.GetString(tokenBytes);

                //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
                //resetToken = Encoding.UTF8.GetString(tokenBytes);

                IdentityResult result = await userManager.ResetPasswordAsync(user, resetToken, newPassword);

                if (result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                }
                else
                {
                    throw new SomeThingsWrongException();
                }
            }
            else
            {
                throw new NotFoundUserException();
            }
        }

        public async Task<Response<List<UserGetDTO>>> GetAllUsersAsync()
        {
            var users = await userManager.Users.ToListAsync();

            try
            {
                if (users != null && users.Count > 0)
                {
                    var data = _mapper.Map<List<UserGetDTO>>(users);

                    return new Response<List<UserGetDTO>>
                    {
                        Data = data,
                        StatusCode = 200,
                    };
                }
                else
                {
                    return new Response<List<UserGetDTO>>
                    {
                        Data = null,
                        StatusCode = 400,
                    };
                }
            }
            catch (Exception ex) { throw new SomeThingsWrongException(ex.Message, ex.InnerException); }
        }

        public async Task<Response<bool>> AssignRoleToUserAsnyc(string userId, string[] roles)
        {
            AppUser user = await userManager.FindByIdAsync(userId);

            try
            {
                if (user != null)
                {
                    var userRoles = await userManager.GetRolesAsync(user);
                    await userManager.RemoveFromRolesAsync(user, userRoles);
                    await userManager.AddToRolesAsync(user, roles);

                    return new()
                    {
                        Data = true,
                        StatusCode = 200
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
            }
            catch (Exception ex) { throw new SomeThingsWrongException(ex.Message, ex.InnerException); }
        }

        public async Task<Response<string[]>> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser user = await userManager.FindByIdAsync(userIdOrName);
            if (user == null)
                user = await userManager.FindByNameAsync(userIdOrName);
            try
            {
                if (user != null)
                {
                    var userRoles = await userManager.GetRolesAsync(user);
                    return new()
                    {
                        Data = userRoles.ToArray(),
                        StatusCode = 200
                    };
                }
                return new()
                {
                    Data = null,
                    StatusCode = 400
                };
            }
            catch (Exception ex )
            {

                throw new SomeThingsWrongException(ex.Message, ex.InnerException);
            }
        }

        public async Task<Response<bool>> DeleteUserAsync(string userIdOrName)
        {
            AppUser user = await userManager.FindByIdAsync(userIdOrName);
            if (user == null)
                user = await userManager.FindByNameAsync(userIdOrName);

            if (user == null)
             throw new NotFoundUserException();

            try
            {
                var data = await userManager.DeleteAsync(user);
                if (data.Succeeded)
                {
                    return new()
                    {
                        Data = true,
                        StatusCode = 200
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
            }
            catch (Exception ex)
            {
                throw new SomeThingsWrongException(ex.Message, ex.InnerException);
            }
        }

        public async Task<Response<bool>> UpdateUserAsync(UserUpdateDTO model)
        {
            AppUser user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
                user = await userManager.FindByNameAsync(model.UserName); //name update elese bele id verecek mecbur onda

            if (user == null)
                throw new NotFoundUserException();

            try
            {
                user.UserName = model.UserName;
                user.BirthDate = model.BirthDate;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                var data = await userManager.UpdateAsync(user);

                if (data.Succeeded)
                {
                    return new()
                    {
                        Data = true,
                        StatusCode = 200
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
            }
            catch (Exception ex)
            {
                throw new SomeThingsWrongException(ex.Message, ex.InnerException);
            }
        }
    }
}
