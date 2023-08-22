using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Exceptions.TableExceptions
{
    public class TableGetFailedException : GenericCustomException<ExceptionDTO>
    {
        public TableGetFailedException(ExceptionDTO customData)
       : base(customData)
        { }

        public TableGetFailedException(string Id)
        : base(CreateExceptionData(Id))
        { }

        public TableGetFailedException()
       : base(CreateExceptionData())
        { }


        private static ExceptionDTO CreateExceptionData(string Id)
        {
            return new ExceptionDTO
            {
                Message = $"An error occurred while searching for the table with the specified ID: {Id}"
            };
        }
        private static ExceptionDTO CreateExceptionData()
        {
            return new ExceptionDTO
            {
                Message = "An error occurred while searching for the table"
            };
        }

    }
}
