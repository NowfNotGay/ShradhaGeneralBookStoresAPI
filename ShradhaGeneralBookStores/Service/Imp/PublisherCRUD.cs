using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class PublisherCRUD : IServiceCRUD<Publisher>
    {
        private readonly DatabaseContext _databaseContext;

        public PublisherCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Publisher entity)
        {
            try
            {
                _databaseContext.Publishers.Add(entity);
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
                var publisher = _databaseContext.Publishers.Find(id);
                if (publisher != null)
                {
                    _databaseContext.Publishers.Remove(publisher);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Publishers.Where(p => p.Id == id).Select(p => new
        {
            p.Id,
            p.Name,
            p.NameShort,
            p.Location,
            p.ContactNumber,
            p.CreatedAt,
            p.UpdatedAt
        });

        public dynamic Read() => _databaseContext.Publishers.Select(p => new
        {
            p.Id,
            p.Name,
            p.NameShort,
            p.Location,
            p.ContactNumber,
            p.CreatedAt,
            p.UpdatedAt
        });

        public bool Update(Publisher entity)
        {
            try
            {
                _databaseContext.Publishers.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
