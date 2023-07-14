using ShradhaGeneralBookStores.Models;

namespace ShradhaGeneralBookStores.Service.Interface;

public interface IProductService
{
    public int AddProduct(ProductAPI product);
    public dynamic Read();
    public dynamic ReadForAuthor(int idAuthor);
    public dynamic ReadForCategory(int categoryId);
    public dynamic ReadForCategoryUser(int categoryId);
    public dynamic ReadForPublisher(int publisherId);
    public dynamic GetById(int id);
    public dynamic GetByIdUser(int id);
    public dynamic ReadByPrice(int min,int max);
    public dynamic ReadForUser();
    public int UpdateProduct(ProductAPI product);
}
