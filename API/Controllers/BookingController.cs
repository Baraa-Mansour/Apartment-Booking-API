using BLL.Interfaces;
using Contracts.DTOs.booking;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CreateBooking([FromBody]CreateBookingRequest createRequest)
        {
            int userId = 7; // TEST!! : Need to be deleted

            await _bookingService.CreateBookingAsync(userId, createRequest);

            return NoContent();
        }
    }
}
