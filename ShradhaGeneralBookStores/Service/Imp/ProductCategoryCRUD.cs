using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class ProductCategoryCRUD : IServiceCRUD<ProductCategory>
    {
        private readonly DatabaseContext _databaseContext;

        public ProductCategoryCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(ProductCategory entity)
        {
            try
            {
                _databaseContext.ProductCategories.Add(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int productId, int categoryId)
        {
            try
            {
                var productCategory = _databaseContext.ProductCategories.FirstOrDefault(pc => pc.ProductId == productId && pc.CategoryId == categoryId);
                if (productCategory != null)
                {
                    _databaseContext.ProductCategories.Remove(productCategory);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public dynamic Get(int productId, int categoryId) => _databaseContext.ProductCategories.FirstOrDefault(pc => pc.ProductId == productId && pc.CategoryId == categoryId)!;

        public dynamic Get(int id)
        {
            throw new NotImplementedException();
        }

        public dynamic Read() => _databaseContext.ProductCategories;

        public bool Update(ProductCategory entity)
        {
            try
            {
                _databaseContext.ProductCategories.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
