using System;
using System.Collections.Generic;

namespace ShradhaGeneralBookStores.Models;

public partial class Voucher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string VarityCode { get; set; } = null!;

    public int Discount { get; set; }

    public int Condition { get; set; }

    public int Quantity { get; set; }

    public DateTime TimeStart { get; set; }

    public DateTime TimeEnd { get; set; }

    public bool? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<VoucherAccount> VoucherAccounts { get; set; } = new List<VoucherAccount>();
}
