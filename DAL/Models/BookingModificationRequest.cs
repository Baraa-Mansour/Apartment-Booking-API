using Contracts.Enums;
using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class BookingModificationRequest
{
    public int RequestId { get; set; }

    public int BookingId { get; set; }

    public DateOnly NewStartDate { get; set; }

    public DateOnly NewEndDate { get; set; }

    public decimal NewTotalPrice { get; set; }

    public BookingModificationRequestStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
