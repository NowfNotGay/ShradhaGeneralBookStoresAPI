using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class AuthorCRUD : IServiceCRUD<Author>
    {
        private readonly DatabaseContext _databaseContext;

        public AuthorCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Author entity)
        {
            try
            {
                _databaseContext.Authors.Add(entity);
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
                var author = _databaseContext.Authors.FirstOrDefault(a => a.Id == id);
                if (author != null)
                {
                    _databaseContext.Authors.Remove(author);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Authors.Where(a => a.Id == id).Select(au => new
        {
            au.Id,
            au.Name,
            au.Biography,
            au.YearOfBirth,
            au.CreatedAt,
            au.UpdatedAt
        });

        public dynamic Read() => _databaseContext.Authors.Select(au => new
        {
            au.Id,
            au.Name,
            au.Biography,
            au.YearOfBirth,
            au.CreatedAt, 
            au.UpdatedAt
        });

        public bool Update(Author entity)
        {
            try
            {
                _databaseContext.Authors.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
