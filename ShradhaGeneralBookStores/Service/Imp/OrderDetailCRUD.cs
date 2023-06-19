﻿using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class OrderDetailCRUD : IServiceCRUD<OrderDetail>
    {
        private readonly DatabaseContext _databaseContext;

        public OrderDetailCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(OrderDetail entity)
        {
            try
            {
                _databaseContext.OrderDetails.Add(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int orderId, int productId)
        {
            try
            {
                var orderDetail = _databaseContext.OrderDetails.FirstOrDefault(od => od.OrderId == orderId && od.ProductId == productId);
                if (orderDetail != null)
                {
                    _databaseContext.OrderDetails.Remove(orderDetail);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public dynamic Get(int orderId, int productId) => _databaseContext.OrderDetails.FirstOrDefault(od => od.OrderId == orderId && od.ProductId == productId)!;

        public dynamic Read() => _databaseContext.OrderDetails;

        public bool Update(OrderDetail entity)
        {
            try
            {
                _databaseContext.OrderDetails.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
