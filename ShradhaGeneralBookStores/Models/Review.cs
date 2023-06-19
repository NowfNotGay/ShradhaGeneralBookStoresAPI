using System;
using System.Collections.Generic;

namespace ShradhaGeneralBookStores.Models;

public partial class Review
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int ProductId { get; set; }

    public string? Content { get; set; }

    public byte Rating { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
