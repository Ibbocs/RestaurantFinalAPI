using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using RestaurantFinalAPI.Application.DTOs.TableDTOs;

namespace RestaurantFinalAPI.Application.Validators.TableValid
{
    public class TableUpdateDTOValidator : AbstractValidator<TableUpdateDTO>
    {
        public TableUpdateDTOValidator()
        {
            RuleFor(dto => dto.TableId)
                .NotNull().NotEmpty().WithMessage("Table Id is required.");

            RuleFor(dto => dto.TableName)
                .NotEmpty().WithMessage("Table name is required.")
                .MaximumLength(50).WithMessage("Table name can be at most 50 characters long.");

            RuleFor(dto => dto.Capacity)
                .NotNull().NotEmpty().WithMessage("Table capacity is required.")
                .GreaterThan(0).WithMessage("Capacity must be greater than zero.");

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithMessage("Description can be at most 200 characters long.")
                .When(dto => !string.IsNullOrWhiteSpace(dto.Description));

            RuleFor(dto => dto)
                .Must(HaveValidTableNameAndDescription).WithMessage("Table name and description cannot both be empty.");
        }

        private bool HaveValidTableNameAndDescription(TableUpdateDTO dto)
        {
            return !string.IsNullOrWhiteSpace(dto.TableName) || !string.IsNullOrWhiteSpace(dto.Description);
        }
    }

}
