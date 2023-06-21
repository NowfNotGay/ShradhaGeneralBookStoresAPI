using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class CategoryCRUD : IServiceCRUD<Category>
    {
        private readonly DatabaseContext _databaseContext;

        public CategoryCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Category entity)
        {
            try
            {
                _databaseContext.Categories.Add(entity);
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
                var category = _databaseContext.Categories.FirstOrDefault(c => c.Id == id);
                if (category != null)
                {
                    _databaseContext.Categories.Remove(category);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Categories.Where(c => c.Id == id).Select(c => new
        {
            c.Id,
            c.Name,
            c.ParentId,
            c.CreatedAt,
            c.UpdatedAt
        });

        public dynamic Read() => _databaseContext.Categories.Select(c=> new
        {
            c.Id,
            c.Name,
            c.ParentId,
            c.CreatedAt, 
            c.UpdatedAt,
            c.InverseParent
        });

        public bool Update(Category entity)
        {
            try
            {
                _databaseContext.Categories.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
