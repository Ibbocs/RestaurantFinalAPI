using FluentValidation;
using RestaurantFinalAPI.Application.DTOs.MenuItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Validators.MenuItemValid
{
    public class MenuItemCreateValidator :AbstractValidator<MenuItemCreateDTO>
    {
        public MenuItemCreateValidator()
        {
            RuleFor(dto => dto.MenuItemName)
                .NotEmpty().WithMessage("Menu Item Name cannot be empty.")
                .MinimumLength(2).WithMessage("Menu Item Name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("Menu Item Name cannot exceed 50 characters.")
                .Must(BeNullOrWhiteSpace).WithMessage("Menu Item Name cannot both be empty or Whith Space."); ;

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.")
                .Must(BeNullOrWhiteSpace).WithMessage("Description cannot both be empty or Whith Space.");

            RuleFor(dto => dto.Price)
                .NotNull().NotEmpty().WithMessage("Price cannot be empty.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(dto => dto.MenuItemCategoryId)
                .NotNull().NotEmpty().WithMessage("Menu Item Category Id cannot be empty.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Invalid Menu Item Category Id format.");
        }

        public bool BeNullOrWhiteSpace(string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }

        //public bool BeNullOrWhiteSpace(string text)
        //{
        //    return !string.IsNullOrEmpty(text) || !string.IsNullOrWhiteSpace(text);
        //}
    }
}
