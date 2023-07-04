namespace ShradhaGeneralBookStores.Models;

public class AccountAPI
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

    public List<int>? roleId { get; set; }
}
