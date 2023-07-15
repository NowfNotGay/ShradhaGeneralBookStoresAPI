using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class RoleCRUD : IServiceCRUD<Role>
    {
        private readonly DatabaseContext _databaseContext;

        public RoleCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Role entity)
        {
            try
            {
                _databaseContext.Roles.Add(entity);
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
                var role = _databaseContext.Roles.Find(id);
                if (role != null)
                {
                    _databaseContext.Roles.Remove(role);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Roles.Where(r => r.Id == id).Select(r => new
        {
            r.Id,
            r.Name,
            r.CreatedAt,
            r.UpdatedAt
        });

        public dynamic Read() => _databaseContext.Roles.Where(r=>r.Id !=1).Select(r => new
        {
            r.Id,
            r.Name,
            r.CreatedAt,
            r.UpdatedAt
        });

        public bool Update(Role entity)
        {
            try
            {
                _databaseContext.Roles.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
