using System.ComponentModel.DataAnnotations;

namespace Contracts.DTOs.booking
{
    public class CreateBookingModificationRequest
    {
        [Required]
        [DataType(DataType.Date)]
        public DateOnly? NewStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly? NewEndDate { get; set; }
    }
}
