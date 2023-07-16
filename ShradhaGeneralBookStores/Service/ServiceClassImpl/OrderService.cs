using Microsoft.EntityFrameworkCore.Storage;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class OrderService : IOrderService
{
    private readonly DatabaseContext _databaseContext;

    public OrderService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public bool Create(OrderDetail orderDetail)
    {

    }
}
