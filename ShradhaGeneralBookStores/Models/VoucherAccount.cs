using System;
using System.Collections.Generic;

namespace ShradhaGeneralBookStores.Models;

public partial class VoucherAccount
{
    public int AccountId { get; set; }

    public int VoucherId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Voucher Voucher { get; set; } = null!;
}
