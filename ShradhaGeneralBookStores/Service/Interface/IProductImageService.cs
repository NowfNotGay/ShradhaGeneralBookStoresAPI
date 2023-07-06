namespace ShradhaGeneralBookStores.Service.Interface;

public interface IProductImageService
{
    public bool Add(int productId, IFormFile[] photo);
}
