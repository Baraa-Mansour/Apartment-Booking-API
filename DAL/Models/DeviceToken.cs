using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class DeviceToken
{
    public int DeviceTokenId { get; set; }

    public int UserId { get; set; }

    public string FcmToken { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
