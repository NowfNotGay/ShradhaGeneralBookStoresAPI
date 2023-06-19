using System;
using System.Collections.Generic;

namespace ShradhaGeneralBookStores.Models;

public partial class Author
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Biography { get; set; }

    public string? YearOfBirth { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
