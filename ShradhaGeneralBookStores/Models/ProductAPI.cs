namespace ShradhaGeneralBookStores.Models;

public class ProductAPI
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

    public List<int> AuthorsId { get; set; } = new List<int>();

    public List<int> CategoriesId { get; set; } = new List<int>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
