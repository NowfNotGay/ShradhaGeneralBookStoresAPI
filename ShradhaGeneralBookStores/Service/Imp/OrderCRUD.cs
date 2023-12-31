﻿using ShradhaGeneralBookStores.Models;
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

        public dynamic Get(int id) => _databaseContext.Orders.Where(o => o.Id == id).Select(o => new
        {
            o.Id,
            o.AccountId,
            o.TotalPrice,
            o.StatusId,
            o.AddressId,
            o.VoucherId,
            o.PaymentMethodId,
            o.CreatedAt,
            o.UpdatedAt
         });

        public dynamic Read() => _databaseContext.Orders.Select(o => new
        {
            o.Id,
            o.AccountId,
            o.TotalPrice,
            o.StatusId,
            o.AddressId,
            o.VoucherId,
            o.PaymentMethodId,
            o.CreatedAt,
            o.UpdatedAt
        });

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
