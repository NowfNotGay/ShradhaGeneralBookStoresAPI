using System;
using System.Collections.Generic;

namespace ShradhaGeneralBookStores.Models;

public partial class Order
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public decimal TotalPrice { get; set; }

    public int StatusId { get; set; }

    public int AddressId { get; set; }

    public int VoucherId { get; set; }

    public int PaymentMethodId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Account? Account { get; set; } = null!;

    public virtual AddressProfile? Address { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual PaymentMethod? PaymentMethod { get; set; } = null!;

    public virtual OrderStatus? Status { get; set; } = null!;

    public virtual Voucher? Voucher { get; set; } = null!;
}
