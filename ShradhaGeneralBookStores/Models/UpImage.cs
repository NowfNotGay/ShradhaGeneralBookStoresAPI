namespace ShradhaGeneralBookStores.Models;

public class UpImage
{
    public int? productId { get; set; }
    public List<IFormFile> Images { get; set; } = new();
}
