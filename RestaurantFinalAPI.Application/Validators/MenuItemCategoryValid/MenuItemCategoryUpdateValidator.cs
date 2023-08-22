using FluentValidation;
using RestaurantFinalAPI.Application.DTOs.MenuItemCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Validators.MenuItemCategoryValid
{
    public class MenuItemCategoryUpdateValidator :AbstractValidator<MenuItemCategoryUpdateDTO>
    {
        public MenuItemCategoryUpdateValidator()
        {
            RuleFor(dto => dto.CategoryName)
               .NotEmpty().WithMessage("CategoryName is required.")
               .MinimumLength(2).WithMessage("CategoryName must be at least 2 characters.")
               .MaximumLength(50).WithMessage("CategoryName cannot exceed 50 characters.")
               .Must(BeNullOrWhiteSpace).WithMessage("Category Name cannot both be empty or Whith Space."); ;

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.")
                .Must(BeNullOrWhiteSpace).WithMessage("Description Name cannot both be empty or Whith Space.");;

            RuleFor(dto => dto.MenuItemCategoryId)
                .NotNull().NotEmpty().WithMessage("Menu Item Category ID is required.")
                .Must(BeValidGuid).WithMessage("Menu Item Category ID must be a valid GUID.");
        }

        private bool BeValidGuid(string Id)
        {
            return Guid.TryParse(Id, out _);
        }

        public bool BeNullOrWhiteSpace(string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }
    }
}
