using Microsoft.EntityFrameworkCore.Storage;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class OrderDetailService : IOrderDetailService
{
    private readonly DatabaseContext _databaseContext;

    public OrderDetailService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public dynamic GetById(int id)=> _databaseContext.OrderDetails.Where(od => od.OrderId == id).Select(od => new
    {
        od.OrderId,
        od.ProductId,
        od.Product!.Name,
        od.Quantity,
        od.Price,
        od.CreatedAt,
        od.UpdatedAt
    });
}
