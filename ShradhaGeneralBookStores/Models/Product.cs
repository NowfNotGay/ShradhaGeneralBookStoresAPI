using System;
using System.Collections.Generic;

namespace ShradhaGeneralBookStores.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Quantity { get; set; }

    public int Price { get; set; }

    public int Cost { get; set; }

    public bool Status { get; set; }
    public bool Hot { get; set; }

    public int? PublisherId { get; set; }

    public string? PublishingYear { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<EventDetail> EventDetails { get; set; } = new List<EventDetail>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual Publisher? Publisher { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
