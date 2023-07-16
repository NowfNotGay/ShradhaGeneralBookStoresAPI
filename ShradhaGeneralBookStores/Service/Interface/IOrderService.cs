using ShradhaGeneralBookStores.Models.ModelTemp;

namespace ShradhaGeneralBookStores.Service.Interface;

public interface IOrderService
{
    public bool Create(OrderAPI orderAPI);
}
