using BLL.Interfaces;
using Contracts.DTOs;
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

        [HttpGet("tenant/history")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<TenantBookingHistoryItem>))]
        public async Task<ActionResult<PagedResponse<TenantBookingHistoryItem>>> GetTenantHistory(
            [FromQuery] TenantBookingHistoryParams filterParams)
        {
            int tenantId = 7; // TEST!! : Need to be deleted

            (List<TenantBookingHistoryItem> Items, int Count) items_total =
                await _bookingService.GetTenantHistoryAsync(tenantId, filterParams);

            return Ok(new PagedResponse<TenantBookingHistoryItem>
            {
                Items = items_total.Items,
                TotalCount = items_total.Count,
                Page = filterParams.Page,
                PageSize = filterParams.PageSize
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CreateBooking([FromBody]CreateBookingRequest createRequest)
        {
            int userId = 7; // TEST!! : Need to be deleted

            await _bookingService.CreateBookingAsync(userId, createRequest);

            return NoContent();
        }

        [HttpPatch("{bookingId:int}/cancel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult> CancelBooking(int bookingId)
        {
            int usereId = 7;

            await _bookingService.CancelBookingAsync(usereId, bookingId);

            return NoContent();
        }
    }
}
