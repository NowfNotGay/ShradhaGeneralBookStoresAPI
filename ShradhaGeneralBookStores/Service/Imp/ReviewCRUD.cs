using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class ReviewCRUD : IServiceCRUD<Review>
    {
        private readonly DatabaseContext _databaseContext;

        public ReviewCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Review entity)
        {
            try
            {
                _databaseContext.Reviews.Add(entity);
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
                var review = _databaseContext.Reviews.Find(id);
                if (review != null)
                {
                    _databaseContext.Reviews.Remove(review);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Reviews.Where(r => r.Id == id).Select(r => new
        {
            r.Id,
            r.AccountId,
            r.ProductId,
            r.Content,
            r.Rating,
            r.CreatedAt,
            r.UpdatedAt
        });

        public dynamic Read() => _databaseContext.Reviews.Select(r => new
        {
            r.Id,
            r.AccountId,
            r.ProductId,
            r.Content,
            r.Rating,
            r.CreatedAt,
            r.UpdatedAt
        });

        public bool Update(Review entity)
        {
            try
            {
                _databaseContext.Reviews.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
