using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Conversation
{
    public int ConversationId { get; set; }

    public int ApartmentId { get; set; }

    public int TenantId { get; set; }

    public int OwnerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Apartment Apartment { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual User Owner { get; set; } = null!;

    public virtual User Tenant { get; set; } = null!;
}
