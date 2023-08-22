using FluentValidation;
using RestaurantFinalAPI.Application.DTOs.TableDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Validators.TableValid
{
    public class TableCreateDTOValidator: AbstractValidator<TableCreateDTO>
    {
        public TableCreateDTOValidator()
        {
            RuleFor(dto => dto.TableName)
               .NotEmpty().WithMessage("Table name is required.")
               .MaximumLength(50).WithMessage("Table name can be at most 50 characters long.")
               .Must(BeNullOrWhiteSpace).WithMessage("Description cannot both be empty or Whith Space.");

            RuleFor(dto => dto.Capacity)
                .NotNull().NotEmpty().WithMessage("Table capacity is required.")
                .GreaterThan(0).WithMessage("Capacity must be greater than zero.");

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithMessage("Description can be at most 200 characters long.")
                .When(dto => !string.IsNullOrWhiteSpace(dto.Description)); //bele de null ve ya whiteSpace yoxlamaq olar
        }

        public bool BeNullOrWhiteSpace(string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }
    }
}
