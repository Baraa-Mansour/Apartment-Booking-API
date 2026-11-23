using API.Common;
using BLL.Interfaces;
using Contracts.DTOs.review;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
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
        /// <response code="404">The apartment or tenant was not found.</response>
        /// <response code="409">The review cannot be created in the current state.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateReview([FromBody] CreateReviewRequest request)
        {
            int tenantId = 7; // TEST!! : Needs to be replaced by the authenticated user

            await _reviewService.CreateReviewAsync(tenantId, request);

            return NoContent();
        }
    }
}
