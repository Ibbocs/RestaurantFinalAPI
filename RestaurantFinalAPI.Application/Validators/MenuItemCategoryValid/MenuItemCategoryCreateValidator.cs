using FluentValidation;
using RestaurantFinalAPI.Application.DTOs.MenuItemCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Validators.MenuItemCategoryValid
{
    public class MenuItemCategoryCreateValidator : AbstractValidator<MenuItemCategoryCreateDTO>
    {
        public MenuItemCategoryCreateValidator()
        {
            RuleFor(dto => dto.CategoryName)
                .NotEmpty().WithMessage("Category Name is required.")
                .MinimumLength(2).WithMessage("Category Name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("Category Name cannot exceed 50 characters.")
                .Must(BeNullOrWhiteSpace).WithMessage("Category Name cannot both be empty or Whith Space.");

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.")
                .Must(BeNullOrWhiteSpace).WithMessage("Description cannot both be empty or Whith Space.");
        }

        public bool BeNullOrWhiteSpace(string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }
    }
}
