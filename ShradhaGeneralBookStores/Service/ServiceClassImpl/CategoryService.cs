using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class CategoryService : ICategoryService
{
    private readonly DatabaseContext _databaseContext;

    public CategoryService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public dynamic GetSubParent(int categoryId)
    {
        return _databaseContext.Categories.Where(c => c.ParentId == categoryId).Select( c=>
            new {
                c.Id,
                c.Name,
                c.ParentId
            }
        );
    }

    //public dynamic GetSubParent(int categoryId) => _databaseContext.Categories.FirstOrDefault(c => c.Id == categoryId)!.InverseParent;

}
