using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Exceptions.UserExceptions
{
    public class NotFoundUserException : GenericCustomException<ExceptionDTO>
    {


        public NotFoundUserException(ExceptionDTO customData)
        : base(customData)
        { }

        public NotFoundUserException()
        : base(CreateExceptionData())
        { }


        private static ExceptionDTO CreateExceptionData()
        {
            return new ExceptionDTO //todo mesaji deyis
            {
                Message = "Username, Email Or Password Incorrect"
            };
        }
    }
}
