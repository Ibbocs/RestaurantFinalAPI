using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Exceptions.TableExceptions
{
    public class IsReservedException : GenericCustomException<ExceptionDTO>
    {
        public IsReservedException(ExceptionDTO customData)
       : base(customData)
        { }

        public IsReservedException()
        : base(CreateExceptionData())
        { }


        private static ExceptionDTO CreateExceptionData()
        {
            return new ExceptionDTO
            {
                Message = "The operation cannot be performed because the booking is reserved."
            };
        }

    }
}
