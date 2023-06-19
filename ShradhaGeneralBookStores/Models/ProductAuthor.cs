using System;
using System.Collections.Generic;

namespace ShradhaGeneralBookStores.Models;

public partial class ProductAuthor
{
    public int ProductId { get; set; }

    public int AuthorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
