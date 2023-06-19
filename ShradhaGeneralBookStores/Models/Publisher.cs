using System;
using System.Collections.Generic;

namespace ShradhaGeneralBookStores.Models;

public partial class Publisher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NameShort { get; set; } = null!;

    public string? Location { get; set; }

    public string? ContactNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
