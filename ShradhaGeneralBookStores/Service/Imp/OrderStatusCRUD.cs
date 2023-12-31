﻿using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class OrderStatusCRUD : IServiceCRUD<OrderStatus>
    {
        private readonly DatabaseContext _databaseContext;

        public OrderStatusCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(OrderStatus entity)
        {
            try
            {
                _databaseContext.OrderStatuses.Add(entity);
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
                var orderStatus = _databaseContext.OrderStatuses.FirstOrDefault(os => os.Id == id);
                if (orderStatus != null)
                {
                    _databaseContext.OrderStatuses.Remove(orderStatus);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.OrderStatuses.Where(os => os.Id == id).Select(os => new
        {
            os.Id,
            os.Name,
            os.CreatedAt,
            os.UpdatedAt
        });

        public dynamic Read() => _databaseContext.OrderStatuses.Select(os => new
        {
            os.Id,
            os.Name,
            os.CreatedAt,
            os.UpdatedAt
        });

        public bool Update(OrderStatus entity)
        {
            try
            {
                _databaseContext.OrderStatuses.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
