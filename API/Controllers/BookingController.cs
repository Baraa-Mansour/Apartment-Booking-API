using API.Common;
using BLL.Interfaces;
using Contracts.DTOs;
using Contracts.DTOs.booking;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    [Tags("Bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>Gets a tenant's paginated booking history.</summary>
        /// <param name="filterParams">Pagination and booking filtering options.</param>
        /// <response code="200">A paginated booking history was returned.</response>
        /// <response code="400">The query parameters are invalid.</response>
        [HttpGet("tenant/history")]
        [ProducesResponseType(typeof(ApiResponse<PagedResponse<TenantBookingHistoryItem>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<PagedResponse<TenantBookingHistoryItem>>>> GetTenantHistory(
            [FromQuery] TenantBookingHistoryParams filterParams)
        {
            int tenantId = 7; // TEST!! : Need to be deleted

            (List<TenantBookingHistoryItem> Items, int Count) items_total =
                await _bookingService.GetTenantHistoryAsync(tenantId, filterParams);

            var response = new PagedResponse<TenantBookingHistoryItem>
            {
                Items = items_total.Items,
                TotalCount = items_total.Count,
                Page = filterParams.Page,
                PageSize = filterParams.PageSize
            };

            return Ok(ApiResponse<PagedResponse<TenantBookingHistoryItem>>.Ok(response));
        }

        /// <summary>Creates a booking for the tenant.</summary>
        /// <param name="createRequest">The booking details.</param>
        /// <response code="204">The booking was created successfully.</response>
        /// <response code="400">The request is invalid.</response>
        /// <response code="404">The apartment or tenant was not found.</response>
        /// <response code="409">The apartment is unavailable for the requested dates.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateBooking([FromBody] CreateBookingRequest createRequest)
        {
            int userId = 7; // TEST!! : Need to be deleted

            await _bookingService.CreateBookingAsync(userId, createRequest);

            return NoContent();
        }

        /// <summary>Cancels a booking owned by the tenant.</summary>
        /// <param name="bookingId">The ID of the booking to cancel.</param>
        /// <response code="204">The booking was cancelled successfully.</response>
        /// <response code="403">The tenant cannot cancel this booking.</response>
        /// <response code="404">The booking was not found.</response>
        /// <response code="409">The booking cannot be cancelled in its current state.</response>
        [HttpPatch("{bookingId:int}/cancel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CancelBooking([FromRoute] int bookingId)
        {
            int userId = 7;

            await _bookingService.CancelBookingAsync(userId, bookingId);

            return NoContent();
        }
    }
}
