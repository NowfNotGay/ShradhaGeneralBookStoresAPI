using ShradhaGeneralBookStores.Models.ModelTemp;

namespace ShradhaGeneralBookStores.Service.Interface;

public interface IOrderService
{
    public int Create(OrderAPI orderAPI);
    public dynamic GetByAccountId(int accountId);
    public dynamic GetById(int id);
    public dynamic Read();
    public bool Paid(int orderId);
}
