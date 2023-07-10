namespace ShradhaGeneralBookStores.Models.ModelTemp;

public class ProductAPI2
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

    //get of Authors

    public List<string> Authors { get; set; } = new List<string>();

    public List<string> Categories { get; set; } = new List<string>();


    //get of imageProduct
    public string? Photo { get; set; }


    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
