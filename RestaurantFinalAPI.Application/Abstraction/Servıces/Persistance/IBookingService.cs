using RestaurantFinalAPI.Application.DTOs.BookingDTOs;
using RestaurantFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance
{
    public interface IBookingService
    {
        Task<Response<BookingCreateDTO>> AddBooking(BookingCreateDTO model);
        Task<Response<bool>> DeleteBooking(string Id);
        Task<Response<bool>> UpdateBooking(BookingUpdateDTO model);

        Task<Response<List<BookingGetDTO>>> GetAllBooking();
        Task<Response<BookingGetDTO>> GetBookingById(string Id);
        Task<Response<List<BookingGetDTO>>> GetAllBookingByUserId(string Id);

        Task<Response<bool>> AcceptReservasion(string Id);
        Task<Response<bool>> RejectReservasion(string Id);




    }
}
