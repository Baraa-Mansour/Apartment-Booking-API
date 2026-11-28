using API.Common;
using BLL.Exceptions;
using BLL.Interfaces;
using Contracts.DTOs.review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    [Authorize(Roles = "Tenant")]
    [Tags("Reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>Creates a review for an apartment.</summary>
        /// <param name="request">The review details.</param>
        /// <response code="204">The review was created successfully.</response>
        /// <response code="400">The request is invalid.</response>
        /// <response code="403">The tenant cannot review this booking.</response>
        /// <response code="404">The booking or tenant was not found.</response>
        /// <response code="409">The booking has already been reviewed or cannot be reviewed.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateReview([FromBody] CreateReviewRequest request)
        {
            int tenantId = 7;

            await _reviewService.CreateReviewAsync(tenantId, request);

            return NoContent();
        }
    }
}
