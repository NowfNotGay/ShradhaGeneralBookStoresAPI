using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class ProductService : IProductService
{
    private readonly DatabaseContext _databaseContext;

    public ProductService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public int AddProduct(ProductAPI product)
    {
        try
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                #region phân loại product
                var productadd = new Product()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    Cost = product.Cost,
                    Status = product.Status,
                    Hot = product.Hot,
                    PublisherId = product.PublisherId,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt
                };
                _databaseContext.Products.Add(productadd);
                _databaseContext.SaveChanges();
                #endregion

                #region phân loại bảng n - n (category - product)
                product.CategoriesId.ForEach(categoryId =>
                {
                    var productcategory = new ProductCategory()
                    {
                        CategoryId = categoryId,
                        ProductId = productadd.Id,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _databaseContext.ProductCategories.Add(productcategory);
                });
                _databaseContext.SaveChanges();
                #endregion

                #region phân loại bảng n - n (author - product)
                product.AuthorsId.ForEach( authorId =>
                {
                    var productAuthor = new ProductAuthor()
                    {
                        AuthorId = authorId,
                        ProductId = productadd.Id,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _databaseContext.ProductAuthors.Add(productAuthor);
                });
                _databaseContext.SaveChanges();
                #endregion

                //làm xong trả về id Product
                transaction.Commit();
                return productadd.Id;
            }
        }
        catch(Exception)
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                transaction.Rollback();
            }
            // lỗi trả về -1 
            return -1;
        }
    }
}
