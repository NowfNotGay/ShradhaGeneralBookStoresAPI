using Microsoft.EntityFrameworkCore.Storage;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Models.ModelTemp;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class OrderService : IOrderService
{
    private readonly DatabaseContext _databaseContext;

    public OrderService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public bool Create(OrderAPI orderAPI)
    {
        try
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                //set order
                var order = new Order()
                {
                    AccountId = orderAPI.AccountId,
                    TotalPrice = orderAPI.TotalPrice,
                    StatusId = orderAPI.StatusId,
                    AddressId = orderAPI.AddressId,
                    VoucherId = orderAPI.VoucherId,
                    PaymentMethodId = orderAPI.PaymentMethodId,
                };
                order.CreatedAt = DateTime.Now;
                order.UpdatedAt = DateTime.Now;


                _databaseContext.Orders.Add(order);
                _databaseContext.SaveChanges();
                foreach(var item in orderAPI.ListOrderDetail)
                {
                    _databaseContext.OrderDetails.Add(
                         new OrderDetail()
                         {
                             OrderId = order.Id,
                             ProductId = item.ProductId,
                             Price = item.Price,
                             Quantity = item.Quantity,
                             UpdatedAt= DateTime.Now,
                             CreatedAt= DateTime.Now,
                         }
                    );
                }
                var a = _databaseContext.SaveChanges();
                transaction.Commit();
                return a > 0;
            }
        }
        catch
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
