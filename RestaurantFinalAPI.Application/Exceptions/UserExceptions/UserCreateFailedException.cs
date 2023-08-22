using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Exceptions.UserExceptions
{
    public class UserCreateFailedException : GenericCustomException<ExceptionDTO>
    {


        public UserCreateFailedException(ExceptionDTO customData)
        : base(customData)
        { }

        public UserCreateFailedException(string username)
        : base(CreateExceptionData(username))
        { }


        private static ExceptionDTO CreateExceptionData(string username)
        {
            return new ExceptionDTO //todo mesaji deyis
            {
                Message = $"Kullanıcı adı bulunamadı: {username}"
            };
        }
        
    }



}
