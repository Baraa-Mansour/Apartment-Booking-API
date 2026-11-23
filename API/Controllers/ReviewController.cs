using BLL.Interfaces;
using Contracts.DTOs.review;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CreateReview([FromBody] CreateReviewRequest request)
        {
            int tenantId = 7; // TEST!! : Needs to be replaced by the authenticated user

            await _reviewService.CreateReviewAsync(tenantId, request);

            return NoContent();
        }
    }
}
