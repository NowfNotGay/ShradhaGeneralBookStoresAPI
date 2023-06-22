using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class ProductImageCRUD : IServiceCRUD<ProductImage>
    {
        private readonly DatabaseContext _databaseContext;

        public ProductImageCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(ProductImage entity)
        {
            try
            {
                _databaseContext.ProductImages.Add(entity);
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
                var productImage = _databaseContext.ProductImages.Find(id);
                if (productImage != null)
                {
                    _databaseContext.ProductImages.Remove(productImage);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.ProductImages.Find(id)!;

        public dynamic Read() => _databaseContext.ProductImages;

        public bool Update(ProductImage entity)
        {
            try
            {
                _databaseContext.ProductImages.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
