using System.ComponentModel.DataAnnotations;

namespace Contracts.DTOs.review
{
    public class CreateReviewRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int? BookingId { get; set; }

        [Required]
        [Range(1, 5)]
        public byte Rating { get; set; }
    }
}
