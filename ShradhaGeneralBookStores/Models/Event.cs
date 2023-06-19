using System;
using System.Collections.Generic;

namespace ShradhaGeneralBookStores.Models;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<EventDetail> EventDetails { get; set; } = new List<EventDetail>();
}
