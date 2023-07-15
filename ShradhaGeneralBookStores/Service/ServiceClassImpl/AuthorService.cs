using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class AuthorService : IAuthorService
{
    private readonly DatabaseContext _databaseContext;

    public AuthorService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public dynamic GetForMenu() => _databaseContext.Authors.OrderBy(p => p.ProductAuthors.Count()).Take(10).Select(au => new
    {
        au.Id,
        au.Name,
        au.Biography,
        au.YearOfBirth,
        au.CreatedAt,
        au.UpdatedAt
    });
}
