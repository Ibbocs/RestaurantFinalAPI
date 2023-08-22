using FluentValidation;
using RestaurantFinalAPI.Application.DTOs.BookingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Validators.BookingValid
{
    public class BookingUpdateDTOValidator:AbstractValidator<BookingUpdateDTO>
    {
        public BookingUpdateDTOValidator()
        {
            RuleFor(dto => dto.StartDate)
                .NotNull().NotEmpty().WithMessage("Start date is required.")
                .GreaterThan(DateTime.Now).WithMessage("Start date must be in the future.")
                .Must(BeAValidDate).WithMessage("Invalid start date format.")
                .Must(BeWorkingHours).WithMessage("Booking start time must be during working hours (9 AM - 10 PM).");

            RuleFor(dto => dto.BookingId)
                .NotNull().NotEmpty().WithMessage("Booking ID is required.")
                .Must(BeValidGuid).WithMessage("Booking ID must be a valid GUID."); //guid yoxlayiram deye uzunluga ehtiyac yoxdu

            RuleFor(dto => dto.TableId)
                .NotNull().NotEmpty().WithMessage("Table ID is required.")
                .Must(BeValidGuid).WithMessage("Table ID must be a valid GUID.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date != DateTime.MinValue;
        }

        private bool BeWorkingHours(DateTime date)
        {
            var workingStartTime = new TimeSpan(9, 0, 0);
            var workingEndTime = new TimeSpan(22, 0, 0);
            var time = date.TimeOfDay;
            return time >= workingStartTime && time <= workingEndTime;
        }

        private bool BeValidGuid(string Id)
        {
            return Guid.TryParse(Id, out _); //discard ile deyer donmurem
        }

    }
}
