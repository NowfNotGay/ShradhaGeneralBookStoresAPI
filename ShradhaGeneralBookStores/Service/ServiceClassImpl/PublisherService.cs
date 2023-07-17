using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class PublisherService : IPublisherService
{
    private readonly DatabaseContext _databaseContext;

    public PublisherService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public dynamic GetForMenu() => _databaseContext.Publishers.OrderByDescending(p => p.Products.Count()).Take(10).Select(p => new
    {
        p.Id,
        p.Name,
        p.NameShort,
        p.Location,
        p.ContactNumber,
        p.CreatedAt,
        p.UpdatedAt
    });
}
