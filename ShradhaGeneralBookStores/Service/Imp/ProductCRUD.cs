using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class ProductCRUD : IServiceCRUD<Product>
    {
        private readonly DatabaseContext _databaseContext;

        public ProductCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Product entity)
        {
            try
            {
                _databaseContext.Products.Add(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var product = _databaseContext.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    _databaseContext.Products.Remove(product);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Products.Where(p => p.Id == id).Select(p => new
        {
            p.Id,
            p.Name,
            p.Description,
            p.Quantity,
            p.Price,
            p.Cost,
            p.Status,
            p.Hot,
            p.PublisherId,
            p.PublishingYear,
            p.CreatedAt,
            p.UpdatedAt
        });

        public dynamic Read() => _databaseContext.Products.Select(p => new
        {
            p.Id,
            p.Name,
            p.Description,
            p.Quantity,
            p.Price,
            p.Cost,
            p.Status,
            p.Hot,
            p.PublisherId,
            p.PublishingYear,
            p.CreatedAt,
            p.UpdatedAt
        });

        public bool Update(Product entity)
        {
            try
            {
                _databaseContext.Products.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
