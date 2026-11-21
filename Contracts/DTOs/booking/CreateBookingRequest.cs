using System.ComponentModel.DataAnnotations;

namespace Contracts.DTOs.booking
{
    public class CreateBookingRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int? ApartmentId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly? StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly? EndDate { get; set; }
    }
}
