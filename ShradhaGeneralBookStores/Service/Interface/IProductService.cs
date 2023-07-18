using ShradhaGeneralBookStores.Models;

namespace ShradhaGeneralBookStores.Service.Interface;

public interface IProductService
{
    #region crud for admin
    public int AddProduct(ProductAPI product);
    public int UpdateProduct(ProductAPI product);
    public bool DeleteProduct(int id);
    public bool EnableProduct(int id);
    #endregion


    #region read for admin
    public dynamic Read();
    public dynamic ReadForAuthor(int idAuthor);
    public dynamic ReadForCategory(int categoryId);
    public dynamic ReadForPublisher(int publisherId);
    public dynamic GetById(int id);
    public dynamic ReadDisable();
    #endregion

    #region read for user
    public dynamic ReadForCategoryUser(int categoryId);
    public dynamic GetByIdUser(int id);
    public dynamic ReadByPrice(int min,int max);
    public dynamic ReadForUser();
    public dynamic ReadForPublisherUser(int publisherId);
    public dynamic ReadForAuthorUser(int idAuthor);
    public dynamic ReadForHot();
    public dynamic ReadForSimilar(int id);
    #endregion

}
