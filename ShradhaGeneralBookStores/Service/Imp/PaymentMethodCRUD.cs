using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class PaymentMethodCRUD : IServiceCRUD<PaymentMethod>
    {
        private readonly DatabaseContext _databaseContext;

        public PaymentMethodCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(PaymentMethod entity)
        {
            try
            {
                _databaseContext.PaymentMethods.Add(entity);
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
                var paymentMethod = _databaseContext.PaymentMethods.FirstOrDefault(pm => pm.Id == id);
                if (paymentMethod != null)
                {
                    _databaseContext.PaymentMethods.Remove(paymentMethod);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.PaymentMethods.FirstOrDefault(pm => pm.Id == id)!;

        public dynamic Read() => _databaseContext.PaymentMethods;

        public bool Update(PaymentMethod entity)
        {
            try
            {
                _databaseContext.PaymentMethods.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
