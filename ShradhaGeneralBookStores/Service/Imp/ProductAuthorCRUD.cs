using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class ProductAuthorCRUD : IServiceCRUD<ProductAuthor>
    {
        private readonly DatabaseContext _databaseContext;

        public ProductAuthorCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(ProductAuthor entity)
        {
            try
            {
                _databaseContext.ProductAuthors.Add(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int productId, int authorId)
        {
            try
            {
                var productAuthor = _databaseContext.ProductAuthors.FirstOrDefault(pa => pa.ProductId == productId && pa.AuthorId == authorId);
                if (productAuthor != null)
                {
                    _databaseContext.ProductAuthors.Remove(productAuthor);
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

        public dynamic Get(int productId, int authorId) => _databaseContext.ProductAuthors.FirstOrDefault(pa => pa.ProductId == productId && pa.AuthorId == authorId)!;

        public dynamic Get(int id)
        {
            throw new NotImplementedException();
        }

        public dynamic Read() => _databaseContext.ProductAuthors;

        public bool Update(ProductAuthor entity)
        {
            try
            {
                _databaseContext.ProductAuthors.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
