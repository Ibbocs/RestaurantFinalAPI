using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance;
using RestaurantFinalAPI.Application.DTOs.BookingDTOs;
using RestaurantFinalAPI.Persistance.Implementation.Services;
using System.Data;

namespace RestaurantFinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService _bookingService)
        {
            this.bookingService = _bookingService;
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> GetAllBooking()
        {
            var data = await bookingService.GetAllBooking();
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> GetBooking(string Id)
        {
            var data = await bookingService.GetBookingById(Id);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin,User")]
        public async Task<IActionResult> GetBookingByUserId(string Id)
        {
            var data = await bookingService.GetAllBookingByUserId(Id);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin,User")]
        public async Task<IActionResult> CreateBooking(BookingCreateDTO model)
        {
            var data = await bookingService.AddBooking(model);
            return StatusCode(data.StatusCode, data);

        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> RejectBooking(string id)
        {
            var data = await bookingService.RejectReservasion(id);
            return StatusCode(data.StatusCode, data);

        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> AcceptBooking(string id)
        {
            var data = await bookingService.AcceptReservasion(id);
            return StatusCode(data.StatusCode, data);

        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "User")]
        public async Task<IActionResult> UpdateBooking(BookingUpdateDTO model)
        {
            var data = await bookingService.UpdateBooking(model);
            return StatusCode(data.StatusCode, data);
        }

        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "User")]
        public async Task<IActionResult> DeleteBooking(string Id)
        {
            var data = await bookingService.DeleteBooking(Id);
            return StatusCode(data.StatusCode, data);
        }
    }
}
