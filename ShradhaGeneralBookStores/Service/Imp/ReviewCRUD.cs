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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Reviews.Find(id);

        public dynamic Read() => _databaseContext.Reviews;

        public bool Update(Review entity)
        {
            try
            {
                _databaseContext.Reviews.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
