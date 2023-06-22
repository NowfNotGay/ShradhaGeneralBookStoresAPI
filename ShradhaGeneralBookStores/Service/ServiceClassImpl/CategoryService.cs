using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class CategoryService : ICategoryService
{
    private readonly DatabaseContext _databaseContext;
         
    private List<Category> categoriesList = new List<Category>() { };

    public CategoryService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public dynamic GetAllCategoryByLevel() => dequy(_databaseContext.Categories.ToList()).Select(c=> new
    {
        c.Id,
        c.Name,
        c.ParentId
    });

    private List<Category> dequy(List<Category> categories, int? parent = null, string level = "")
    {
        foreach (var category in categories)
        {
            
            if (category.ParentId == parent)
            {
                category.Name = level + category.Name;
                categoriesList.Add(category);
                dequy(categories, parent : category.Id, level : level+ "--|");
            }
        }
        return categoriesList;
    }

}
