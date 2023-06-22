using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class OrderCRUD : IServiceCRUD<Order>
    {
        private readonly DatabaseContext _databaseContext;

        public OrderCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Order entity)
        {
            try
            {
                _databaseContext.Orders.Add(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var order = _databaseContext.Orders.FirstOrDefault(o => o.Id == id);
                if (order != null)
                {
                    _databaseContext.Orders.Remove(order);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Orders.FirstOrDefault(o => o.Id == id)!;

        public dynamic Read() => _databaseContext.Orders;

        public bool Update(Order entity)
        {
            try
            {
                _databaseContext.Orders.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
