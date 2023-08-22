using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Exceptions.MenuItemCategoryExceptions
{
    public class MenuItemCategoryGetFailedException : GenericCustomException<ExceptionDTO>
    {
        public MenuItemCategoryGetFailedException(ExceptionDTO customData)
       : base(customData)
        { }

        public MenuItemCategoryGetFailedException(string Id)
        : base(CreateExceptionData(Id))
        { }

        public MenuItemCategoryGetFailedException()
       : base(CreateExceptionData())
        { }


        private static ExceptionDTO CreateExceptionData(string Id)
        {
            return new ExceptionDTO
            {
                Message = $"An error occurred while searching for the Menu Item Category with the specified ID: {Id}"
            };
        }
        private static ExceptionDTO CreateExceptionData()
        {
            return new ExceptionDTO
            {
                Message = "An error occurred while searching for the Menu Item  Category"
            };
        }

    }
}
