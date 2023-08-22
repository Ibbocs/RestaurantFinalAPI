using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Exceptions.CommonExceptions
{
    
    public class InvalidIdFormatException : GenericCustomException<ExceptionDTO>
    {
        public InvalidIdFormatException(ExceptionDTO customData)
       : base(customData)
        { }

        public InvalidIdFormatException(string Id)
        : base(CreateExceptionData(Id))
        { }


        private static ExceptionDTO CreateExceptionData(string Id)
        {
            return new ExceptionDTO
            {
                Message = $"The provided ID format is invalid: {Id}"
            };
        }

    }
}
