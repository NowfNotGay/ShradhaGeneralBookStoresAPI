using System;
using System.Collections.Generic;

namespace ShradhaGeneralBookStores.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string? Avatar { get; set; }

    public bool? Status { get; set; }

    public string? SecurityCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<AccountRole> AccountRoles { get; set; } = new List<AccountRole>();

    public virtual ICollection<AddressProfile> AddressProfiles { get; set; } = new List<AddressProfile>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<VoucherAccount> VoucherAccounts { get; set; } = new List<VoucherAccount>();
}
