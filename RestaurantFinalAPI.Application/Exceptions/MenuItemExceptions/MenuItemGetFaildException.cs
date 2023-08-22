using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Exceptions.MenuItemExceptions
{
    public class MenuItemGetFaildException : GenericCustomException<ExceptionDTO>
    {
        public MenuItemGetFaildException(ExceptionDTO customData)
       : base(customData)
        { }

        public MenuItemGetFaildException(string Id)
        : base(CreateExceptionData(Id))
        { }

        public MenuItemGetFaildException()
       : base(CreateExceptionData())
        { }


        private static ExceptionDTO CreateExceptionData(string Id)
        {
            return new ExceptionDTO
            {
                Message = $"An error occurred while searching for the Menu Item with the specified ID: {Id}"
            };
        }
        private static ExceptionDTO CreateExceptionData()
        {
            return new ExceptionDTO
            {
                Message = "An error occurred while searching for the Menu Item"
            };
        }

    }
}
