using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
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

    public int Create(OrderAPI orderAPI)
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
                return order.Id;
            }
        }
        catch
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                transaction.Rollback();
                return -1;
            }
        }
    }

    public dynamic GetByAccountId(int accountId) 
        => _databaseContext.Orders.Where(o => o.AccountId == accountId).Select(o => new
    {
        o.Id,
        o.AccountId,
        o.TotalPrice,
        o.StatusId,
        StatusPayment = o.Status.Name,
        o.AddressId,
        Address = o.Address.Street +" "+o.Address.District + " " + o.Address.City,
        o.VoucherId,
        o.PaymentMethodId,
        PaymentMethod = o.PaymentMethod.Name,
        o.CreatedAt,
            o.UpdatedAt
        });

    public dynamic GetById(int id) => _databaseContext.Orders.Where(o => o.Id == id).Select(o => new
    {
        o.Id,
        o.AccountId,
        o.TotalPrice,
        o.StatusId,
        StatusPayment = o.Status.Name,
        o.AddressId,
        Address = o.Address.Street + " " + o.Address.District + " " + o.Address.City,
        o.VoucherId,
        o.PaymentMethodId,
        PaymentMethod = o.PaymentMethod.Name,
        o.CreatedAt,
        o.UpdatedAt
    });

    public bool Paid(int orderId)
    {
        try
        {
            var o = _databaseContext.Orders.Find(orderId);
            o.StatusId = 9;
            o.Status = _databaseContext.OrderStatuses.Find(9)!;
            o.UpdatedAt = DateTime.Now;
            _databaseContext.Orders.Update(o);
            return _databaseContext.SaveChanges() >0;
        }
        catch
        {
            return false;
        }
    }

    public dynamic Read() => _databaseContext.Orders.Select(o => new
    {
        o.Id,
        o.AccountId,
        o.TotalPrice,
        o.StatusId,
        StatusPayment = o.Status.Name,
        o.AddressId,
        Address = o.Address.Street + " " + o.Address.District + " " + o.Address.City,
        o.VoucherId,
        o.PaymentMethodId,
        PaymentMethod = o.PaymentMethod.Name,
        o.CreatedAt,
        o.UpdatedAt
    });
}
