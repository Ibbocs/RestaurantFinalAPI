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

        /// <summary>
        /// Gets all bookings.
        /// </summary>
        /// <remarks>
        /// URL: GET /api/Booking/GetAllBooking
        /// </remarks>
        /// <returns>List of all bookings</returns>
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> GetAllBooking()
        {
            var data = await bookingService.GetAllBooking();
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Gets a specific booking by ID.
        /// </summary>
        /// <param name="Id">ID of the booking</param>
        /// <remarks>
        /// URL: GET /api/Booking/GetBooking/{Id}
        /// </remarks>
        /// <returns>Booking details</returns>
        [HttpGet("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> GetBooking(string Id)
        {
            var data = await bookingService.GetBookingById(Id);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Gets all bookings of a specific user.
        /// </summary>
        /// <param name="Id">ID of the user</param>
        /// <remarks>
        /// URL: GET /api/Booking/GetBookingByUserId/{Id}
        /// </remarks>
        /// <returns>List of user's bookings</returns>
        [HttpGet("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin,User")]
        public async Task<IActionResult> GetBookingByUserId(string Id)
        {
            var data = await bookingService.GetAllBookingByUserId(Id);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Creates a new booking.
        /// </summary>
        /// <param name="model">Booking information</param>
        /// <remarks>
        /// URL: POST /api/Booking/CreateBooking
        /// </remarks>
        /// <returns>Result of booking creation</returns>
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin,User")]
        public async Task<IActionResult> CreateBooking(BookingCreateDTO model)
        {
            var data = await bookingService.AddBooking(model);
            return StatusCode(data.StatusCode, data);

        }

        /// <summary>
        /// Rejects or Deleting a booking with ID by Admin.
        /// </summary>
        /// <param name="id">ID of the booking to reject</param>
        /// <remarks>
        /// URL: PUT /api/Booking/RejectBooking
        /// </remarks>
        /// <returns>Result of booking rejection</returns>
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> RejectBooking(string id)
        {
            var data = await bookingService.RejectReservasion(id);
            return StatusCode(data.StatusCode, data);

        }

        /// <summary>
        /// Accepts a booking by ID.
        /// </summary>
        /// <param name="id">ID of the booking to accept</param>
        /// <remarks>
        /// URL: PUT /api/Booking/AcceptBooking
        /// </remarks>
        /// <returns>Result of booking acceptance</returns>
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> AcceptBooking(string id)
        {
            var data = await bookingService.AcceptReservasion(id);
            return StatusCode(data.StatusCode, data);

        }

        /// <summary>
        /// Updates an existing booking.
        /// </summary>
        /// <param name="model">Updated booking information</param>
        /// <remarks>
        /// URL: PUT /api/Booking/UpdateBooking
        /// </remarks>
        /// <returns>Result of booking update</returns>
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "User")]
        public async Task<IActionResult> UpdateBooking(BookingUpdateDTO model)
        {
            var data = await bookingService.UpdateBooking(model);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Deletes a booking.
        /// </summary>
        /// <param name="Id">ID of the booking to delete</param>
        /// <remarks>
        /// URL: DELETE /api/Booking/DeleteBooking/{Id}
        /// </remarks>
        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "User")]
        public async Task<IActionResult> DeleteBooking(string Id)
        {
            var data = await bookingService.DeleteBooking(Id);
            return StatusCode(data.StatusCode, data);
        }
    }
}
