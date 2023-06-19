using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class InvoiceCRUD : IServiceCRUD<Invoice>
    {
        private readonly DatabaseContext _databaseContext;

        public InvoiceCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Invoice entity)
        {
            try
            {
                _databaseContext.Invoices.Add(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var invoice = _databaseContext.Invoices.FirstOrDefault(i => i.Id == id);
                if (invoice != null)
                {
                    _databaseContext.Invoices.Remove(invoice);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Invoices.FirstOrDefault(i => i.Id == id)!;

        public dynamic Read() => _databaseContext.Invoices;

        public bool Update(Invoice entity)
        {
            try
            {
                _databaseContext.Invoices.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
