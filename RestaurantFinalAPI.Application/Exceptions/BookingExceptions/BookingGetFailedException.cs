using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Exceptions.BookingExceptions
{
    public class BookingGetFailedException : GenericCustomException<ExceptionDTO>
    {
        public BookingGetFailedException(ExceptionDTO customData)
       : base(customData)
        { }

        public BookingGetFailedException(string Id)
        : base(CreateExceptionData(Id))
        { }

        public BookingGetFailedException()
       : base(CreateExceptionData())
        { }


        private static ExceptionDTO CreateExceptionData(string Id)
        {
            return new ExceptionDTO
            {
                Message = $"An error occurred while searching for the booking with the specified ID: {Id}"
            };
        }
        private static ExceptionDTO CreateExceptionData()
        {
            return new ExceptionDTO
            {
                Message = "An error occurred while searching for the booking"
            };
        }

    }
}
