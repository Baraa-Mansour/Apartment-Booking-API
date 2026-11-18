using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Message
{
    public long MessageId { get; set; }

    public int ConversationId { get; set; }

    public int SenderId { get; set; }

    public string Body { get; set; } = null!;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
