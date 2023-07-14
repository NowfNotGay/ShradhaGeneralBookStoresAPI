using Microsoft.EntityFrameworkCore;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class ProductService : IProductService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IConfiguration _configuration;

    public ProductService(DatabaseContext databaseContext, IConfiguration configuration)
    {
        _databaseContext = databaseContext;
        _configuration = configuration;
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
                    PublishingYear = product.PublishingYear,
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

    public bool DeleteProduct(int id)
    {
        try
        {
            var product = _databaseContext.Products.Find(id);
            product.Status = false;
            _databaseContext.Products.Update(product);
            return _databaseContext.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public dynamic GetById(int id) => _databaseContext.Products
    .Include(p => p.ProductAuthors)
    .ThenInclude(pa => pa.Author)
    .Include(p => p.ProductCategories)
    .ThenInclude(pc => pc.Category)
    .Include(p => p.ProductImages)
    .Where(p=>p.Id == id)
    .Select(p => new
    {
        p.Id,
        p.Name,
        p.Description,
        p.Quantity,
        p.Price,
        p.Cost,
        p.PublisherId,
        p.Status,
        p.Hot,
        p.PublishingYear,
        p.CreatedAt,
        p.UpdatedAt,
        AuthorsId = p.ProductAuthors.Select(pa => pa.Author.Id),
        CategoriesId = p.ProductCategories.Select(pc => pc.Category.Id),
        Photos = p.ProductImages.Where(pi=>pi.ProductId == id).Select(pi=> _configuration["BaseURL"] + "Images/ProductImages/"+ pi.ImagePath)
    });

    public dynamic GetByIdUser(int id) => _databaseContext.Products
    .Include(p => p.ProductAuthors)
    .ThenInclude(pa => pa.Author)
    .Include(p => p.ProductCategories)
    .ThenInclude(pc => pc.Category)
    .Include(p => p.ProductImages)
    .Where(p => p.Id == id)
    .Select(p => new
    {
        p.Id,
        p.Name,
        p.Description,
        p.Quantity,
        p.Price,
        p.Cost,
        p.PublisherId,
        p.Status,
        p.Hot,
        p.PublishingYear,
        p.CreatedAt,
        p.UpdatedAt,
        Authors = p.ProductAuthors.Select(pa => pa.Author.Name),
        Categories = p.ProductCategories.Select(pc => pc.Category.Name),
        Photos = p.ProductImages.Where(pi => pi.ProductId == p.Id).Select(pi => _configuration["BaseURL"] + "Images/ProductImages/" + pi.ImagePath)
    });

    public dynamic Read() => _databaseContext.Products
    .Include(p => p.ProductAuthors)
    .ThenInclude(pa => pa.Author)
    .Include(p => p.ProductCategories)
    .ThenInclude(pc => pc.Category)
    .Include(p => p.ProductImages)
    .Where(p=>p.Status == true)
    .Select(p => new
    {
        p.Id,
        p.Name,
        p.Description,
        p.Quantity,
        p.Price,
        p.Cost,
        p.PublisherId,
        p.Status,
        p.Hot,
        p.PublishingYear,
        p.CreatedAt,
        p.UpdatedAt,
        Authors = p.ProductAuthors.Select(pa=>pa.Author.Name),
        Categories = p.ProductCategories.Select(pc => pc.Category.Name),
        Photo = _configuration["BaseURL"] + "Images/ProductImages/"+ p.ProductImages.First().ImagePath,
    });
    public dynamic ReadDisable() => _databaseContext.Products
        .Include(p => p.ProductAuthors)
        .ThenInclude(pa => pa.Author)
        .Include(p => p.ProductCategories)
        .ThenInclude(pc => pc.Category)
        .Include(p => p.ProductImages)
        .Where(p => p.Status == false)
        .Select(p => new
        {
            p.Id,
            p.Name,
            p.Description,
            p.Quantity,
            p.Price,
            p.Cost,
            p.PublisherId,
            p.Status,
            p.Hot,
            p.PublishingYear,
            p.CreatedAt,
            p.UpdatedAt,
            Authors = p.ProductAuthors.Select(pa => pa.Author.Name),
            Categories = p.ProductCategories.Select(pc => pc.Category.Name),
            Photo = _configuration["BaseURL"] + "Images/ProductImages/" + p.ProductImages.First().ImagePath,
        });

    public dynamic ReadByPrice(int min, int max) => _databaseContext.Products
            .Include(p => p.ProductAuthors)
            .ThenInclude(pa => pa.Author)
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.ProductImages)
            .Where(p => p.Cost >= min && p.Cost <= max)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.Quantity,
                p.Price,
                p.Cost,
                p.PublisherId,
                p.Status,
                p.Hot,
                p.PublishingYear,
                p.CreatedAt,
                p.UpdatedAt,
                Authors = p.ProductAuthors.Select(pa => pa.Author.Name),
                Categories = p.ProductCategories.Select(pc => pc.Category.Name),
                Photos = p.ProductImages.Where(pi => pi.ProductId == p.Id).Select(pi => _configuration["BaseURL"] + "Images/ProductImages/" + pi.ImagePath)
            });

    public dynamic ReadForAuthor(int idAuthor) => _databaseContext.Products
            .Include(p => p.ProductAuthors)
            .ThenInclude(pa => pa.Author)
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.ProductImages)
            .Where(p => p.ProductAuthors.Any(pa => pa.AuthorId == idAuthor))
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.Quantity,
                p.Price,
                p.Cost,
                p.PublisherId,
                p.Status,
                p.Hot,
                p.PublishingYear,
                p.CreatedAt,
                p.UpdatedAt,
                Authors = p.ProductAuthors.Select(pa => pa.Author.Name),
                Categories = p.ProductCategories.Select(pc => pc.Category.Name),
                Photo = _configuration["BaseURL"] + "Images/ProductImages/" + p.ProductImages.First().ImagePath,
            });
    public dynamic ReadForAuthorUser(int idAuthor) => _databaseContext.Products
            .Include(p => p.ProductAuthors)
            .ThenInclude(pa => pa.Author)
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.ProductImages)
            .Where(p => p.ProductAuthors.Any(pa => pa.AuthorId == idAuthor))
            .Select(p => new
             {
                 p.Id,
                 p.Name,
                 p.Description,
                 p.Quantity,
                 p.Price,
                 p.Cost,
                 p.PublisherId,
                 p.Status,
                 p.Hot,
                 p.PublishingYear,
                 p.CreatedAt,
                 p.UpdatedAt,
                 Authors = p.ProductAuthors.Select(pa => pa.Author.Name),
                 Categories = p.ProductCategories.Select(pc => pc.Category.Name),
                 Photos = p.ProductImages.Where(pi => pi.ProductId == p.Id).Select(pi => _configuration["BaseURL"] + "Images/ProductImages/" + pi.ImagePath)
             });

    public dynamic ReadForCategory(int categoryId) => _databaseContext.Products
            .Include(p => p.ProductAuthors)
            .ThenInclude(pa => pa.Author)
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.ProductImages)
            .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId))
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.Quantity,
                p.Price,
                p.Cost,
                p.PublisherId,
                p.Status,
                p.Hot,
                p.PublishingYear,
                p.CreatedAt,
                p.UpdatedAt,
                Authors = p.ProductAuthors.Select(pa => pa.Author.Name),
                Categories = p.ProductCategories.Select(pc => pc.Category.Name),
                Photo = _configuration["BaseURL"] + "Images/ProductImages/" + p.ProductImages.First().ImagePath,
            });

    public dynamic ReadForCategoryUser(int categoryId) => _databaseContext.Products
            .Include(p => p.ProductAuthors)
            .ThenInclude(pa => pa.Author)
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.ProductImages)
            .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId))
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.Quantity,
                p.Price,
                p.Cost,
                p.PublisherId,
                p.Status,
                p.Hot,
                p.PublishingYear,
                p.CreatedAt,
                p.UpdatedAt,
                Authors = p.ProductAuthors.Select(pa => pa.Author.Name),
                Categories = p.ProductCategories.Select(pc => pc.Category.Name),
                Photos = p.ProductImages.Where(pi => pi.ProductId == p.Id).Select(pi => _configuration["BaseURL"] + "Images/ProductImages/" + pi.ImagePath)
            });

    public dynamic ReadForPublisher(int publisherId) => _databaseContext.Products
            .Include(p => p.ProductAuthors)
            .ThenInclude(pa => pa.Author)
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.ProductImages)
            .Where(p => p.PublisherId == publisherId)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.Quantity,
                p.Price,
                p.Cost,
                p.PublisherId,
                p.Status,
                p.Hot,
                p.PublishingYear,
                p.CreatedAt,
                p.UpdatedAt,
                Authors = p.ProductAuthors.Select(pa => pa.Author.Name),
                Categories = p.ProductCategories.Select(pc => pc.Category.Name),
                Photo = _configuration["BaseURL"] + "Images/ProductImages/" + p.ProductImages.First().ImagePath,
            });
    public dynamic ReadForPublisherUser(int publisherId) => _databaseContext.Products
            .Include(p => p.ProductAuthors)
            .ThenInclude(pa => pa.Author)
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.ProductImages)
            .Where(p => p.PublisherId == publisherId)
             .Select(p => new
             {
                 p.Id,
                 p.Name,
                 p.Description,
                 p.Quantity,
                 p.Price,
                 p.Cost,
                 p.PublisherId,
                 p.Status,
                 p.Hot,
                 p.PublishingYear,
                 p.CreatedAt,
                 p.UpdatedAt,
                 Authors = p.ProductAuthors.Select(pa => pa.Author.Name),
                 Categories = p.ProductCategories.Select(pc => pc.Category.Name),
                 Photos = p.ProductImages.Where(pi => pi.ProductId == p.Id).Select(pi => _configuration["BaseURL"] + "Images/ProductImages/" + pi.ImagePath)
             });


    public dynamic ReadForUser() => _databaseContext.Products
            .Include(p => p.ProductAuthors)
            .ThenInclude(pa => pa.Author)
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.ProductImages)
            .Where(p=>p.Status == true)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.Quantity,
                p.Price,
                p.Cost,
                p.PublisherId,
                p.Status,
                p.Hot,
                p.PublishingYear,
                p.CreatedAt,
                p.UpdatedAt,
                Authors = p.ProductAuthors.Select(pa => pa.Author.Name),
                Categories = p.ProductCategories.Select(pc => pc.Category.Name),
                Photos = p.ProductImages.Where(pi => pi.ProductId == p.Id).Select(pi => _configuration["BaseURL"] + "Images/ProductImages/" + pi.ImagePath)
            });

    public int UpdateProduct(ProductAPI product)
    {
        try
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                #region phân loại product
                var productUpdate = _databaseContext.Products.Find(product.Id);
                product.Name = product.Name;
                productUpdate.Description = product.Description;
                productUpdate.Quantity = product.Quantity;
                productUpdate.Price = product.Price;
                productUpdate.Cost = product.Cost;
                productUpdate.Status = product.Status;
                productUpdate.Hot = product.Hot;
                productUpdate.PublisherId = product.PublisherId;
                productUpdate.PublishingYear = product.PublishingYear;
                productUpdate.CreatedAt = product.CreatedAt;
                productUpdate.UpdatedAt = product.UpdatedAt;
                _databaseContext.Products.Update(productUpdate);
                _databaseContext.SaveChanges();
                #endregion

                #region phân loại bảng n - n (category - product)
                _databaseContext.ProductCategories.RemoveRange(_databaseContext.ProductCategories.Where(pc => pc.ProductId == productUpdate.Id));

                product.CategoriesId.ForEach(categoryId =>
                {
                    var productcategory = new ProductCategory()
                    {
                        CategoryId = categoryId,
                        ProductId = productUpdate.Id,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _databaseContext.ProductCategories.Add(productcategory);
                });
                _databaseContext.SaveChanges();
                #endregion

                #region phân loại bảng n - n (author - product)

                _databaseContext.ProductAuthors.RemoveRange(_databaseContext.ProductAuthors.Where(pa => pa.ProductId == productUpdate.Id));

                product.AuthorsId.ForEach(authorId =>
                {
                    var productAuthor = new ProductAuthor()
                    {
                        AuthorId = authorId,
                        ProductId = productUpdate.Id,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _databaseContext.ProductAuthors.Add(productAuthor);
                });
                _databaseContext.SaveChanges();
                #endregion

                //làm xong trả về id Product
                transaction.Commit();
                return productUpdate.Id;
            }
        }
        catch (Exception)
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                transaction.Rollback();
            }
            // lỗi trả về -1 
            return -1;
        }
    }

    public bool EnableProduct(int id)
    {
        try
        {
            var product = _databaseContext.Products.Find(id);
            product.Status = true;
            _databaseContext.Products.Update(product);
            return _databaseContext.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
