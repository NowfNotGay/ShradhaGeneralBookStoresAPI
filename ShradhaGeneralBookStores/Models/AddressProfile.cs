using System;
using System.Collections.Generic;

namespace ShradhaGeneralBookStores.Models;

public partial class AddressProfile
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string Street { get; set; } = null!;

    public string District { get; set; } = null!;

    public string City { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Account? Account { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
